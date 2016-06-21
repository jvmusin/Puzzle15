using Puzzle15.Interfaces;

namespace Puzzle15.Implementations.ShiftPerformerFactories
{
    public class ImmutableGameShiftPerformerFactory : IShiftPerformerFactory
    {
        public IShiftPerformer Create()
        {
            return new ShiftPerformer(true, x => x.Clone());
        }
    }
}
