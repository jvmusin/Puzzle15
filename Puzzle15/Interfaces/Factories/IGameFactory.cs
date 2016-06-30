using RectangularField.Interfaces;

namespace Puzzle15.Interfaces.Factories
{
    public interface IGameFactory<TCell>
    {
        IGame<TCell> Create(IField<TCell> initialField, IField<TCell> target);
    }
}
