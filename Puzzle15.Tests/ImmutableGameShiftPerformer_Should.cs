using System;
using System.Drawing;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using Puzzle15.Base;
using Puzzle15.Implementations;
using Puzzle15.Implementations.GameFieldValidating;
using Puzzle15.Implementations.ShiftPerforming;
using Puzzle15.Interfaces;

namespace Puzzle15.Tests
{
    [TestFixture]
    public class ImmutableGameShiftPerformer_Should : TestBase
    {
        private IGameFieldValidator gameFieldValidator;
        private IShiftPerformerFactory shiftPerformerFactory;
        private IGameFactory gameFactory;
        private IShiftPerformer shiftPerformer;

        [SetUp]
        public void SetUp()
        {
            gameFieldValidator = StrictFake<IGameFieldValidator>();
            A.CallTo(() => gameFieldValidator.Validate(A<RectangularField<int>>._)).Returns(ValidationResult.Success());

            shiftPerformerFactory = new ImmutableGameShiftPerformerFactory();
            shiftPerformer = shiftPerformerFactory.Create();

            gameFactory = new GameFactory(gameFieldValidator, shiftPerformerFactory);
        }

        [Test]
        public void Shift_WhenValueCorrect()
        {
            var field = FieldFromArray(new Size(3, 3),
                1, 2, 3,
                6, 8, 7,
                5, 0, 4);
            var game = gameFactory.Create(field);
            var result = shiftPerformer.Perform(game, field, 4);

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
            new Action(() => shiftPerformer.Perform(null, field, value)).ShouldThrow<Exception>();
        }

        [Test]
        public void NotChangeSource()
        {
            var field = FieldFromArray(new Size(3, 3),
                1, 2, 3,
                6, 8, 7,
                5, 0, 4);
            var game = gameFactory.Create(field);
            var result = shiftPerformer.Perform(game, field, 4);
            result.Should().NotBeSameAs(game);

            foreach (var location in field.EnumerateLocations())
                game[location].Should().Be(field[location]);
        }
    }
}
