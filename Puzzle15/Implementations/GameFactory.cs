﻿using System;
using Puzzle15.Interfaces;
using RectangularField.Core;

namespace Puzzle15.Implementations
{
    public class GameFactory<TCell> : IGameFactory<TCell>
    {
        private readonly IFieldValidator<TCell> fieldValidator;
        private readonly IShiftPerformerFactory<TCell> shiftPerformerFactory;

        public GameFactory(IFieldValidator<TCell> fieldValidator, IShiftPerformerFactory<TCell> shiftPerformerFactory)
        {
            if (fieldValidator == null)
                throw new ArgumentNullException(nameof(fieldValidator));
            if (shiftPerformerFactory == null)
                throw new ArgumentNullException(nameof(shiftPerformerFactory));

            this.fieldValidator = fieldValidator;
            this.shiftPerformerFactory = shiftPerformerFactory;
        }

        public IGame<TCell> Create(IRectangularField<TCell> initialField, IRectangularField<TCell> target)
        {
            var validationResult = fieldValidator.Validate(initialField, target);
            if (!validationResult.Successful)
                throw new ArgumentException(validationResult.Cause);

            return new Game<TCell>(initialField, target, shiftPerformerFactory.Create());
        }
    }
}
