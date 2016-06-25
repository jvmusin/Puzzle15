using System.Linq;
using Puzzle15.Interfaces;
using RectangularField.Core;

namespace Puzzle15.Implementations
{
    public class GameFieldValidator : IGameFieldValidator<int>
    {
        public ValidationResult Validate(IRectangularField<int> initialField, IRectangularField<int> target)
        {
            var errorMessage = GetErrorMessage(initialField, target);

            return errorMessage == null
                ? ValidationResult.Success()
                : ValidationResult.Unsuccess(errorMessage);
        }

        private static string GetErrorMessage(IRectangularField<int> initialField, IRectangularField<int> target)
        {
            if (initialField == null)
                return "Initial field shouldn't be null";

            if (target == null)
                return "Target shouldn't be null";

            if (initialField.Size != target.Size)
                return "Initial field and target should have same size";

            return GetErrorMessageWithName(initialField, nameof(initialField)) ??
                   GetErrorMessageWithName(target, nameof(target));
        }

        private static string GetErrorMessageWithName(IRectangularField<int> field, string fieldName)
        {
            var errorMessage = GetErrorMessage(field);
            return errorMessage == null
                ? null
                : $"{fieldName}: {errorMessage}";
        }

        private static string GetErrorMessage(IRectangularField<int> initialField)
        {
            var fieldSize = initialField.Size;
            if (fieldSize.Height <= 0 || fieldSize.Width <= 0)
                return "Field should have positive size";

            var elements = initialField.Select(x => x.Value).Distinct().ToList();
            if (elements.Count != initialField.Count())
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
