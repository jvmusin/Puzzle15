using System;
using System.Linq;

namespace Puzzle15
{
    public class Game : GameBase
    {
        public Game(int[][] table) : base(table)
        {
        }

        public Game(RectangularField<int> field) : this(field.ToTable())
        {
        }

        public void Shift(int value)
        {
            var empty = Field.GetLocation(0);
            var toShift = Field.GetLocation(value);

            var neighbour = empty.ByEdgeHeighbours.Contains(toShift);
            if (!neighbour)
                throw new ArgumentException("Requested cell is not a neighbour of empty cell");

            Field.Swap(empty, toShift);
        }
    }
}
