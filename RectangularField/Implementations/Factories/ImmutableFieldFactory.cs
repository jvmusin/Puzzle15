using System.Drawing;
using RectangularField.Interfaces;

namespace RectangularField.Implementations.Factories
{
    public class ImmutableFieldFactory<T> : IFieldFactory<T>
    {
        public IField<T> Create(Size size)
        {
            return new ImmutableField<T>(size);
        }
    }
}
