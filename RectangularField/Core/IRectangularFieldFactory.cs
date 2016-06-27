using System.Drawing;

namespace RectangularField.Core
{
    public interface IRectangularFieldFactory<T>
    {
        IRectangularField<T> Create(Size size);
    }
}
