using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.Login
{
    public partial class LoginForm : Form
    {
        public LoginForm(MainForm parent)
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (Login() == true)
            {
                SetConfigs();
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.No;
                this.Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            Close();
        }

        private bool CheckRequiredItems()
        {
            //string id = Utils.ReplaceSpecialChar(idTextBox.Text.Trim());
            //string password = Utils.ReplaceSpecialChar(passwordTextBox.Text.Trim());
            string id = idTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();

            if (id == "")
            {
                MessageBox.Show("아이디를 입력해 십시오.");
                idTextBox.Focus();
                return false;
            }
            if (password == "")
            {
                MessageBox.Show("비밀번호를 입력해 십시오.");
                passwordTextBox.Focus();
                return false;
            }

            return true;
        }

        private bool Login()
        {
            // 입력가능 문자 검사

            // 필수 항목 체크
            if (false == CheckRequiredItems())
                return false;

            //string id = Utils.ReplaceSpecialChar(idTextBox.Text.Trim());
            //string password = Utils.ReplaceSpecialChar(passwordTextBox.Text.Trim());
            string userId = idTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();
            //string encryptedPassword = Utils.EncryptString(password, Global.ENC_KEY);


            DataSet dataSet = DbHelper.SelectQuery(string.Format("EXEC SelectUserItem '{0}'", userId));
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("사용자 정보를 가져올 수 없습니다.");
                return false;
            }

            if (dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("아이디를 다시 확인해 주십시오.");
                idTextBox.Focus();
                return false;
            }

            string savedPassword = dataSet.Tables[0].Rows[0]["password"].ToString();
            //if (encryptedPassword != savedPassword)
            if (password != savedPassword)
            {
                MessageBox.Show("비밀번호를 다시 확인해 주십시오.");
                passwordTextBox.Focus();
                return false;
            }

            Global.loginInfo = new LoginInfo();
            Global.loginInfo.id = dataSet.Tables[0].Rows[0]["id"].ToString();
            Global.loginInfo.password = dataSet.Tables[0].Rows[0]["password"].ToString();
            Global.loginInfo.name = dataSet.Tables[0].Rows[0]["name"].ToString();
            Global.loginInfo.authorityId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["authorityId"]);
            Global.loginInfo.authorityName = dataSet.Tables[0].Rows[0]["authorityName"].ToString();
            Global.loginInfo.departmentId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["departmentId"]);
            Global.loginInfo.departmentName = dataSet.Tables[0].Rows[0]["departmentName"].ToString();
            Global.loginInfo.jobId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["jobId"]);
            Global.loginInfo.jobName = dataSet.Tables[0].Rows[0]["jobName"].ToString();
            Global.loginInfo.phoneNumber = dataSet.Tables[0].Rows[0]["phoneNumber"].ToString();
            Global.loginInfo.cellphoneNumber = dataSet.Tables[0].Rows[0]["cellphoneNumber"].ToString();
            Global.loginInfo.emailAddress = dataSet.Tables[0].Rows[0]["emailAddress"].ToString();
            Global.loginInfo.loginTime = DateTime.Now;

            return true;
        }


        private void SetConfigs()
        {
            DataSet dataSet = DbHelper.SelectQuery("EXEC SelectConfigs");
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("환경 정보를 가져올 수 없습니다.");
                return;
            }

            if (dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("환경 정보를 가져올 수 없습니다.");
                return;
            }

            Global.configInfo = new Common.Configs(); 
            Global.configInfo.gatherQuantity = dataSet.Tables[0].Rows[0]["gatherQuantity"].ToString();
            Global.configInfo.manufactureQuantity = dataSet.Tables[0].Rows[0]["manufactureQuantity"].ToString();
            Global.configInfo.manufactureAdminId = dataSet.Tables[0].Rows[0]["manufactureAdminId"].ToString();
            Global.configInfo.manufactureAdminName = dataSet.Tables[0].Rows[0]["manufactureAdminName"].ToString();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.AcceptButton = loginButton;
        }
    }
}
