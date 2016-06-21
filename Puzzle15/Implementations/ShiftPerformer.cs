using System;
using System.Linq;
using Puzzle15.Base;
using Puzzle15.Interfaces;

namespace Puzzle15.Implementations
{
    public class ShiftPerformer : IShiftPerformer
    {
        private readonly bool createNewGame;
        private readonly FieldCloner cloneField;

        internal ShiftPerformer(bool createNewGame = false, FieldCloner fieldCloner = null)
        {
            if (fieldCloner == null)
                fieldCloner = field => field;

            this.createNewGame = createNewGame;
            cloneField = fieldCloner;
        }

        public IGame Perform(IGame game, RectangularField<int> gameField, int value)
        {
            var empty = gameField.GetLocation(0);
            var toShift = gameField.GetLocation(value);

            if (!empty.ByEdgeNeighbours.Contains(toShift))
                throw new ArgumentException("Requested cell is not a neighbour of empty cell");

            var newField = cloneField(gameField).Swap(empty, toShift);
            return createNewGame
                ? new Game(newField, this, false)
                : game;
        }
    }

    public delegate RectangularField<int> FieldCloner(RectangularField<int> original);
}
