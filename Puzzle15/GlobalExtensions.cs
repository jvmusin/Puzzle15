using System;
using System.Collections.Generic;

namespace Puzzle15
{
    public static class GlobalExtensions
    {
        public static void Swap<T>(this T[] source, int position1, int position2)
        {
            var temp = source[position1];
            source[position1] = source[position2];
            source[position2] = temp;
        }

        public static TValue ComputeIfAbsent<TKey, TValue>(
            this Dictionary<TKey, TValue> source,
            TKey key, Func<TValue> getValue)
        {
            TValue value;
            if (!source.TryGetValue(key, out value))
                value = source[key] = getValue();
            return value;
        }
    }
}
