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
using System.IO;
using System.Threading;
using System.Xml;
using DevExpress.Xpf.Core;
using System.ComponentModel;

using DMSGalaxy.Common.Helper;
using DMSGalaxy.Common.Infos;
using DMSGalaxy.Common.Utils;
using System.Data;
using DMSGalaxy.Main.Cons;
using DMSGalaxy.Main.Common;


namespace DMSGalaxy.Main
{
    /// <summary>
    /// MainWindowHome.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindowHome : Page
    {

        //private Thread thrd_tempWorkDone = null;

        private MainWindow Main = Application.Current.MainWindow as MainWindow;
        private UIHelper m_Helper = new UIHelper();
        private  XmlUtils m_xmlUtils = new XmlUtils();
        private CommonUtil m_Utils = new CommonUtil();
        //Wd_Loader wd_loader = null; // 1번로딩화면 사용안함
        private Con_Tile[] con_tile = null;

        public static Point OwnerPosition;
        public static Size OwnerSize;

        public MainWindowHome()
        {
            /*  1번 로딩화면
            wd_loader = new Wd_Loader();
            wd_loader.Show();
            wd_loader.Topmost = true;
            wd_loader.StartThrd();
            */

            OwnerPosition = new Point(0, 0);
            OwnerSize = new Size(SystemParameters.MaximizedPrimaryScreenWidth, SystemParameters.MaximizedPrimaryScreenHeight);

            SystemInfo.SPoint = OwnerPosition;
            SystemInfo.SSize = OwnerSize;
            //Main.PositionGet();

            DXSplashScreen.Show<Wd_DevLoader>();

            InitializeComponent();
            SetGridImage();
            
            SetUserMenu();

            /*  1번 로딩화면
            wd_loader.bl_done = true;
            */
        }

        
        public void SetGridImage()
        {
            try
            {
                if(Properties.Settings.Default.MainHomeImage.Length > 10)
                {
                    ImageBrush myBrush = new ImageBrush();
                    Image image = new Image();
                    image.Source = StringToImage(Properties.Settings.Default.MainHomeImage);
                    myBrush.ImageSource = image.Source;
                    dd.Background = myBrush;
                }
                else
                {
                    ImageBrush myBrush = new ImageBrush();
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Images/Background0.jpg"));
                    myBrush.ImageSource = image.Source;
                    dd.Background = myBrush;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "MainWindowHome");
            }
        }

        private BitmapImage StringToImage(string value)
        {
            byte[] bytes = Convert.FromBase64String(value);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(bytes);
            bi.EndInit();
            return bi;
        }

        private void SetUserMenu()
        {
            try
            {
                _Im_User.Source = m_Utils.byteArrayToBitmaplmage(UserInfo.US_IMAGES);
                UseName.Content = UserInfo.US_NM;
                UseLevel.Content = SetLevel(UserInfo.US_LEV);
                UseMail.Content = UserInfo.US_MAIL;
                UseNum.Content = UserInfo.US_EPBER;
                UseJob.Content = UserInfo.US_JOB;
                UseWG.Content = UserInfo.US_WG;
                UseLine.Content = m_xmlUtils.XmlUserSelectToString(UserInfo.US_LINE, "/Setting/Line", ",", "", "");
                UsePhone.Content = UserInfo.US_PON;
                UseIP.Content = UserInfo.US_IP;

                Main._tbUserName.Content = "User : " +  UserInfo.US_NM; 

                SetMenu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "MainWindowHome");
            }
            finally
            {

            }
        }

        private string SetLevel(int Level)
        {
            string _Level = "";

            switch (Level)
            {
                case 1:
                    _Level = "Level : General";
                    break;
                case 2:
                    _Level = "Level : Specialist";
                    break;
                case 3:
                    _Level = "Level : Asset Leader";
                    break;
                case 4:
                    _Level = "Level : Engineer";
                    break;
                case 5:
                    _Level = "Level : Master";
                    break;
            }
            return _Level;
        }
        
        private void SetMenu()
        {
            try
            {
                DataTable dt_data = new DataTable();

                dt_data = UserInfo.USERSET.Tables["Level" + UserInfo.US_LEV.ToString()];
                //dt_data = UserInfo.USERSET.Tables["Level3"];
                con_tile = new Con_Tile[dt_data.Rows.Count];

                if (dt_data.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_data.Rows.Count; i++)
                    {
                        con_tile[i] = new Con_Tile();
                        con_tile[i].SetBackImg(dt_data.Rows[i]["Tile"].ToString(), dt_data.Rows[i]["Col"].ToString());
                        Wrap_menu.Children.Add(con_tile[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "MainWindowHome");
            }
            finally
            {

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (DXSplashScreen.IsActive)
            {
                DXSplashScreen.Close();
            }
        }

    }
}
