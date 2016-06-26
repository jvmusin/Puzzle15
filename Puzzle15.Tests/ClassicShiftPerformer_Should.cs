using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using Puzzle15.Implementations;
using Puzzle15.Implementations.ClassicGame;

namespace Puzzle15.Tests
{
    [TestFixture]
    public class ClassicShiftPerformer_Should : TestBase
    {
        private ClassicShiftPerformerFactory shiftPerformerFactory;
        private ClassicShiftPerformer shiftPerformer;

        [SetUp]
        public void SetUp()
        {
            shiftPerformerFactory = new ClassicShiftPerformerFactory();
            shiftPerformer = (ClassicShiftPerformer) shiftPerformerFactory.Create();
        }

        #region Shift tests

        [Test]
        public void ShiftCorrectly_WhenValueOnFieldAndConnectedByEdge()
        {
            var size = new Size(3, 3);
            var field = CreateMutableField(size,
                1, 2, 3,
                6, 0, 4,
                7, 5, 8);
            var cloned = field.Clone();

            var result = shiftPerformer.PerformShift(field.Clone(), 5);

            field.Should().BeEquivalentTo(cloned);

            var expectedField = CreateMutableField(size,
                1, 2, 3,
                6, 5, 4,
                7, 0, 8);
            result.Should().BeEquivalentTo(expectedField);
        }

        [Test]
        public void FailShift_WhenValueNotOnFieldOrNotConnectdByEdge(
            [Values(-1, 10, 100, 8, 1, 2, 0)] int value)
        {
            var size = new Size(3, 3);
            var field = CreateMutableField(size,
                1, 2, 3,
                7, 5, 8,
                6, 0, 4);
            var cloned = field.Clone();

            new Action(() => shiftPerformer.PerformShift(field, value)).ShouldThrow<Exception>();
            field.Should().BeEquivalentTo(cloned);
        }

        #endregion
    }
}
