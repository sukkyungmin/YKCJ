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
using DevExpress.Xpf.Core;
using DMSGalaxy.Common.Utils;
using DMSGalaxy.Common.Infos;

using DMSGalaxy.ViewModel.F2;

namespace DMSGalaxy.Main.F_2._01
{
    /// <summary>
    /// F2_01.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F2_01 : Page
    {

        private MainWindow Main = Application.Current.MainWindow as MainWindow;
        private XmlUtils m_xmlUtils = new XmlUtils();
        private F2_01_ViewModel F2_ViewModel = null;

        public F2_01()
        {
            InitializeComponent();

            F2_ViewModel = new F2_01_ViewModel();
            DataContext = F2_ViewModel;
            ComboBoxadd();
        }


        private void F2_01_Main_Loaded(object sender, RoutedEventArgs e)
        {
            if (DXSplashScreen.IsActive)
            {
                DXSplashScreen.Close();
            }
        }

        private void ComboBoxadd()
        {
            try
            {
                _cbMcNumber.ItemsSource = m_xmlUtils.XmlUserSelectToCombox(UserInfo.US_LINE, "/Setting/Line", "CA#");
                if (F2_ViewModel.MCnumber == "미 선택")
                    F2_ViewModel.MCnumber = _cbMcNumber.SelectedValue.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "F2_01");
            }
            finally
            {
                //Login _Login = new Login();
                //_Login.UserStatusReset(UserInfo.US_ID, false);
            }
        }

    }
}
