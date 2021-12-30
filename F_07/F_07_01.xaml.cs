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

using YKCJViewModel.F_07;
using YKCJ_EngineerReport.F_07.Wds;

namespace YKCJ_EngineerReport.F_07
{
    /// <summary>
    /// F_07_01.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F_07_01 : Page
    {
        private MainWindow Main = Application.Current.MainWindow as MainWindow;
        private F_07_01_SavePopup SavePopup = null;
        private F_07_01_ModifyPopup ModifyPopup = null;

        private F_07_01_ViewModel ViewModel = new F_07_01_ViewModel();
        
        public F_07_01()
        {
            InitializeComponent();

            DataContext = ViewModel;
        }

        private void SavePopup_Click(object sender, RoutedEventArgs e)
        {
            SavePopup = new F_07_01_SavePopup(ViewModel)
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
                Owner = this.Main
            };

            SavePopup.ShowDialog();
        }

        private async void ModifyPopup_Click(object sender, RoutedEventArgs e)
        {
            await Dispatcher.BeginInvoke(new Action(() => SetPopup()));
        }

        private void SetPopup()
        {
            ModifyPopup = new F_07_01_ModifyPopup(ViewModel)
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
                Owner = this.Main
            };

            ModifyPopup.ShowDialog();
        }
    }
}
