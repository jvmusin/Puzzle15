using System;
using System.Collections;
using System.Drawing;
using System.Linq;

namespace Puzzle15.Base
{
    public static class Helpers
    {
        public static T[][] CreateTable<T>(int height, int width)
        {
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
            if (width <= 0)  throw new ArgumentOutOfRangeException(nameof(width));

            return Enumerable
                .Range(0, height)
                .Select(x => new T[width])
                .ToArray();
        }

        public static T[][] CreateTable<T>(Size size) => CreateTable<T>(size.Height, size.Width);

        public static bool Equals<T>(T obj1, T obj2)
        {
            return StructuralComparisons.StructuralEqualityComparer.Equals(obj1, obj2);
        }

        public static int GetHashCode<T>(T obj)
        {
            return StructuralComparisons.StructuralEqualityComparer.GetHashCode(obj);
        }
    }
}
