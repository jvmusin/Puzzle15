﻿namespace Puzzle15
{
    public class ImmutableGameShiftPerformer : IShiftPerformer
    {
        private static readonly IShiftPerformer StandardShiftPerformer = new ShiftPerformer();

        public IGame Perform(IGame game, RectangularField<int> gameField, int value)
        {
            var newField = gameField.Clone();
            var newGame = new Game(newField, this, false);
            return StandardShiftPerformer.Perform(newGame, newField, value);
        }
    }
}
