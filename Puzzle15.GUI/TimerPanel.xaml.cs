using System.Windows;

namespace Puzzle15.GUI
{
    public partial class TimerPanel
    {
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register(nameof(Time), typeof(string), typeof(TimerPanel),
                new FrameworkPropertyMetadata("17 : 03"));

        public TimerPanel()
        {
            InitializeComponent();
        }

        public string Time
        {
            get { return (string) GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }
    }
}
