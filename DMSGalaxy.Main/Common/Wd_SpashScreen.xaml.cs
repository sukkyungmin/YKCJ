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
using System.Threading;
using System.Windows.Threading;

namespace DMSGalaxy.Main.Common
{
    /// <summary>
    /// Wd_SpashScreen.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Wd_SpashScreen : Window, ISplashScreen
    {
        public Wd_SpashScreen()
        {
            InitializeComponent();
            //Image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Images/backgrand2.jpg"));
            //Image.Source = new BitmapImage(new Uri(@"pack://application:,,,/DMSGalaxy.Main;component/Images/backgrand2.jpg"));
            //Logotype.Source = new BitmapImage(new Uri(@"pack://application:,,,/DMSGalaxy.Main;component/Images/FrmHome_logo.png"));
            this.board.Completed += OnAnimationCompleted;
        }

        #region ISplashScreen
        public void Progress(double value)
        {
            progressBar.Value = value;
        }
        public void CloseSplashScreen()
        {
            this.board.Begin(this);
        }
        public void SetProgressState(bool isIndeterminate)
        {
            progressBar.IsIndeterminate = isIndeterminate;
        }
        #endregion

        #region Event Handlers
        void OnAnimationCompleted(object sender, EventArgs e)
        {
            this.board.Completed -= OnAnimationCompleted;
            this.Close();
        }
        #endregion

    }
}
