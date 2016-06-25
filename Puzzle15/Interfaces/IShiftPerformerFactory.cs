namespace Puzzle15.Interfaces
{
    public interface IShiftPerformerFactory<TCell>
    {
        IShiftPerformer<TCell> Create();
    }
}
