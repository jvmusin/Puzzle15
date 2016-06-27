using Puzzle15.Interfaces;

namespace Puzzle15.Implementations.ClassicGame
{
    public class ClassicGameShiftPerformerFactory : IShiftPerformerFactory<int>
    {
        public IShiftPerformer<int> Create()
        {
            return new ClassicGameShiftPerformer();
        }
    }
}
