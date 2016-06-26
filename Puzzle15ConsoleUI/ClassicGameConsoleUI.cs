using System;
using System.Drawing;
using System.Linq;
using Puzzle15.Interfaces;
using RectangularField.Core;
using RectangularField.Factories;
using RectangularField.Utils;

namespace Puzzle15ConsoleUI
{
    public class ClassicGameConsoleUI
    {
        private readonly IRectangularFieldFactory<int> rectangularFieldFactory;
        private readonly IGameFactory<int> gameFactory;
        private readonly IGameFieldValidator<int> gameFieldValidator;
        private readonly IShiftPerformerFactory<int> shiftPerformer;
        private readonly IGameFieldShuffler<int> gameFieldShuffler;

        public ClassicGameConsoleUI(IRectangularFieldFactory<int> rectangularFieldFactory, IGameFactory<int> gameFactory, IGameFieldValidator<int> gameFieldValidator, IShiftPerformerFactory<int> shiftPerformer, IGameFieldShuffler<int> gameFieldShuffler)
        {
            this.rectangularFieldFactory = rectangularFieldFactory;
            this.gameFactory = gameFactory;
            this.gameFieldValidator = gameFieldValidator;
            this.shiftPerformer = shiftPerformer;
            this.gameFieldShuffler = gameFieldShuffler;
        }

        public void Run()
        {
            var size = new Size(3, 3);

            var target = GetDefaultField(size);
            var initialField = gameFieldShuffler.Shuffle(target.Immutable ? target : target.Clone(), 5);

            var game = gameFactory.Create(initialField, target);

            while (!game.Finished)
            {
                DrawField(game.CurrentField);
            }
        }

        private void DrawField(IReadOnlyRectangularField<int> field)
        {
            Console.WriteLine(field);
            Console.WriteLine();
            Console.WriteLine("------------------------------------");
            Console.WriteLine();
        }

        private IRectangularField<int> GetDefaultField(Size size)
        {
            var values = Enumerable.Range(0, size.Height * size.Width).ToArray();
            return rectangularFieldFactory.Create(size, values);
        }
    }
}
