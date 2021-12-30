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
using System.Data;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using System.Threading;

namespace DMSGalaxy.Main.F_1._01
{
    /// <summary>
    /// F1_01_MCstatusOff.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F1_01_MCstatusOff : UserControl
    {

        private F1_01_PopMCStatus PopMcStatus = null;

        public DataRow dtrow
        {
            get;
            set;
        }

        private DataTable dtble = null;

        int MCIndex;

        public F1_01_MCstatusOff(DataTable dt, DataRow rt, int Index)
        {
            InitializeComponent();
            MCIndex = Index;
            dtble = dt;
            dtrow = rt;
            DataContext = dtrow;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PopMcStatus = new F1_01_PopMCStatus(dtrow, dtble, MCIndex);
            //PopMcStatus.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            PopMcStatus.ShowDialog();
        }
    }
}
