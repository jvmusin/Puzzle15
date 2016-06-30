using System;
using System.Drawing;
using RectangularField.Implementations.Base;
using RectangularField.Interfaces;

namespace RectangularField.Implementations
{
    public class StandardFieldShuffler<T> : IFieldShuffler<T>
    {
        private readonly Random rnd = new Random();

        public IField<T> Shuffle(IField<T> field, int quality)
        {
            var fieldSize = field.Size;

            for (var done = 0; done < quality; done++)
            {
                var prevLocation = RandomLocation(fieldSize);
                var newLocation = RandomLocation(fieldSize);

                while (newLocation.Equals(prevLocation))
                    newLocation = RandomLocation(fieldSize);

                field = field.Swap(prevLocation, newLocation);
            }

            return field;
        }

        private CellLocation RandomLocation(Size fieldSize)
        {
            var row = rnd.Next(fieldSize.Height);
            var column = rnd.Next(fieldSize.Width);
            return new CellLocation(row, column);
        }
    }
}
