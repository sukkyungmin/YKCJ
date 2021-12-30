using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Data;
using System.Windows.Navigation;
using System.Windows.Threading;

using YKCJ_EngineerReport.Commons.Cons;
using YKCJ_EngineerReport.Commons.Wds;
using Common.Infos;
using Common.Helper;
using Common.Utils;
using DBConnection.CommonProvider;

using YKCJViewModel.F_00;

namespace YKCJ_EngineerReport
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
        private F_00_ViewModel Viewmodel;
        private YK_Menu[] Con_Menu = null;
        private MainWindowHome MainHome = null;
        private MainWindowBackground Mainbackground = null;
        private Page pg = null;
        private DataRow dr_data = null;

        private int menuindexnum;


        public double orginalWidth, originalHeight;
        public ScaleTransform scale = new ScaleTransform();

        public bool bl_IsHome = false;
        

        public MainWindow()
        {

            ApplicationStartup();

            InitializeComponent();

            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            this.Loaded += new RoutedEventHandler(Window1_Loaded);
            Frame_Home.NavigationService.Navigated += new NavigatedEventHandler(NavigationService_Navigated);
            MainHome = new MainWindowHome(Viewmodel);
            m_Helper.NavigateMain(Frame_Home, MainHome);
            SetUserMenu();
            m_LOGDELETE.RunLogDelete();

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate {
                this.DateText.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); }, this.Dispatcher);

        }


        private void ApplicationStartup()
        {
            Logins _SJLogin = null;
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            string str = appSettings["ApplicationMode"].ToString();

            if (str == "REAL")
            {
                SystemInfo.ExcMode = Common.Utils.GlobalVar.ExecuteMODE.REAL;
            }
            else
            {
                SystemInfo.ExcMode = Common.Utils.GlobalVar.ExecuteMODE.DEV;
            }

            try
            {
                Viewmodel = new F_00_ViewModel();
                _SJLogin = new Logins();
                _SJLogin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                _SJLogin.ShowDialog();
                bool bLoginOK = _SJLogin.LoginOK;
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

        private void SetUserMenu()
        {
            try
            {
                //UserImage.ImageSource = m_Commonutil.ByteArrayToBitmaplmage(UserInfo.US_IMAGES);

                //UserImage.ImageSource = new ImageBrush(m_Commonutil.ByteArrayToBitmaplmage(UserInfo.US_IMAGES)).ImageSource;

                UserName.Text = UserInfo.US_NM;
                UserJob.Text = UserInfo.US_JOB;
                UserId.Text = UserInfo.US_ID;

                Wrap_menu.Children.Clear();

                dr_data = UserInfo.USERITEM;
                Con_Menu = new YK_Menu[dr_data.Table.Columns.Count];
                //Con_MenuCount = dr_data.Table.Columns.Count - 1;


                if (dr_data.Table.Columns.Count > 0)
                {
                    for (int i = 0; i < dr_data.Table.Columns.Count; i++)
                    {
                        if ((bool)dr_data[i])
                        {
                            Con_Menu[i] = new YK_Menu();
                            Con_Menu[i].SendIndex += new YK_Menu.SendIntIndex(child_SendMsg);
                            Con_Menu[i].SetMenu(dr_data.Table.Columns[i].ColumnName, i);
                            Wrap_menu.Children.Add(Con_Menu[i]);
                        }
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
            for (int i = 0; i < dr_data.Table.Columns.Count; i++)
            {
                if ((bool)dr_data[i])
                {
                    if (i == Row)
                    {
                        Con_Menu[i]._LbHidden.Content = "1";
                        menuindexnum = i;

                    }
                    else
                    {
                        Con_Menu[i]._LbHidden.Content = "0";
                    }
                }
            }
        }

        private void Navigate(string menuID)
        {
            try
            {
                Frame_Home.NavigationService.RemoveBackEntry();
                pg = (Page)Application.LoadComponent(new Uri(@"/YKCJ_EngineerReport;component/" + menuID + ".xaml", UriKind.RelativeOrAbsolute));

                //pg.Loaded += new RoutedEventHandler(c_Loaded);

                pg.Width = Double.NaN;
                pg.Height = Double.NaN;
                pg.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                pg.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                Frame_Home.NavigationService.Navigate(pg);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log_Write(ex.ToString(), "Con_Menu");
            }
            finally
            {

            }
        }


        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            Frame_Home.NavigationService.RemoveBackEntry();
        }


        private void wd_main_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                if (new CheckPing().Check_PGM_Overlap() == true)
                {
                    Log_Write("프로그램 중복 실행", "MainWindow");
                    MessageBox.Show("프로그램이 이미 실행중 입니다.");
                    Exit();
                    return;
                }

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

            //this.Width = SystemParameters.MaximizedPrimaryScreenWidth - 16;
            //this.Height = SystemParameters.MaximizedPrimaryScreenHeight - 16;

            //this.Width = 1600;
            //this.Height = 800;

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

            m_Helper.NavigateMain(Frame_Home, MainHome);

        }




        /*
        public void LoaderTimeCheck()
        {
            
             sw.Stop();
            MessageBox.Show("실행까지 : " + (sw.ElapsedMilliseconds + 400).ToString() + "ms " );
                                                        //+ '\r' + '\n' + "초기 실행시 메모리 사용량 : " + m_Commonutil.Memoryusage());
             
        }
         */

        void wnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.System && e.SystemKey == Key.F4)
            {
                e.Handled = true;
            }
        }

        //private void VersionInfos_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    versioninfos.VersionInfosReset();
        //    wd_version = new Wd_VersionInformation(versioninfos);
        //    wd_version.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
        //    wd_version.Owner = this;
        //    wd_version.ShowDialog();
        //}

        //public void PositionGet()
        //{
        //    if (this.WindowState == WindowState.Maximized & this.Top != 0)
        //    {
        //        if (this.Left < -SystemParameters.MaximizedPrimaryScreenWidth / 2)
        //        {
        //            OwnerPosition = new Point(-SystemParameters.MaximizedPrimaryScreenWidth, 0);
        //            OwnerSize = new Size(this.GetRealWidth(), this.GetRealHeight());

        //            SystemInfo.SPoint = OwnerPosition;
        //            SystemInfo.SSize = OwnerSize;
        //        }
        //        else
        //        {
        //            OwnerPosition = new Point(0, 0);
        //            OwnerSize = new Size(this.GetRealWidth(), this.GetRealHeight());

        //            SystemInfo.SPoint = OwnerPosition;
        //            SystemInfo.SSize = OwnerSize;
        //        }
        //    }
        //    else
        //    {
        //        OwnerPosition = new Point(this.Left, this.Top);
        //        OwnerSize = new Size(this.GetRealWidth(), this.GetRealHeight());

        //        SystemInfo.SPoint = OwnerPosition;
        //        SystemInfo.SSize = OwnerSize;
        //    }

        //}

        public void Log_Write(string txt, string filename)
        {
            m_LOG.LOG(txt, filename);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("종료 하시겠습니까?", "종료 확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
            {
                Exit();
            }
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            _subtopgrid.RowDefinitions[0].Height = new GridLength(0);
            //MainHome._Homeviewer.Visibility = Visibility.Hidden;
            UserInfo.US_PAGE = "MenuBack";
            Mainbackground = new MainWindowBackground();
            m_Helper.NavigateMain(Frame_Home, Mainbackground);
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            _subtopgrid.RowDefinitions[0].Height = new GridLength(40);
            UserInfo.US_PAGE = "Home";
            //Navigate("MainWindowHome");
            //MainHome = new MainWindowHome(Viewmodel);
            m_Helper.NavigateMain(Frame_Home, MainHome);
            Con_Menu[menuindexnum]._LbHidden.Content = "0";
        }

        private void grid_main_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //this.DragMove();
        }

        //private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        //{
        //    this.WindowState = (this.WindowState != WindowState.Maximized) ? WindowState.Maximized : WindowState.Normal;
        //}

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = (this.WindowState != WindowState.Minimized) ? WindowState.Minimized : WindowState.Normal;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

    }
}
