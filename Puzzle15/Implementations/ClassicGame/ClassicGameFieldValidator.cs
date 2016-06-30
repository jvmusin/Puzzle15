using System.Linq;
using Puzzle15.Implementations.Base;
using Puzzle15.Interfaces;
using RectangularField.Interfaces;

namespace Puzzle15.Implementations.ClassicGame
{
    public class ClassicGameFieldValidator : IGameFieldValidator<int>
    {
        public ValidationResult Validate(IField<int> initialField, IField<int> target)
        {
            var errorMessage = GetErrorMessage(initialField, target);

            return errorMessage == null
                ? ValidationResult.Success()
                : ValidationResult.Unsuccess(errorMessage);
        }

        private static string GetErrorMessage(IField<int> initialField, IField<int> target)
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

        private static string GetErrorMessageWithName(IField<int> field, string fieldName)
        {
            var errorMessage = GetErrorMessage(field);
            return errorMessage == null
                ? null
                : $"{fieldName}: {errorMessage}";
        }

        private static string GetErrorMessage(IField<int> field)
        {
            var fieldSize = field.Size;
            if (fieldSize.Height <= 0 || fieldSize.Width <= 0)
                return "Field should have positive size";

            var elements = field.Select(x => x.Value).Distinct().ToList();
            if (elements.Count != field.Count())
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
