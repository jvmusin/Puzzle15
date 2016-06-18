using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Puzzle15
{
    public class RectangularField<T> : IEnumerable<CellInfo<T>>
    {
        private readonly T[][] field;
        private readonly Dictionary<T, List<CellLocation>> locations;

        public Size Size;
        public int Height => field.GetLength(0);
        public int Width => field.GetLength(1);

        #region Constructors

        public RectangularField(Size size)
        {
            Size = size;
            field = new T[Height][];
            locations = new Dictionary<T, List<CellLocation>> {{default(T), new List<CellLocation>()}};

            for (var rowIndex = 0; rowIndex < Height; rowIndex++)
            {
                var row = field[rowIndex] = new T[Width];
                for (var columnIndex = 0; columnIndex < Width; columnIndex++)
                {
                    var value = row[columnIndex] = default(T);
                    if (value != null)
                        locations[value].Add(new CellLocation(rowIndex, columnIndex));
                }
            }
        }

        public RectangularField(int height, int width) : this(new Size(width, height))
        {
        }

        #endregion

        #region Primary actions

        public void Fill(Func<CellLocation, T> getValue)
        {
            foreach (var position in EnumerateLocations())
                this[position] = getValue(position);
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
            newField.Fill(pos => this[pos]);
            return newField;
        }

        public T[][] ToTable()
        {
            var table = new T[Height][];
            for (var rowIndex = 0; rowIndex < Height; rowIndex++)
            {
                var row = table[rowIndex] = new T[Width];
                for (var columnIndex = 0; columnIndex < Width; columnIndex++)
                    row[columnIndex] = this[rowIndex, columnIndex];
            }
            return table;
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

        public IEnumerable<CellLocation> GetLocations(T value)
        {
            return value == null
                ? this.Where(x => x.Value == null).Select(x => x.Location)
                : locations.ComputeIfAbsent(value, () => new List<CellLocation>());
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
            get { return field[location.Row][location.Column]; }
            set
            {
                var valueToRemove = this[location];
                if (valueToRemove != null)
                    locations[valueToRemove].Remove(location);

                field[location.Row][location.Column] = value;
                if (value != null)
                    locations.ComputeIfAbsent(value, () => new List<CellLocation>())
                        .Add(location);
            }
        }

        #endregion
    }
}
