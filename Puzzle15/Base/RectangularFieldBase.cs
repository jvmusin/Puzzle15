using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Puzzle15.Base
{
    public abstract class RectangularFieldBase<T> : IRectangularField<T>
    {
        public Size Size { get; }
        public int Height => Size.Height;
        public int Width => Size.Width;

        protected RectangularFieldBase(Size size)
        {
            Size = size;
        }

        #region Primary actions

        public abstract IRectangularField<T> Swap(CellLocation location1, CellLocation location2);

        public abstract IRectangularField<T> Fill(Func<CellLocation, T> getValue);

        public abstract IRectangularField<T> Clone();

        #endregion

        #region Enumerators

        public IEnumerable<CellLocation> EnumerateLocations()
        {
            return
                from row in Enumerable.Range(0, Height)
                from col in Enumerable.Range(0, Width)
                select new CellLocation(row, col);
        }

        public IEnumerator<CellInfo<T>> GetEnumerator()
        {
            return EnumerateLocations()
                .Select(location => new CellInfo<T>(location, this[location]))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Indexers

        public abstract IEnumerable<CellLocation> GetLocations(T value);

        public abstract CellLocation GetLocation(T value);

        public abstract T this[CellLocation location] { get; set; }

        public virtual T GetValue(CellLocation location)
        {
            return this[location];
        }

        public virtual IRectangularField<T> SetValue(T value, CellLocation location)
        {
            this[location] = value;
            return this;
        }

        #endregion

        #region Equals, GetHashCode and ToString methods

        protected bool Equals(IRectangularField<T> other)
        {
            return Size == other.Size && this.SequenceEqual(other);
        }

        public override bool Equals(object obj)
        {
            var other = obj as IRectangularField<T>;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return Helpers.StructuralGetHashCode(this.ToArray());
        }

        public override string ToString()
        {
            return ToString(info => info.Value.ToString());
        }

        public string ToString(Func<CellInfo<T>, string> getCapture, string lineSeparator = "\n")
        {
            var result = Helpers.CreateTable<string>(Height, Width);
            foreach (var info in this)
                result[info.Location.Row][info.Location.Column] = getCapture(info);

            return string.Join(lineSeparator,
                result.Select(row => string.Join(" ", row)));
        }

        #endregion
    }
}
