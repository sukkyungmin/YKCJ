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
using DevExpress.Xpf.Core;
using System.Data;

using DMSGalaxy.Common.Helper;
using DMSGalaxy.Common.Infos;
using DMSGalaxy.Main.Common;
using DMSGalaxy.Main.F_2;
using DMSGalaxy.ViewModel.F2;
using DMSGalaxy.Common.Utils;
using DMSGalaxy.DBConnection.F2;

namespace DMSGalaxy.Main.F_2._02
{
    /// <summary>
    /// F2_02_Menu2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F2_02_Menu2 : Window
    {
        private MainWindow Main = Application.Current.MainWindow as MainWindow;
        private Frame CloseframeChange = null;
        private Page pg = null;
        private F2_02_ViewModel F2_ViewModel;
        private F2_02Provider F2_Provider = null;

        private XmlUtils m_xmlUtils = new XmlUtils();

        double ownerCenterX, ownerCenterY;

        public F2_02_Menu2(F2_02 dd, F2_02_ViewModel ViewModel)
        {
            double ownerLeft = SystemInfo.SPoint.X;
            double ownerRight = SystemInfo.SPoint.X + SystemInfo.SSize.Width;
            ownerCenterX = (ownerRight - ownerLeft) / 2 + ownerLeft;

            double ownerTop = SystemInfo.SPoint.Y;
            double ownerBottom = SystemInfo.SPoint.Y + SystemInfo.SSize.Height;
            ownerCenterY = (ownerBottom - ownerTop) / 2 + ownerTop;

            InitializeComponent();
            //this.Activate();
            F2_ViewModel = ViewModel;
            DataContext = F2_ViewModel;
            CloseframeChange = dd.Frame_content;
            ModeButtonTextChange(F2_ViewModel.St);
            F2_Provider = new F2_02Provider();
            ComboBoxadd();
        }

        private void ModeButtonTextChange(string Mode)
        {
            switch(Mode)
            {
                case "Production":
                    _btMode1.Content = "ProductionDate";
                    _btMode2.Content = "ProductionShift";
                    _btMode3.Content = "ChangeOver";
                    break;
                case "Waste":
                    _btMode1.Content = "WasteDate";
                    _btMode2.Content = "WasteGroup";
                    _btMode3.Content = "WasteCode";
                    break;
                case "Delay":
                    _btMode1.Content = "DelayDate";
                    _btMode2.Content = "DelayGroup";
                    _btMode3.Content = "DelayCode";
                    break;
            }
        }

        private void F2_Maun_Closed(object sender, EventArgs e)
        {
            if (F2_ViewModel.GridSearch)
            {
                switch (F2_ViewModel.ReportMode)
                {
                    case "ProductionDate":
                        Navigate(CloseframeChange, "2_02", "_Create_ProductionGrid");
                        break;
                    case "ProductionShift":
                        Navigate(CloseframeChange, "2_02", "_Create_ProductionShiftGrid");
                        break;
                    case "ChangeOver":
                        Navigate(CloseframeChange, "2_02", "_Create_ChangeOverGrid");
                        break;
                    case "WasteDate":
                        Navigate(CloseframeChange, "2_02", "_Create_WWasteGrid");
                        break;
                    case "WasteGroup":
                        Navigate(CloseframeChange, "2_02", "_Create_WGroupGrid");
                        break;
                    case "WasteCode":
                        Navigate(CloseframeChange, "2_02", "_Create_WCodeGrid");
                        break;
                    case "DelayDate":
                        Navigate(CloseframeChange, "2_02", "_Create_DDelayGrid");
                        break;
                    case "DelayGroup":
                        Navigate(CloseframeChange, "2_02", "_Create_DGroupGrid");
                        break;
                    case "DelayCode":
                        Navigate(CloseframeChange, "2_02", "_Create_DCodeGrid");
                        break;
                }

                F2_ViewModel.OldReportMode = F2_ViewModel.ReportMode;
            }
        }

        public void Navigate(Frame Frm, string menuID, string menuitem)
        {
            try
            {
                DXSplashScreen.Show<Wd_DevLoader>();

                pg = (Page)Application.LoadComponent(new Uri(@"/DMSGalaxy.Main;component/F_" + menuID.Substring(0, 1) + "/" + menuID.Substring(2, 2)
                                              + "/F" + menuID.Substring(0, 4) + menuitem + ".xaml", UriKind.RelativeOrAbsolute));

                Main.PositionGet();

                pg.Loaded += new RoutedEventHandler(c_Loaded);

                pg.DataContext = this.DataContext;

                pg.Width = Double.NaN;
                pg.Height = Double.NaN;
                pg.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                pg.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                Frm.NavigationService.Navigate(pg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void c_Loaded(object sender, RoutedEventArgs e)
        {
            pg.Loaded -= c_Loaded;
            DXSplashScreen.Close();
        }

        private void F2_Maun_Loaded(object sender, RoutedEventArgs e)
        {
            //this.Top = 2000;
            this.Top = ownerCenterY - this.ActualHeight / 1;
            this.Left = ownerCenterX - this.ActualWidth / 2;
            //this.Activate();
        }

        private void ComboBoxadd()
        {
            try
            {
                _cbMcNumber.ItemsSource = m_xmlUtils.XmlUserSelectToCombox(UserInfo.US_LINE, "/Setting/Line","CA#");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Login _Login = new Login();
                //_Login.UserStatusReset(UserInfo.US_ID, false);
            }
        }

    }
}
