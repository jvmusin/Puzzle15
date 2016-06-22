using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Puzzle15.Base.Field
{
    public class RectangularField<T> : RectangularFieldBase<T>
    {
        private readonly T[,] table;
        private readonly Dictionary<T, List<CellLocation>> locations;

        public override bool Mutable => true;

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

        public override IRectangularField<T> Fill(Func<CellLocation, T> getValue)
        {
            foreach (var location in EnumerateLocations())
                this[location] = getValue(location);

            return this;
        }

        public override IRectangularField<T> Clone()
        {
            return new RectangularField<T>(Size).Fill(location => this[location]);
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

        public override T this[CellLocation location]
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
    }
}
