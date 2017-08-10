using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OpenHab.Wpf.View.Resources.Converters
{
    public class StateToDimmerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strvalue = value as string;
            return double.TryParse(strvalue, out double num) ? num : DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToInt32(value).ToString();
        }
    }
}