using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Puzzle15.Tests
{
    [TestFixture]
    public class RectangularField_Should
    {
        private Lazy<RectangularField<int>> field;

        [SetUp]
        public void SetUp()
        {
            field = new Lazy<RectangularField<int>>(() => FromArray(DefaultFieldSize, DefaultFieldData));
        }

        [Test]
        public void WorkWithDifferentSizesCorrectly(
            [Values(-1232, 0, 133)] int width,
            [Values(-13123, 0, 2)] int height)
        {
            var size = new Size(width, height);

            if (size.Height < 0 || size.Width < 0)
            {
                new Action(() => new RectangularField<int>(size)).ShouldThrow<Exception>();
            }
            else
            {
                var field = new RectangularField<int>(size);

                field.Size.Should().Be(size);
                field.Height.Should().Be(size.Height);
                field.Width.Should().Be(size.Width);
            }

        }

        [Test]
        public void SwapElements_WhenNotConnected()
        {
            var size = new Size(3, 3);

            var oldField = FromArray(size,
                1, 2, 3,
                4, 5, 17,
                9, 0, 0);
            var expectedField = FromArray(size,
                17, 2, 3,
                4, 5, 1,
                9, 0, 0);

            oldField.Swap(new CellLocation(0, 0), new CellLocation(1, 2));
            
            oldField.Should().BeEquivalentTo(expectedField);
        }

        [Test]
        public void SwapElementAtSamePlace()
        {
            var size = new Size(3, 3);

            var oldField = FromArray(size,
                1, 2, 3,
                4, 5, 17,
                9, 0, 0);
            var expectedField = FromArray(size,
                1, 2, 3,
                4, 5, 17,
                9, 0, 0);

            oldField.Swap(new CellLocation(1, 2), new CellLocation(1, 2));

            oldField.Should().BeEquivalentTo(expectedField);
        }

        [Test]
        public void Fail_WhenElementNotOnField()
        {
            new Action(() => field.Value.Swap(new CellLocation(0, 0), new CellLocation(3, 0)))
                .ShouldThrow<Exception>();
        }

        #region Data preparing

        private static readonly Size DefaultFieldSize = new Size(3, 3);

        private static readonly int[] DefaultFieldData =
        {
            1, 2, 3,
            4, -1, 5,
            6, 7, -2
        };

        private static RectangularField<T> FromArray<T>(Size size, params T[] source)
        {
            var field = new RectangularField<T>(size);
            field.Fill(location => source[location.Row * 3 + location.Column]);
            return field;
        }

        #endregion
    }
}
