using System.Drawing;
using RectangularField.Core;
using RectangularField.Implementations;

namespace RectangularField.Factories
{
    public class RectangularFieldFactory<T> : IRectangularFieldFactory<T>
    {
        public IRectangularField<T> Create(Size size)
        {
            return new RectangularField<T>(size);
        }
    }
}
