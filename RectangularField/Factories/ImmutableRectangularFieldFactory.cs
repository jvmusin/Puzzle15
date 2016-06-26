using System.Drawing;
using RectangularField.Core;
using RectangularField.Implementations;

namespace RectangularField.Factories
{
    public class ImmutableRectangularFieldFactory<T> : IRectangularFieldFactory<T>
    {
        public IRectangularField<T> Create(Size size)
        {
            return new ImmutableRectangularField<T>(size);
        }
    }
}
