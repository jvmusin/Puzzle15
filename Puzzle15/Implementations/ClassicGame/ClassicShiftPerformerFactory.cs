using Puzzle15.Interfaces;

namespace Puzzle15.Implementations.ClassicGame
{
    public class ClassicShiftPerformerFactory : IShiftPerformerFactory<int>
    {
        public IShiftPerformer<int> Create()
        {
            return new ClassicShiftPerformer();
        }
    }
}
