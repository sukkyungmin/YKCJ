using System;
using System.Windows.Data;

namespace DMSGalaxy.ViewModel.Common
{
    public class CommandMultitoStringObj : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new toString(values[0], values[1]);
        }

        public object[] ConvertBack(object values, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class toString
    {
        public string Text1;
        public string Text2;

        public toString(object str1, object str2)
        {
            Text1 = str1 as string;
            Text2 = str2 as string;
        }
    }
}
