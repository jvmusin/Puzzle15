using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Puzzle15.Base
{
    public class RectangularField<T> : IEnumerable<CellInfo<T>>
    {
        private readonly T[,] table;
        private readonly Dictionary<T, List<CellLocation>> locations;

        public Size Size { get; }
        public int Height => Size.Height;
        public int Width => Size.Width;

        #region Constructors

        protected RectangularField(Size size, bool shouldInit)
        {
            Size = size;

            if (shouldInit)
            {
                table = new T[Height, Width];
                locations = new Dictionary<T, List<CellLocation>>();

                var defaultValue = default(T);
                if (defaultValue != null)
                    locations[defaultValue] = EnumerateLocations().ToList();
            }
        }

        public RectangularField(Size size) : this(size, true)
        {
        }

        public RectangularField(int height, int width) : this(new Size(width, height), true)
        {
        }

        #endregion

        #region Primary actions

        public virtual RectangularField<T> Swap(CellLocation location1, CellLocation location2)
        {
            var temp = this[location1];
            this[location1] = this[location2];
            this[location2] = temp;

            return this;
        }

        public virtual RectangularField<T> Fill(Func<CellLocation, T> getValue)
        {
            foreach (var location in EnumerateLocations())
                this[location] = getValue(location);

            return this;
        }

        public virtual RectangularField<T> Clone()
        {
            var newField = new RectangularField<T>(Size);
            newField.Fill(location => this[location]);

            return newField;
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

        private List<CellLocation> GetLocationsSafe(T value)
        {
            List<CellLocation> result;
            if (!locations.TryGetValue(value, out result))
                result = locations[value] = new List<CellLocation>();
            return result;
        }

        public virtual IEnumerable<CellLocation> GetLocations(T value)
        {
            return value == null
                ? this.Where(x => x.Value == null).Select(x => x.Location)
                : GetLocationsSafe(value);
        }

        public virtual CellLocation GetLocation(T value)
        {
            return GetLocations(value).FirstOrDefault();
        }

        public virtual T this[CellLocation location]
        {
            get { return table[location.Row, location.Column]; }
            set
            {
                var valueToRemove = this[location];
                if (valueToRemove != null)
                    locations[valueToRemove].Remove(location);
                
                table[location.Row, location.Column] = value;
                if (value != null)
                    GetLocationsSafe(value).Add(location);
            }
        }

        #endregion

        #region Equals, GetHashCode and ToString methods

        protected bool Equals(RectangularField<T> other)
        {
            return Size == other.Size && this.SequenceEqual(other);
        }

        public override bool Equals(object obj)
        {
            var other = obj as RectangularField<T>;
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
