using System.Drawing;

namespace Puzzle15.Interfaces.Factories
{
    public interface IGameFieldFactory<T>
    {
        IGameField<T> Create(Size size);
    }
}
