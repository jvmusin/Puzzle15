using RectangularField.Core;

namespace Puzzle15.Interfaces
{
    public interface IGameFactory
    {
        IGame Create(IRectangularField<int> initialField);
    }
}
