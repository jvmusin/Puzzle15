using RectangularField.Core;

namespace Puzzle15.Interfaces
{
    public interface IShiftPerformer<TCell>
    {
        IRectangularField<TCell> Perform(IRectangularField<TCell> field, TCell value);

        IRectangularField<TCell> Perform(IRectangularField<TCell> field, CellLocation valueLocation);
    }
}
