using Puzzle15.Interfaces;
using Puzzle15.Interfaces.Factories;

namespace Puzzle15.Implementations.ClassicGame.Factories
{
    public class ClassicGameFieldShufflerFactory : IGameFieldShufflerFactory<int>
    {
        public IGameFieldShuffler<int> Create()
        {
            return new ClassicGameFieldShuffler();
        }
    }
}
