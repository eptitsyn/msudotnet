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
        public bool CanPause
        {
            get { return MediaWindow.CanPause; }
            private set { }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MediaElement_OnMediaEnded(object sender, RoutedEventArgs e)
        {
            //MainViewModel.
        }

        private void PlaylistList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((MainViewModel)DataContext).PlayPauseCommand.Execute(this);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(MediaWindow.CanPause.ToString());
        }

        private void MediaWindow_OnMediaOpened(object sender, RoutedEventArgs e)
        {
            TimelineSlider.Maximum = MediaWindow.NaturalDuration.TimeSpan.TotalMilliseconds;
            var timerVideoTime = new DispatcherTimer();
            timerVideoTime.Interval = TimeSpan.FromSeconds(1);
            timerVideoTime.Tick += new EventHandler(timer_Tick);
            timerVideoTime.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            // Check if the movie finished calculate it's total time
            if (MediaWindow.NaturalDuration.TimeSpan.TotalSeconds > 0)
            {
                //if (TotalTime.TotalSeconds > 0)
                {
                    // Updating time slider
                    TimelineSlider.Value = MediaWindow.Position.TotalMilliseconds;
                }
            }
        }

        private void TimelinesSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void TimelineSlider_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            int SliderValue = (int)TimelineSlider.Value;
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            MediaWindow.Position = ts;
        }
    }
}
