using System.Drawing;
using FakeItEasy;
using RectangularField.Core;
using RectangularField.Factories;
using RectangularField.Utils;

namespace Puzzle15.Tests
{
    public abstract class TestBase
    {
        protected static IRectangularField<T> CreateMutableField<T>(Size size, params T[] values)
        {
            return new RectangularFieldFactory<T>().Create(size, values);
        }

        protected static IRectangularField<T> CreateImmutableField<T>(Size size, params T[] values)
        {
            return new ImmutableRectangularFieldFactory<T>().Create(size, values);
        }

        protected static T StrictFake<T>()
        {
            return A.Fake<T>(x => x.Strict());
        }
    }
}
