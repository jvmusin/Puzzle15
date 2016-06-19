using System;
using System.Linq;

namespace Puzzle15
{
    public class GameFieldValidator : IGameFieldValidator
    {
        public Exception Validate(RectangularField<int> field)
        {
            if (field == null)
                return new ArgumentNullException(nameof(field), "Field shouldn't be null");

            var elementCount = field.Height * field.Width;
            if (elementCount == 0)
                return new ArgumentException("Field doesn't have any cell");

            var elements = field.Select(x => x.Value).Distinct().ToList();
            if (elements.Count != elementCount)
                return new ArgumentException("Not all elements are distinct");

            var min = elements.Min();
            if (min < 0)
                return new ArgumentException("Field contain negative numbers");

            if (min != 0)
                return new ArgumentException("Field doesn't contain an empty cell");

            if (elements.Max() != elements.Count - 1)
                return new ArgumentException("Some values are skipped");

            return null;
        }
    }
}
