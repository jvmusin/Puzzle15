using System.Drawing;
using FakeItEasy;
using RectangularField.Core;
using RectangularField.Implementations;
using RectangularField.Utils;

namespace Puzzle15.Tests
{
    public abstract class TestBase
    {
        protected static IRectangularField<T> CreateMutableField<T>(Size size, params T[] values)
        {
            return CreateField(sz => new RectangularField<T>(sz), size, values);
        }

        protected static IRectangularField<T> CreateImmutableField<T>(Size size, params T[] values)
        {
            return CreateField(sz => new ImmutableRectangularField<T>(sz), size, values);
        }

        protected static IRectangularField<T> CreateField<T>(
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

        protected static T StrictFake<T>()
        {
            return A.Fake<T>(x => x.Strict());
        }
    }
}
