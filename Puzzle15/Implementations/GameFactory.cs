using System;
using Puzzle15.Base;
using Puzzle15.Base.Field;
using Puzzle15.Interfaces;

namespace Puzzle15.Implementations
{
    public class GameFactory : IGameFactory
    {
        public IGameFieldValidator GameFieldValidator { get; }
        public IShiftPerformerFactory ShiftPerformerFactory { get; }

        public GameFactory(IGameFieldValidator gameFieldValidator, IShiftPerformerFactory shiftPerformerFactory)
        {
            GameFieldValidator = gameFieldValidator;
            ShiftPerformerFactory = shiftPerformerFactory;
        }

        public IGame Create(IRectangularField<int> initialField)
        {
            var validationResult = GameFieldValidator.Validate(initialField);
            if (!validationResult.Successful)
                throw new ArgumentException(validationResult.Cause);

            return new Game(initialField, ShiftPerformerFactory.Create());
        }
    }
}
