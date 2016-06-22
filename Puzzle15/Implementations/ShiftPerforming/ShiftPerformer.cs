using System;
using System.Linq;
using Puzzle15.Base;
using Puzzle15.Interfaces;

namespace Puzzle15.Implementations.ShiftPerforming
{
    public class ShiftPerformer : IShiftPerformer
    {
        private readonly FieldCloner cloneField;

        public bool MutatesGame { get; }

        public ShiftPerformer(bool mutatesGame, FieldCloner fieldCloner = null)
        {
            if (fieldCloner == null)
                fieldCloner = field => field;

            MutatesGame = mutatesGame;
            cloneField = fieldCloner;
        }

        public static ShiftPerformer Mutable() => new ShiftPerformer(true);
        public static ShiftPerformer Immutable(FieldCloner fieldCloner) => new ShiftPerformer(false, fieldCloner);

        public IGame Perform(IGame game, IRectangularField<int> gameField, int value)
        {
            var empty = gameField.GetLocation(0);
            var toShift = gameField.GetLocation(value);

            if (!empty.ByEdgeNeighbours.Contains(toShift))
                throw new ArgumentException("Requested cell is not a neighbour of empty cell");

            var newField = cloneField(gameField).Swap(empty, toShift);
            return MutatesGame
                ? game
                : new Game(newField, this, false);
        }
    }
}
