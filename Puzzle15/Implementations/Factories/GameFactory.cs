using System;
using Puzzle15.Interfaces;
using Puzzle15.Interfaces.Factories;
using RectangularField.Interfaces;

namespace Puzzle15.Implementations.Factories
{
    public class GameFactory<TCell> : IGameFactory<TCell>
    {
        private readonly IGameFieldValidatorFactory<TCell> gameFieldValidatorFactory;
        private readonly IShiftPerformerFactory<TCell> shiftPerformerFactory;

        public GameFactory(IGameFieldValidatorFactory<TCell> gameFieldValidatorFactory, IShiftPerformerFactory<TCell> shiftPerformerFactory)
        {
            if (gameFieldValidatorFactory == null)
                throw new ArgumentNullException(nameof(gameFieldValidatorFactory));
            if (shiftPerformerFactory == null)
                throw new ArgumentNullException(nameof(shiftPerformerFactory));

            this.gameFieldValidatorFactory = gameFieldValidatorFactory;
            this.shiftPerformerFactory = shiftPerformerFactory;
        }

        public IGame<TCell> Create(IField<TCell> initialField, IField<TCell> target)
        {
            var gameFieldValidator = gameFieldValidatorFactory.Create();

            var validationResult = gameFieldValidator.Validate(initialField, target);
            if (!validationResult.Successful)
                throw new ArgumentException(validationResult.Cause);

            return new Game<TCell>(initialField, target, shiftPerformerFactory.Create());
        }
    }
}
