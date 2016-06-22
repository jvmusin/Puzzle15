using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzle15.Base;
using Puzzle15.Implementations;

namespace Puzzle15.Tests
{
    [TestFixture]
    public class RectangularField_Should : TestBase
    {
        private static IEnumerable<Func<Size, IRectangularField<int>>> Ctors
        {
            get
            {
                yield return size => new RectangularField<int>(size);
                yield return size => new WrappedRectangularField<int>(new RectangularField<int>(size));
            }
        }

        private static IEnumerable<IRectangularField<int>> Fields
        {
            get { return Ctors.Select(ctor => FieldFromConstructor(ctor, DefaultFieldSize, DefaultFieldData)); }
        }

        #region Size tests

        [Test]
        public void WorkWithDifferentSizesCorrectly(
            [ValueSource(nameof(Ctors))] Func<Size, IRectangularField<int>> ctor,
            [Values(-1232, 0, 133)] int width,
            [Values(-13123, 0, 2)] int height)
        {
            var size = new Size(width, height);

            if (size.Height < 0 || size.Width < 0)
            {
                new Action(() => ctor(size)).ShouldThrow<Exception>();
            }
            else
            {
                var field = ctor(size);

                field.Size.Should().Be(size);
                field.Height.Should().Be(size.Height);
                field.Width.Should().Be(size.Width);
            }
        }

        #endregion

        #region Swap tests

        [Test]
        public void SwapElements_WhenNotConnectedByEdge(
            [ValueSource(nameof(Ctors))] Func<Size, IRectangularField<int>> ctor)
        {
            var size = new Size(3, 3);

            var original = FieldFromConstructor(ctor, size,
                1, 2, 3,
                4, 5, 17,
                9, 0, 0);
            var expected = FieldFromConstructor(ctor, size,
                17, 2, 3,
                4, 5, 1,
                9, 0, 0);

            original = original.Swap(new CellLocation(0, 0), new CellLocation(1, 2));

            original.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void SwapElementAtSamePlace(
            [ValueSource(nameof(Ctors))] Func<Size, IRectangularField<int>> ctor)
        {
            var size = new Size(3, 3);

            var original = FieldFromConstructor(ctor, size,
                1, 2, 3,
                4, 5, 17,
                9, 0, 0);
            var expected = original.Clone();

            original = original.Swap(new CellLocation(1, 2), new CellLocation(1, 2));

            original.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void FailSwap_WhenElementNotOnField(
            [ValueSource(nameof(Fields))] IRectangularField<int> field)
        {
            new Action(() => field.Swap(new CellLocation(0, 0), new CellLocation(3, 0)))
                .ShouldThrow<Exception>();
        }

        [Test]
        public void SwapElementsOnClonedField(
            [ValueSource(nameof(Ctors))] Func<Size, IRectangularField<int>> ctor)
        {
            var size = new Size(3, 3);
            var original = FieldFromConstructor(ctor, size,
                1, 2, 3,
                5, 9, 1,
                1, 1, 1);
            var cloned = original.Clone();
            var expected = FieldFromConstructor(ctor, size,
                1, 2, 3,
                5, 1, 1,
                1, 9, 1);

            cloned = cloned.Swap(new CellLocation(1, 1), new CellLocation(2, 1));

            cloned.Should().BeEquivalentTo(expected);
            cloned.Should().NotBeEquivalentTo(original);
        }

        #endregion

        #region Clone tests

        [Test]
        public void CloneCorrectly(
            [ValueSource(nameof(Fields))] IRectangularField<int> field)
        {
            var cloned = field.Clone();

            cloned.Should().NotBeSameAs(field);
            cloned.Should().BeEquivalentTo(field);
        }

        [Test]
        public void NotChangeOriginalField_AfterChangingClonedField(
            [ValueSource(nameof(Fields))] IRectangularField<int> field)
        {
            var original = field.ToArray();
            var cloned = field.Clone();

            cloned = cloned.Swap(new CellLocation(0, 0), new CellLocation(0, 2));
            cloned = cloned.Swap(new CellLocation(1, 1), new CellLocation(0, 0));
            cloned = cloned.Swap(new CellLocation(2, 0), new CellLocation(2, 2));
            cloned = cloned.Swap(new CellLocation(1, 2), new CellLocation(0, 2));
            cloned = cloned.Swap(new CellLocation(0, 1), new CellLocation(0, 0));

            original.SequenceEqual(cloned).Should().BeFalse();
        }

        #endregion

        #region Enumerate tests

        [Test]
        public void EnumerateLocationsCorrecly(
            [ValueSource(nameof(Fields))] IRectangularField<int> field)
        {
            var expected = new List<CellLocation>();
            for (var row = 0; row < field.Height; row++)
                for (var column = 0; column < field.Width; column++)
                    expected.Add(new CellLocation(row, column));
            field.EnumerateLocations().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void EnumerateCorrectly(
            [ValueSource(nameof(Fields))] IRectangularField<int> field)
        {
            var expected = new List<CellInfo<int>>();
            for (var row = 0; row < field.Height; row++)
                for (var column = 0; column < field.Width; column++)
                {
                    var location = new CellLocation(row, column);
                    expected.Add(new CellInfo<int>(location, field[location]));
                }
            field.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void EnumerateCorrectly_AfterChanges(
            [ValueSource(nameof(Ctors))] Func<Size, IRectangularField<int>> ctor)
        {
            var size = new Size(3, 3);
            var original = FieldFromConstructor(ctor, size,
                1, 2, 3,
                5, 9, 1,
                1, 1, 1);
            var expected = FieldFromConstructor(ctor, size,
                2, 9, 1,
                5, 3, 1,
                1, 1, 1);
            
            original = original.Swap(new CellLocation(0, 0), new CellLocation(0, 2));
            original = original.Swap(new CellLocation(1, 1), new CellLocation(0, 0));
            original = original.Swap(new CellLocation(2, 0), new CellLocation(2, 2));
            original = original.Swap(new CellLocation(1, 2), new CellLocation(0, 2));
            original = original.Swap(new CellLocation(0, 1), new CellLocation(0, 0));

            original.ToList().Should().BeEquivalentTo(expected.ToList());
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

        [Test]
        public void ReturnLocations_AfterChanges(
            [ValueSource(nameof(Ctors))] Func<Size, IRectangularField<int>> ctor)
        {
            var size = new Size(3, 3);
            var original = FieldFromConstructor(ctor, size,
                1, 2, 3,
                5, 9, 1,
                1, 1, 1);
            var expected = FieldFromConstructor(ctor, size,
                2, 9, 1,
                5, 3, 1,
                1, 1, 1);

            var changes = new List<IRectangularField<int>> {original};
            changes.Add(original = original.Swap(new CellLocation(0, 0), new CellLocation(0, 2)));
            changes.Add(original = original.Swap(new CellLocation(1, 1), new CellLocation(0, 0)));
            changes.Add(original = original.Swap(new CellLocation(2, 0), new CellLocation(2, 2)));
            changes.Add(original = original.Swap(new CellLocation(1, 2), new CellLocation(0, 2)));
            changes.Add(original = original.Swap(new CellLocation(0, 1), new CellLocation(0, 0)));

            original.Should().BeEquivalentTo(expected);
            if (original is WrappedRectangularField<int>)
                changes.Distinct(new SameObjectsComparer<IRectangularField<int>>()).Count().Should().Be(changes.Count);
        }

        #endregion

        #region Indexer tests

        [Test]
        public void ReturnCorrectValuesByIndex(
            [ValueSource(nameof(Fields))] IRectangularField<int> field)
        {
            foreach (var location in field.EnumerateLocations())
            {
                var value = field[location];
                var expected = DefaultFieldData[location.Row*DefaultFieldSize.Width + location.Column];
                value.Should().Be(expected);
            }
        }

        #endregion
    }
}
