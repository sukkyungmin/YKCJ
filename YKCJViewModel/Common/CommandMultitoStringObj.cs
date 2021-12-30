using System;
using System.Windows.Data;

namespace YKCJViewModel.Common
{
    public class CommandMultitoStringObj : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new ModelToMultiObj(values[0], values[1], values[2]);
        }

        public object[] ConvertBack(object values, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class ModelToMultiObj
    {
        public object obj01;
        public object obj02;
        public object obj03;

        public ModelToMultiObj(object obj1, object obj2, object obj3)
        {
            obj01 = obj1;
            obj02 = obj2;
            obj03 = obj3;
        }
    }
}
