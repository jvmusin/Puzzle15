using Puzzle15.Base;

namespace Puzzle15.Interfaces
{
    public interface IGameFactory
    {
        IGame Create(IRectangularField<int> initialField);
    }
}
