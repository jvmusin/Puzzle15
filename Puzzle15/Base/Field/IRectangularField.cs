using System;
using System.Collections.Generic;
using System.Drawing;

namespace Puzzle15.Base.Field
{
    public interface IRectangularField<T> : IEnumerable<CellInfo<T>>
    {
        Size Size { get; }
        int Height { get; }
        int Width { get; }

        IRectangularField<T> Swap(CellLocation location1, CellLocation location2);
        IRectangularField<T> Fill(Func<CellLocation, T> getValue);
        IRectangularField<T> Clone();

        IEnumerable<CellLocation> EnumerateLocations();

        IEnumerable<CellLocation> GetLocations(T value);
        CellLocation GetLocation(T value);

        T this[CellLocation location] { get; set; }
        T GetValue(CellLocation location);
        IRectangularField<T> SetValue(T value, CellLocation location);
    }
}