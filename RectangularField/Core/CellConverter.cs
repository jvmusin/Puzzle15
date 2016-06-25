namespace RectangularField.Core
{
    public delegate TResult CellConverter<TValue, out TResult>(CellInfo<TValue> cellInfo);
}
