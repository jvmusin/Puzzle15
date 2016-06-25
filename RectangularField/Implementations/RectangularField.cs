using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RectangularField.Core;

namespace RectangularField.Implementations
{
    public class RectangularField<T> : RectangularFieldBase<T>
    {
        private readonly T[,] table;
        private readonly Dictionary<T, List<CellLocation>> locations;

        public override bool Immutable => false;

        #region Constructors

        public RectangularField(Size size) : base(size)
        {
            table = new T[Height, Width];
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

        public override IRectangularField<T> Swap(CellLocation location1, CellLocation location2)
        {
            var temp = this[location1];
            this[location1] = this[location2];
            this[location2] = temp;

            return this;
        }

        public override IRectangularField<T> Fill(CellConverter<T, T> getValue)
        {
            foreach (var cellInfo in this)
                this[cellInfo.Location] = getValue(cellInfo);

            return this;
        }

        public override IRectangularField<T> Clone()
        {
            return new RectangularField<T>(Size).Fill(cellInfo => this[cellInfo.Location]);
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
                ? this.Where(x => x.Value == null).Select(x => x.Location)
                : GetLocationsSafe(value);
        }

        public override CellLocation GetLocation(T value)
        {
            return GetLocations(value).FirstOrDefault();
        }

        public override T GetValue(CellLocation location)
        {
            CheckLocation(location);
            return table[location.Row, location.Column];
        }

        public override IRectangularField<T> SetValue(T value, CellLocation location)
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
