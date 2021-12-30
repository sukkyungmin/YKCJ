using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace YKCJViewModel.Common
{
    public class CommandMultiObj : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new PasswordClear(values[0], values[1], values[2]);
        }

        public object[] ConvertBack(object values, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class PasswordClear
    {
        public PasswordBox F08_password;
        public PasswordBox F08_confirmpassword;
        public Button F08_getuserlist;

        public PasswordClear(object _pas, object _copas, object _listbut)
        {
            F08_password = _pas as PasswordBox;
            F08_confirmpassword = _copas as PasswordBox;
            F08_getuserlist = _listbut as Button;
        }
    }

}
