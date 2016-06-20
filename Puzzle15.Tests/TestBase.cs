using System.Drawing;
using FakeItEasy;
using Puzzle15.Base;

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

        protected static RectangularField<T> FieldFromArray<T>(Size size, params T[] source)
        {
            var field = new RectangularField<T>(size);
            field.Fill(location => source[location.Row * size.Width + location.Column]);
            return field;
        }

        protected static T StrictFake<T>()
        {
            return A.Fake<T>(x => x.Strict());
        }
    }
}
