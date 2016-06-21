using Puzzle15.Interfaces;

namespace Puzzle15.Implementations.ShiftPerformerFactories
{
    public class MutableGameShiftPerformerFactory : IShiftPerformerFactory
    {
        public IShiftPerformer Create()
        {
            return new ShiftPerformer();
        }
    }
}
