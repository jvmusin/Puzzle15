using FluentAssertions;
using NUnit.Framework;

namespace Puzzle15.Tests
{
    [TestFixture]
    public class ArraySpecification
    {
        [Test]
        public void SwapsItemsInOneDimensionalArray()
        {
            var array    = new[] {5, 16, -100, 5};
            var expected = new[] {5, 5, -100, 16};

            var index1 = 1;
            var index2 = 3;
            array.Swap(index1, index2);

            for (var i = 0; i < array.Length; i++)
                array[i].Should().Be(expected[i]);
        }

        [Test]
        public void CalculatesHashForTwoDimensionalArray()
        {
            var array1 = new[]
            {
                new[] {1, 5, 7},
                new[] {2, 7, 93, 13132},
                new int[0]
            };
            var array2 = new[]
            {
                new[] {1, 5, 7},
                new[] {2, 7, 93, 13132},
                null    //TODO WTF??
            };
            
            var hash1 = Helpers.GetHashCode(array1);
            var hash2 = Helpers.GetHashCode(array2);

            hash1.Should().Be(hash2);
        }

        [Test]
        public void ComparesTwoDimensionalArrays_WhenEqual()
        {
            var array1 = new[]
            {
                new[] {1, 5, 7},
                new[] {2, 7, 93, 13132},
                new int[0]
            };
            var array2 = new[]
            {
                new[] {1, 5, 7},
                new[] {2, 7, 93, 13132},
                new int[0]
            };

            Helpers.Equals(array1, array2).Should().BeTrue();
        }

        [Test]
        public void ComparesTwoDimensionalArrays_WhenNotEqual()
        {
            var array1 = new[]
            {
                new[] {1, 5, 7},
                new[] {2, 0, 93, 13132},
                new int[0]
            };
            var array2 = new[]
            {
                new[] {1, 5, 7},
                new[] {2, 7, 93, 13132},
                new int[0]
            };

            Helpers.Equals(array1, array2).Should().BeFalse();
            object.Equals(array1, array2).Should().BeFalse();

            Helpers.Equals(new object(), new object()).Should().BeFalse();
            Helpers.Equals((object) null, null).Should().BeTrue();
        }

        [Test]
        public void Something()
        {
            Helpers.Equals(null, "s").Should().BeFalse();
            Helpers.Equals("s", null).Should().BeFalse();
            Helpers.Equals("aaa", "BBB").Should().BeFalse();
            Helpers.Equals("aaa", "aaa").Should().BeTrue();
            Helpers.Equals(new RectangularField<int>(5, 5), new RectangularField<int>(5, 4)).Should().BeFalse();
            Helpers.Equals(new RectangularField<int>(5, 5), new RectangularField<int>(5, 5)).Should().BeTrue();
        }
    }
}
