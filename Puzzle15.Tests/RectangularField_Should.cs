using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzle15.Base;
using Puzzle15.Base.Field;

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

        private static IEnumerable<Func<Size, IRectangularField<string>>> CtorsWithString
        {
            get
            {
                yield return size => new RectangularField<string>(size);
                yield return size => new WrappedRectangularField<string>(new RectangularField<string>(size));
            }
        }

        private static IEnumerable<IRectangularField<int>> Fields
        {
            get { return Ctors.Select(ctor => FieldFromConstructor(ctor, DefaultFieldSize, DefaultFieldData)); }
        }

        private static IEnumerable<IRectangularField<string>> FieldsWithString
        {
            get
            {
                var size = DefaultFieldSize;
                var elementsCount = size.Height * size.Width;
                var elements = new string[elementsCount];
                return CtorsWithString.Select(ctor => FieldFromConstructor(ctor, size, elements));
            }
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
                10, 9, 1,
                5, 3, 1,
                1, 1, 11);

            original = original.Swap(new CellLocation(0, 0), new CellLocation(0, 2));
            original = original.Swap(new CellLocation(1, 1), new CellLocation(0, 0));
            original = original.Swap(new CellLocation(2, 0), new CellLocation(2, 2));
            original = original.Swap(new CellLocation(1, 2), new CellLocation(0, 2));
            original = original.SetValue(10, new CellLocation(0, 1));
            original = original.Swap(new CellLocation(0, 1), new CellLocation(0, 0));
            original = original.SetValue(11, new CellLocation(2, 2));

            original.ToList().Should().BeEquivalentTo(expected);
        }

        #endregion

        #region GetLocations tests

        [Test]
        public void ReturnLocations_ForNonNulls(
            [ValueSource(nameof(CtorsWithString))] Func<Size, IRectangularField<string>> ctor)
        {
            var field = FieldFromConstructor(ctor, new Size(3, 3),
                "aa", "asda", null,
                "rr", null, "asda",
                "asda", "fdfg", "lel");

            field.GetLocations("asda").Should()
                .BeEquivalentTo(new CellLocation(0, 1), new CellLocation(1, 2), new CellLocation(2, 0));
        }

        [Test]
        public void ReturnLocations_ForNulls(
            [ValueSource(nameof(CtorsWithString))] Func<Size, IRectangularField<string>> ctor)
        {
            var field = FieldFromConstructor(ctor, new Size(3, 3),
                "aa", "asda", null,
                "rr", null, "asda",
                "asda", "fdfg", "lel");

            field.GetLocations(null).Should()
                .BeEquivalentTo(new CellLocation(0, 2), new CellLocation(1, 1));
        }

        [Test]
        public void ReturnLocations_WhenNotFound(
            [ValueSource(nameof(CtorsWithString))] Func<Size, IRectangularField<string>> ctor)
        {
            var field = FieldFromConstructor(ctor, new Size(3, 3),
                "aa", "asda", null,
                "rr", null, "asda",
                "asda", "fdfg", "lel");

            field.GetLocations("some other string").Should().BeEmpty();
        }

        [Test]
        public void ReturnLocations_AfterChangesByIndex(
            [ValueSource(nameof(Ctors))] Func<Size, IRectangularField<int>> ctor)
        {
            var size = new Size(3, 3);
            var original = FieldFromConstructor(ctor, size,
                1, 2, 3,
                5, 9, 1,
                1, 1, 1);

            var expectedLocations = new Dictionary<int, CellLocation[]>
            {
                {0, new[] {new CellLocation(2, 1)}},
                {
                    1,
                    new[]
                    {new CellLocation(0, 0), new CellLocation(0, 2), new CellLocation(2, 0), new CellLocation(2, 2)}
                },
                {2, new[] {new CellLocation(0, 1)}},
                {5, new[] {new CellLocation(1, 0)}},
                {9, new[] {new CellLocation(1, 1)}},
                {200, new[] {new CellLocation(1, 2)}}
            };

            original = original.SetValue(1, new CellLocation(0, 2));
            original = original.SetValue(0, new CellLocation(2, 1));
            original = original.SetValue(200, new CellLocation(1, 2));
            original = original.SetValue(1, new CellLocation(2, 0));

            //  Should be
            //  1 2 1
            //  5 9 200
            //  1 0 1

            var realLocations = new Dictionary<int, CellLocation[]>();
            foreach (var value in original.Select(x => x.Value))
                realLocations[value] = original.GetLocations(value).ToArray();

            realLocations.Count.Should().Be(expectedLocations.Count);
            foreach (var numberAndLocations in realLocations)
                // ReSharper disable once CoVariantArrayConversion
                numberAndLocations.Value.Should().BeEquivalentTo(expectedLocations[numberAndLocations.Key]);
        }

        [Test]
        public void ReturnLocations_AfterSwaps(
            [ValueSource(nameof(Ctors))] Func<Size, IRectangularField<int>> ctor)
        {
            var size = new Size(3, 3);
            var original = FieldFromConstructor(ctor, size,
                1, 2, 3,
                5, 9, 1,
                1, 1, 1);

            var expectedLocations = new Dictionary<int, CellLocation[]>
            {
                {
                    1,
                    new[]
                    {
                        new CellLocation(0, 2),
                        new CellLocation(1, 2),
                        new CellLocation(2, 0), new CellLocation(2, 1), new CellLocation(2, 2)
                    }
                },
                {2, new[] {new CellLocation(0, 1)}},
                {3, new[] {new CellLocation(1, 1)}},
                {5, new[] {new CellLocation(1, 0)}},
                {9, new[] {new CellLocation(0, 0)}}
            };

            original = original.Swap(new CellLocation(0, 0), new CellLocation(1, 1));
            original = original.Swap(new CellLocation(2, 1), new CellLocation(2, 0));
            original = original.Swap(new CellLocation(1, 1), new CellLocation(0, 2));
            original = original.Swap(new CellLocation(1, 1), new CellLocation(1, 1));

            //  Should be
            //  9 2 1
            //  5 3 1
            //  1 1 1

            var realLocations = new Dictionary<int, CellLocation[]>();
            foreach (var value in original.Select(x => x.Value))
                realLocations[value] = original.GetLocations(value).ToArray();

            realLocations.Count.Should().Be(expectedLocations.Count);
            foreach (var numberAndLocations in realLocations)
                // ReSharper disable once CoVariantArrayConversion
                numberAndLocations.Value.Should().BeEquivalentTo(expectedLocations[numberAndLocations.Key]);
        }

        [Test]
        public void ReturnLocations_WithoutChangingClonedField(
            [ValueSource(nameof(Ctors))] Func<Size, IRectangularField<int>> ctor)
        {
            var size = new Size(3, 3);
            var original = FieldFromConstructor(ctor, size,
                1, 5, 3,
                0, 2, 8,
                7, 4, 6);
            var cloned = original.Clone();
            original.Should().BeEquivalentTo(cloned);

            original = original.Swap(new CellLocation(0, 0), new CellLocation(0, 2));
            original = original.Swap(new CellLocation(1, 1), new CellLocation(0, 0));
            original = original.Swap(new CellLocation(2, 0), new CellLocation(2, 2));
            original = original.Swap(new CellLocation(1, 2), new CellLocation(0, 2));
            original = original.Swap(new CellLocation(0, 1), new CellLocation(0, 0));

            var expectedOrder = new[] { 5, 2, 8, 0, 3, 1, 6, 4, 7 };
            original.EnumerateLocations()
                .OrderBy(x => x)
                .Zip(expectedOrder, (location, number) => new
                {
                    RealLocation = original.GetLocation(number),
                    ExpectedLocation = location
                })
                .Select(x => x.RealLocation.Equals(x.ExpectedLocation))
                .ShouldAllBeEquivalentTo(true);

            original.Should().NotBeEquivalentTo(cloned);
        }

        #endregion

        #region Indexer tests

        [Test]
        public void ReturnCorrectValues(
            [ValueSource(nameof(Ctors))] Func<Size, IRectangularField<int>> ctor)
        {
            var size = new Size(3, 3);
            var values = new[]
            {
                1, 2, 3,
                5, 9, 1,
                1, 1, 1
            };
            var original = FieldFromConstructor(ctor, size, values.ToArray());

            foreach (var location in original.EnumerateLocations())
                original[location].Should().Be(values[location.Row * size.Width + location.Column]);
        }

        [Test]
        public void ReturnCorrectValuesByIndex_AfterChanges(
            [ValueSource(nameof(Ctors))] Func<Size, IRectangularField<int>> ctor)
        {
            var size = new Size(3, 3);
            var original = FieldFromConstructor(ctor, size,
                1, 2, 3,
                5, 9, 1,
                1, 1, 1);
            original = original.Swap(new CellLocation(0, 0), new CellLocation(0, 2));
            original = original.Swap(new CellLocation(1, 1), new CellLocation(0, 0));
            original = original.Swap(new CellLocation(2, 0), new CellLocation(2, 2));
            original = original.Swap(new CellLocation(1, 2), new CellLocation(0, 2));
            original = original.Swap(new CellLocation(0, 1), new CellLocation(0, 0));

            var expected = new[]
            {
                2, 9, 1,
                5, 3, 1,
                1, 1, 1
            };

            foreach (var location in original.EnumerateLocations())
                original[location].Should().Be(expected[location.Row * size.Width + location.Column]);
        }

        [Test]
        public void ReturnCorrectValuesByIndex_OnClonedField(
            [ValueSource(nameof(Ctors))] Func<Size, IRectangularField<int>> ctor)
        {
            var size = new Size(3, 3);
            var original = FieldFromConstructor(ctor, size,
                1, 2, 3,
                5, 9, 1,
                1, 1, 1).Clone();
            original = original.Swap(new CellLocation(0, 0), new CellLocation(0, 2));
            original = original.Swap(new CellLocation(1, 1), new CellLocation(0, 0));
            original = original.Swap(new CellLocation(2, 0), new CellLocation(2, 2));
            original = original.Swap(new CellLocation(1, 2), new CellLocation(0, 2));
            original = original.Swap(new CellLocation(0, 1), new CellLocation(0, 0));

            var expected = new[]
            {
                2, 9, 1,
                5, 3, 1,
                1, 1, 1
            };

            foreach (var location in original.EnumerateLocations())
                original[location].Should().Be(expected[location.Row * size.Width + location.Column]);
        }

        #endregion
    }
}
