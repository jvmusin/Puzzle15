using Puzzle15.Base;
using Puzzle15.Implementations;
using Puzzle15.Implementations.GameFieldValidating;

namespace Puzzle15.Interfaces
{
    public interface IGameFieldValidator
    {
        ValidationResult Validate(IRectangularField<int> field);
    }
}
