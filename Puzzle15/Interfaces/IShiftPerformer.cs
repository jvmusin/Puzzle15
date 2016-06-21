using Puzzle15.Base;

namespace Puzzle15.Interfaces
{
    public interface IShiftPerformer
    {
        IGame Perform(IGame game, RectangularField<int> gameField, int value);
    }
}