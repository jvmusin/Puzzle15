using Puzzle15.Base;
using Puzzle15.Base.Field;

namespace Puzzle15.Interfaces
{
    public interface IShiftPerformer
    {
        bool MutatesGame { get; }
        IGame Perform(IGame game, IRectangularField<int> gameField, int value);
    }
}