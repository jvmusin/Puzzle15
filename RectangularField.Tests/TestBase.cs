using System.Drawing;

namespace RectangularField.Tests
{
    public abstract class TestBase
    {
        protected static readonly Size DefaultFieldSize = new Size(3, 3);

        protected static readonly int[] DefaultFieldData =
        {
            1, 2, 3,
            4, -1, 5,
            6, 7, -2
        };
    }
}
