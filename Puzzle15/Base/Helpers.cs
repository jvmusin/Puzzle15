using System.Drawing;
using System.Linq;

namespace Puzzle15.Base
{
    public static class Helpers
    {
        public static T[][] CreateTable<T>(Size size)
        {
            return Enumerable.Range(0, size.Height)
                .Select(x => new T[size.Width])
                .ToArray();
        }
    }
}
