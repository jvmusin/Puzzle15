using Puzzle15.Interfaces;

namespace Puzzle15.Implementations
{
    public class ShiftPerformerFactory : IShiftPerformerFactory
    {
        public IShiftPerformer Create()
        {
            return new ShiftPerformer();
        }
    }
}
