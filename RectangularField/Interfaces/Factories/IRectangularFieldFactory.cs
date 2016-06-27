using System.Drawing;

namespace RectangularField.Interfaces.Factories
{
    public interface IRectangularFieldFactory<T>
    {
        IRectangularField<T> Create(Size size);
    }
}
