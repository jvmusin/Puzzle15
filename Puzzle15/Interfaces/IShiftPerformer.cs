using RectangularField.Core;

namespace Puzzle15.Interfaces
{
    public interface IShiftPerformer<TCell>
    {
        IRectangularField<TCell> PerformShift(IRectangularField<TCell> field, TCell value);

        IRectangularField<TCell> PerformShift(IRectangularField<TCell> field, CellLocation valueLocation);
    }
}
