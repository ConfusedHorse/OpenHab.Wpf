using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OpenHab.Wpf.View.Resources.Converters
{
    public class StateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var values = (value as string)?.Split(',') ?? new[]{""};
            var index = System.Convert.ToInt32(parameter);
            return double.TryParse(values[index], out double num) ? num : DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}