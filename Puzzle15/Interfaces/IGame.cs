using Puzzle15.Base;
using Puzzle15.Base.Field;

namespace Puzzle15.Interfaces
{
    public interface IGame
    {
        IGame Shift(int value);
        IShiftPerformer ShiftPerformer { get; }

        CellLocation GetLocation(int value);
        int this[CellLocation location] { get; }
    }
}
