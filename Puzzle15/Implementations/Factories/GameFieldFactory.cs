using System.Drawing;
using RectangularField.Interfaces;
using RectangularField.Interfaces.Factories;

namespace Puzzle15.Implementations.Factories
{
    public class GameFieldFactory<T> : IFieldFactory<T>
    {
        private readonly IFieldFactory<T> backingFieldFactory;
        private readonly IFieldShufflerFactory<T> shufflerFactory;

        public GameFieldFactory(IFieldFactory<T> backingFieldFactory, IFieldShufflerFactory<T> shufflerFactory)
        {
            this.backingFieldFactory = backingFieldFactory;
            this.shufflerFactory = shufflerFactory;
        }

        public IField<T> Create(Size size)
        {
            var field = backingFieldFactory.Create(size);
            field.Shuffler = shufflerFactory.Create();
            return field;
        }
    }
}
