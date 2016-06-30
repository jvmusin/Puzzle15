using RectangularField.Implementations.Base;
using RectangularField.Interfaces;

namespace Puzzle15.Interfaces
{
    public interface IShiftPerformer<TCell>
    {
        IField<TCell> Perform(IField<TCell> field, TCell value);

        IField<TCell> Perform(IField<TCell> field, CellLocation valueLocation);
    }
}
