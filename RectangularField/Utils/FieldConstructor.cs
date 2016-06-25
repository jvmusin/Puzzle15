using System.Drawing;
using RectangularField.Core;

namespace RectangularField.Utils
{
    public delegate IRectangularField<T> FieldConstructor<T>(Size size);
}
