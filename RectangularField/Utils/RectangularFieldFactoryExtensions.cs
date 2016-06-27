using System;
using System.Drawing;
using System.Linq;
using RectangularField.Core;

namespace RectangularField.Utils
{
    public static class RectangularFieldFactoryExtensions
    {
        public static IRectangularField<T> Create<T>(this IRectangularFieldFactory<T> factory, Size size, params T[] values)
        {
            var height = size.Height;
            var width = size.Width;
            var elementCount = height*width;

            if (values.Length != elementCount)
                throw new ArgumentException("Values count should be size.Height * size.Width");

            return factory.Create(size).Fill(cellInfo =>
            {
                var location = cellInfo.Location;
                var row = location.Row;
                var column = location.Column;
                return values[row * width + column];
            });
        }

        public static IRectangularField<T> Create<T>(this IRectangularFieldFactory<T> factory, T[,] values)
        {
            var size = new Size(values.GetLength(0), values.GetLength(1));
            var valuesArray = values.Cast<T>().ToArray();
            return factory.Create(size, valuesArray);
        }
    }
}
