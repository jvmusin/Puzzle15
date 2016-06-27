namespace Puzzle15.Interfaces.Factories
{
    public interface IGameFieldValidatorFactory<T>
    {
        IGameFieldValidator<T> Create();
    }
}
