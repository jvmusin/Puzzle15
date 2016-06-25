using System;
using Puzzle15.Interfaces;
using RectangularField.Core;

namespace Puzzle15.Implementations
{
    public class GameFactory : IGameFactory
    {
        public IGameFieldValidator GameFieldValidator { get; }

        public GameFactory(IGameFieldValidator gameFieldValidator)
        {
            if (gameFieldValidator == null)
                throw new ArgumentNullException(nameof(gameFieldValidator));
            GameFieldValidator = gameFieldValidator;
        }

        public IGame Create(IRectangularField<int> initialField)
        {
            var validationResult = GameFieldValidator.Validate(initialField);
            if (!validationResult.Successful)
                throw new ArgumentException(validationResult.Cause, nameof(initialField));

            return new Game(initialField);
        }
    }
}
