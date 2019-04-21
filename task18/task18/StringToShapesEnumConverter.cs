using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shapes;

namespace task18
{
    [ValueConversion(typeof(MainViewModel.ShapesEnum), typeof(string))]
    class StringToShapesEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value != null)
            {
                if (((ListViewItem) value).Content.ToString() == "Rectangle") return MainViewModel.ShapesEnum.Rectangle;
                if (((ListViewItem) value).Content.ToString() == "Rounded Rectangle") return MainViewModel.ShapesEnum.RRectangle;
                if (((ListViewItem) value).Content.ToString() == "Circle") return MainViewModel.ShapesEnum.Circle;
                if (((ListViewItem) value).Content.ToString() == "Triangle") return MainViewModel.ShapesEnum.Triangle;
            }
            return MainViewModel.ShapesEnum.Circle;
        }
    }
}
