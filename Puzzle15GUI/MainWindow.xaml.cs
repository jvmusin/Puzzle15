using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Puzzle15GUI
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button) sender;
            var animation = new DoubleAnimation
            {
//                From = btn.Width,
                To = Width - 20,
                Duration = TimeSpan.FromSeconds(5)
            };
            btn.BeginAnimation(WidthProperty, animation);
        }
    }
}
