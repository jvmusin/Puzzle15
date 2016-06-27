namespace Puzzle15.Interfaces.Factories
{
    public interface IShiftPerformerFactory<TCell>
    {
        IShiftPerformer<TCell> Create();
    }
}
