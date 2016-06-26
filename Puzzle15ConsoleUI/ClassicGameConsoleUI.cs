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
        private readonly IFieldValidator<int> fieldValidator;
        private readonly IShiftPerformerFactory<int> shiftPerformer;
        private readonly IGameFieldShuffler<int> gameFieldShuffler;

        public ClassicGameConsoleUI(IRectangularFieldFactory<int> rectangularFieldFactory, IGameFactory<int> gameFactory, IFieldValidator<int> fieldValidator, IShiftPerformerFactory<int> shiftPerformer, IGameFieldShuffler<int> gameFieldShuffler)
        {
            this.rectangularFieldFactory = rectangularFieldFactory;
            this.gameFactory = gameFactory;
            this.fieldValidator = fieldValidator;
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
                DrawTurns(game.Turns);
                DrawField(game.CurrentField);
                Console.Write("Enter a number to shift: ");
                while (true)
                {
                    try
                    {
                        game = game.Shift(int.Parse(Console.ReadLine()));
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Incorrect number. Please, repeat: ");
                    }
                }
            }

            Console.WriteLine("YAHOOO!!!");
            DrawTurns(game.Turns);
            DrawField(game.CurrentField);
        }

        private void DrawTurns(int turns)
        {
            Console.WriteLine($"Turns: {turns}");
        }

        private void DrawField(IRectangularField<int> field)
        {
            Console.WriteLine(field);
            Console.WriteLine();
            Console.WriteLine("------------------------------------");
            Console.WriteLine();
        }

        private IRectangularField<int> GetDefaultField(Size size)
        {
            var count = size.Height*size.Width;
            var values = Enumerable.Range(0, count).Select(x => ++x%count).ToArray();
            
            return rectangularFieldFactory.Create(size, values);
        }
    }
}
