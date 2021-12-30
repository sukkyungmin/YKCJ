using Common.Infos;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using YKCJViewModel.CommonCons;

namespace YKCJ_EngineerReport.Commons.Cons
{
    /// <summary>
    /// YK_Menu.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class YK_Menu : UserControl
    {

        private readonly MainWindow Main = Application.Current.MainWindow as MainWindow;

        private readonly MenuViewModel Viewmodel = null;

        private Page pg = null;
        private string menuID = "";

        public delegate void SendIntIndex(int index);
        public event SendIntIndex SendIndex;

        public YK_Menu()
        {
            InitializeComponent();

            Viewmodel = new MenuViewModel();
            DataContext = Viewmodel;
        }

        public void SetMenu(string Page, int index)
        {
            menuID = Page;
            Viewmodel.Menuindex = index;
            Viewmodel.Menutoptile = Page;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                //Ping ping = new Ping();
                //var reply = await Task.Run(() => ping.Send("192.168.10.233"));

                //if (!(reply.Status == IPStatus.Success))
                //{
                //    MessageBox.Show("현재 네트워크 또는 SERVER PC를 확인 하십시요.");
                //    return;
                //}


                //if (_LbToptile.Content.ToString() == "자재" || _LbToptile.Content.ToString() == "통합현황" ||
                //     _LbToptile.Content.ToString() == "주간현황" || _LbToptile.Content.ToString() == "월간현황" || _LbToptile.Content.ToString() == "월간보고" || _LbToptile.Content.ToString() == "대시보드" ||
                //     _LbToptile.Content.ToString() == "분석" || _LbToptile.Content.ToString() == "GRS" || _LbToptile.Content.ToString() == "소통")
                //{
                //    MessageBox.Show(Main, "개발 준비중 입니다.");
                //    return;
                //}


                if (UserInfo.US_PAGE == menuID)
                {
                    return;
                }

                SendIndex(Viewmodel.Menuindex);

                UserInfo.US_PAGE = menuID;
                //UserInfo.US_PAGENAME = _LbToptile.Content.ToString();

                /* 1번 로딩화면
                wd_loader = new Wd_Loader();
                wd_loader.Show();
                wd_loader.Topmost = true;
                wd_loader.StartThrd();
                */

                //thrd_tempWorkDone = new Thread(new ThreadStart(WorkAndDone));
                //thrd_tempWorkDone.Start();

                /* 나중에 GetRealWidth 다른걸로 코딩해줘야함
                OwnerPosition = new Point(Main.Left, Main.Top);
                OwnerSize = new Size(Main.GetRealWidth(), Main.GetRealHeight());

                SystemInfo.SPoint = OwnerPosition;
                SystemInfo.SSize = OwnerSize;
                */

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

        private void Navigate(Frame Frm, string menuID)
        {
            try
            {

                pg = (Page)Application.LoadComponent(new Uri(string.Format(@"/YKCJ_EngineerReport;component/{0}/{0}_01.xaml", menuID), UriKind.RelativeOrAbsolute));

                //pg = (Page)Application.LoadComponent(new Uri(@"/YKCJ_EngineerReport;component/" + menuID.Substring(0, 1) + "/" + menuID.Substring(2, 2)
                //                                              + "/F" + menuID.Substring(0, 4) + ".xaml", UriKind.RelativeOrAbsolute));

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

    }
}
