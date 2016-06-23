namespace Puzzle15.Base.Field
{
    public delegate TResult CellConverter<TValue, out TResult>(CellInfo<TValue> callInfo);
}
