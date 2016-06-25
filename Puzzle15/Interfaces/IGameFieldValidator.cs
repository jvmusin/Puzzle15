using Puzzle15.Implementations;
using RectangularField.Core;

namespace Puzzle15.Interfaces
{
    public interface IGameFieldValidator
    {
        ValidationResult Validate(IRectangularField<int> field);
    }
}
