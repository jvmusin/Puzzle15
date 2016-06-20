using System;

namespace Puzzle15
{
    public class GameFactory : IGameFactory
    {
        public IGameFieldValidator GameFieldValidator { get; }
        public IShiftPerformer ShiftPerformer { get; }

        public GameFactory(IGameFieldValidator gameFieldValidator, IShiftPerformer shiftPerformer)
        {
            GameFieldValidator = gameFieldValidator;
            ShiftPerformer = shiftPerformer;
        }

        public IGame Create(RectangularField<int> initialField)
        {
            var validationResult = GameFieldValidator.Validate(initialField);
            if (validationResult != null)
                throw new ArgumentException(validationResult.Cause);

            return new Game(initialField, ShiftPerformer);
        }
    }
}
