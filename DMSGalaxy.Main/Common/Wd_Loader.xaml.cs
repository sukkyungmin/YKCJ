using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Threading;
using DMSGalaxy.Common.Helper;

namespace DMSGalaxy.Main.Common
{
    /// <summary>
    /// Wd_Loader.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Wd_Loader : Window
    {

        private Thread thrd_check = null;
        public bool bl_done = false;

        public Wd_Loader()
        {
            InitializeComponent();
        }

        public void StartThrd()
        {
            thrd_check = new Thread(new ThreadStart(RunCheckAndClose));
            thrd_check.Start();
        }

        private void RunCheckAndClose()
        {
            while (true)
            {
                if (bl_done)
                {
                    Dispatcher.BeginInvoke(new UIHelper.D_CloseWindow(new UIHelper().CloseWindow), new object[] { this });
                    break;
                }

                Thread.Sleep(300);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            thrd_check.Abort();
        }
    }
    
}
