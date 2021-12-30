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
using System.Windows.Shapes;
using DMSGalaxy.ViewModel.F2;

namespace DMSGalaxy.Main.Cons
{
    /// <summary>
    /// Wd_PopExportReport.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Wd_PopExportReport : Window
    {

        private F2_02_ViewModel F2_ViewModel;

        public Wd_PopExportReport(F2_02_ViewModel ViewModel)
        {
            InitializeComponent();
            F2_ViewModel = ViewModel;
            DataContext = F2_ViewModel;
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

    }
}
