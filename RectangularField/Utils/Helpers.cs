using System;
using System.Collections;
using System.Drawing;
using System.Linq;

namespace RectangularField.Utils
{
    public static class Helpers
    {
        public static T[][] CreateTable<T>(int height, int width)
        {
            if (height < 0) throw new ArgumentOutOfRangeException(nameof(height));
            if (width < 0)  throw new ArgumentOutOfRangeException(nameof(width));

            return Enumerable
                .Range(0, height)
                .Select(x => new T[width])
                .ToArray();
        }

        public static T[][] CreateTable<T>(Size size) => CreateTable<T>(size.Height, size.Width);

        public static bool Equals<T>(T x, T y)
        {
            return StructuralComparisons.StructuralEqualityComparer.Equals(x, y);
        }

        public static int GetHashCode<T>(T obj)
        {
            return StructuralComparisons.StructuralEqualityComparer.GetHashCode(obj);
        }

        public static int Compare(object x, object y)
        {
            return StructuralComparisons.StructuralComparer.Compare(x, y);
        }
    }
}
