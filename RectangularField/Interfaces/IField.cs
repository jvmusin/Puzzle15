using System.Collections.Generic;
using System.Drawing;
using RectangularField.Implementations.Base;

namespace RectangularField.Interfaces
{
    public interface IField<T> : IEnumerable<CellInfo<T>>
    {
        Size Size { get; }
        int Height { get; }
        int Width { get; }

        bool Immutable { get; }
        IFieldShuffler<T> Shuffler { get; set; }

        IField<T> Swap(CellLocation location1, CellLocation location2);
        IField<T> Shuffle(int quality);
        IField<T> Fill(CellConverter<T, T> getValue);
        IField<T> Clone();

        bool Contains(CellLocation location);

        IEnumerable<CellLocation> EnumerateLocations();

        IEnumerable<CellLocation> GetLocations(T value);
        CellLocation GetLocation(T value);

        T this[CellLocation location] { get; set; }
        T GetValue(CellLocation location);
        IField<T> SetValue(T value, CellLocation location);
    }
}
