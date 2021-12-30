using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace YKCJViewModel.Common
{
    public class F_01_SingleValueImageConverter : IValueConverter
    {
        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            //return (int.Parse(string.Format("{0}", parameter)) == 1) ? ((bool)values) ? Visibility.Visible : Visibility.Hidden : (!(bool)values) ? Visibility.Visible : Visibility.Hidden;

            return (int.Parse(string.Format("{0}", parameter)) == 1 && ((string)values == ".jpg" || (string)values == ".jpeg" || (string)values == ".bmp" ||
                    (string)values == ".png")) ? Visibility.Visible : ((string)values == ".xlsx" || (string)values == ".pdf" || (string)values == ".docx" || (string)values == ".docx") ? Visibility.Visible : Visibility.Hidden;

            //return ((string)values == ".xlsx" || (string)values == ".pdf" || (string)values == ".docx" || (string)values == ".pptx") ? Visibility.Visible : Visibility.Hidden;

            //object ob;

            //if (int.Parse(string.Format("{0}", parameter)) == 1 && ((string)values == ".jpg" || (string)values == ".jpeg" || (string)values == ".bmp" || (string)values == ".png"))
            //{
            //    ob = Visibility.Visible;
            //}
            //else
            //{
            //    if ((string)values == ".xlsx" || (string)values == ".pdf" || (string)values == ".docx" || (string)values == ".pptx")
            //    {
            //        ob = Visibility.Visible;
            //    }

            //    ob = Visibility.Hidden;
            //}

            //return ob;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
