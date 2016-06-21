using System.Collections.Generic;

namespace Puzzle15.Tests
{
    public class SameObjectsComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            return ReferenceEquals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return 0;
        }
    }
}
