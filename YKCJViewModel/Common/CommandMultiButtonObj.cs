using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace YKCJViewModel.Common
{
    public class CommandMultiButtonObj : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new ListViewPopup(values[0], values[1]);
        }

        public object[] ConvertBack(object values, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class ListViewPopup
    {
        public Button Button;
        public object Idx;

        public ListViewPopup(object _button, object _obj)
        {
            Button = _button as Button;
            Idx = _obj;
        }
    }
}
