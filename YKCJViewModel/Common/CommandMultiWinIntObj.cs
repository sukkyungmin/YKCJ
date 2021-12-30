using System;
using System.Windows;
using System.Windows.Data;

namespace YKCJViewModel.Common
{
    public class CommandMultiWinIntObj : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new ObjtoWindowMultiObj(values[0], values[1], values[2]);
        }

        public object[] ConvertBack(object values, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ObjtoWindowMultiObj
    {
        public object obj01;
        public object obj02;
        public Window windowsclose;

        public ObjtoWindowMultiObj(object obj1, object obj2, object obj3)
        {
            obj01 = obj1;
            obj02 = obj2;
            windowsclose = obj3 as Window;
        }
    }
}
