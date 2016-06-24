using System.Drawing;
using FakeItEasy;
using Puzzle15.Base.Field;

namespace Puzzle15.Tests
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

        protected static IRectangularField<T> FieldFromArray<T>(Size size, params T[] values)
        {
            return FieldFromConstructor(sz => new RectangularField<T>(sz), size, values);
        }

        protected static IRectangularField<T> FieldFromConstructor<T>(
            FieldConstructor<T> createInstance, Size size, params T[] values)
        {
            return createInstance(size)
                .Fill(cellInfo =>
                {
                    var location = cellInfo.Location;
                    var row = location.Row;
                    var column = location.Column;
                    return values[row * size.Width + column];
                });
        }

        protected static void Swap<T>(ref T obj1, ref T obj2)
        {
            var temp = obj1;
            obj1 = obj2;
            obj2 = temp;
        }

        protected static T StrictFake<T>()
        {
            return A.Fake<T>(x => x.Strict());
        }
    }
}
