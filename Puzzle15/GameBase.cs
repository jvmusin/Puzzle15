using System;
using System.Linq;

namespace Puzzle15
{
    public abstract class GameBase
    {
        protected RectangularField<int> Field { get; }

        protected GameBase(RectangularField<int> field, bool needCheck = true, bool needClone = true)
        {
            if (needCheck)
                CheckField(field);
            Field = needClone ? field.Clone() : field;
        }

        protected void CheckField(RectangularField<int> field)
        {
            if (field == null)
                throw new ArgumentNullException(nameof(field), "Field shouldn't be null");

            var elementCount = field.Height*field.Width;
            if (elementCount == 0)
                throw new ArgumentException("Field doesn't have any cell");

            var elements = field.Select(x => x.Value).Distinct().ToList();
            if (elements.Count != elementCount)
                throw new ArgumentException("Not all elements are distinct");

            var min = elements.Min();
            if (min < 0)
                throw new ArgumentException("Field contain negative numbers");

            if (min != 0)
                throw new ArgumentException("Field doesn't contain an empty cell");

            if (elements.Max() != elements.Count - 1)
                throw new ArgumentException("Some values are skipped");
        }

        public abstract GameBase Shift(int value);

        #region Indexers

        public int this[int row, int column]
        {
            get { return this[new CellLocation(row, column)]; }
            protected set { this[new CellLocation(row, column)] = value; }
        }

        public virtual int this[CellLocation location]
        {
            get { return Field[location]; }
            protected set { Field[location] = value; }
        }

        #endregion

        #region Equals, GetHashCode and ToString methods

        protected bool Equals(GameBase other)
        {
            return Field.Equals(other.Field);
        }

        public override bool Equals(object obj)
        {
            var other = obj as GameBase;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return Field.GetHashCode();
        }

        public override string ToString()
        {
            return Field.ToString();
        }

        #endregion
    }
}
