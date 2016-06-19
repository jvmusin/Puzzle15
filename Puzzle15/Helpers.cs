using System;
using System.Collections.Generic;
using System.Linq;

namespace Puzzle15
{
    public static class Helpers
    {
        public static T[][] CreateTable<T>(int height, int width)
        {
            return Enumerable
                .Range(0, height)
                .Select(x => new T[width])
                .ToArray();
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

    public static class DictionaryExtensions
    {
        public static TValue ComputeIfAbsent<TKey, TValue>(
            this IDictionary<TKey, TValue> source,
            TKey key, Func<TValue> getValue)
        {
            TValue value;
            if (!source.TryGetValue(key, out value))
                value = source[key] = getValue();
            return value;
        }
    }

    public static class ArrayExtensions
    {
        public static int GetHeight<T>(this T[][] source) => source.GetLength(0);
        public static int GetWidth<T>(this T[][] source) => source.GetLength(1);

        public static T GetValue<T>(this T[][] source, CellLocation location)
            => source[location.Row][location.Column];

        public static void SetValue<T>(this T[][] source, CellLocation location, T value)
            => source[location.Row][location.Column] = value;

        public static void Swap<T>(this T[] source, int position1, int position2)
        {
            var temp = source[position1];
            source[position1] = source[position2];
            source[position2] = temp;
        }
    }
}
