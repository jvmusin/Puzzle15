namespace Puzzle15.Interfaces
{
    public interface IGameFieldShuffler<T>
    {
        IGameField<T> Shuffle(IGameField<T> field, int quality);
    }
}
