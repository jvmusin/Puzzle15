using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RectangularField.Implementations.Base;
using RectangularField.Interfaces;
using RectangularField.Utils;

namespace RectangularField.Implementations
{
    public abstract class FieldBase<T> : IField<T>
    {
        public Size Size { get; }
        public int Height => Size.Height;
        public int Width => Size.Width;

        public abstract bool Immutable { get; }
        public IFieldShuffler<T> Shuffler { get; set; }

        protected FieldBase(Size size)
        {
            if (size.Height < 0 || size.Width < 0)
                throw new ArgumentException("Field shouldn't have negative size", nameof(size));

            Size = size;
            Shuffler = new StandardFieldShuffler<T>();
        }

        #region Primary actions

        public IField<T> Shuffle(int quality)
        {
            return Shuffler.Shuffle(this, quality);
        }

        public virtual IField<T> Swap(CellLocation location1, CellLocation location2)
        {
            var value1 = this[location1];
            var value2 = this[location2];

            // ReSharper disable once ArrangeThisQualifier
            return this
                .SetValue(value1, location2)
                .SetValue(value2, location1);
        }

        public virtual IField<T> Fill(CellConverter<T, T> getValue)
        {
            return this
                .Aggregate(this as IField<T>,
                    (field, cellInfo) => field.SetValue(getValue(cellInfo), cellInfo.Location));
        }

        public abstract IField<T> Clone();

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

        public virtual IEnumerable<CellLocation> GetLocations(T value)
        {
            return this
                .Where(x => Helpers.Equals(x.Value, value))
                .Select(x => x.Location);
        }

        public virtual CellLocation GetLocation(T value)
        {
            return GetLocations(value).FirstOrDefault();
        }

        public virtual T this[CellLocation location]
        {
            get { return GetValue(location); }
            set { SetValue(value, location); }
        }

        public abstract T GetValue(CellLocation location);

        public abstract IField<T> SetValue(T value, CellLocation location);

        #endregion

        #region Equals, GetHashCode and ToString methods

        protected bool Equals(IField<T> other)
        {
            return Size == other.Size && this.SequenceEqual(other);
        }

        public override bool Equals(object obj)
        {
            var other = obj as IField<T>;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return Helpers.GetHashCode(this.ToArray());
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
