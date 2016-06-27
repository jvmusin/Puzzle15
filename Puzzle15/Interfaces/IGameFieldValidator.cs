using Puzzle15.Implementations.Base;

namespace Puzzle15.Interfaces
{
    public interface IGameFieldValidator<TCell>
    {
        ValidationResult Validate(IGameField<TCell> initialField, IGameField<TCell> target);
    }
}
