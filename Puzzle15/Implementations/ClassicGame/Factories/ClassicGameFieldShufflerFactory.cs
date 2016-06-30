using RectangularField.Interfaces;
using RectangularField.Interfaces.Factories;

namespace Puzzle15.Implementations.ClassicGame.Factories
{
    public class ClassicGameFieldShufflerFactory : IFieldShufflerFactory<int>
    {
        public IFieldShuffler<int> Create()
        {
            return new ClassicGameFieldShuffler();
        }
    }
}
