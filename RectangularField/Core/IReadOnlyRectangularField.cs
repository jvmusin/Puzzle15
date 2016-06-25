using System.Collections.Generic;
using System.Drawing;

namespace RectangularField.Core
{
    public interface IReadOnlyRectangularField<T>
    {
        Size Size { get; }
        int Height { get; }
        int Width { get; }

        bool Contains(CellLocation location);

        IEnumerable<CellLocation> EnumerateLocations();

        IEnumerable<CellLocation> GetLocations(T value);
        CellLocation GetLocation(T value);

        T this[CellLocation location] { get; }
        T GetValue(CellLocation location);
    }
}
