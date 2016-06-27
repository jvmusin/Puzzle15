using System;
using System.Collections.Generic;
using Puzzle15.Interfaces;
using RectangularField.Implementations;
using RectangularField.Implementations.Base;
using RectangularField.Interfaces;

namespace Puzzle15.Implementations
{
    public class GameField<T> : RectangularFieldBase<T>, IGameField<T>
    {
        private readonly IRectangularField<T> backingField;
        private readonly IGameFieldShuffler<T> gameFieldShuffler;

        public override bool Immutable => true;

        #region Constructors

        internal GameField(IRectangularField<T> backingField, IGameFieldShuffler<T> gameFieldShuffler) : base(backingField.Size)
        {
            if (gameFieldShuffler == null)
                throw new ArgumentNullException(nameof(gameFieldShuffler));

            this.backingField = backingField;
            this.gameFieldShuffler = gameFieldShuffler;
        }

        private GameField<T> FromCurrent(IRectangularField<T> field)
        {
            return new GameField<T>(field, gameFieldShuffler);
        }

        #endregion

        #region Primary actions

        public override IRectangularField<T> Swap(CellLocation location1, CellLocation location2)
        {
            return FromCurrent(backingField.Swap(location1, location2));
        }

        public override IRectangularField<T> Fill(CellConverter<T, T> getValue)
        {
            return FromCurrent(backingField.Fill(getValue));
        }

        public override IRectangularField<T> Clone()
        {
            return FromCurrent(backingField.Clone());
        }

        public IGameField<T> Shuffle(int quality)
        {
            return gameFieldShuffler.Shuffle(this, quality);
        }

        #endregion

        #region Indexers

        public override IEnumerable<CellLocation> GetLocations(T value)
        {
            return backingField.GetLocations(value);
        }

        public override CellLocation GetLocation(T value)
        {
            return backingField.GetLocation(value);
        }

        public override T GetValue(CellLocation location)
        {
            return backingField.GetValue(location);
        }

        public override IRectangularField<T> SetValue(T value, CellLocation location)
        {
            return FromCurrent(backingField.SetValue(value, location));
        }

        #endregion
    }
}
