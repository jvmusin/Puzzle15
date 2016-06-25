using System.Collections.Generic;
using RectangularField.Core;

namespace Puzzle15.Interfaces
{
    public interface IGame : IEnumerable<CellInfo<int>>
    {
        IGame Shift(int value);

        CellLocation GetLocation(int value);
        int this[CellLocation location] { get; }
    }
}
