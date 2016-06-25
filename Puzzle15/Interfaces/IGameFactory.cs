using RectangularField.Core;

namespace Puzzle15.Interfaces
{
    public interface IGameFactory<TCell>
    {
        IGame<TCell> Create(IRectangularField<TCell> initialField, IRectangularField<TCell> target);
    }
}
