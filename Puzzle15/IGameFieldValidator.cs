namespace Puzzle15
{
    public interface IGameFieldValidator
    {
        ValidationResult Validate(RectangularField<int> field);
    }
}