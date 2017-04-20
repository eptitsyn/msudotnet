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
            DataContext = new MainViewModel();
        }

        private void Canvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            //Shape figure = (Shape)Activator.CreateInstance(((MainViewModel)DataContext).SelectedShape.GetType());
            Shape figure = ((MainViewModel)DataContext).SelectedShape;
            figure = 

            figure = ((MainViewModel) DataContext).SelectedShape;
            figure.Fill = ((MainViewModel)DataContext).BackgroundBrush;
            figure.Stroke = ((MainViewModel)DataContext).ForegroundBrush;
            figure.StrokeThickness = 1;
            figure.Width = 100;
            figure.Height = 100;
            figure.MouseDown += figure_OnMouseDown; 
            

            Canvas.Children.Add(figure);
            Canvas.SetTop(figure, e.GetPosition(this).Y-100);
            Canvas.SetLeft(figure, e.GetPosition(this).X);
            Shape l = new Line();
            
        }

        private void figure_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ((Ellipse)sender).Fill = Brushes.Red;
            e.Handled = true;
        }

        private void ColorSelector_OnSelected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Asd");
        }

        private void ColorSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Затычка переделать.
            if (fgBrushButton.IsChecked == true)
            {
                ((MainViewModel)DataContext).ForegroundBrush = (Brush)ColorSelector.SelectedItem;
            }
            else
            {
                ((MainViewModel)DataContext).BackgroundBrush = (Brush)ColorSelector.SelectedItem;
            }
            
        }

        private void Shapes_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((MainViewModel) DataContext).SelectedShape = (Shape)Shapes.SelectedItem;
        }
    }
}
