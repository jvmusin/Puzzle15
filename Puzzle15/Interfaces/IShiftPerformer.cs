using Puzzle15.Base;

namespace Puzzle15.Interfaces
{
    public interface IShiftPerformer
    {
        bool MutatesGame { get; }
        IGame Perform(IGame game, IRectangularField<int> gameField, int value);
    }
}