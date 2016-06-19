namespace Puzzle15
{
    public interface IGameFactory
    {
        IGame Create(RectangularField<int> initialField);
    }
}