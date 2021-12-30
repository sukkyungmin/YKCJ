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
    /// F2_02_Menu.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F2_02_Menu : UserControl
    {

        public delegate void SendF2_02_Menu(string St);
        public event SendF2_02_Menu SendMsg;
        string SelectLabelText = "";


        public F2_02_Menu()
        {
            InitializeComponent();
        }

        private void Label1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectLabelText = "1";
        }

        private void Label2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectLabelText = "2";
        }

        public void ClosePop(bool ClosePopBool)
        {
            if (ClosePopBool)
            {
                this.SendMsg(SelectLabelText);
            }
        }
    }
}
