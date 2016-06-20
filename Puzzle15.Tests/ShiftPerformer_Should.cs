using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using Puzzle15.Implementations;
using Puzzle15.Interfaces;

namespace Puzzle15.Tests
{
    [TestFixture]
    public class ShiftPerformer_Should : TestBase
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
            shiftPerformer.Perform(null, field, 4);

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
            new Action(() => shiftPerformer.Perform(null, field, value)).ShouldThrow<Exception>();
        }

        [Test]
        public void ReturnSameGameAfterPerforming()
        {
            var field = FieldFromArray(new Size(3, 3),
                1, 2, 3,
                6, 8, 7,
                5, 0, 4);
            var game = StrictFake<IGame>();

            shiftPerformer.Perform(game, field, 8).Should().BeSameAs(game);
        }
    }
}
