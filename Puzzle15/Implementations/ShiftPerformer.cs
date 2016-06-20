using System;
using System.Linq;
using Puzzle15.Base;
using Puzzle15.Interfaces;

namespace Puzzle15.Implementations
{
    public class ShiftPerformer : IShiftPerformer
    {
        public IGame Perform(IGame game, RectangularField<int> gameField, int value)
        {
            var empty = gameField.GetLocation(0);
            var toShift = gameField.GetLocation(value);

            if (!empty.ByEdgeHeighbours.Contains(toShift))
                throw new ArgumentException("Requested cell is not a neighbour of empty cell");

            gameField.Swap(empty, toShift);
            return game;
        }
    }
}
