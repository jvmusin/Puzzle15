using System.Drawing;
using RectangularField.Core;
using RectangularField.Utils;

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

        protected static IRectangularField<T> FieldFromConstructor<T>(
            FieldConstructor<T> createInstance, Size size, params T[] values)
        {
            return createInstance(size)
                .Fill(cellInfo =>
                {
                    var location = cellInfo.Location;
                    var row = location.Row;
                    var column = location.Column;
                    return values[row*size.Width + column];
                });
        }
    }
}
