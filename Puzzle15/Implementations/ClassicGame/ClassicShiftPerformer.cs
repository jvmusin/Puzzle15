using System;
using System.Linq;
using Puzzle15.Interfaces;
using RectangularField.Implementations.Base;

namespace Puzzle15.Implementations.ClassicGame
{
    public class ClassicShiftPerformer : IShiftPerformer<int>
    {
        public IGameField<int> PerformShift(IGameField<int> field, int value)
        {
            return PerformShift(field, field.GetLocation(value));
        }

        public IGameField<int> PerformShift(IGameField<int> field, CellLocation valueLocation)
        {
            var emptyLocation = field.GetLocation(0);

            if (valueLocation == null) throw new ArgumentException("Empty cell not found");
            if (emptyLocation == null) throw new ArgumentException("Requested cell not found");

            if (!emptyLocation.ByEdgeNeighbours.Contains(valueLocation))
                throw new ArgumentException("Requested cell is not a neighbour of empty cell");

            return (IGameField<int>) field.Swap(emptyLocation, valueLocation);
        }
    }
}
