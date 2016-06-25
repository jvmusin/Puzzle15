using RectangularField.Utils;

namespace RectangularField.Core
{
    public class CellInfo<T>
    {
        public CellLocation Location { get; }
        public T Value { get; }

        public CellInfo(CellLocation location, T value)
        {
            Location = location;
            Value = value;
        }

        #region Equals, GetHashCode and ToString methods

        protected bool Equals(CellInfo<T> other)
        {
            return Location.Equals(other.Location) &&
                   Helpers.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            var other = obj as CellInfo<T>;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return Location.GetHashCode() ^ Helpers.GetHashCode(Value);
        }

        public override string ToString()
        {
            return $"Location: {{{Location}}}, Value: {Value}";
        }

        #endregion
    }
}
