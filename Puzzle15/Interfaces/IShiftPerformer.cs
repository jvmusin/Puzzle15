using Puzzle15.Base.Field;

namespace Puzzle15.Interfaces
{
    public interface IShiftPerformer
    {
        IRectangularField<int> Perform(IRectangularField<int> field, int value);
    }
}
