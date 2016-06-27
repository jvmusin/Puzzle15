using System;
using System.Drawing;
using FluentAssertions;
using Ninject;
using NUnit.Framework;
using Puzzle15.Implementations.ClassicGame;
using Puzzle15.Interfaces.Factories;
using Puzzle15.Tests.Modules;
using Puzzle15.Utils;

namespace Puzzle15.Tests
{
    [TestFixture]
    public class ClassicGameShiftPerformer_Should
    {
        private IGameFieldFactory<int> gameFieldFactory;
        private ClassicShiftPerformer shiftPerformer;

        [SetUp]
        public void SetUp()
        {
            var kernel = new StandardKernel(new GameBaseModule(), new ClassicGameModule());

            gameFieldFactory = kernel.Get<IGameFieldFactory<int>>();
            shiftPerformer = new ClassicShiftPerformer();
        }

        #region Shift tests

        [Test]
        public void ShiftCorrectly_WhenValueOnFieldAndConnectedByEdge()
        {
            var size = new Size(3, 3);
            var field = gameFieldFactory.Create(size,
                1, 2, 3,
                6, 0, 4,
                7, 5, 8);
            var cloned = field.Clone();

            var result = shiftPerformer.PerformShift(field, 5);

            field.Should().BeEquivalentTo(cloned);

            var expectedField = gameFieldFactory.Create(size,
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
            var field = gameFieldFactory.Create(size,
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
