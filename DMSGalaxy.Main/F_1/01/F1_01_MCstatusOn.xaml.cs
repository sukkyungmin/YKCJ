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

using DMSGalaxy.Common.Helper;

namespace DMSGalaxy.Main.F_1._01
{
    /// <summary>
    /// F1_01_MCstatusOn.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F1_01_MCstatusOn : UserControl
    {

        private F1_01_PopMCStatus PopMcStatus = null;

        private MainWindow Main = Application.Current.MainWindow as MainWindow;

        public DataRow dtrow
        {
            get; set;
        }
        
        private DataTable dtble = null;

        int MCIndex;

        private UIHelper m_UIhelper = new UIHelper();

        public F1_01_MCstatusOn(DataTable dt,DataRow rt,int Index)
        {
            InitializeComponent();
            MCIndex = Index;
            dtble = dt;
            dtrow = rt;
            DataContext = dtrow;
            SetMenu();
        }

        public void SetMenu()
        {
            try
            {

                string[] SplitStringName = splitString(dtrow["Name"].ToString(), ' ');
                _TbkPrdNameFirst.Text = SplitStringName[0].Trim();
                _TbkPrdNameList.Text = SplitStringName[1].Trim();
                m_UIhelper.RichBox(_RtbBoxTarget, 26, 26, string.Format("{0:N0}", Convert.ToInt32(dtrow["CBox"].ToString().TrimEnd())),
                                    "/" + string.Format("{0:N0}", Convert.ToInt32(dtrow["TBox"].ToString().TrimEnd())), CurrentBoxColor(dtrow["Brand"].ToString()), "#FF88888B");
                _RgBoxGraph.Width = Convert.ToInt32(dtrow["Box%"].ToString().TrimEnd()) * 3.58;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "F1_01_MCstatusOn");
            }
            finally
            {

            }
        }

        private string CurrentBoxColor(string Brand)
        {
            string BrandColor = "";

            switch (Brand)
            {
                case "화이트":
                    BrandColor = "#51B1EA";
                    break;
                case "좋은느낌":
                    BrandColor = "#F1859E";
                    break;
                case "디펜드":
                    BrandColor = "#377E50";
                    break;
                case "라네이쳐":
                    BrandColor = "#002E05";
                    break;
                case "암웨이":
                    BrandColor = "#DF0800";
                    break;
                case "RSR":
                    BrandColor = "#A1CFC0";
                    break;
                case "없음":
                    BrandColor = "#51B1EA";
                    break;
            }

            return BrandColor;
        }

        private string[] splitString(string Name, char sp)
        {
            string[] split = { " ", " "};

            string[] spstring = Name.Split(sp);

            for (int i = 0; i < spstring.Length; i++)
            {
                if (i < 3)
                {
                    split[0] += " " + spstring[i].ToString();
                }
                else
                {
                    split[1] += " " + spstring[i].ToString();
                }
            }
            return split;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PopMcStatus = new F1_01_PopMCStatus(dtrow, dtble, MCIndex);
            //PopMcStatus.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            PopMcStatus.ShowDialog();
        }
    }
     
}
