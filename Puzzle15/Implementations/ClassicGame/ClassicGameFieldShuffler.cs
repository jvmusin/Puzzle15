using Puzzle15.Interfaces;
using RectangularField.Core;

namespace Puzzle15.Implementations.ClassicGame
{
    public class ClassicGameFieldShuffler: IGameFieldShuffler<int>
    {
        public IRectangularField<int> Shuffle(IRectangularField<int> field, int quality)
        {
            var zeroLocation = field.GetLocation(0);

            for (var i = 0; i < quality; i++)
                foreach (var neighbour in zeroLocation.ByEdgeNeighbours.Shuffle())
                    if (field.Contains(neighbour))
                    {
                        field = field.Swap(zeroLocation, neighbour);
                        zeroLocation = neighbour;
                        break;
                    }

            return field;
        }
    }
}
