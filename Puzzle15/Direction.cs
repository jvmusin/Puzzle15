namespace Puzzle15
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
                default: throw null;    // Never throws
            }
        }
    }
}
