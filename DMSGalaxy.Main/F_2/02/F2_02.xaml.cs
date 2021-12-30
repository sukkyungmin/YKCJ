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
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Core;

using DMSGalaxy.Main.Cons;
using DMSGalaxy.ViewModel.F2;
using DMSGalaxy.DBConnection.F2;
using DMSGalaxy.Common.Infos;
using DMSGalaxy.Main.Common;

namespace DMSGalaxy.Main.F_2._02
{
    /// <summary>
    /// F2_02.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F2_02 : Page
    {

        private MainWindow Main = Application.Current.MainWindow as MainWindow;
        /* Pop Menu 1 미사용
        private Wd_PopSlideReport PopSlidReport = null;
        private F2_02_Menu F2_Menu = null;
         */
        private F2_02_Menu3 F2_Menu = null;
        private Wd_PopExportReport Wd_Export = null;
        private F2_02_ViewModel F2_ViewModel = null;
        private Page pg = null;

        public F2_02()
        {
            InitializeComponent();

            F2_ViewModel = new F2_02_ViewModel();
            DataContext = F2_ViewModel;

            Frame_content.NavigationService.Navigated += new NavigatedEventHandler(NavigationService_Navigated);
        }

        /// <summary>
        /// PopSlidReport 종료시 F2_Menu에게 F2_02로 선택한 사항들을 전달하라고 Bool값을 이벤로 알려줌.
        /// 이벤트 관계
        /// PopSlidReport   >>>>    F2_Menu     >>>>    F2_02
        /// </summary>
        private void PopUpScreen(object sender, RoutedEventArgs e)
        {
            /* Pop Menu 1 미사용
            Main.Opacity = 0.7;
            F2_Menu = new F2_02_Menu();
            F2_Menu.DataContext = this.DataContext;
            F2_Menu.SendMsg += new F2_02_Menu.SendF2_02_Menu(Main_SendMsg);
            PopSlidReport = new Wd_PopSlideReport(F2_Menu, 400, 400, 1200);
            PopSlidReport.SendMsg += new Wd_PopSlideReport.SendPopSlideReport(F2_Menu.ClosePop);
            PopSlidReport.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            PopSlidReport.Owner = this.Main;
            PopSlidReport.ShowDialog();
             */

            //OwnerPosition = new Point(Main.Left, Main.Top);
            //OwnerSize = new Size(Main.GetRealWidth(), Main.GetRealHeight());

            //SystemInfo.SPoint = OwnerPosition;
            //SystemInfo.SSize = OwnerSize;

            Main.PositionGet();

            F2_Menu = new F2_02_Menu3(this, F2_ViewModel);
            //F2_Menu.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            F2_Menu.Owner = this.Main;
            F2_Menu.ShowDialog();

        }

        private void PopUpExport(object sender, RoutedEventArgs e)
        {
            Wd_Export = new Wd_PopExportReport(F2_ViewModel);
            Wd_Export.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            Wd_Export.Owner = this.Main;
            Wd_Export.ShowDialog();
        }

        void Main_SendMsg(string St)
        {
            if (St != "")
            {
                //Label1.Content = St;
            }

            Main.Opacity = 1;
        }

        /*  사용안함
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            F2_Production = new F2_02_Create_ProductionGrid();
            F2_Production.DataContext = this.DataContext;
            F2_Production.Width = double.NaN;
            F2_Production.Height = double.NaN;
            F2_Production.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            F2_Production.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            Frame_content.NavigationService.Navigate(F2_Production);
        }
        */

        private void Button_Click(object sender, RoutedEventArgs e)
        {

                switch (F2_ViewModel.ReportMode)
                {
                    case "ProductionDate":
                        Navigate(Frame_content, "2_02", "_Create_ProductionGrid");
                        break;
                    case "ProductionShift":
                        Navigate(Frame_content, "2_02", "_Create_ProductionShiftGrid");
                        break;
                    case "ChangeOver":
                        Navigate(Frame_content, "2_02", "_Create_ChangeOverGrid");
                        break;
                    case "WasteDate":
                        Navigate(Frame_content, "2_02", "_Create_WWasteGrid");
                        break;
                    case "WasteGroup":
                        Navigate(Frame_content, "2_02", "_Create_WGroupGrid");
                        break;
                    case "WasteCode":
                        Navigate(Frame_content, "2_02", "_Create_WCodeGrid");
                        break;
                    case "DelayDate":
                        Navigate(Frame_content, "2_02", "_Create_DDelayGrid");
                        break;
                    case "DelayGroup":
                        Navigate(Frame_content, "2_02", "_Create_DGroupGrid");
                        break;
                    case "DelayCode":
                        Navigate(Frame_content, "2_02", "_Create_DCodeGrid");
                        break;
                }

                F2_ViewModel.OldReportMode = F2_ViewModel.ReportMode;

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
                Main.Log_Write(ex.ToString(), "F2_02");
            }
            finally
            {

            }
        }

        private void c_Loaded(object sender, RoutedEventArgs e)
        {
            pg.Loaded -= c_Loaded;

            if (DXSplashScreen.IsActive)
            {
                DXSplashScreen.Close();
            }
        }


        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            Frame_content.NavigationService.RemoveBackEntry();
        }

        private void F2_02_Main_Loaded(object sender, RoutedEventArgs e)
        {
            if (DXSplashScreen.IsActive)
            {
                DXSplashScreen.Close();
            }
        }

    }
}
