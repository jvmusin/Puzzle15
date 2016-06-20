using Puzzle15.Base;
using Puzzle15.Implementations;

namespace Puzzle15.Interfaces
{
    public interface IGameFieldValidator
    {
        ValidationResult Validate(RectangularField<int> field);
    }
}
