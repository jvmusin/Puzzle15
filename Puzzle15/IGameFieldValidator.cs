using System;

namespace Puzzle15
{
    public interface IGameFieldValidator
    {
        Exception Validate(RectangularField<int> field);
    }
}