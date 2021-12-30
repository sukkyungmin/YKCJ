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
using System.ComponentModel;
//using System.Threading;

using DMSGalaxy.Main;
using DMSGalaxy.Common.Helper;
using DMSGalaxy.Main.Common;
using DMSGalaxy.Common.Infos;
using DMSGalaxy.Common.Utils;


namespace DMSGalaxy.Main.Cons
{
    /// <summary>
    /// Con_Menu.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Con_Menu : UserControl
    {
        //private Thread thrd_tempWorkDone = null;
        private MainWindow Main = Application.Current.MainWindow as MainWindow;
        private Page pg = null;
        //Wd_Loader wd_loader = null; // 로딩1 미사용
        private UIHelper m_Helper = new UIHelper();
        private CommonUtil C_util = new CommonUtil();
        string menuID = "";
        int ControlIndex;

        public static Point OwnerPosition;
        public static Size OwnerSize;

        public delegate void SendIntIndex(int index);
        public event SendIntIndex SendIndex;

        public Con_Menu()
        {
            InitializeComponent();
        }

        public void SetMenu(string Toptile,string Bottomtile , string Page, int index)
        {
            _LbToptile.Content = Toptile;
            _LbBottomtile.Content = Bottomtile;
            menuID = Page;
            ControlIndex = index;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                if (!C_util.IPCheck())
                {
                    MessageBox.Show(Main, "현재 네트워크 또는 SERVER PC를 확인 하십시요.");
                    return;
                }


                if (_LbToptile.Content.ToString() == "자재" || _LbToptile.Content.ToString() == "통합현황" ||
                     _LbToptile.Content.ToString() == "주간현황" || _LbToptile.Content.ToString() == "월간현황" || _LbToptile.Content.ToString() == "월간보고" || _LbToptile.Content.ToString() == "대시보드" ||
                     _LbToptile.Content.ToString() == "분석" || _LbToptile.Content.ToString() == "GRS" || _LbToptile.Content.ToString() == "소통" )
                {
                    MessageBox.Show(Main, "개발 준비중 입니다.");
                    return;
                }


                if (UserInfo.US_PAGE == menuID)
                {
                    return;
                }

                SendIndex(ControlIndex);

                UserInfo.US_PAGE = menuID;
                UserInfo.US_PAGENAME = _LbToptile.Content.ToString();

                /* 1번 로딩화면
                wd_loader = new Wd_Loader();
                wd_loader.Show();
                wd_loader.Topmost = true;
                wd_loader.StartThrd();
                */
                 
                //thrd_tempWorkDone = new Thread(new ThreadStart(WorkAndDone));
                //thrd_tempWorkDone.Start();

                OwnerPosition = new Point(Main.Left, Main.Top);
                OwnerSize = new Size(Main.GetRealWidth(), Main.GetRealHeight());

                SystemInfo.SPoint = OwnerPosition;
                SystemInfo.SSize = OwnerSize;

                Navigate(Main.Frame_Home, menuID);
                
                /* DxLoader 로딩화면 사용을 위하여 Dll에서 사용안하고 직접 로직에 작성함
                m_Helper.Navigate(Main.Frame_Home, menuID); 
                */

                /* 1번 로딩화면
                wd_loader.bl_done = true;
                */

                //MainMenuFirst = new MainWindowMenuFirst();
                //MainMenuFirst.Loaded += new RoutedEventHandler(c_Loaded);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Main.Log_Write(ex.ToString(), "Con_Menu");
            }
        }

        //private void WorkAndDone()
        //{
        //    Thread.Sleep(1500);
        //    wd_loader.bl_done = true;
        //}

        void Navigate(Frame Frm, string menuID)
        {
            try
            {
                DXSplashScreen.Show<Wd_DevLoader>();

                pg = (Page)Application.LoadComponent(new Uri(@"/DMSGalaxy.Main;component/F_" + menuID.Substring(0, 1) + "/" + menuID.Substring(2, 2)
                                                              + "/F" + menuID.Substring(0, 4) + ".xaml", UriKind.RelativeOrAbsolute));

                //pg.Loaded += new RoutedEventHandler(c_Loaded);

                pg.Width = Double.NaN;
                pg.Height = Double.NaN;
                pg.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                pg.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                Frm.NavigationService.Navigate(pg);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "Con_Menu");
            }
            finally
            {

            }
        }

        //void c_Loaded(object sender, RoutedEventArgs e)
        //{
        //    pg.Loaded -= c_Loaded;
        //    DXSplashScreen.Close();
        //}

    }
}
