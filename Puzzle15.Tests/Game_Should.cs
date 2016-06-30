using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using Ninject;
using NUnit.Framework;
using Puzzle15.Interfaces;
using Puzzle15.Interfaces.Factories;
using Puzzle15.Tests.Modules;
using RectangularField.Interfaces;
using RectangularField.Utils;

namespace Puzzle15.Tests
{
    [TestFixture]
    public class Game_Should
    {
        private IFieldFactory<int> gameFieldFactory;
        private IGameFactory<int> gameFactory;

        [SetUp]
        public void SetUp()
        {
            var kernel = new StandardKernel(new GameBaseModule(), new ClassicGameModule());

            gameFieldFactory = kernel.Get<IFieldFactory<int>>();
            gameFactory = kernel.Get<IGameFactory<int>>();
        }

        private IGame<int> CreateGame(IField<int> field) => gameFactory.Create(field, field);

        #region Consistency tests

        [Test]
        public void NotChangeInitialField_AfterActions()
        {
            var field = gameFieldFactory.Create(new Size(3, 3),
                1, 2, 3,
                6, 0, 4,
                7, 5, 8);
            var game = CreateGame(field);

            game.Shift(5);

            game.Should().BeEquivalentTo(field);
        }

        [Test]
        public void HasZeroTurns_AfterCreating()
        {
            var field = gameFieldFactory.Create(new Size(3, 3),
                   1, 2, 3,
                   6, 0, 4,
                   7, 5, 8);
            var game = CreateGame(field);

            game.Turns.Should().Be(0);
        }

        [Test]
        public void IncrementTurns_OnShift()
        {
            var field = gameFieldFactory.Create(new Size(3, 3),
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
            var size = new Size(3, 3);
            var initialField = gameFieldFactory.Create(size,
                1, 2, 3,
                6, 0, 4,
                7, 5, 8);
            var target = gameFieldFactory.Create(size,
                1, 0, 5,
                6, 7, 2,
                3, 4, 8);
            var game = gameFactory.Create(initialField, target);
            var shifts = new[] {2, 3, 4, 8, 8};

            game.Target.Should().BeEquivalentTo(target);
            foreach (var valueToShift in shifts)
            {
                game = game.Shift(valueToShift);
                game.Target.Should().BeEquivalentTo(target);
            }
            for (var i = shifts.Length; i > 0; i--)
            {
                game = game.PreviousState;
                game.Target.Should().BeEquivalentTo(target);
            }
        }

        #endregion

        #region Using new field in games created by Shift tests

        [Test]
        public void CreateNewGameWithShiftedElement()
        {
            var size = new Size(3, 3);
            var field = gameFieldFactory.Create(size,
                   1, 2, 3,
                   6, 0, 4,
                   7, 5, 8);
            var game = CreateGame(field);

            var toShiftElement = 5;
            var expectedField = gameFieldFactory.Create(size,
                1, 2, 3,
                6, 5, 4,
                7, 0, 8);
            game.Shift(toShiftElement).Should().BeEquivalentTo(expectedField);
        }

        [Test]
        public void CreateNewGamesWithShiftedElements()
        {
            var size = new Size(3, 3);
            var initialField = gameFieldFactory.Create(size,
                   1, 2, 3,
                   6, 0, 4,
                   7, 5, 8);
            var allFields = new[]
            {
                initialField,
                gameFieldFactory.Create(size,
                    1, 2, 3,
                    6, 5, 4,
                    7, 0, 8),
                gameFieldFactory.Create(size,
                    1, 2, 3,
                    6, 5, 4,
                    7, 8, 0),
                gameFieldFactory.Create(size,
                    1, 2, 3,
                    6, 5, 0,
                    7, 8, 4),
                gameFieldFactory.Create(size,
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

        #endregion

        #region Returning previous game tests

        [Test]
        public void ReturnPreviousStatesCorrectly()
        {
            var size = new Size(3, 3);
            var initialField = gameFieldFactory.Create(size,
                   1, 2, 3,
                   6, 0, 4,
                   7, 5, 8);
            var allFields = new[]
            {
                initialField,
                gameFieldFactory.Create(size,
                    1, 2, 3,
                    6, 5, 4,
                    7, 0, 8),
                gameFieldFactory.Create(size,
                    1, 2, 3,
                    6, 5, 4,
                    7, 8, 0),
                gameFieldFactory.Create(size,
                    1, 2, 3,
                    6, 5, 0,
                    7, 8, 4),
                gameFieldFactory.Create(size,
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
            var field = gameFieldFactory.Create(size,
                1, 2, 3,
                6, 0, 4,
                7, 5, 8);
            var game = CreateGame(field);

            game.Shift(5).Should().NotBeSameAs(game);
        }

        #endregion
    }
}
