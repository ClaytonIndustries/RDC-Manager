using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RDCManager.Converters
{
    public class BooleanToVisiblityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                bool boolean = System.Convert.ToBoolean(value);

                return boolean ? Visibility.Visible : Visibility.Collapsed;
            }
            catch
            {
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}