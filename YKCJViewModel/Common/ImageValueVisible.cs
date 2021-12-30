using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace YKCJViewModel.Common
{
    public class ImageValueVisible : IValueConverter
    {
        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)values == ".jpg" || (string)values == ".jpeg" || (string)values == ".gif" || (string)values == ".bmp" || (string)values == ".png") ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
