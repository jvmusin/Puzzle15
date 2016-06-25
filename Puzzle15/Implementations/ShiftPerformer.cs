using System;
using System.Linq;
using Puzzle15.Interfaces;
using RectangularField.Core;

namespace Puzzle15.Implementations
{
    public class ShiftPerformer : IShiftPerformer<int>
    {
        public IRectangularField<int> Perform(IRectangularField<int> field, int value)
        {
            return Perform(field, field.GetLocation(value));
        }

        public IRectangularField<int> Perform(IRectangularField<int> field, CellLocation valueLocation)
        {
            var emptyLocation = field.GetLocation(0);

            if (valueLocation == null) throw new ArgumentException("Empty cell not found");
            if (emptyLocation == null) throw new ArgumentException("Requested cell not found");

            if (!emptyLocation.ByEdgeNeighbours.Contains(valueLocation))
                throw new ArgumentException("Requested cell is not a neighbour of empty cell");

            return field.Swap(emptyLocation, valueLocation);
        }
    }
}