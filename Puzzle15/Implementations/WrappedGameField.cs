using System;
using System.Collections.Generic;
using System.Linq;
using Puzzle15.Base;

namespace Puzzle15.Implementations
{
    public class WrappedGameField : RectangularField<int>
    {
        private readonly RectangularField<int> parent;
        private readonly CellInfo<int>[] changedCells;

        public WrappedGameField(RectangularField<int> parent, params CellInfo<int>[] changedCells)
            : base(parent.Size, false)
        {
            this.parent = parent;
            this.changedCells = changedCells.ToArray();
        }

        #region Primary actions

        public override RectangularField<int> Swap(CellLocation location1, CellLocation location2)
        {
            var value1 = this[location1];
            var value2 = this[location2];

            return new WrappedGameField(this,
                new CellInfo<int>(location1, value2),
                new CellInfo<int>(location2, value1));
        }

        public override RectangularField<int> Fill(Func<CellLocation, int> getValue)
        {
            var newField = new RectangularField<int>(Size).Fill(getValue);
            return new WrappedGameField(newField);
        }

        public override RectangularField<int> Clone()
        {
            return Fill(location => this[location]);
        }

        #endregion

        #region Indexers

        public override IEnumerable<CellLocation> GetLocations(int value)
        {
            return EnumerateLocations().Where(x => this[x] == value);
        }

        public override CellLocation GetLocation(int value)
        {
            var changedCell = changedCells.FirstOrDefault(x => x.Value == value);
            return changedCell == null
                ? parent.GetLocation(value)
                : changedCell.Location;
        }

        public override int this[CellLocation location]
        {
            get
            {
                var changedCell = changedCells.FirstOrDefault(x => x.Location.Equals(location));
                return changedCell?.Value ?? parent[location];
            }
            set { throw new NotImplementedException(); }
        }
        
        #endregion
    }
}
