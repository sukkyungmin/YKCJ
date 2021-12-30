using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.Common
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        private void openpdflabel_Click(object sender, EventArgs e)
        {
            string openpdffile = new System.IO.DirectoryInfo(Application.StartupPath + "/helppdf/2018_J023_YKCJ_YKCJ_QTRS-User Manual(00-제출191101).pdf").ToString();
            System.Diagnostics.Process.Start(openpdffile);
        }

        private void openpdflabel_MouseEnter(object sender, EventArgs e)
        {
            this.openpdflabel.ForeColor = Color.FromArgb(255, 0, 0);
        }

        private void openpdflabel_MouseLeave(object sender, EventArgs e)
        {
            this.openpdflabel.ForeColor = Color.FromArgb(192, 192, 192);
        }
    }
}
