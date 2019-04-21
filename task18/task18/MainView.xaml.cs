using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        /// <summary>
        /// Получаем координаты мыши. Не нашел как в комманду передать MouseEventArgs по этому сделал событием
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Canvas)
            {
                Point coords = e.GetPosition((Canvas)sender);
                ((MainViewModel)DataContext).MouseCoords = coords;
            }
        }
    }
}
