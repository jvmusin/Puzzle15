namespace RectangularField.Implementations.Base
{
    public delegate TResult CellConverter<TValue, out TResult>(CellInfo<TValue> cellInfo);
}
