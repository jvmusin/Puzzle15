using Puzzle15.Interfaces;

namespace Puzzle15.Implementations
{
    public class ImmutableGameShiftPerformerFactory : IShiftPerformerFactory
    {
        public IShiftPerformer Create()
        {
            return new ImmutableGameShiftPerformer();
        }
    }
}
