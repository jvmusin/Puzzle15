using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Puzzle15.Base
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

        public static bool StructuralEquals<T>(T obj1, T obj2)
        {
            return StructuralComparisons.StructuralEqualityComparer.Equals(obj1, obj2);
        }

        public static int StructuralGetHashCode<T>(T obj)
        {
            return StructuralComparisons.StructuralEqualityComparer.GetHashCode(obj);
        }
    }

    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var value in source)
                action(value);
        }
    }
}
