using RectangularField.Implementations.Base;

namespace RectangularField.Utils
{
    public enum Direction
    {
        Down,
        Left,
        Up,
        Right
    }

    public static class DirectionExtensions
    {
        public static CellLocation GetDelta(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:  return new CellLocation(1, 0);
                case Direction.Left:  return new CellLocation(0, -1);
                case Direction.Up:    return new CellLocation(-1, 0);
                case Direction.Right: return new CellLocation(0, 1);
                // ReSharper disable once PossibleNullReferenceException
                default: throw null;    // Never throws
            }
        }
    }
}
