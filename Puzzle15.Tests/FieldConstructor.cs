using System.Drawing;
using Puzzle15.Base.Field;

namespace Puzzle15.Tests
{
    public delegate IRectangularField<T> FieldConstructor<T>(Size size);
}
