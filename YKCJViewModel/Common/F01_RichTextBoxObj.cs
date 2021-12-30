using System;
using System.Windows;
using System.Windows.Data;

namespace YKCJViewModel.Common
{
    public class F01_RichTextBoxObj : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new F01_RichtextboxModel(values[0]);
        }

        public object[] ConvertBack(object values, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class F01_RichtextboxModel
    {
        public C1.WPF.RichTextBox.C1RichTextBox C1richtextbox;

        public F01_RichtextboxModel(object _richtextbox)
        {
            C1richtextbox = _richtextbox as C1.WPF.RichTextBox.C1RichTextBox;
        }
    }
}
