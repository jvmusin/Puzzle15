using RectangularField.Implementations.Base;

namespace Puzzle15.Interfaces
{
    public interface IShiftPerformer<TCell>
    {
        IGameField<TCell> PerformShift(IGameField<TCell> field, TCell value);

        IGameField<TCell> PerformShift(IGameField<TCell> field, CellLocation valueLocation);
    }
}
