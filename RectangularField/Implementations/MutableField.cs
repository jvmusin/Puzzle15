using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RectangularField.Implementations.Base;
using RectangularField.Interfaces;

namespace RectangularField.Implementations
{
    public class MutableField<T> : FieldBase<T>
    {
        private readonly T[,] table;
        private readonly Dictionary<T, List<CellLocation>> locations;

        public override bool Immutable => false;

        #region Constructors

        public MutableField(Size size) : base(size)
        {
            table = new T[Height, Width];
            locations = new Dictionary<T, List<CellLocation>>();

            var defaultValue = default(T);
            if (defaultValue != null)
                locations[defaultValue] = EnumerateLocations().ToList();
        }

        #endregion

        #region Primary actions

        public override IField<T> Clone()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var field = new MutableField<T>(Size);
            field.Shuffler = Shuffler;
            return field.Fill(cellInfo => this[cellInfo.Location]);
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

        public override T GetValue(CellLocation location)
        {
            CheckLocation(location);
            return table[location.Row, location.Column];
        }

        public override IField<T> SetValue(T value, CellLocation location)
        {
            CheckLocation(location);

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
