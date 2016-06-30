using System;
using System.Collections;
using System.Collections.Generic;
using Puzzle15.Interfaces;
using RectangularField.Implementations.Base;
using RectangularField.Interfaces;

namespace Puzzle15.Implementations
{
    public class Game<TCell> : IGame<TCell>
    {
        private readonly IShiftPerformer<TCell> shiftPerformer;

        public int Turns { get; }
        public bool Finished => CurrentField.Equals(Target);
        public IGame<TCell> PreviousState { get; }
        public IField<TCell> CurrentField { get; }
        public IField<TCell> Target { get; }

        private Game(IField<TCell> initialField, IField<TCell> target, IShiftPerformer<TCell> shiftPerformer, IGame<TCell> previousState)
        {
            if (!initialField.Immutable || !target.Immutable)
                throw new ArgumentException("Sorry, only immutable fields allowed");

            PreviousState = previousState;
            CurrentField = initialField;
            Target = target;

            this.shiftPerformer = shiftPerformer;

            Turns = previousState?.Turns + 1 ?? 0;
        }

        internal Game(IField<TCell> initialField, IField<TCell> target, IShiftPerformer<TCell> shiftPerformer)
            : this(initialField.Clone(), target.Clone(), shiftPerformer, null)
        {
            if (shiftPerformer == null)
                throw new ArgumentNullException(nameof(shiftPerformer));
        }

        public IGame<TCell> Shift(TCell value)
        {
            var newField = shiftPerformer.Perform(CurrentField, value);
            return new Game<TCell>(newField, Target, shiftPerformer, this);
        }

        public IGame<TCell> Shift(CellLocation valueLocation)
        {
            var newField = shiftPerformer.Perform(CurrentField, valueLocation);
            return new Game<TCell>(newField, Target, shiftPerformer, this);
        }

        #region Enumerators

        public IEnumerator<CellInfo<TCell>> GetEnumerator() => CurrentField.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Indexers

        public CellLocation GetLocation(TCell value) => CurrentField.GetLocation(value);

        public TCell this[CellLocation location] => CurrentField[location];

        #endregion
    }
}
