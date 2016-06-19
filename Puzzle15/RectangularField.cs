using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Puzzle15
{
    public class RectangularField<T> : IEnumerable<CellInfo<T>>
    {
        private readonly T[][] table;
        private readonly Dictionary<T, List<CellLocation>> locations;

        public Size Size;
        public int Height => Size.Height;
        public int Width => Size.Width;

        #region Constructors

        public RectangularField(Size size)
        {
            Size = size;
            table = Helpers.CreateTable<T>(Height, Width);
            locations = new Dictionary<T, List<CellLocation>>();

            var defaultValue = default(T);
            if (defaultValue != null)
                locations[defaultValue] = EnumerateLocations().ToList();
        }

        public RectangularField(int height, int width) : this(new Size(width, height))
        {
        }

        #endregion

        #region Primary actions

        public void Fill(Func<CellLocation, T> getValue)
        {
            EnumerateLocations()
                .ForEach(location => this[location] = getValue(location));
        }

        public void Swap(CellLocation position1, CellLocation position2)
        {
            var temp = this[position1];
            this[position1] = this[position2];
            this[position2] = temp;
        }

        public RectangularField<T> Clone()
        {
            var newField = new RectangularField<T>(Height, Width);
            newField.Fill(location => this[location]);
            return newField;
        }

        public T[][] ToTable()
        {
            return table.Select(row => row.ToArray()).ToArray();
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
            return locations.ComputeIfAbsent(value, () => new List<CellLocation>());
        }

        public IEnumerable<CellLocation> GetLocations(T value)
        {
            return value == null
                ? this.Where(x => x.Value == null).Select(x => x.Location)
                : GetLocationsSafe(value);
        }

        public CellLocation GetLocation(T value)
        {
            return GetLocations(value).FirstOrDefault();
        }

        public T this[int row, int column]
        {
            get { return this[new CellLocation(row, column)]; }
            set { this[new CellLocation(row, column)] = value; }
        }

        public T this[CellLocation location]
        {
            get { return table[location.Row][location.Column]; }
            set
            {
                var valueToRemove = this[location];
                if (valueToRemove != null)
                    locations[valueToRemove].Remove(location);

                table.SetValue(location, value);
                if (value != null)
                    GetLocationsSafe(value).Add(location);
            }
        }

        #endregion

        #region Equals, GetHashCode and ToString methods

        protected bool Equals(RectangularField<T> other)
        {
            return Helpers.Equals(table, other.table);
        }

        public override bool Equals(object obj)
        {
            var other = obj as RectangularField<T>;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return Helpers.GetHashCode(table);
        }

        public override string ToString()
        {
            return ToString(info => info.Value.ToString());
        }

        public string ToString(Func<CellInfo<T>, string> getCapture, string lineSeparator = "\n")
        {
            var result = Helpers.CreateTable<string>(Height, Width);
            this.ForEach(info => result.SetValue(info.Location, getCapture(info)));
            return string.Join(lineSeparator,
                result.Select(row => string.Join(" ", row)));
        }

        #endregion
    }
}
