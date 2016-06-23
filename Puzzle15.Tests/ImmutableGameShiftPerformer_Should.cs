using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using Puzzle15.Implementations;

namespace Puzzle15.Tests
{
    [TestFixture]
    public class ImmutableGameShiftPerformer_Should : TestBase
    {
        private ShiftPerformer shiftPerformer;

        [SetUp]
        public void SetUp()
        {
            shiftPerformer = new ShiftPerformer();
        }

        [Test]
        public void Shift_WhenValueCorrect()
        {
            var field = FieldFromArray(new Size(3, 3),
                1, 2, 3,
                6, 8, 7,
                5, 0, 4);
            var result = shiftPerformer.Perform(field, 4);

            var expectedField = FieldFromArray(new Size(3, 3),
                1, 2, 3,
                6, 8, 7,
                5, 4, 0);

            for (var i = 0; i < 9; i++)
            {
                var realLocation = result.GetLocation(i);
                var expectedLocation = expectedField.GetLocation(i);
                realLocation.Should().Be(expectedLocation);

                var realValue = result[realLocation];
                var expectedValue = expectedField[expectedLocation];
                realValue.Should().Be(expectedValue);
            }
        }

        [Test]
        public void Fail_WhenValueIncorrect([Values(7, 10, 0)] int value)
        {
            var field = FieldFromArray(new Size(3, 3),
                1, 2, 3,
                6, 8, 7,
                5, 0, 4);
            new Action(() => shiftPerformer.Perform(field, value)).ShouldThrow<Exception>();
        }

        [Test]
        public void NotChangeSource()
        {
            var field = FieldFromArray(new Size(3, 3),
                1, 2, 3,
                6, 8, 7,
                5, 0, 4);
            var cloned = field.Clone();
            shiftPerformer.Perform(field, 4);

            foreach (var location in field.EnumerateLocations())
                field[location].Should().Be(cloned[location]);
        }
    }
}
