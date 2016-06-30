using RectangularField.Interfaces;
using RectangularField.Interfaces.Factories;

namespace RectangularField.Implementations.Factories
{
    public class StandardFieldShufflerFactory<T> : IFieldShufflerFactory<T>
    {
        public IFieldShuffler<T> Create()
        {
            return new StandardFieldShuffler<T>();
        }
    }
}
