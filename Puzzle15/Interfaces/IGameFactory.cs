using Puzzle15.Base.Field;

namespace Puzzle15.Interfaces
{
    public interface IGameFactory
    {
        IGame Create(IRectangularField<int> initialField);
    }
}
