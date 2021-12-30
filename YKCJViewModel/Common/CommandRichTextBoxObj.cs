using System;
using System.Windows;
using System.Windows.Data;

namespace YKCJViewModel.Common
{
    public class CommandRichTextBoxObj : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new RichtextboxModel(values[0], values[1]);
        }

        public object[] ConvertBack(object values, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class RichtextboxModel
    {
        public C1.WPF.RichTextBox.C1RichTextBox C1richtextbox;
        public Window WindwsPopup;

        public RichtextboxModel(object _richtextbox, object _windws)
        {
            C1richtextbox = _richtextbox as C1.WPF.RichTextBox.C1RichTextBox;
            WindwsPopup = _windws as Window;
        }
    }
}

