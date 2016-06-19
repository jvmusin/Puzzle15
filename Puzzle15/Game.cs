using System;
using System.Linq;

namespace Puzzle15
{
    public class Game
    {
        private readonly RectangularField<int> field;

        public Game(int[][] table)
        {
            CheckTable(table);

            var height = table.Length;
            var width = table[0].Length;
            field = new RectangularField<int>(height, width);
            field.Fill(location => table[location.Row][location.Column]);
        }

        public Game(RectangularField<int> field) : this(field.ToTable())
        {
        }

        private static void CheckTable(int[][] table)
        {
            var height = table.GetLength(0);
            var width = table.GetLength(1);
            var elementCount = height*width;

            if (elementCount == 0)
                throw new ArgumentException("Field doesn't have any cell");
            
            if (table.Any(row => row.Length != table[0].Length))
                throw new ArgumentException("Field is not rectangular");
            
            var elements = table.SelectMany(row => row).Distinct().ToList();
            if (elements.Count != elementCount)
                throw new ArgumentException("Not all elements are distinct");
            
            if (elements.Min() != 0)
                throw new ArgumentException("Field doesn't contain an empty cell");
            
            if (elements.Max() != elements.Count - 1)
                throw new ArgumentException("Some values are skipped");
        }

        public void Shift(int value)
        {
            var empty = field.GetLocation(0);
            var toShift = field.GetLocation(value);

            var neighbour = empty.ByEdgeHeighbours.Contains(toShift);
            if (!neighbour)
                throw new ArgumentException("Requested cell is not a neighbour of empty cell");

            field.Swap(empty, toShift);
        }

        public int this[int row, int column]
        {
            get { return this[new CellLocation(row, column)]; }
            private set { this[new CellLocation(row, column)] = value; }
        }

        public int this[CellLocation location]
        {
            get { return field[location]; }
            private set { field[location] = value; }
        }
    }
}
