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

using DMSGalaxy.Common.Infos;

namespace DMSGalaxy.Main.Common
{
    /// <summary>
    /// Wd_DevLoader.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Wd_DevLoader : Window, ISplashScreen
    {

        double ownerCenterX, ownerCenterY;

        public Wd_DevLoader()
        {

            double ownerLeft = SystemInfo.SPoint.X;
            double ownerRight = SystemInfo.SPoint.X +SystemInfo.SSize.Width;
            ownerCenterX = (ownerRight - ownerLeft) / 2 + ownerLeft;

            double ownerTop = SystemInfo.SPoint.Y;
            double ownerBottom = SystemInfo.SPoint.Y + SystemInfo.SSize.Height;
            ownerCenterY = (ownerBottom - ownerTop) / 2 + ownerTop;

            SizeChanged += new SizeChangedEventHandler(Wd_DevLoader_SizeChanged);
             
            InitializeComponent();
        }

        void Wd_DevLoader_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Top = ownerCenterY - this.ActualHeight / 2;
            this.Left = ownerCenterX - this.ActualWidth / 2;
        }

        #region ISplashScreen
        public void Progress(double value)
        {
        }
        public void CloseSplashScreen()
        {
            Close();
        }
        public void SetProgressState(bool isIndeterminate)
        {
        }
        #endregion
    }
}
