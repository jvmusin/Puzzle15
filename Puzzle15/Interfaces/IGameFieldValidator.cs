using Puzzle15.Base.Field;
using Puzzle15.Implementations;

namespace Puzzle15.Interfaces
{
    public interface IGameFieldValidator
    {
        ValidationResult Validate(IRectangularField<int> field);
    }
}
