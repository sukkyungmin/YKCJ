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

using DMSGalaxy.Main;
using DMSGalaxy.Common.Helper;
using DMSGalaxy.Common.Infos;
using DMSGalaxy.Main.Common;

namespace DMSGalaxy.Main.Cons
{
    /// <summary>
    /// Con_Tile.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Con_Tile : UserControl
    {
        private MainWindow Main = Application.Current.MainWindow as MainWindow;
        private MainWindowMenuFirst MainMenuFirst = null;
        private UIHelper m_Helper = new UIHelper();
        string menuID = "";

        //Wd_Loader wd_loader = null;

        public static Point OwnerPosition;
        public static Size OwnerSize;

        public Con_Tile()
        {
            InitializeComponent();           
        }

        public void SetBackImg(string mnId ,string Col)
        {

            MuImage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Images/MU_" + mnId + ".png"));
            lbl_title.Text = mnId;
            var bc = new BrushConverter();
            grid_sub.Background = (Brush)bc.ConvertFrom(Col);
            menuID = mnId;
        }

        private void grid_main_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                /*  1번 로딩화면
                wd_loader = new Wd_Loader();
                wd_loader.Show();
                wd_loader.Topmost = true;
                wd_loader.StartThrd();
                */

                Main.SetMenu(menuID);
                Main.grid_main.ColumnDefinitions[0].Width = new GridLength(200);
                Main.grid_Eye.SetValue(Grid.ColumnProperty, 0);

                OwnerPosition = new Point(Main.Left, Main.Top);
                OwnerSize = new Size(Main.GetRealWidth(), Main.GetRealHeight());

                SystemInfo.SPoint = OwnerPosition;
                SystemInfo.SSize = OwnerSize;

                //DXSplashScreen.Show<Wd_DevLoader>();  로딩2번 충돌로 막아둠
                
                MainMenuFirst = new MainWindowMenuFirst();
                //MainMenuFirst.Loaded += new RoutedEventHandler(c_Loaded); 로딩2번 충돌로 막아둠
                m_Helper.NavigateMain(Main.Frame_Home, MainMenuFirst);

                /* 1번 로딩화면
                wd_loader.bl_done = true;
                */
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Main.Log_Write(ex.ToString(), "Con_Tile");
            }
            finally
            {

            }
        }

        void c_Loaded(object sender,RoutedEventArgs e)
        {
            MainMenuFirst.Loaded -= c_Loaded;
            DXSplashScreen.Close();
        }

    }
}
