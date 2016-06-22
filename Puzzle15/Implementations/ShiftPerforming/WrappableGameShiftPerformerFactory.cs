using Puzzle15.Interfaces;

namespace Puzzle15.Implementations.ShiftPerforming
{
    public class WrappableGameShiftPerformerFactory : IShiftPerformerFactory
    {
        public IShiftPerformer Create()
        {
            return ShiftPerformer.Immutable(
                field => field is WrappedGameField
                    ? field
                    : new WrappedGameField(field));
        }
    }
}
