namespace Puzzle15.Interfaces.Factories
{
    public interface IGameFieldShufflerFactory<T>
    {
        IGameFieldShuffler<T> Create();
    }
}
