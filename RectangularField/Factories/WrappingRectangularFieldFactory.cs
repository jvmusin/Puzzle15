using System.Drawing;
using RectangularField.Core;
using RectangularField.Implementations;

namespace RectangularField.Factories
{
    public class WrappingRectangularFieldFactory<T> : IRectangularFieldFactory<T>
    {
        public IRectangularField<T> Create(Size size)
        {
            return new WrappingRectangularField<T>(new RectangularField<T>(size));
        }
    }
}
