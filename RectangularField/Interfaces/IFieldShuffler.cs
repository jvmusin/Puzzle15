namespace RectangularField.Interfaces
{
    public interface IFieldShuffler<T>
    {
        IField<T> Shuffle(IField<T> field, int quality);
    }
}