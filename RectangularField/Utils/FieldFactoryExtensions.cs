using System;
using System.Drawing;
using RectangularField.Interfaces;

namespace RectangularField.Utils
{
    public static class FieldFactoryExtensions
    {
        public static IField<T> Create<T>(this IFieldFactory<T> factory, Size size, params T[] values)
        {
            var height = size.Height;
            var width = size.Width;
            var elementCount = height * width;

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
    }
}
