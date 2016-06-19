using System;
using System.Linq;

namespace Puzzle15
{
    public class Game : GameBase
    {
        public Game(RectangularField<int> field) : base(field)
        {
        }

        protected Game(RectangularField<int> field, bool needCheck = true, bool needClone = true)
            : base(field, needCheck, needClone)
        {
        }

        public override GameBase Shift(int value)
        {
            var empty = Field.GetLocation(0);
            var toShift = Field.GetLocation(value);

            if (!empty.ByEdgeHeighbours.Contains(toShift))
                throw new ArgumentException("Requested cell is not a neighbour of empty cell");

            Field.Swap(empty, toShift);
            return this;
        }
    }
}
