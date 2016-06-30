using System.Drawing;
using RectangularField.Interfaces;
using RectangularField.Interfaces.Factories;

namespace RectangularField.Implementations.Factories
{
    public class MutableFieldFactory<T> : IFieldFactory<T>
    {
        public IField<T> Create(Size size)
        {
            return new MutableField<T>(size);
        }
    }
}
