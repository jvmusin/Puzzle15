using System;

namespace RectangularField.Core
{
    public class InvalidLocationException : ArgumentOutOfRangeException
    {
        public InvalidLocationException(CellLocation location)
            : this(nameof(location), location) { }

        public InvalidLocationException(string paramName, CellLocation actualValue)
            : base(paramName, actualValue, "The field doesn't contain requested cell.") { }
    }
}
