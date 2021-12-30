using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using YKCJ_EngineerReport.F_01.Wds;

using YKCJViewModel.F_01;

namespace YKCJ_EngineerReport.F_01
{
    /// <summary>
    /// F_01_01.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F_01_01 : Page
    {
        private MainWindow Main = Application.Current.MainWindow as MainWindow;

        private F_01_01_ViewModel Viewmodel = new F_01_01_ViewModel();
        private F_01_01_ReportPopup ReportPopup = null;

        public F_01_01()
        {
            InitializeComponent();

            DataContext = Viewmodel;
        }

        private void Checktime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
            _Checktimetextbox.CaretIndex = _Checktimetextbox.Text.Length;
        }

        private void Delaytime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
            _Dleaytimetextbox.CaretIndex = _Dleaytimetextbox.Text.Length;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(_TestRichTextbox.Visibility != Visibility.Visible)
            {
                _TestRichTextbox.Visibility = Visibility.Visible;
                _C1rtb.Height = 310;
            }
            else
            {
                _TestRichTextbox.Visibility = Visibility.Collapsed;
                _C1rtb.Height = 437;
            }
        }

        private void SetReportPopup()
        {
            ReportPopup = new F_01_01_ReportPopup(Viewmodel)
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
                Owner = this.Main
            };

            ReportPopup.ShowDialog();
        }

        private async void ReportOpen_Click(object sender, RoutedEventArgs e)
        {
            await Dispatcher.BeginInvoke(new Action(() => SetReportPopup()));
        }
    }
}
