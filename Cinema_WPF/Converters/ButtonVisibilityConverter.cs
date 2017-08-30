using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Cinema_WPF.Converters
{
    class ButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool Value = (bool)value;
            bool Parameter = Boolean.Parse((string)parameter);

            if (Value == true)
            {
                if (Parameter == true)
                    return Visibility.Hidden;
                else
                    return Visibility.Visible;
            }
            else
            {
                if (Parameter == true)
                    return Visibility.Visible;
                else
                    return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
