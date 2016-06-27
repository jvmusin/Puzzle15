using System;
using System.Linq;
using System.Drawing;
using Ninject;
using Puzzle15.Interfaces;
using Puzzle15.Interfaces.Factories;
using Puzzle15.UI.Modules;
using Puzzle15.Utils;

namespace Puzzle15.GUI.ConsoleUI
{
    public class ClassicGameConsoleUI
    {
        private readonly IGameFieldFactory<int> gameFieldFactory;
        private readonly IGameFactory<int> gameFactory;

        public ClassicGameConsoleUI(IGameFieldFactory<int> gameFieldFactory, IGameFactory<int> gameFactory)
        {
            this.gameFieldFactory = gameFieldFactory;
            this.gameFactory = gameFactory;
        }

        public static void MainX(string[] args)
        {
            new StandardKernel(new GameBaseModule(), new ClassicGameModule()).Get<ClassicGameConsoleUI>().Run();
        }

        public void Run()
        {
            DrawGreetings(PlayGame(CreateGame()));
        }

        #region Game build methods

        private IGame<int> CreateGame()
        {
            var size = new Size(3, 3);
            var initialField = GetDefaultField(size);
            var target = (IGameField<int>) initialField.Clone();

            var difficulity = InputDifficulity();
            initialField = initialField.Shuffle(difficulity);

            return gameFactory.Create(initialField, target);
        }

        private IGameField<int> GetDefaultField(Size size)
        {
            var count = size.Height * size.Width;
            var values = Enumerable.Range(0, count).Select(x => (x + 1) % count).ToArray();

            return gameFieldFactory.Create(size, values);
        }

        #endregion

        #region Play methods

        private static IGame<int> PlayGame(IGame<int> game)
        {
            while (!game.Finished)
            {
                DrawGame(game);
                game = DoShift(game);
            }
            return game;
        }

        private static IGame<int> DoShift(IGame<int> game)
        {
            InputNumber("Input value to shift: ", x =>
            {
                try
                {
                    game = game.Shift(x);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
            return game;
        }

        #endregion

        #region Input methods

        private static int InputDifficulity()
        {
            return InputNumber("Input difficulity: ", x => x > 0);
        }

        private static int InputNumber(string phrase, Predicate<int> isGood)
        {
            Console.WriteLine(phrase);
            while (true)
            {
                int value;
                if (int.TryParse(Console.ReadLine(), out value) && isGood(value))
                {
                    Console.WriteLine();
                    return value;
                }
                Console.Write("Incorrect value. Please, repeat: ");
            }
        }


        #endregion

        #region Draw methods

        private static void DrawGreetings(IGame<int> game)
        {
            Console.WriteLine("YAHOOO!!!");
            DrawGame(game);
        }

        private static void DrawGame(IGame<int> game)
        {
            DrawTurns(game.Turns);
            DrawField(game.CurrentField);
        }

        private static void DrawTurns(int turns)
        {
            Console.WriteLine($"Turns: {turns}");
        }

        private static void DrawField(IGameField<int> field)
        {
            Console.WriteLine(field.ToString().Replace("0", " "));
            Console.WriteLine();
            Console.WriteLine("------------------------------------");
            Console.WriteLine();
        }

        #endregion
    }
}
