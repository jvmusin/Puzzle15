namespace Puzzle15.Interfaces.Factories
{
    public interface IGameFactory<TCell>
    {
        IGame<TCell> Create(IGameField<TCell> initialField, IGameField<TCell> target);
    }
}
