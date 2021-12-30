using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Data;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Threading;
using System.IO;
using DevExpress.Xpf.Core;
using System.Diagnostics;

using DMSGalaxy.Main.Cons;
using DMSGalaxy.Common.Infos;
using DMSGalaxy.Common.Helper;
using DMSGalaxy.Common.Utils;
using DMSGalaxy.Main.F_8;
using DMSGalaxy.EyeControl;
using DMSGalaxy.DBConnection.CommonProvider;
using DMSGalaxy.ViewModel.Infos;

namespace DMSGalaxy.Main
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {

        private LOG_DELETE m_LOGDELETE = new LOG_DELETE();          //로그 일년단위로 지워주는 클래스
        //private Login dpLogin = null;
        private LOG_Write m_LOG = new LOG_Write();
        private UIHelper m_Helper = new UIHelper();
        private CommonUtil m_Commonutil = new CommonUtil();
        private MainWindowHome MainHome = null;
        private Con_Menu[] Con_Mune = null;
        private Wd_EyesControl wd_Eyes = null;
        private Wd_VersionInformation wd_version = null;
        private VersionInfos versioninfos = null;

        public double orginalWidth, originalHeight;
        public ScaleTransform scale = new ScaleTransform();
        private static Point OwnerPosition;
        private static Size OwnerSize;

        public bool bl_IsHome = false;

        int Con_MenuCount = 0;

        /*
         Stopwatch sw = new Stopwatch();
         */

        public MainWindow()
        {

            /*
             sw.Start();  // 로딩시간 체크
             */

            //Thread thread = new Thread(() =>
            //{
            //    Com w = new Com();
            //    w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //    w.Show();
            //    w.Hide();
            //    w.Closed += (sender2, e2) =>
            //    w.Dispatcher.InvokeShutdown();
            //    System.Windows.Threading.Dispatcher.Run();
            //});

            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Start();

            //this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
            //{
            //    Com w = new Com();
            //    w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //    w.Show();
            //});
            Com w = new Com();
            versioninfos = new VersionInfos();

            ApplicationStartup();

            InitializeComponent();

            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            this.Loaded += new RoutedEventHandler(Window1_Loaded);
            Frame_Home.NavigationService.Navigated += new NavigatedEventHandler(NavigationService_Navigated);
            MainHome = new MainWindowHome();
            m_Helper.NavigateMain(Frame_Home, MainHome);
            m_LOGDELETE.RunLogDelete();
      
        }

        private void ApplicationStartup()
        {
            Wd_Logins _SJLogin = null;

            bool bLoginOK = false;

            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            string str = appSettings["ApplicationMode"].ToString();

            if (str == "REAL")
            {
                SystemInfo.ExcMode = DMSGalaxy.Common.Utils.GlobalVar.ExecuteMODE.REAL;
            }
            else
            {
                SystemInfo.ExcMode = DMSGalaxy.Common.Utils.GlobalVar.ExecuteMODE.DEV;
            }

            try
            {
                _SJLogin = new Wd_Logins();
                _SJLogin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                _SJLogin.ShowDialog();
                bLoginOK = _SJLogin.LoginOK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log_Write(ex.ToString(), "MainWindow");
            }
            finally
            {
                if (null != _SJLogin)
                {
                    _SJLogin = null;
                }
            }
        }

        public void SetMenu(string Menu)
        {
            try
            {
                Wrap_menu.Children.Clear();
                Con_MenuCount = 0;
                DataTable dt_data = new DataTable();

                dt_data = UserInfo.MENUSET.Tables[Menu];
                Con_Mune = new Con_Menu[dt_data.Rows.Count];
                Con_MenuCount = dt_data.Rows.Count;

                if (dt_data.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_data.Rows.Count; i++)
                    {
                        Con_Mune[i] = new Con_Menu();
                        Con_Mune[i].SendIndex += new Con_Menu.SendIntIndex(child_SendMsg);
                        //con_tile[i].SetBackImg(dt_data.Rows[i]["Tile"].ToString(), dt_data.Rows[i]["Col"].ToString());
                        Con_Mune[i].SetMenu(dt_data.Rows[i]["TileTop"].ToString(), dt_data.Rows[i]["TileBottom"].ToString(), dt_data.Rows[i]["Page"].ToString(), i);
                        Wrap_menu.Children.Add(Con_Mune[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log_Write(ex.ToString(), "MainWindow");
            }
            finally
            {

            }
        }

        void child_SendMsg(int Row)
        {
            for (int i = 0; i < Con_MenuCount; i++)
            {
                if (i == Row)
                {
                    Con_Mune[i]._LbHidden.Content = "1";
                }
                else
                {
                    Con_Mune[i]._LbHidden.Content = "0";
                }
            }
        }

        private void NavigationService_Navigated(object sender , NavigationEventArgs e)
        {
            Frame_Home.NavigationService.RemoveBackEntry();
        }

        //홈으로 이동
        public void Set_girdWidth()
        {
            if (bl_IsHome)
            {
                //SetNavBtn("BACK");
                //Navigate(frmHome);
                //wp_rel.Children.Clear();
                //bl_IsHome = false;
            }
            else
            {
                //SetNavBtn("HOME");
                //GoBack();
                //bl_IsHome = true;
            }
        }

        //프레임 뒤로가기
        public void GoBack()
        {
            try
            {
                Frame_Home.NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                SetNavBtn("BACK");
                Console.WriteLine(ex.ToString());
            }
        }

        //상단 오른쪽 뒤로가기/홈 버튼 전환
        public void SetNavBtn(string toDo)
        {
            switch (toDo)
            {
                case "HOME":
                    //ibtn_backHome.Source = new BitmapImage(new Uri(@"pack://application:,,,/JIJI.Main;component/Images/FrmHome_HomeBtn.png"));
                    //lbl_navBtn.Content = "메인화면";
                    break;
                case "BACK":
                    //ibtn_backHome.Source = new BitmapImage(new Uri(@"pack://application:,,,/JIJI.Main;component/Images/FrmHome_BackBtn.png"));
                    //lbl_navBtn.Content = "뒤로가기";
                    break;
            }
        }

        private void wd_main_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                if (new CHK_PGM().Check_PGM_Overlap() == true)
                {
                    Log_Write("프로그램 중복 실행", "MainWindow");
                    MessageBox.Show("프로그램이 이미 실행중 입니다.");
                    Exit();
                    return;
                }

                //con_msg = new CON_MSG(this);

                //lbl_userGrp.Content = UserInfo.GRP_ID;
                //lbl_userName.Content = UserInfo.US_NM;
                //SetFavorite(UserInfo.US_ID);
            }
            catch (Exception ex)
            {
                Log_Write(ex.ToString(), "MainWindow");
            }
        }

        private void wd_main_Closed(object sender, EventArgs e)
        {
            m_LOGDELETE.Dispose();
        }

        private void wd_main_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("종료 하시겠습니까?", "종료 확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
            {
                Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        //종료 함수
        private void Exit()
        {
            try
            {
                String[] args = Environment.GetCommandLineArgs();

                Login _Login = new Login();
                // args[0] is the program name and, args[1] is the first argument.
                // Test for a command-line argument.
                if (args.Length > 1)
                {

                    // Parse the argument. If successful, exit with the parsed code.
                    try
                    {
                        int exitCode = int.Parse(args[1]);

                        Environment.Exit(exitCode);
                    }
                    // If the parse fails, you fall out of the program.
                    catch
                    {

                        /* IP 테스트
                        if (m_Commonutil.IPCheck("121.137.95.29"))
                        {
                            _Login.UserStatusReset(UserInfo.US_ID, false);
                        }
                        else
                        {
                            MessageBox.Show("현재 네트워크 또는 SERVER PC를 확인 하십시요.");
                        }
                        */

                        Environment.Exit(0);
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                        this.Close();
                    }
                }

                /* IP 테스트
                if (m_Commonutil.IPCheck("121.137.95.29"))
                {
                    _Login.UserStatusReset(UserInfo.US_ID, false);     
                }
                else
                {
                    MessageBox.Show("현재 네트워크 또는 SERVER PC를 확인 하십시요.");
                }
                */

                //_Login.UserStatusReset(UserInfo.US_ID, false);     
                Environment.Exit(0);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log_Write(ex.ToString(), "MainWindow");
            }
        }

        #region Form Size Change
        void Window1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChangeSize(e.NewSize.Width, e.NewSize.Height);
        }

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {

            orginalWidth = this.Width;
            originalHeight = this.Height;

            //this.WindowState = System.Windows.WindowState.Maximized;

            if (this.WindowState == WindowState.Maximized)
            {
                ChangeSize(this.ActualWidth, this.ActualHeight);
            }

            this.SizeChanged += new SizeChangedEventHandler(Window1_SizeChanged);

            this.Width = SystemParameters.MaximizedPrimaryScreenWidth - 16;
            this.Height = SystemParameters.MaximizedPrimaryScreenHeight - 16;

            this.Left = 0;
            this.Top = 0;

        }

        private void ChangeSize(double width, double height)
        {
            scale.ScaleX = width / orginalWidth;
            scale.ScaleY = height / originalHeight;

            FrameworkElement rootElement = this.Content as FrameworkElement;

            rootElement.LayoutTransform = scale;
        }
        #endregion

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserInfo.US_PAGE = "";
            grid_Eye.SetValue(Grid.ColumnProperty, 1);
            grid_main.ColumnDefinitions[0].Width = new GridLength(0);
            m_Helper.NavigateMain(Frame_Home, MainHome);
            MainHome.SetGridImage();
            //this.WindowState = System.Windows.WindowState.Normal;
        }

        private void Naver_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //if (!m_Commonutil.IsInternetConnected())
                //{
                //    CommonUtil.MessageAlert("X9001", "");
                //}
                //else
                //{
                //    System.Diagnostics.Process.Start("https://www.naver.com/");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log_Write(ex.ToString(), "MainWindow");
            }
            finally
            {

            }
        }

        private void Daum_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //if (!m_Commonutil.IsInternetConnected())
                //{
                //    CommonUtil.MessageAlert("X9001", "");
                //}
                //else
                //{
                //    System.Diagnostics.Process.Start("https://www.daum.net/");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log_Write(ex.ToString(), "MainWindow");
            }
            finally
            {

            }
        }

        #region Eye Point Control 
        private void grid_main_MouseMove(object sender, MouseEventArgs e)
        {
            Point center = PointToScreen(Mouse.GetPosition(this));

            
            //center.Offset(Width / 1.5, Height / 1.5);

            foreach (DependencyObject dependencyObject in GetChildren<Eye>(this))
                if (dependencyObject is Eye)
                    ((Eye)dependencyObject).FocusPoint = center;

        }

        private static IEnumerable<DependencyObject> GetChildren<T>(DependencyObject dependencyObject)
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
            {
                DependencyObject visual = VisualTreeHelper.GetChild(dependencyObject, i);
                if (visual is T)
                    yield return visual;
                if (VisualTreeHelper.GetChildrenCount(visual) > 0)
                {
                    foreach (DependencyObject child in GetChildren<T>(visual))
                        yield return child;

                }
            }
        }

        private void grid_Eye_MouseDown(object sender, MouseButtonEventArgs e)
        {
            wd_Eyes = new Wd_EyesControl();
            wd_Eyes.Left = this.PointToScreen(new Point(0, 0)).X + Mouse.GetPosition(this).X;
            wd_Eyes.Top = this.PointToScreen(new Point(0, 0)).Y + Mouse.GetPosition(this).Y;
            wd_Eyes.ShowDialog();
        }
        #endregion

        /*
        public void LoaderTimeCheck()
        {
            
             sw.Stop();
            MessageBox.Show("실행까지 : " + (sw.ElapsedMilliseconds + 400).ToString() + "ms " );
                                                        //+ '\r' + '\n' + "초기 실행시 메모리 사용량 : " + m_Commonutil.Memoryusage());
             
        }
         */

        private void _LbLogOut_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("로그아웃 하시겠습니까?", "로그아웃 확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
            {
                Exit();
            }
        }

        void wnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.System && e.SystemKey == Key.F4)
            {
                e.Handled = true;
            }
        }

        private void VersionInfos_MouseDown(object sender, MouseButtonEventArgs e)
        {
            versioninfos.VersionInfosReset();
            wd_version = new Wd_VersionInformation(versioninfos);
            wd_version.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            wd_version.Owner = this;
            wd_version.ShowDialog();
        }

        public void PositionGet()
        {
            if (this.WindowState == WindowState.Maximized & this.Top != 0)
            {
                if (this.Left < - SystemParameters.MaximizedPrimaryScreenWidth/2)
                {
                    OwnerPosition = new Point(-SystemParameters.MaximizedPrimaryScreenWidth, 0);
                        OwnerSize = new Size(this.GetRealWidth(), this.GetRealHeight());

                        SystemInfo.SPoint = OwnerPosition;
                        SystemInfo.SSize = OwnerSize;
                }
                else
                {
                    OwnerPosition = new Point(0, 0);
                    OwnerSize = new Size(this.GetRealWidth(), this.GetRealHeight());

                    SystemInfo.SPoint = OwnerPosition;
                    SystemInfo.SSize = OwnerSize;
                }
            }
            else
            {
                OwnerPosition = new Point(this.Left, this.Top);
                OwnerSize = new Size(this.GetRealWidth(), this.GetRealHeight());

                SystemInfo.SPoint = OwnerPosition;
                SystemInfo.SSize = OwnerSize;
            }

        }

        public void Log_Write(string txt, string filename)
        {
            m_LOG.LOG(txt, filename);
        }

    }
}
