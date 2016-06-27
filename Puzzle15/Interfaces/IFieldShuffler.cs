using RectangularField.Core;

namespace Puzzle15.Interfaces
{
    public interface IFieldShuffler<TCell>
    {
        IRectangularField<TCell> Shuffle(IRectangularField<TCell> field, int quality);
    }
}
