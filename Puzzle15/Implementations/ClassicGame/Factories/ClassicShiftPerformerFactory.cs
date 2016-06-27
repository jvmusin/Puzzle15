using Puzzle15.Interfaces;
using Puzzle15.Interfaces.Factories;

namespace Puzzle15.Implementations.ClassicGame.Factories
{
    public class ClassicShiftPerformerFactory : IShiftPerformerFactory<int>
    {
        public IShiftPerformer<int> Create()
        {
            return new ClassicShiftPerformer();
        }
    }
}
