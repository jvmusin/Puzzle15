using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RectangularField.Implementations.Base;
using RectangularField.Interfaces;

namespace RectangularField.Implementations
{
    public class ImmutableField<T> : FieldBase<T>
    {
        private readonly T[,] table;
        private readonly Dictionary<T, List<CellLocation>> locations;

        public override bool Immutable => true;

        #region Constructors

        public ImmutableField(Size size) : base(size)
        {
            table = new T[Height, Width];
            locations = new Dictionary<T, List<CellLocation>>();

            var defaultValue = default(T);
            if (defaultValue != null)
                locations[defaultValue] = EnumerateLocations().ToList();
        }

        #endregion

        #region Primary actions

        public override IField<T> Swap(CellLocation location1, CellLocation location2)
        {
            var value1 = this[location1];
            var value2 = this[location2];

            return ((ImmutableField<T>) Clone())
                .SetValue0(value1, location2)
                .SetValue0(value2, location1);
        }

        public override IField<T> Fill(CellConverter<T, T> getValue)
        {
            var result = new ImmutableField<T>(Size);
            result.Shuffler = Shuffler;
            return this.Aggregate(result,
                (field, cellInfo) => field.SetValue0(getValue(cellInfo), cellInfo.Location));
        }

        public override IField<T> Clone()
        {
            return Fill(cellInfo => this[cellInfo.Location]);
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

        public override IEnumerable<CellLocation> GetLocations(T value)
        {
            return value == null
                // ReSharper disable once ExpressionIsAlwaysNull
                ? base.GetLocations(value)
                : GetLocationsSafe(value);
        }

        public override T this[CellLocation location]
        {
            set
            {
                throw new NotSupportedException(
                    "The field is immutable. " +
                    "To change the value, use SetValue() method instead of indexer.");
            }
        }

        public override T GetValue(CellLocation location)
        {
            CheckLocation(location);
            return table[location.Row, location.Column];
        }

        public override IField<T> SetValue(T value, CellLocation location)
        {
            CheckLocation(location);
            return ((ImmutableField<T>) Clone()).SetValue0(value, location);
        }

        private ImmutableField<T> SetValue0(T value, CellLocation location)
        {
            var valueToRemove = this[location];
            if (valueToRemove != null)
                locations[valueToRemove].Remove(location);

            table[location.Row, location.Column] = value;
            if (value != null)
                GetLocationsSafe(value).Add(location);

            return this;
        }

        #endregion
    }
}
