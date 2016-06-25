using System.Collections.Generic;
using RectangularField.Core;

namespace Puzzle15.Interfaces
{
    public interface IGame<TCell> : IEnumerable<CellInfo<TCell>>
    {
        int Turns { get; }
        bool Finished { get; }
        IGame<TCell> PreviousState { get; }
        IReadOnlyRectangularField<TCell> Target { get; }

        IGame<TCell> Shift(TCell value);
        IGame<TCell> Shift(CellLocation valueLocation);

        CellLocation GetLocation(TCell value);
        TCell this[CellLocation location] { get; }
    }
}
