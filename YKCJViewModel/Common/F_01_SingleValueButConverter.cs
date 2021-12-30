using System;
using System.Globalization;
using System.Windows;
using System.Linq;
using System.Windows.Data;
using Common.Infos;

namespace YKCJViewModel.Common 
{
    public class F_01_SingleValueButConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            if ((UserInfo.US_ID == "hgfa" && (int)values[1] > 0) || (UserInfo.US_ID == "J13461" && (int)values[1] > 0))
            {
                return true;
            }

            return ((string)values[0] == UserInfo.US_NM && (int)values[1] > 0) ? true : false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
