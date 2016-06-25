using Puzzle15.Implementations;
using RectangularField.Core;

namespace Puzzle15.Interfaces
{
    public interface IGameFieldValidator<TCell>
    {
        ValidationResult Validate(IRectangularField<TCell> initialField, IRectangularField<TCell> target);
    }
}
