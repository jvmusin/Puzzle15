using System;
using Puzzle15.Interfaces;
using RectangularField.Core;

namespace Puzzle15.Implementations
{
    public class GameFactory<TCell> : IGameFactory<TCell>
    {
        private readonly IGameFieldValidator<TCell> gameFieldValidator;
        private readonly IShiftPerformerFactory<TCell> shiftPerformerFactory;

        public GameFactory(IGameFieldValidator<TCell> gameFieldValidator, IShiftPerformerFactory<TCell> shiftPerformerFactory)
        {
            if (gameFieldValidator == null)
                throw new ArgumentNullException(nameof(gameFieldValidator));
            if (shiftPerformerFactory == null)
                throw new ArgumentNullException(nameof(shiftPerformerFactory));

            this.gameFieldValidator = gameFieldValidator;
            this.shiftPerformerFactory = shiftPerformerFactory;
        }

        public IGame<TCell> Create(IRectangularField<TCell> initialField, IRectangularField<TCell> target)
        {
            var validationResult = gameFieldValidator.Validate(initialField, target);
            if (!validationResult.Successful)
                throw new ArgumentException(validationResult.Cause);

            return new Game<TCell>(initialField, target, shiftPerformerFactory.Create());
        }
    }
}
