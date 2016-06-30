using System.Windows;
using System.Windows.Controls;
using RectangularField.Implementations.Base;

namespace Puzzle15.GUI
{
    public static class GridExtensions
    {
        public static void AddChild(this Grid grid, UIElement element, CellLocation location)
        {
            grid.Children.Add(element);
            Grid.SetRow(element, location.Row);
            Grid.SetColumn(element, location.Column);
        }
    }
}
