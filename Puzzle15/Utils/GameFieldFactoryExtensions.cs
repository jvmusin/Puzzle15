using System;
using System.Drawing;
using Puzzle15.Interfaces;
using Puzzle15.Interfaces.Factories;

namespace Puzzle15.Utils
{
    public static class GameFieldFactoryExtensions
    {
        public static IGameField<T> Create<T>(this IGameFieldFactory<T> factory, Size size, params T[] values)
        {
            var height = size.Height;
            var width = size.Width;
            var elementCount = height * width;

            if (values.Length != elementCount)
                throw new ArgumentException("Values count should be size.Height * size.Width");

            return (IGameField<T>) factory.Create(size).Fill(cellInfo =>
            {
                var location = cellInfo.Location;
                var row = location.Row;
                var column = location.Column;
                return values[row * width + column];
            });
        }
    }
}
