using System.Drawing;
using RectangularField.Interfaces;
using RectangularField.Interfaces.Factories;

namespace RectangularField.Implementations.Factories
{
    public class WrappingRectangularFieldFactory<T> : IRectangularFieldFactory<T>
    {
        public IRectangularField<T> Create(Size size)
        {
            return new WrappingRectangularField<T>(new MutableRectangularField<T>(size));
        }
    }
}
