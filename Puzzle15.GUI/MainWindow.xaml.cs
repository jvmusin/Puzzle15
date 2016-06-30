using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Ninject;
using Puzzle15.GUI.Modules;
using Puzzle15.Interfaces;
using Puzzle15.Interfaces.Factories;
using RectangularField.Implementations.Base;
using RectangularField.Interfaces;
using Size = System.Drawing.Size;

namespace Puzzle15.GUI
{
    public partial class MainWindow
    {
        private readonly IFieldFactory<int> gameFieldFactory;
        private readonly IGameFactory<int> gameFactory;

        private int difficulity;
        private IGame<int> game;
        private IGame<int> Game
        {
            get { return game; }
            set
            {
                game = value;

                GameTable.Update(game);
                Difficulity.Value = difficulity.ToString();
                Turns.Value = game.Turns.ToString();

                if (game.Finished)
                    MessageBox.Show(this, "You win!");
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            var kernel = new StandardKernel(new GameBaseModule(), new ClassicGameModule());
            gameFieldFactory = kernel.Get<IFieldFactory<int>>();
            gameFactory = kernel.Get<IGameFactory<int>>();
        }

        private void BackButtonHandle(object sender, MouseButtonEventArgs e)
        {
            var prevGame = Game.PreviousState;
            if (prevGame != null)
                Game = prevGame;
        }

        private void NewGameButtonHandle(object sender, MouseButtonEventArgs e)
        {
            var field = GetTarget(new Size(4, 4));
            var target = field.Clone();
            
            field = field.Shuffle(difficulity = 4);

            Game = gameFactory.Create(field, target);
        }

        private IField<int> GetTarget(Size size)
        {
            var field = gameFieldFactory.Create(size);
            var cellCount = size.Height * size.Width;
            return field.Fill(cellInfo =>
           {
               var location = cellInfo.Location;
               var row = location.Row;
               var column = location.Column;
               return (row * size.Width + column + 1) % cellCount;
           });
        }

        private static readonly Dictionary<Key, CellLocation> Deltas = new Dictionary<Key, CellLocation>
        {
            [Key.Up] = new CellLocation(-1, 0),
            [Key.Right] = new CellLocation(0, 1),
            [Key.Down] = new CellLocation(1, 0),
            [Key.Left] = new CellLocation(0, -1)
        };

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (Game == null || Game.Finished)
                return;

            try
            {
                var numberLocation = Game.GetLocation(0) + Deltas[e.Key].Reverse();
                Game = Game.Shift(numberLocation);
            }
            catch(KeyNotFoundException) { }
            catch (InvalidLocationException)
            {
                MessageBox.Show(this, "Invalid turn");
            }
        }
    }
}
