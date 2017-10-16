using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OpenHab.Wpf.View.Resources.Converters
{
    public class ConfigurationToJavaScriptConverter : IValueConverter
    {
        private const string Type = "application/javascript";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            dynamic configuration = value;
            if (value == null) return DependencyProperty.UnsetValue;
            string script = configuration.script;
            if (script == null) return DependencyProperty.UnsetValue;
            return script;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new
            {
                script = value ?? string.Empty,
                type = Type
            };
        }
    }
}