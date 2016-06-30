using Puzzle15.Implementations.Base;
using RectangularField.Interfaces;

namespace Puzzle15.Interfaces
{
    public interface IGameFieldValidator<TCell>
    {
        ValidationResult Validate(IField<TCell> initialField, IField<TCell> target);
    }
}
