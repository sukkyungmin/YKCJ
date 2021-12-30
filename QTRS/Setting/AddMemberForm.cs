using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.Setting
{
    public partial class AddMemberForm : Form
    {
        private MemberControl _parent = null;
        string _userId = "";
        private string _saveMode = "ADD"; // ADD, UPDATE

        public AddMemberForm(MemberControl parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void InitControls()
        {
            InitRawDatas();
        }

        private void InitRawDatas()
        {
            //this.Enabled = false;

            //// 
            // 권한, 부서, 잡 기초 데이터 셋팅
            ComboBox[] comboBoxArray = { departmentComboBox, groupComboBox, jobComboBox, machineComboBox, teamComboBox };
            string[] groupIdArray = { "1010", "1020", "1030", "1040", "1050" };

            for (int i = 0; i < comboBoxArray.Length; i++)
            {
                string query = "EXEC SelectCodeList " + groupIdArray[i];

                DataSet dataSet = DbHelper.SelectQuery(query);
                if (dataSet == null || dataSet.Tables.Count == 0)
                {
                    MessageBox.Show("기초 데이터를 가져올 수 없습니다.");
                    ////this.Enabled = true;
                    return;
                }

                comboBoxArray[i].Items.Clear();

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    ////this.Enabled = true;
                    return;
                }

                //comboBoxArray[i].Items.Add(new ComboBoxItem("선택", -1));
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    comboBoxArray[i].Items.Add(new ComboBoxItem(dataRow["codeName"].ToString(), dataRow["codeId"]));
                }

                comboBoxArray[i].SelectedIndex = 0;
            }


            ////this.Enabled = true;



            if (_saveMode == "UPDATE")
            {
                adUserButton.Text = "수정";
                this.Text = "사용자 정보 수정";

                // 수정일 때는 수정 불가능
                idTextBox.Enabled = false;

                SetSelectedValues();
            }
            else
            {
                // 신규일 때는 수정 가능
                idTextBox.Enabled = true;
            }
        }

        private void adUserButton_Click(object sender, EventArgs e)
        {
            if (CheckRequiredItems() == false)
                return;

            if (_saveMode == "ADD")
                AddMember();
            else
                UpdateMember();

        }

        private void AddMember()
        {
            string id = idTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();
            string name = nameTextBox.Text.Trim();
            string authorityId = Utils.GetSelectedComboBoxItemValue(groupComboBox);
            string authorityName = Utils.GetSelectedComboBoxItemText(groupComboBox);
            string departmentId = Utils.GetSelectedComboBoxItemValue(departmentComboBox);
            string departmentName = Utils.GetSelectedComboBoxItemText(departmentComboBox);
            string jobId = Utils.GetSelectedComboBoxItemValue(jobComboBox);
            string jobName = Utils.GetSelectedComboBoxItemText(jobComboBox);
            string machineId = Utils.GetSelectedComboBoxItemValue(machineComboBox);
            string machineName = Utils.GetSelectedComboBoxItemText(machineComboBox);
            string teamId = Utils.GetSelectedComboBoxItemValue(teamComboBox);
            string teamName = Utils.GetSelectedComboBoxItemText(teamComboBox);
            string phoneNumber = phoneNumberTextBox.Text.Trim();
            string cellphoneNumber = cellphoneNumberTextBox.Text.Trim();
            string emailAddress = emailAddressTextBox.Text.Trim();
            string note = noteTextBox.Text.Trim();

            long retVal = DbHelper.ExecuteNonQuery(string.Format("EXEC InsertUserItem '{0}', '{1}', '{2}', {3}, '{4}', {5}, '{6}', {7}, '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}' ",
                  id,
             password,
             name,
             authorityId,
             authorityName,
             departmentId,
             departmentName,
             jobId,
             jobName,
             machineId,
            machineName,
            teamId,
             teamName,
             phoneNumber,
             cellphoneNumber,
             emailAddress,
             note)
                );

            if (retVal != -1)
            {
                _parent.userDataGridView.Rows.Add(false, id, name, authorityName, departmentName, jobName, machineName, teamName, phoneNumber, cellphoneNumber, emailAddress, note);
                Utils.OddDataGridViewRow(_parent.userDataGridView);
                MessageBox.Show("사용자를 추가했습니다.");
                _parent.GetData();
                this.Close();
            }
            else
            {
                MessageBox.Show("사용자를 추가할 수 없습니다.");
            }
        }

        private void UpdateMember()
        {
            string id = idTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();
            string name = nameTextBox.Text.Trim();
            string authorityId = Utils.GetSelectedComboBoxItemValue(groupComboBox);
            string authorityName = Utils.GetSelectedComboBoxItemText(groupComboBox);
            string departmentId = Utils.GetSelectedComboBoxItemValue(departmentComboBox);
            string departmentName = Utils.GetSelectedComboBoxItemText(departmentComboBox);
            string jobId = Utils.GetSelectedComboBoxItemValue(jobComboBox);
            string jobName = Utils.GetSelectedComboBoxItemText(jobComboBox);
            string machineId = Utils.GetSelectedComboBoxItemValue(machineComboBox);
            string machineName = Utils.GetSelectedComboBoxItemText(machineComboBox);
            string teamId = Utils.GetSelectedComboBoxItemValue(teamComboBox);
            string teamName = Utils.GetSelectedComboBoxItemText(teamComboBox);
            string phoneNumber = phoneNumberTextBox.Text.Trim();
            string cellphoneNumber = cellphoneNumberTextBox.Text.Trim();
            string emailAddress = emailAddressTextBox.Text.Trim();
            string note = noteTextBox.Text.Trim();

            long retVal = DbHelper.ExecuteNonQuery(string.Format("EXEC UpdateUserItem '{0}', '{1}', '{2}', {3}, '{4}', {5}, '{6}', {7}, '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}' ",
                  id,
             password,
             name,
             authorityId,
             authorityName,
             departmentId,
             departmentName,
             jobId,
             jobName,
               machineId,
            machineName,
            teamId,
             teamName,
             phoneNumber,
             cellphoneNumber,
             emailAddress,
             note)
                );

            if (retVal != -1)
            {
                int selectedRowIndex = _parent.userDataGridView.SelectedRows[0].Index;

                _parent.userDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.Setting.MemberControl.eUserList.id].Value = id;
                _parent.userDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.Setting.MemberControl.eUserList.name].Value = name;
                _parent.userDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.Setting.MemberControl.eUserList.authorityName].Value = authorityName;
                _parent.userDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.Setting.MemberControl.eUserList.departmentName].Value = departmentName;
                _parent.userDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.Setting.MemberControl.eUserList.jobName].Value = jobName;
                _parent.userDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.Setting.MemberControl.eUserList.machineName].Value = jobName;
                _parent.userDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.Setting.MemberControl.eUserList.teamName].Value = jobName;
                _parent.userDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.Setting.MemberControl.eUserList.phoneNumber].Value = phoneNumber;
                _parent.userDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.Setting.MemberControl.eUserList.cellphoneNumber].Value = cellphoneNumber;
                _parent.userDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.Setting.MemberControl.eUserList.emailAddress].Value = emailAddress;
                _parent.userDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.Setting.MemberControl.eUserList.note].Value = note;

                MessageBox.Show("사용자 정보를 수정했습니다.");
                _parent.GetData();
                this.Close();
            }
            else
            {
                MessageBox.Show("사용자 정보를 수정 할 수 없습니다.");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private bool CheckRequiredItems()
        {
            string id = idTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();
            string passwordRe = passwordReTextBox.Text.Trim();
            string name = nameTextBox.Text.Trim();
            string authorityId = Utils.GetSelectedComboBoxItemValue(groupComboBox);
            string authorityName = Utils.GetSelectedComboBoxItemText(groupComboBox);
            string departmentId = Utils.GetSelectedComboBoxItemValue(departmentComboBox);
            string departmentName = Utils.GetSelectedComboBoxItemText(departmentComboBox);
            string jobId = Utils.GetSelectedComboBoxItemValue(jobComboBox);
            string jobName = Utils.GetSelectedComboBoxItemText(jobComboBox);

            if (id == "")
            {
                MessageBox.Show("아이디를 입력해 주십시오.");
                idTextBox.Focus();
                return false;
            }

            if (password == "")
            {
                MessageBox.Show("비밀번호를 입력해 주십시오.");
                passwordTextBox.Focus();
                return false;
            }

            if (passwordRe == "")
            {
                MessageBox.Show("비밀번호를 한번 더 입력해 주십시오.");
                passwordReTextBox.Focus();
                return false;
            }

            if (password != passwordRe)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.");
                passwordReTextBox.Focus();
                return false;
            }

            if (name == "")
            {
                MessageBox.Show("이름을 입력해 주십시오.");
                nameTextBox.Focus();
                return false;
            }

            if (authorityId == "0")
            {
                MessageBox.Show("권한을 입력해 주십시오.");
                groupComboBox.Focus();
                return false;
            }

            if (departmentId == "0")
            {
                MessageBox.Show("부서를 입력해 주십시오.");
                departmentComboBox.Focus();
                return false;
            }

            if (jobId == "0")
            {
                MessageBox.Show("JOB을 입력해 주십시오.");
                jobComboBox.Focus();
                return false;
            }

            return true;
        }

        private void AcceptOnlyDigit(object sender, KeyPressEventArgs e)
        {
            Utils.AcceptOnlyDigit(sender, e);
        }

        public void SetSaveMode(string saveMode)
        {
            _saveMode = saveMode;
        }

        public void SetUserId(string userId)
        {
            _userId = userId;
        }

        private void SetSelectedValues()
        {
            string query = string.Format("EXEC SelectUserItem '{0}' ", _userId);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("사용자 정보를 가져올 수 없습니다.");
                return;
            }

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            DataRow dataRow = dataSet.Tables[0].Rows[0];

            idTextBox.Text = dataRow["id"].ToString();
            passwordTextBox.Text = dataRow["password"].ToString();
            nameTextBox.Text = dataRow["name"].ToString();
            Utils.SelectComboBoxItem(groupComboBox, dataRow["authorityId"].ToString());
            Utils.SelectComboBoxItem(departmentComboBox, dataRow["departmentId"].ToString());
            Utils.SelectComboBoxItem(jobComboBox, dataRow["jobId"].ToString());
            Utils.SelectComboBoxItem(machineComboBox, dataRow["machineId"].ToString());
            Utils.SelectComboBoxItem(teamComboBox, dataRow["teamId"].ToString());
            phoneNumberTextBox.Text = dataRow["phoneNumber"].ToString();
            cellphoneNumberTextBox.Text = dataRow["cellphoneNumber"].ToString();
            emailAddressTextBox.Text = dataRow["emailAddress"].ToString();
            noteTextBox.Text = dataRow["note"].ToString();
        }
    }
}