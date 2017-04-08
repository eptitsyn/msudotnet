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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace task18
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

        private void Canvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse figure = new Ellipse();
            figure.Fill = Brushes.BurlyWood;
            figure.Stroke = Brushes.Brown;
            figure.StrokeThickness = 1;
            figure.Width = 100;
            figure.Height = 100;
            figure.MouseDown += figure_OnMouseDown; 
            

            Canvas.Children.Add(figure);
            Canvas.SetTop(figure, e.GetPosition(this).Y-100);
            Canvas.SetLeft(figure, e.GetPosition(this).X);
        }

        private void figure_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ((Ellipse)sender).Fill = Brushes.Red;
            e.Handled = true;
        }
    }
}
