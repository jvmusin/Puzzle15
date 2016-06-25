using Puzzle15.Interfaces;

namespace Puzzle15.Implementations
{
    public class ShiftPerformerFactory : IShiftPerformerFactory<int>
    {
        public IShiftPerformer<int> Create()
        {
            return new ShiftPerformer();
        }
    }
}
