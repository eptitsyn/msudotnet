using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace task19
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MediaWindow_OnMediaOpened(object sender, RoutedEventArgs e)
        {
            TimelineSlider.Maximum = MediaWindow.NaturalDuration.TimeSpan.TotalMilliseconds;
            var timerVideoTime = new DispatcherTimer();
            timerVideoTime.Interval = TimeSpan.FromSeconds(1);
            timerVideoTime.Tick += new EventHandler(timer_Tick);
            timerVideoTime.Start();
        }
        /// <summary>
        /// MediaWindow.Position не является Dependency Property по этому не получилось сделать эту часть во ViewModel
        /// </summary>
        void timer_Tick(object sender, EventArgs e)
        {
            // Check if the movie finished calculate it's total time
            if (MediaWindow.NaturalDuration.HasTimeSpan && (MediaWindow.NaturalDuration.TimeSpan.TotalSeconds > 0))
            {
                //if (TotalTime.TotalSeconds > 0)
                {
                    // Updating time slider
                    TimelineSlider.Value = MediaWindow.Position.TotalMilliseconds;
                }
            }
        }

        private void TimelineSlider_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            int SliderValue = (int)TimelineSlider.Value;
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            MediaWindow.Position = ts;
        }
    }
}
