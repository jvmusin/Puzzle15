using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Puzzle15.Base.Field
{
    public abstract class RectangularFieldBase<T> : IRectangularField<T>
    {
        public Size Size { get; }
        public int Height => Size.Height;
        public int Width => Size.Width;

        public abstract bool Immutable { get; }

        protected RectangularFieldBase(Size size)
        {
            if (size.Height < 1 || size.Width < 1)
                throw new ArgumentException("Field should have positive size", nameof(size));
            Size = size;
        }

        #region Primary actions

        public abstract IRectangularField<T> Swap(CellLocation location1, CellLocation location2);

        public abstract IRectangularField<T> Fill(CellConverter<T, T> getValue);

        public abstract IRectangularField<T> Clone();

        public bool Contains(CellLocation location)
        {
            return
                0 <= location.Row && location.Row < Height &&
                0 <= location.Column && location.Column < Width;
        }

        protected void CheckLocation(CellLocation location)
        {
            if (!Contains(location))
                throw new InvalidLocationException(location);
        }

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

        public virtual T this[CellLocation location]
        {
            get { return GetValue(location); }
            set { SetValue(value, location); }
        }

        public abstract T GetValue(CellLocation location);

        public abstract IRectangularField<T> SetValue(T value, CellLocation location);

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
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return ToString(info => info.Value.ToString());
        }

        public string ToString(CellConverter<T, string> toString, string lineSeparator = "\n")
        {
            var result = Helpers.CreateTable<string>(Size);

            foreach (var info in this)
                result[info.Location.Row][info.Location.Column] = toString(info);

            return string.Join(lineSeparator,
                result.Select(row => string.Join(" ", row)));
        }

        #endregion
    }
}
