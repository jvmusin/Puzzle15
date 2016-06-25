using System.Collections.Generic;
using System.Drawing;

namespace RectangularField.Core
{
    public interface IRectangularField<T> : IEnumerable<CellInfo<T>>
    {
        Size Size { get; }
        int Height { get; }
        int Width { get; }

        bool Immutable { get; }

        IRectangularField<T> Swap(CellLocation location1, CellLocation location2);
        IRectangularField<T> Fill(CellConverter<T, T> getValue);
        IRectangularField<T> Clone();

        bool Contains(CellLocation location);

        IEnumerable<CellLocation> EnumerateLocations();

        IEnumerable<CellLocation> GetLocations(T value);
        CellLocation GetLocation(T value);

        T this[CellLocation location] { get; set; }
        T GetValue(CellLocation location);
        IRectangularField<T> SetValue(T value, CellLocation location);
    }
}
