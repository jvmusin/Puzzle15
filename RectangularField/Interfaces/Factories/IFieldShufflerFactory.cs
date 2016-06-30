namespace RectangularField.Interfaces.Factories
{
    public interface IFieldShufflerFactory<T>
    {
        IFieldShuffler<T> Create();
    }
}
