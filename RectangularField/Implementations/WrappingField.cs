using System;
using RectangularField.Implementations.Base;
using RectangularField.Interfaces;

namespace RectangularField.Implementations
{
    public class WrappingField<T> : FieldBase<T>
    {
        private readonly IField<T> parent;
        private readonly CellInfo<T> changedCell;

        public override bool Immutable => true;

        private WrappingField(IField<T> parent, CellInfo<T> changedCell)
            : base(parent.Size)
        {
            this.parent = parent;
            this.changedCell = changedCell;
        }

        public WrappingField(IField<T> parent)
            : this(parent, null)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));
        }

        #region Primary actions

        public override IField<T> Clone()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var field = new WrappingField<T>(this);
            field.Shuffler = Shuffler;
            return field;
        }

        #endregion

        #region Indexers

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

        public override IField<T> SetValue(T value, CellLocation location)
        {
            CheckLocation(location);
            return new WrappingField<T>(this, new CellInfo<T>(location, value));
        }

        #endregion
    }
}
