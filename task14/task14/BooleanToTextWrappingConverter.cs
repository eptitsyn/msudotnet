using System;
using System.Windows;
using System.Windows.Data;

namespace task14
{
    [ValueConversion(typeof(bool), typeof(TextWrapping))]
    class BooleanToTextWrappingConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Boolean && (bool)value)
            {
                return TextWrapping.Wrap;
            }
            return TextWrapping.NoWrap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is TextWrapping && (TextWrapping)value == TextWrapping.Wrap)
            {
                return true;
            }
            return false;
        }
    }
}
