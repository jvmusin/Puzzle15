namespace Puzzle15
{
    public interface IShiftPerformer
    {
        IGame Perform(IGame game, RectangularField<int> gameField, int value);
    }
}
