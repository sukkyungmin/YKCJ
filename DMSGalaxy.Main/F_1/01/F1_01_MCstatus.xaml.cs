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
    /// F1_01_MCstatus.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F1_01_MCstatus : UserControl
    {

        private F1_01_PopMCStatus PopMcStatus = null;

        private DataRow dtrow = null;
        private DataTable dtble = null;

        int MCIndex;

        private UIHelper m_UIhelper = new UIHelper();

        public F1_01_MCstatus(DataTable dt, int Index)
        {
            InitializeComponent();
            MCIndex = Index;
            dtble = dt;
        }

        public void SetMenu(DataRow Row, string Col)
        {
            try
            {
                dtrow = Row;

                string[] SplitStringName = splitString(Row["Name"].ToString(), ' ');

                var bc = new BrushConverter();
                _LbMCNum.Content = "CA#" + Row["MCNum"].ToString().TrimEnd();
                _LbCode.Content = Row["Code"].ToString().TrimEnd();
                _LbName.Content = SplitStringName[0].Trim();
                _LbName2.Content = SplitStringName[1].Trim();
                _RgMCCol.Fill = (Brush)bc.ConvertFrom(Col);
                Rgbar(_RgBoxGraph, Convert.ToInt16(Row["Box%"].ToString().TrimEnd()), _LbBar);
                m_UIhelper.RichBox(_LbBox, 18, 14, string.Format("{0:N0}", Convert.ToInt16(Row["CBox"].ToString().TrimEnd())),
                                                    "/" + string.Format("{0:N0}", Convert.ToInt16(Row["TBox"].ToString().TrimEnd())), "#3953A4", "#D9D9D8");
                //_LbClock.Content = "완료까지 " + Row["Time"].ToString().TrimEnd();
                m_UIhelper.RichBox(_LbClock, 8, 14, "Remain Time : ", Row["Time"].ToString().TrimEnd(), "#D9D9D8", "#333F50");
                Blinking1.Content = Row["Status"].ToString();
                _ImProduct.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Images/Product_" + Row["MCNum"].ToString().TrimEnd() + ".png"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        
        private void Rgbar (Rectangle Rgbar,int Length, Label BarText)
        {

            var bc = new BrushConverter();
            Rgbar.Width = Length * 2.35;
            BarText.Content = Length.ToString() + "%";

            if(Length <= 25)
            {
                Rgbar.Fill = (Brush)bc.ConvertFrom("#95A9F9");
                BarText.Foreground = (Brush)bc.ConvertFrom("#95A9F9");
            }
                else if (Length <= 50)
                {
                    Rgbar.Fill = (Brush)bc.ConvertFrom("#6075CC");
                    BarText.Foreground = (Brush)bc.ConvertFrom("#6075CC");
                }
                else if (Length <= 75)
                {
                    Rgbar.Fill = (Brush)bc.ConvertFrom("#4E60AF");
                    BarText.Foreground = (Brush)bc.ConvertFrom("#4E60AF");
                }
            else
            {
                    Rgbar.Fill = (Brush)bc.ConvertFrom("#223272");
                    BarText.Foreground = (Brush)bc.ConvertFrom("#223272");
            }
        }

       private string[] splitString(string Name ,char sp)
        {
            string[] split = { " " ," "};

            string[] spstring = Name.Split(sp);

           for (int i = 0; i < spstring.Length; i++)
           {
               if(i  < 2)
               {
                   split[0] += " " + spstring[i].ToString();
               }
               else
               {
                   split[1] +=" " +  spstring[i].ToString();
               }
           }
           return split;
        }

       private void grid_main_MouseDown(object sender, MouseButtonEventArgs e)
       {
           PopMcStatus = new F1_01_PopMCStatus(dtrow, dtble, MCIndex);
           PopMcStatus.WindowStartupLocation = WindowStartupLocation.CenterScreen;
           PopMcStatus.ShowDialog();
       }

    }
}
