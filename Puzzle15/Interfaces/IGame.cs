using System.Collections.Generic;
using Puzzle15.Base.Field;

namespace Puzzle15.Interfaces
{
    public interface IGame : IEnumerable<CellInfo<int>>
    {
        IGame Shift(int value);

        CellLocation GetLocation(int value);
        int this[CellLocation location] { get; }
    }
}
