using System;
using System.Windows.Data;
using System.Windows.Media;

namespace Qupla.IndicatorServer.TrayClient
{
    [ValueConversion(typeof(string), typeof(Brush))]
    public class StringToBrushConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == value)
            {
                return null;
            }
            if (value is string)
            {
                var colorString = (string) value;
                return new BrushConverter().ConvertFromString(colorString) as SolidColorBrush;
            }
            var type = value.GetType();
            throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}