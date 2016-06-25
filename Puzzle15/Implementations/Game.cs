using System;
using System.Collections;
using System.Collections.Generic;
using Puzzle15.Interfaces;
using RectangularField.Core;

namespace Puzzle15.Implementations
{
    internal class Game<TCell> : IGame<TCell>
    {
        private readonly IRectangularField<TCell> field;
        private readonly IRectangularField<TCell> target;
        private readonly IShiftPerformer<TCell> shiftPerformer;
        private readonly IGame<TCell> previousState;

        public int Turns { get; }
        public bool Finished => field.Equals(target);
        public IGame<TCell> PreviousState => GetPreviousState();

        public IReadOnlyRectangularField<TCell> Target
        {
            get
            {
                var readOnlyTarget = target as IReadOnlyRectangularField<TCell>;

                if (readOnlyTarget == null)
                    throw new NotSupportedException(
                        $"Target should implement {nameof(IReadOnlyRectangularField<TCell>)} interface");

                return readOnlyTarget;
            }
        }
        
        private Game(IRectangularField<TCell> initialField, IRectangularField<TCell> target, IShiftPerformer<TCell> shiftPerformer,
            IGame<TCell> previousState)
        {
            field = initialField;
            this.target = target;

            this.shiftPerformer = shiftPerformer;

            Turns = previousState?.Turns + 1 ?? 0;
            this.previousState = previousState;
        }

        public Game(IRectangularField<TCell> initialField, IRectangularField<TCell> target, IShiftPerformer<TCell> shiftPerformer)
            : this(initialField.Clone(), target.Clone(), shiftPerformer, null)
        {
            if (shiftPerformer == null)
                throw new ArgumentNullException(nameof(shiftPerformer));
        }

        public IGame<TCell> Shift(TCell value)
        {
            var newField = shiftPerformer.Perform(field, value);
            return new Game<TCell>(newField, target, shiftPerformer, this);
        }

        public IGame<TCell> Shift(CellLocation valueLocation)
        {
            var newField = shiftPerformer.Perform(field, valueLocation);
            return new Game<TCell>(newField, target, shiftPerformer, this);
        }

        private IGame<TCell> GetPreviousState()
        {
            if (!field.Immutable)
                throw new NotSupportedException("Previous state is available only for immutable fields");

            return previousState;
        }

        #region Enumerators

        public IEnumerator<CellInfo<TCell>> GetEnumerator() => field.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Indexers

        public CellLocation GetLocation(TCell value) => field.GetLocation(value);

        public TCell this[CellLocation location] => field[location];

        #endregion
    }
}
