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

using DMSGalaxy.Common.Utils;

namespace DMSGalaxy.Main.F_8._02
{
    /// <summary>
    /// F8_02_Create_User.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F8_02_Create_User : UserControl
    {

        public delegate void SendMsgDele(DataRow Row);
        public event SendMsgDele SendMsg;

        private CommonUtil m_Utils = new CommonUtil();

        private DataRow dataRow = null;

        public F8_02_Create_User()
        {
            InitializeComponent();
        }

        public void SetMenu(DataRow Row)
        {
            try
            {
                dataRow = Row;
                _IgUserImage.Source = m_Utils.byteArrayToBitmaplmage((Byte[])Row["Image"]);
                _TkUserName.Text = Row["이름"].ToString().Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SendMsg(dataRow);
        }

    }
}
