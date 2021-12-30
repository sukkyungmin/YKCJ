using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Runtime;
using DevExpress.Xpf.Core;
using DMSGalaxy.Main.Common;
using System.Windows.Threading;
using System.Threading;

namespace DMSGalaxy.Main
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (ForceSoftwareRendering)
            {
                RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
            }

            base.OnStartup(e);
            DXSplashScreen.Show<Wd_SpashScreen>();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ProfileOptimization.SetProfileRoot(System.Windows.Forms.Application.StartupPath + "/DMSGalaxyProfile");
            ProfileOptimization.StartProfile("DMSGalaxy");
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();
        }

        public bool ForceSoftwareRendering
        {
            get
            {
                int renderingTier = (System.Windows.Media.RenderCapability.Tier >> 16);
                return renderingTier == 0;
            }
        }
    }
}
