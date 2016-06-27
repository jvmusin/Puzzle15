using System.Drawing;
using Puzzle15.Interfaces;
using Puzzle15.Interfaces.Factories;
using RectangularField.Interfaces.Factories;

namespace Puzzle15.Implementations.Factories
{
    public class GameFieldFactory<T> : IGameFieldFactory<T>
    {
        private readonly IRectangularFieldFactory<T> backingFieldFactory;
        private readonly IGameFieldShufflerFactory<T> gameFieldShufflerFactory;

        public GameFieldFactory(IRectangularFieldFactory<T> backingFieldFactory, IGameFieldShufflerFactory<T> gameFieldShufflerFactory)
        {
            this.backingFieldFactory = backingFieldFactory;
            this.gameFieldShufflerFactory = gameFieldShufflerFactory;
        }

        public IGameField<T> Create(Size size)
        {
            var rectangularField = backingFieldFactory.Create(size);
            var gameFieldShuffler = gameFieldShufflerFactory.Create();
            return new GameField<T>(rectangularField, gameFieldShuffler);
        }
    }
}
