using System.Drawing;

namespace RectangularField.Interfaces
{
    public interface IFieldFactory<T>
    {
        IField<T> Create(Size size);
    }
}
