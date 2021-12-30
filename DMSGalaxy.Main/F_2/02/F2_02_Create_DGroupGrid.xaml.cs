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
using DevExpress.Xpf.Grid;

namespace DMSGalaxy.Main.F_2._02
{
    /// <summary>
    /// F2_02_Create_DGroupGrid.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F2_02_Create_DGroupGrid : Page
    {
        public F2_02_Create_DGroupGrid()
        {
            InitializeComponent();            
        }

        private void maingrid_ItemsSourceChanged(object sender, DevExpress.Xpf.Grid.ItemsSourceChangedEventArgs e)
        {
            maingrid.ShowLoadingPanel = false;
        }

        private void maingrid_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            if (e.Column.FieldName == "GROUP" || e.Column.FieldName == "DESCRIPTION")
            {
               e.Result = e.ListSourceRowIndex1.CompareTo(e.ListSourceRowIndex2);
               e.Handled = true;
            }
        }

    }
}
