using System.Drawing;
using RectangularField.Core;

namespace RectangularField.Factories
{
    public interface IRectangularFieldFactory<T>
    {
        IRectangularField<T> Create(Size size);
    }
}
