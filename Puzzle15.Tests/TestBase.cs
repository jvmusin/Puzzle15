using System;
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

        protected static RectangularField<T> FieldFromArray<T>(Size size, params T[] values)
        {
            return new RectangularField<T>(size)
                .Fill(location => values[location.Row*size.Width + location.Column]);
        }

        protected static RectangularField<T> FieldFromConstructor<T>(
            Func<Size, RectangularField<T>> constructor, Size size, params T[] values)
        {
            return constructor(size)
                .Fill(location => values[location.Row*size.Width + location.Column]);
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
