using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Runtime;
using System.Windows.Threading;
using System.Threading;

namespace YKCJ_EngineerReport
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (ForceSoftwareRendering)
            {
                RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
            }

            ProfileOptimization.SetProfileRoot(System.Windows.Forms.Application.StartupPath + "\\EngineerReportProfile");
            ProfileOptimization.StartProfile("EngineerReport");
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
