using System;
using System.Linq;
using RectangularField.Interfaces;

namespace Puzzle15.Implementations.ClassicGame
{
    public class ClassicGameFieldShuffler : IFieldShuffler<int>
    {
        private readonly Random random = new Random();

        public IField<int> Shuffle(IField<int> field, int quality)
        {
            var zeroLocation = field.GetLocation(0);
            var result = field;

            var count = 1 << quality;
            for (var i = 0; i < count; i++)
                foreach (var neighbour in zeroLocation.ByEdgeNeighbours.OrderBy(x => random.Next()))
                    if (field.Contains(neighbour))
                    {
                        result = result.Swap(zeroLocation, neighbour);
                        zeroLocation = neighbour;
                        break;
                    }

            return result;
        }
    }
}
