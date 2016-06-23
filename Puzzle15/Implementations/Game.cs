using Puzzle15.Base.Field;
using Puzzle15.Interfaces;

namespace Puzzle15.Implementations
{
    internal class Game : IGame
    {
        private readonly IRectangularField<int> field;
        private readonly IShiftPerformer shiftPerformer;

        internal Game(IRectangularField<int> field, IShiftPerformer shiftPerformer, bool needCloneField = true)
        {
            this.field = needCloneField ? field.Clone() : field;
            this.shiftPerformer = shiftPerformer;
        }

        public IGame Shift(int value)
        {
            var newField = shiftPerformer.Perform(field, value);

            return field.Immutable
                ? new Game(newField, shiftPerformer, false)
                : this;
        }

        #region Indexers

        public CellLocation GetLocation(int value)
        {
            return field.GetLocation(value);
        }

        public int this[CellLocation location]
        {
            get { return field[location]; }
            protected set { field[location] = value; }
        }

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
