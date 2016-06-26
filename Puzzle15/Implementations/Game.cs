using System;
using System.Collections;
using System.Collections.Generic;
using Puzzle15.Interfaces;
using RectangularField.Core;

namespace Puzzle15.Implementations
{
    public class Game<TCell> : IGame<TCell>
    {
        private readonly IRectangularField<TCell> currentField;
        private readonly IRectangularField<TCell> target;
        private readonly IShiftPerformer<TCell> shiftPerformer;
        private readonly IGame<TCell> previousState;

        public int Turns { get; }
        public bool Finished => currentField.Equals(target);
        public IGame<TCell> PreviousState => GetPreviousState();
        public IReadOnlyRectangularField<TCell> CurrentField => GetAsReadOnlyField(currentField);
        public IReadOnlyRectangularField<TCell> Target => GetAsReadOnlyField(target);

        private Game(IRectangularField<TCell> initialField, IRectangularField<TCell> target, IShiftPerformer<TCell> shiftPerformer,
            IGame<TCell> previousState)
        {
            currentField = initialField;
            this.target = target;

            this.shiftPerformer = shiftPerformer;

            Turns = previousState?.Turns + 1 ?? 0;
            this.previousState = previousState;
        }

        internal Game(IRectangularField<TCell> initialField, IRectangularField<TCell> target, IShiftPerformer<TCell> shiftPerformer)
            : this(initialField.Clone(), target.Clone(), shiftPerformer, null)
        {
            if (shiftPerformer == null)
                throw new ArgumentNullException(nameof(shiftPerformer));
        }

        public IGame<TCell> Shift(TCell value)
        {
            var newField = shiftPerformer.PerformShift(currentField, value);
            return new Game<TCell>(newField, target, shiftPerformer, this);
        }

        public IGame<TCell> Shift(CellLocation valueLocation)
        {
            var newField = shiftPerformer.PerformShift(currentField, valueLocation);
            return new Game<TCell>(newField, target, shiftPerformer, this);
        }

        private IGame<TCell> GetPreviousState()
        {
            if (!currentField.Immutable)
                throw new NotSupportedException("Previous state is available only for immutable fields");

            return previousState;
        }

        private IReadOnlyRectangularField<TCell> GetAsReadOnlyField(IRectangularField<TCell> field)
        {
            var readOnlyField = field as IReadOnlyRectangularField<TCell>;

            if (readOnlyField == null)
                throw new NotSupportedException(
                    $"Target should implement {nameof(IReadOnlyRectangularField<TCell>)} interface");

            return readOnlyField;
        }

        #region Enumerators

        public IEnumerator<CellInfo<TCell>> GetEnumerator() => currentField.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Indexers

        public CellLocation GetLocation(TCell value) => currentField.GetLocation(value);

        public TCell this[CellLocation location] => currentField[location];

        #endregion
    }
}
