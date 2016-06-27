using System;
using System.Linq;
using Puzzle15.Interfaces;
using RectangularField.Interfaces;

namespace Puzzle15.Implementations.ClassicGame
{
    public class ClassicGameFieldShuffler: IGameFieldShuffler<int>
    {
        private readonly Random random = new Random();

        public IGameField<int> Shuffle(IGameField<int> field, int quality)
        {
            var zeroLocation = field.GetLocation(0);
            var result = (IRectangularField<int>) field;

            for (var i = 0; i < quality; i++)
                foreach (var neighbour in zeroLocation.ByEdgeNeighbours.OrderBy(x => random.Next()))
                    if (field.Contains(neighbour))
                    {
                        result = result.Swap(zeroLocation, neighbour);
                        zeroLocation = neighbour;
                        break;
                    }

            return (IGameField<int>) result;
        }
    }
}
