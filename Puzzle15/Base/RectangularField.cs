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

        public RectangularField(Size size)
        {
            Size = size;
            table = new T[Height,Width];
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

        public T this[CellLocation location]
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
            var curTable = table.Cast<T>();
            var otherTable = other.table.Cast<T>();
            return Size == other.Size && curTable.SequenceEqual(otherTable);
        }

        public override bool Equals(object obj)
        {
            var other = obj as RectangularField<T>;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return Helpers.StructuralGetHashCode(table.Cast<T>().ToArray());
        }

        public override string ToString()
        {
            return ToString(info => info.Value.ToString());
        }

        public string ToString(Func<CellInfo<T>, string> getCapture, string lineSeparator = "\n")
        {
            var result = Helpers.CreateTable<string>(Height, Width);
            this.ForEach(info => result[info.Location.Row][info.Location.Column] = getCapture(info));

            return string.Join(lineSeparator,
                result.Select(row => string.Join(" ", row)));
        }

        #endregion
    }
}
