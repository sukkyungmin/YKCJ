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

namespace DMSGalaxy.Main.F_2._02
{
    /// <summary>
    /// F2_02_Create_ProductionShiftGrid.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F2_02_Create_ProductionShiftGrid : Page
    {
        public F2_02_Create_ProductionShiftGrid()
        {
            InitializeComponent();
        }

        private void maingrid_ItemsSourceChanged(object sender, DevExpress.Xpf.Grid.ItemsSourceChangedEventArgs e)
        {
            maingrid.ShowLoadingPanel = false;
        }
    }
}
