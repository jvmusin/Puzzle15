using Puzzle15.Base.Field;
using Puzzle15.Interfaces;

namespace Puzzle15.Implementations.ShiftPerforming
{
    public class WrappableGameShiftPerformerFactory : IShiftPerformerFactory
    {
        public IShiftPerformer Create()
        {
            return ShiftPerformer.Immutable(
                field => field is WrappingRectangularField<int>
                    ? field
                    : new WrappingRectangularField<int>(field));
        }
    }
}
