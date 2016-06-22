using System;
using System.Collections.Generic;
using System.Linq;

namespace Puzzle15.Base
{
    public class WrappedRectangularField<T> : RectangularField<T>
    {
        private readonly IRectangularField<T> parent;
        private readonly CellInfo<T> changedCell;

        public WrappedRectangularField(IRectangularField<T> parent, CellInfo<T> changedCell = null)
            : base(parent.Size, false)
        {
            this.parent = parent;
            this.changedCell = changedCell;
        }

        #region Primary actions

        public override IRectangularField<T> Swap(CellLocation location1, CellLocation location2)
        {
            var value1 = this[location1];
            var value2 = this[location2];

            return this
                .SetValue(value1, location2)
                .SetValue(value2, location1);
        }

        public override IRectangularField<T> Fill(Func<CellLocation, T> getValue)
        {
            return EnumerateLocations().Aggregate(this as IRectangularField<T>,
                (current, location) => current.SetValue(getValue(location), location));
        }

        public override IRectangularField<T> Clone()
        {
            return new WrappedRectangularField<T>(this);
        }

        #endregion

        #region Indexers

        public override IEnumerable<CellLocation> GetLocations(T value)
        {
            return EnumerateLocations().Where(x => Helpers.StructuralEquals(this[x], value));
        }

        public override CellLocation GetLocation(T value)
        {
            return changedCell != null && Helpers.StructuralEquals(changedCell.Value, value)
                ? changedCell.Location
                : parent.GetLocation(value);
        }

        public override T this[CellLocation location]
        {
            get
            {
                return changedCell != null && changedCell.Location.Equals(location)
                    ? changedCell.Value
                    : parent[location];
            }
            set { throw new NotImplementedException(); }
        }

        public override T GetValue(CellLocation location)
        {
            return this[location];
        }

        public override IRectangularField<T> SetValue(T value, CellLocation location)
        {
            return new WrappedRectangularField<T>(this, new CellInfo<T>(location, value));
        }

        #endregion
    }
}
