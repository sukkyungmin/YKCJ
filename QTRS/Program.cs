using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());

            MainForm mainForm = new MainForm();
            QTRS.Login.LoginForm loginForm = new QTRS.Login.LoginForm(mainForm);
            loginForm.StartPosition = FormStartPosition.CenterScreen;
            mainForm.StartPosition = FormStartPosition.CenterScreen;
            if (DialogResult.Yes == loginForm.ShowDialog())
            {
                Application.Run(mainForm);
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
