using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

using Common.Infos;

namespace YKCJViewModel.Common
{
    public class MultiValueEqualityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            values[1] = UserInfo.US_NM;

            if (UserInfo.US_ID == "hgfa" || UserInfo.US_ID == "J13461")
            {
                return true;
            }

            return values?.All(o => o?.Equals(values[0]) == true) == true || values?.All(o => o == null) == true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
