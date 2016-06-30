using System;
using System.Windows;
using System.Windows.Media;
using Puzzle15.Interfaces;
using Size = System.Drawing.Size;

namespace Puzzle15.GUI
{
    public partial class ClassicGameTable
    {
        private readonly ResourceDictionary resources = Application.Current.Resources;

        public ClassicGameTable()
        {
            InitializeComponent();
        }

        public void Update(IGame<int> game)
        {
            if (game.CurrentField.Size != new Size(4, 4))
                throw new ArgumentException("Game field size should be 4x4");

            foreach (var cellInfo in game)
            {
                var location = cellInfo.Location;
                var number = cellInfo.Value;
                var empty = number == 0;
                var cell = new ClassicGameCell
                {
                    BackgroundColor = (Brush)resources[empty ? "EmptyCell" : "FilledCell"],
                    Number = empty ? "" : number.ToString(),
                    FontSize = 50,
                    Margin = new Thickness(3)
                };
                Table.AddChild(cell, location);
            }
        }
    }
}
