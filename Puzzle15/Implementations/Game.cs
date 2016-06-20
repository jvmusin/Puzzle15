using Puzzle15.Base;
using Puzzle15.Interfaces;

namespace Puzzle15.Implementations
{
    public class Game : IGame
    {
        protected RectangularField<int> Field { get; }
        public IShiftPerformer ShiftPerformer { get; }

        internal Game(RectangularField<int> field, IShiftPerformer shiftPerformer, bool needCloneField = true)
        {
            Field = needCloneField ? field.Clone() : field;
            ShiftPerformer = shiftPerformer;
        }

        public IGame Shift(int value)
            => ShiftPerformer.Perform(this, Field, value);

        #region Indexers

        public CellLocation GetLocation(int value)
        {
            return Field.GetLocation(value);
        }

        public int this[CellLocation location]
        {
            get { return Field[location]; }
            protected set { Field[location] = value; }
        }

        #endregion

        #region Equals, GetHashCode and ToString methods

        protected bool Equals(Game other)
        {
            return Field.Equals(other.Field);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Game;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return Field.GetHashCode();
        }

        public override string ToString()
        {
            return Field.ToString();
        }

        #endregion
    }
}
