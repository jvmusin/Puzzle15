using Puzzle15.Base.Field;

namespace Puzzle15.Interfaces
{
    public interface IShiftPerformer
    {
        bool MutatesField { get; }
        IRectangularField<int> Perform(IRectangularField<int> field, int value);
    }
}
