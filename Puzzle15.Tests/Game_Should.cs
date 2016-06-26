using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using Puzzle15.Implementations;
using Puzzle15.Interfaces;
using RectangularField.Core;

namespace Puzzle15.Tests
{
    [TestFixture]
    public class Game_Should : TestBase
    {
        private IGameFieldValidator<int> gameFieldValidator;
        private IShiftPerformerFactory<int> shiftPerformerFactory;
        private IGameFactory<int> gameFactory;

        [SetUp]
        public void SetUp()
        {
            gameFieldValidator = StrictFake<IGameFieldValidator<int>>();
            A.CallTo(() => gameFieldValidator.Validate(null, null))
                .WhenArgumentsMatch(x => true)
                .Returns(ValidationResult.Success());

            shiftPerformerFactory = new ClassicShiftPerformerFactory();

            gameFactory = new GameFactory<int>(gameFieldValidator, shiftPerformerFactory);
        }

        private IGame<int> CreateGame(IRectangularField<int> field) => gameFactory.Create(field, field);

        #region Consistency tests

        [Test]
        public void NotChangeInitialField_AfterActions()
        {
            var field = CreateMutableField(new Size(3, 3),
                1, 2, 3,
                6, 0, 4,
                7, 5, 8);
            var clonedField = field.Clone();
            var game = CreateGame(field);

            field.Should().BeEquivalentTo(clonedField);

            game.Shift(5);

            game.Should().NotBeEquivalentTo(field);
            field.Should().BeEquivalentTo(clonedField);
        }

        [Test]
        public void HasZeroTurns_AfterCreating()
        {
            var field = CreateMutableField(new Size(3, 3),
                   1, 2, 3,
                   6, 0, 4,
                   7, 5, 8);
            var game = CreateGame(field);

            game.Turns.Should().Be(0);
        }

        [Test]
        public void IncrementTurns_OnShift()
        {
            var field = CreateMutableField(new Size(3, 3),
                   1, 2, 3,
                   6, 0, 4,
                   7, 5, 8);
            var game = CreateGame(field);
            var shifts = new[] { 2, 3, 4, 8, 8 };

            for (var i = 0; i < shifts.Length; i++)
            {
                game = game.Shift(shifts[i]);
                game.Turns.Should().Be(i + 1);
            }
        }

        [Test]
        public void RememberTarget()
        {
            var initialField = CreateImmutableField(new Size(3, 3),
                1, 2, 3,
                6, 0, 4,
                7, 5, 8);
            var target = CreateMutableField(new Size(3, 3),
                1, 0, 5,
                6, 7, 2,
                3, 4, 8);
            var game = gameFactory.Create(initialField, target);
            var shifts = new[] {2, 3, 4, 8, 8};

            game.Target.Should().Be(target);
            foreach (var valueToShift in shifts)
            {
                game = game.Shift(valueToShift);
                game.Target.Should().Be(target);
            }
            for (var i = shifts.Length; i > 0; i--)
            {
                game = game.PreviousState;
                game.Target.Should().Be(target);
            }
        }

        [Test]
        public void FailOnGettingTarget_IfTargetIsNotReadOnlyRectangularField()
        {
            var initialField = CreateImmutableField(new Size(3, 3),
                   1, 2, 3,
                   6, 0, 4,
                   7, 5, 8);
            var target = StrictFake<IRectangularField<int>>();
            A.CallTo(() => target.Clone()).Returns(target);
            var game = gameFactory.Create(initialField, target);

            new Action(() =>
            {
                // ReSharper disable once UnusedVariable
                var x = game.Target;
            }).ShouldThrow<Exception>();
        }

        #endregion

        #region Using new field in games created by Shift tests

        [Test]
        public void CreateNewGameWithShiftedElement()
        {
            var size = new Size(3, 3);
            var field = CreateMutableField(size,
                   1, 2, 3,
                   6, 0, 4,
                   7, 5, 8);
            var game = CreateGame(field);

            var toShiftElement = 5;
            var expectedField = CreateMutableField(size,
                1, 2, 3,
                6, 5, 4,
                7, 0, 8);
            game.Shift(toShiftElement).Should().BeEquivalentTo(expectedField);
        }

        [Test]
        public void CreateNewGamesWithShiftedElements()
        {
            var size = new Size(3, 3);
            var initialField = CreateImmutableField(size,
                   1, 2, 3,
                   6, 0, 4,
                   7, 5, 8);
            var allFields = new[]
            {
                initialField,
                CreateImmutableField(size,
                    1, 2, 3,
                    6, 5, 4,
                    7, 0, 8),
                CreateImmutableField(size,
                    1, 2, 3,
                    6, 5, 4,
                    7, 8, 0),
                CreateImmutableField(size,
                    1, 2, 3,
                    6, 5, 0,
                    7, 8, 4),
                CreateImmutableField(size,
                    1, 2, 0,
                    6, 5, 3,
                    7, 8, 4)
            };
            var shifts = new[] { 5, 8, 4, 3 };

            var game = CreateGame(initialField);

            var results = new List<IGame<int>> { game };
            results.AddRange(shifts.Select(value => game = game.Shift(value)));

            for (var i = 0; i < allFields.Length; i++)
                results[i].Should().BeEquivalentTo(allFields[i]);
        }

        [Test]
        public void MutateFirstCreatedGame_IfFieldNotImmutable()
        {
            var size = new Size(3, 3);
            var initialField = CreateMutableField(size,
                   1, 2, 3,
                   6, 0, 4,
                   7, 5, 8);
            var game = CreateGame(initialField);
            var shifts = new[] { 5, 8, 4, 3 };

            var allGames = new List<IGame<int>> { game };
            allGames.AddRange(shifts.Select(value => game = game.Shift(value)));

            for (var i = 0; i < allGames.Count; i++)
                for (var j = i + 1; j < allGames.Count; j++)
                    allGames[i].Should().BeEquivalentTo(allGames[j]);
        }

        #endregion

        #region Returning previous game tests

        [Test]
        public void ReturnPreviousStatesCorrectly()
        {
            var size = new Size(3, 3);
            var initialField = CreateImmutableField(size,
                   1, 2, 3,
                   6, 0, 4,
                   7, 5, 8);
            var allFields = new[]
            {
                initialField,
                CreateImmutableField(size,
                    1, 2, 3,
                    6, 5, 4,
                    7, 0, 8),
                CreateImmutableField(size,
                    1, 2, 3,
                    6, 5, 4,
                    7, 8, 0),
                CreateImmutableField(size,
                    1, 2, 3,
                    6, 5, 0,
                    7, 8, 4),
                CreateImmutableField(size,
                    1, 2, 0,
                    6, 5, 3,
                    7, 8, 4)
            };
            var shifts = new[] { 5, 8, 4, 3 };
            
            var game = shifts.Aggregate(CreateGame(initialField), 
                (currentGame, valueToShift) => currentGame.Shift(valueToShift));

            for (var i = allFields.Length - 2; i >= 0; i--)
            {
                game = game.PreviousState;
                game.Should().BeEquivalentTo(allFields[i]);
                game.Turns.Should().Be(i);
            }

            game.PreviousState.Should().BeNull();
        }

        #endregion

        #region Creating new game on shift tests

        [Test]
        public void ReturnNewGameOnShift()
        {
            var size = new Size(3, 3);
            var field = CreateMutableField(size,
                1, 2, 3,
                6, 0, 4,
                7, 5, 8);
            var game = CreateGame(field);

            game.Shift(5).Should().NotBeSameAs(game);
        }

        #endregion
    }
}
