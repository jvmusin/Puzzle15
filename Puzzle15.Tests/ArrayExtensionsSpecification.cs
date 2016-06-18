using FluentAssertions;
using NUnit.Framework;

namespace Puzzle15.Tests
{
    [TestFixture]
    public class ArrayExtensionsSpecification
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
    }
}
