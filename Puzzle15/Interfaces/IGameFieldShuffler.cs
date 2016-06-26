using RectangularField.Core;

namespace Puzzle15.Interfaces
{
    public interface IGameFieldShuffler<TCell>
    {
        IRectangularField<TCell> Shuffle(IRectangularField<TCell> field, int quality);
    }
}
