using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.Report
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        private void PasswordForm_Load(object sender, EventArgs e)
        {
            this.AcceptButton = confirmButton;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if(passwordTextBox.Text.Trim() == "")
            {
                MessageBox.Show("비밀번호를 입력해 주십시오.");
                passwordTextBox.Focus();
                return; 
            }

            if (Global.loginInfo.password == passwordTextBox.Text.Trim())
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.");
                passwordTextBox.Focus();
                return;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
