using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using Puzzle15.Implementations;
using Puzzle15.Interfaces;

namespace Puzzle15.Tests
{
    [TestFixture]
    public class MutableGameShiftPerformer_Should : TestBase
    {
        private static readonly IShiftPerformerFactory ShiftPerformerFactory = new ShiftPerformerFactory();
        private IShiftPerformer shiftPerformer;

        [SetUp]
        public void SetUp()
        {
            shiftPerformer = ShiftPerformerFactory.Create();
        }
        
        [Test]
        public void Shift_WhenValueCorrect()
        {
            var field = FieldFromArray(new Size(3, 3),
                1, 2, 3,
                6, 8, 7,
                5, 0, 4);
            shiftPerformer.Perform(field, 4);

            field.Should().BeEquivalentTo(FieldFromArray(new Size(3, 3),
                1, 2, 3,
                6, 8, 7,
                5, 4, 0));
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
        public void ReturnSameFieldAfterPerforming()
        {
            var field = FieldFromArray(new Size(3, 3),
                1, 2, 3,
                6, 8, 7,
                5, 0, 4);

            var result = shiftPerformer.Perform(field, 8);
            result.Should().BeSameAs(field);
        }
    }
}
