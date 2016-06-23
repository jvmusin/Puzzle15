using System;
using System.Linq;
using Puzzle15.Base.Field;
using Puzzle15.Interfaces;

namespace Puzzle15.Implementations
{
    public class ShiftPerformer : IShiftPerformer
    {
        public IRectangularField<int> Perform(IRectangularField<int> field, int value)
        {
            var empty = field.GetLocation(0);
            var toShift = field.GetLocation(value);

            if (!empty.ByEdgeNeighbours.Contains(toShift))
                throw new ArgumentException("Requested cell is not a neighbour of empty cell");

            return field.Swap(empty, toShift);
        }
    }
}
