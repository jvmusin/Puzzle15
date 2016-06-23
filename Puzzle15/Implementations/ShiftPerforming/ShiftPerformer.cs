using System;
using System.Linq;
using Puzzle15.Base.Field;
using Puzzle15.Interfaces;

namespace Puzzle15.Implementations.ShiftPerforming
{
    public class ShiftPerformer : IShiftPerformer
    {
        private readonly FieldCloner cloneField;

        public bool MutatesField { get; }

        public ShiftPerformer(bool mutatesField, FieldCloner fieldCloner = null)
        {
            if (fieldCloner == null)
                fieldCloner = field => field;

            MutatesField = mutatesField;
            cloneField = fieldCloner;
        }

        public static ShiftPerformer Mutable() => new ShiftPerformer(true);
        public static ShiftPerformer Immutable(FieldCloner fieldCloner) => new ShiftPerformer(false, fieldCloner);

        public IRectangularField<int> Perform(IRectangularField<int> field, int value)
        {
            var empty = field.GetLocation(0);
            var toShift = field.GetLocation(value);

            if (!empty.ByEdgeNeighbours.Contains(toShift))
                throw new ArgumentException("Requested cell is not a neighbour of empty cell");

            return cloneField(field).Swap(empty, toShift);
        }
    }
}
