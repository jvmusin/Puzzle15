using System.Drawing;
using RectangularField.Interfaces;
using RectangularField.Interfaces.Factories;

namespace RectangularField.Implementations.Factories
{
    public class ImmutableRectangularFieldFactory<T> : IRectangularFieldFactory<T>
    {
        public IRectangularField<T> Create(Size size)
        {
            return new ImmutableRectangularField<T>(size);
        }
    }
}
