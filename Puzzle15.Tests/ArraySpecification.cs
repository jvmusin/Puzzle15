using FluentAssertions;
using NUnit.Framework;
using Puzzle15.Base;

namespace Puzzle15.Tests
{
    [TestFixture]
    public class ArraySpecification
    {
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
            
            var hash1 = Helpers.StructuralGetHashCode(array1);
            var hash2 = Helpers.StructuralGetHashCode(array2);

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

            Helpers.StructuralEquals(array1, array2).Should().BeTrue();
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

            Helpers.StructuralEquals(array1, array2).Should().BeFalse();
            object.Equals(array1, array2).Should().BeFalse();

            Helpers.StructuralEquals(new object(), new object()).Should().BeFalse();
            Helpers.StructuralEquals((object) null, null).Should().BeTrue();
        }

        [Test]
        public void Something()
        {
//            var arr1 = new[,] {{1, 2, 3}, {4, 5, 6}};
//            var arr2 = new[,] {{1, 2, 3}, {4, 5, 6}};
//            Helpers.StructuralGetHashCode(arr1).Should().Be(Helpers.StructuralGetHashCode(arr2));
//            Helpers.StructuralEquals(arr1, arr2).Should().BeTrue();

            Helpers.StructuralEquals(null, "s").Should().BeFalse();
            Helpers.StructuralEquals("s", null).Should().BeFalse();
            Helpers.StructuralEquals("aaa", "BBB").Should().BeFalse();
            Helpers.StructuralEquals("aaa", "aaa").Should().BeTrue();
            Helpers.StructuralEquals(new RectangularField<int>(5, 5), new RectangularField<int>(5, 4)).Should().BeFalse();
            Helpers.StructuralEquals(new RectangularField<int>(5, 5), new RectangularField<int>(5, 5)).Should().BeTrue();

            new RectangularField<int>(5, 5).Equals(new RectangularField<int>(5, 4)).Should().BeFalse();
            new RectangularField<int>(5, 5).Equals(new RectangularField<int>(5, 5)).Should().BeTrue();
        }
    }
}
