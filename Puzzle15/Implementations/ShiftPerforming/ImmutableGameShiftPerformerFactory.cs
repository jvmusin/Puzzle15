using Puzzle15.Interfaces;

namespace Puzzle15.Implementations.ShiftPerforming
{
    public class ImmutableGameShiftPerformerFactory : IShiftPerformerFactory
    {
        public IShiftPerformer Create()
        {
            return ShiftPerformer.Immutable(x => x.Clone());
        }
    }
}
