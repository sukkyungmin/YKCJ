using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Markup;

namespace DMSGalaxy.ViewModel.Common
{
    public class CommandMultiObj : IMultiValueConverter
    {
        /* 보류
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FindCommandParameters parameters = new FindCommandParameters();
            foreach (var obj in values)
            {
                if (obj is string) parameters.Text = (string)obj;
                else if (obj is Frame) parameters.Iwindow = (Frame)obj;
            }
            return parameters;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
         */

        public object Convert(object[] values, Type targetType,object parameter, System.Globalization.CultureInfo culture)
        {
            return new StringandFrame(values[0], values[1]);
        }

        public object [] ConvertBack(object values, Type[] targetTypes,object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    /*보류
    public class FindCommandParameters
    {
        public string Text { get; set; }
        public  Frame Iwindow { get; set; }
    }
    */

    public class StringandFrame
    {
        public string Text;
        public Frame frame;

        //public StringandFrame(string str, Frame Fam)
        //{
        //    Text = str;
        //    frame = Fam;
        //}

        public StringandFrame(object str, object Fam)
        {
            Text = str as string;
            frame = Fam as Frame;
        }
    }

}
