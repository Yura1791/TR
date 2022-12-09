using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TR
{
    public class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateOnly)
            {
                return ((DateOnly)value).ToString();
            }
            if (value is TimeOnly)
            {
                return ((TimeOnly)value).ToString();
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DateOnly.TryParse(value.ToString(), out DateOnly result))
            {
                return result;
            }

            if (TimeOnly.TryParse(value.ToString(), out TimeOnly result2))
            {
                return result2;
            }
            return value;
        }
    }
}
