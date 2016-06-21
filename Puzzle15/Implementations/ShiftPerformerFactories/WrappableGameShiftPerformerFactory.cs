using Puzzle15.Interfaces;

namespace Puzzle15.Implementations.ShiftPerformerFactories
{
    public class WrappableGameShiftPerformerFactory : IShiftPerformerFactory
    {
        public IShiftPerformer Create()
        {
            return new ShiftPerformer(true,
                field => field is WrappedGameField
                    ? field
                    : new WrappedGameField(field));
        }
    }
}
