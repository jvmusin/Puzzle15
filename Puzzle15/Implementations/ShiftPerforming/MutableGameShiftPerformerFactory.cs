using Puzzle15.Interfaces;

namespace Puzzle15.Implementations.ShiftPerforming
{
    public class MutableGameShiftPerformerFactory : IShiftPerformerFactory
    {
        public IShiftPerformer Create()
        {
            return ShiftPerformer.Mutable();
        }
    }
}
