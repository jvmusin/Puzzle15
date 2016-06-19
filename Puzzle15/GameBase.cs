using System;
using System.Linq;

namespace Puzzle15
{
    public class GameBase
    {
        protected virtual RectangularField<int> Field { get; }

        public GameBase(int[][] table)
        {
            CheckTable(table);

            var height = table.GetLength(0);
            var width = table.GetLength(1);
            var field = new RectangularField<int>(height, width);
            field.Fill(location => table[location.Row][location.Column]);

            Field = field;
        }

        public GameBase(RectangularField<int> field) : this(field.ToTable())
        {
        }

        private static void CheckTable(int[][] table)
        {
            var height = table.GetLength(0);
            var width = table.GetLength(1);
            var elementCount = height * width;

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

        public int this[int row, int column]
        {
            get { return this[new CellLocation(row, column)]; }
            protected set { this[new CellLocation(row, column)] = value; }
        }

        public virtual int this[CellLocation location]
        {
            get { return Field[location]; }
            protected set { Field[location] = value; }
        }
    }
}
