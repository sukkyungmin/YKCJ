using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace YKCJViewModel.Common
{
    public class SingleValueNegativeConverter : IValueConverter
    {
        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            return (int.Parse(string.Format("{0}", parameter)) == 1) ? ((bool)values) ? Visibility.Visible : Visibility.Hidden : (!(bool)values) ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
