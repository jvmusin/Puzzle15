using System;
using System.Linq;
using Puzzle15.Base.Field;
using Puzzle15.Interfaces;

namespace Puzzle15.Implementations
{
    internal class Game : IGame
    {
        private readonly IRectangularField<int> field;

        private Game(IRectangularField<int> field, bool needCloneField)
        {
            this.field = needCloneField ? field.Clone() : field;
        }

        internal Game(IRectangularField<int> field) : this(field, true)
        {
        }

        public IGame Shift(int value)
        {
            var empty = field.GetLocation(0);
            var toShift = field.GetLocation(value);

            if (!empty.ByEdgeNeighbours.Contains(toShift))
                throw new ArgumentException("Requested cell is not a neighbour of empty cell");

            var newField = field.Swap(empty, toShift);

            return field.Immutable
                ? new Game(newField, false)
                : this;
        }

        #region Indexers

        public CellLocation GetLocation(int value) => field.GetLocation(value);

        public int this[CellLocation location] => field[location];

        #endregion

        #region Equals, GetHashCode and ToString methods

        protected bool Equals(Game other)
        {
            return field.Equals(other.field);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Game;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return field.GetHashCode();
        }

        public override string ToString()
        {
            return field.ToString();
        }

        #endregion
    }
}
