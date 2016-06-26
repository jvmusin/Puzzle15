﻿using System;
using System.Collections.Generic;
using System.Linq;
using RectangularField.Core;
using RectangularField.Utils;

namespace RectangularField.Implementations
{
    internal class WrappingRectangularField<T> : RectangularFieldBase<T>
    {
        private readonly IRectangularField<T> parent;
        private readonly CellInfo<T> changedCell;

        public override bool Immutable => true;

        private WrappingRectangularField(IRectangularField<T> parent, CellInfo<T> changedCell)
            : base(parent.Size)
        {
            this.parent = parent;
            this.changedCell = changedCell;
        }

        public WrappingRectangularField(IRectangularField<T> parent)
            : this(parent, null)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));
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

        public override IRectangularField<T> Fill(CellConverter<T, T> getValue)
        {
            return this
                .Aggregate(this as IRectangularField<T>,
                    (field, cellInfo) => field.SetValue(getValue(cellInfo), cellInfo.Location));
        }

        public override IRectangularField<T> Clone()
        {
            return new WrappingRectangularField<T>(this);
        }

        #endregion

        #region Indexers

        public override IEnumerable<CellLocation> GetLocations(T value)
        {
            return EnumerateLocations().Where(x => Helpers.Equals(this[x], value));
        }

        public override CellLocation GetLocation(T value)
        {
            if (changedCell != null && changedCell.Value.Equals(value))
                return changedCell.Location;
            return parent.GetLocation(value);
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

            if (changedCell != null && changedCell.Location.Equals(location))
                return changedCell.Value;
            return parent.GetValue(location);
        }

        public override IRectangularField<T> SetValue(T value, CellLocation location)
        {
            CheckLocation(location);
            return new WrappingRectangularField<T>(this, new CellInfo<T>(location, value));
        }

        #endregion
    }
}
