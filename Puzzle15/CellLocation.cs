using System;
using System.Collections.Generic;
using System.Linq;

namespace Puzzle15
{
    public class CellLocation : IComparable<CellLocation>
    {
        public int Row { get; }
        public int Column { get; }

        private static readonly IEnumerable<CellLocation> Deltas;
        public IEnumerable<CellLocation> ByEdgeHeighbours => Deltas.Select(delta => this + delta);

        public CellLocation(int row, int column)
        {
            Row = row;
            Column = column;
        }

        static CellLocation()
        {
            Deltas = Enum.GetValues(typeof(Direction))
                .Cast<Direction>()
                .Select(direction => direction.GetDelta())
                .ToArray();
        }

        public static CellLocation operator +(CellLocation location, CellLocation delta)
        {
            var row = location.Row + delta.Row;
            var column = location.Column + delta.Column;
            return new CellLocation(row, column);
        }

        #region CompareTo, Equals, GetHashCode and ToString methods

        public int CompareTo(CellLocation other)
        {
            var cmp = Row.CompareTo(other.Row);
            if (cmp == 0)
                cmp = Column.CompareTo(other.Column);
            return cmp;
        }

        protected bool Equals(CellLocation other)
        {
            return CompareTo(other) == 0;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CellLocation;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return Row ^ Column;
        }

        public override string ToString()
        {
            return $"Row: {Row}, Column: {Column}";
        }

        #endregion
    }
}
