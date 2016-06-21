using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using Puzzle15.Base;

namespace Puzzle15.Tests
{
    [TestFixture]
    public class RectangularField_Should : TestBase
    {
        private Lazy<RectangularField<int>> field;

        [SetUp]
        public void SetUp()
        {
            field = new Lazy<RectangularField<int>>(() => FieldFromArray(DefaultFieldSize, DefaultFieldData));
        }

        #region Size tests

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

        #endregion

        #region Swap tests

        [Test]
        public void SwapElements_WhenNotConnectedByEdge()
        {
            var size = new Size(3, 3);

            var oldField = FieldFromArray(size,
                1, 2, 3,
                4, 5, 17,
                9, 0, 0);
            var expectedField = FieldFromArray(size,
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

            var oldField = FieldFromArray(size,
                1, 2, 3,
                4, 5, 17,
                9, 0, 0);
            var expectedField = oldField.Clone();

            oldField.Swap(new CellLocation(1, 2), new CellLocation(1, 2));

            oldField.Should().BeEquivalentTo(expectedField);
        }

        [Test]
        public void FailSwap_WhenElementNotOnField()
        {
            new Action(() => field.Value.Swap(new CellLocation(0, 0), new CellLocation(3, 0)))
                .ShouldThrow<Exception>();
        }

        [Test]
        public void SwapElementsOnClonedField()
        {
            var size = new Size(3, 3);
            var original = FieldFromArray(size,
                1, 2, 3,
                5, 9, 1,
                1, 1, 1);
            var cloned = original.Clone();

            cloned.Swap(new CellLocation(1, 1), new CellLocation(2, 1));

            cloned.Should().BeEquivalentTo(FieldFromArray(size,
                1, 2, 3,
                5, 1, 1,
                1, 9, 1));
            cloned.Should().NotBeEquivalentTo(original);
        }

        #endregion

        #region Clone tests

        [Test]
        public void CloneCorrectly()
        {
            var original = field.Value;

            var cloned = original.Clone();

            cloned.Should().NotBeSameAs(original);
            cloned.Should().BeEquivalentTo(original);
        }

        #endregion

        #region Enumerate tests

        [Test]
        public void EnumerateLocationsCorrecly()
        {
            var field = this.field.Value;
            var expected = new List<CellLocation>();
            for (var row = 0; row < field.Height; row++)
                for (var column = 0; column < field.Width; column++)
                    expected.Add(new CellLocation(row, column));
            field.EnumerateLocations().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void EnumerateCorrectly()
        {
            var field = this.field.Value;
            var expected = new List<CellInfo<int>>();
            for (var row = 0; row < field.Height; row++)
                for (var column = 0; column < field.Width; column++)
                {
                    var location = new CellLocation(row, column);
                    expected.Add(new CellInfo<int>(location, field[location]));
                }
            field.Should().BeEquivalentTo(expected);
        }

        #endregion

        #region GetLocations tests

        [Test]
        public void ReturnLocations_ForNonNulls()
        {
            var field = FieldFromArray(new Size(3, 3),
                "aa", "asda", null,
                "rr", null, "asda",
                "asda", "fdfg", "lel");

            field.GetLocations("asda").Should()
                .BeEquivalentTo(new CellLocation(0, 1), new CellLocation(1, 2), new CellLocation(2, 0));
        }

        [Test]
        public void ReturnLocations_ForNulls()
        {
            var field = FieldFromArray(new Size(3, 3),
                "aa", "asda", null,
                "rr", null, "asda",
                "asda", "fdfg", "lel");

            field.GetLocations(null).Should()
                .BeEquivalentTo(new CellLocation(0, 2), new CellLocation(1, 1));
        }

        [Test]
        public void ReturnLocations_WhenNotFound()
        {
            var field = FieldFromArray(new Size(3, 3),
                "aa", "asda", null,
                "rr", null, "asda",
                "asda", "fdfg", "lel");

            field.GetLocations("some other string").Should().BeEmpty();
        }

        #endregion

        #region Indexer tests

        [Test]
        public void ReturnCorrectValuesByIndex()
        {
            var field = this.field.Value;
            foreach (var location in field.EnumerateLocations())
            {
                var real = field[location];
                var expected = DefaultFieldData[location.Row*DefaultFieldSize.Width + location.Column];
                real.Should().Be(expected);
            }
        }

        #endregion
    }
}
