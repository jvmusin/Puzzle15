using System.Linq;
using Puzzle15.Base.Field;
using Puzzle15.Interfaces;

namespace Puzzle15.Implementations.GameFieldValidating
{
    public class GameFieldValidator : IGameFieldValidator
    {
        public ValidationResult Validate(IRectangularField<int> field)
        {
            var errorMessage = GetErrorMessage(field);
            return errorMessage == null
                ? ValidationResult.Success()
                : ValidationResult.Unsuccess(errorMessage);
        }

        private static string GetErrorMessage(IRectangularField<int> field)
        {
            if (field == null)
                return "Field shouldn't be null";

            var elementCount = field.Height * field.Width;
            if (elementCount == 0)
                return "Field doesn't have any cell";

            var elements = field.Select(x => x.Value).Distinct().ToList();
            if (elements.Count != elementCount)
                return "Not all elements are distinct";

            var min = elements.Min();
            if (min < 0)
                return "Field contain negative numbers";

            if (min != 0)
                return "Field doesn't contain an empty cell";

            if (elements.Max() != elements.Count - 1)
                return "Some values are skipped";

            return null;
        }
    }
}
