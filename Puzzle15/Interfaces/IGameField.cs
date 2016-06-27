using RectangularField.Interfaces;

namespace Puzzle15.Interfaces
{
    public interface IGameField<TCell> : IRectangularField<TCell>
    {
        IGameField<TCell> Shuffle(int quality);
    }
}
