using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.Setting
{
    public partial class MemberControl : UserControl
    {
        public enum eUserList {  checkBox, id, name, authorityName, departmentName, jobName, machineName, teamName, phoneNumber, cellphoneNumber, emailAddress, note };
        public MemberControl()
        {
            InitializeComponent();
        }

        private void MemberControl_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void InitControls()
        {

            InitDataGrid();
        }

        private void InitDataGrid()
        {
            DataGridView dataGridView = userDataGridView;

            // Common style
            dataGridView.ReadOnly = false; 
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridView.GridColor = Global.GRID_COLOR;
            dataGridView.BackgroundColor = Color.White;         // BackgroundColor 
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // SelectionMode
            dataGridView.ScrollBars = ScrollBars.Both;


            // Default column style
            dataGridView.ColumnHeadersVisible = true;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Dotum", 11, FontStyle.Regular);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Global.GRID_COLUMN_FORE_COLOR;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Global.GRID_COLUMN_BACK_COLOR;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersHeight = Global.GRID_COLUMN_HEIGHT;
            dataGridView.AllowUserToResizeColumns = true;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            //dataGridView.AdvancedColumnHeadersBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;


            // Default row header style
            dataGridView.RowHeadersVisible = false;

            // Default row style
            dataGridView.RowsDefaultCellStyle.Font = new Font("Dotum", 11, FontStyle.Regular);
            dataGridView.RowsDefaultCellStyle.ForeColor = Global.GRID_ROW_FORE_COLOR;
            dataGridView.RowsDefaultCellStyle.BackColor = Global.GRID_ROW_BACK_COLOR;
            dataGridView.RowTemplate.Height = Global.GRID_ROW_HEIGHT;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            //changeManagementDataGridView.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            //changeManagementDataGridView.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None; 

            // Each column style 
            dataGridView.Columns[(int)eUserList.checkBox].ReadOnly = false; 
            for (int i = (int)eUserList.id; i <= (int)eUserList.note; i++)
                dataGridView.Columns[i].ReadOnly = true;


            /*
            // Each row style
            for (int i = 0; i < (int)eChangeManagementDataGridView.ID; i++)
            {
                if (i == (int)eChangeManagementDataGridView.TITLE || i == (int)eChangeManagementDataGridView.FOCUSED_CONTROL)
                    changeManagementDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                else
                    changeManagementDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            */
            dataGridView.MultiSelect = false;
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            AddMemberForm form = new AddMemberForm(this);
            form.SetSaveMode("ADD");
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        public void GetData()
        {
            SelectUserList();
        }

        private void SelectUserList()
        {
            DataSet dataSet = DbHelper.SelectQuery("EXEC SelectAllUserList");
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("사용자 정보를 가져올 수 없습니다.");
                return;
            }

            userDataGridView.Rows.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string id = dataRow["id"].ToString();
                //string password = dataRow["password"].ToString();
                string name = dataRow["name"].ToString();
                //string authorityId = dataRow["authorityId"].ToString();
                string authorityName = dataRow["authorityName"].ToString();
                //string departmentId = dataRow["departmentId"].ToString();
                string departmentName = dataRow["departmentName"].ToString();
                //string jobId = dataRow["jobId"].ToString();
                string jobName = dataRow["jobName"].ToString();
                string machineName = dataRow["machineName"].ToString();
                string teamName = dataRow["teamName"].ToString();
                string phoneNumber = dataRow["phoneNumber"].ToString();
                string cellphoneNumber = dataRow["cellphoneNumber"].ToString();
                string emailAddress = dataRow["emailAddress"].ToString();
                string note = dataRow["note"].ToString();

                userDataGridView.Rows.Add(false, id, name, authorityName, departmentName, jobName, machineName, teamName, phoneNumber, cellphoneNumber, emailAddress, note);
                Utils.OddDataGridViewRow(userDataGridView);
            }

            userDataGridView.ClearSelection();
        }

        private void deleteUserButton_Click(object sender, EventArgs e)
        {
            string ids = "";
            for (int i = 0; i < userDataGridView.Rows.Count; i++)
            {
                if (Convert.ToBoolean(userDataGridView.Rows[i].Cells[(int)eUserList.checkBox].Value) == true)
                {
                    ids += ("''" + userDataGridView.Rows[i].Cells[(int)eUserList.id].Value.ToString() + "'',");
                }
            }
            if (ids != "")
                ids = ids.Substring(0, ids.Length - 1);

            if (ids == "")
            {
                MessageBox.Show("삭제할 항목을 선택해 주십시오.");
                return;
            }

            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "항목 삭제", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                long retVal = DbHelper.ExecuteNonQuery(string.Format("EXEC DeleteUserItem '{0}'", ids));

                if (retVal != -1)
                {
                    for (int i = 0; i < userDataGridView.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(userDataGridView.Rows[i].Cells[(int)eUserList.checkBox].Value) == true)
                        {
                            userDataGridView.Rows.RemoveAt(i);
                            i--;
                        }
                    }

                    MessageBox.Show("선택한 항목을 삭제했습니다.");
                }
                else
                {
                    MessageBox.Show("선택한 항목을 삭제 할 수 없습니다.");
                }
            }
        }

        private void userDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            OpenProductTestForm();
        }

        private void OpenProductTestForm()
        {
            if (userDataGridView.SelectedRows.Count > 0)
            {
                string userId = userDataGridView.SelectedRows[0].Cells[(int)eUserList.id].Value.ToString();
                AddMemberForm form = new AddMemberForm(this);
                form.SetSaveMode("UPDATE");
                form.SetUserId(userId);
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();
            }
        }

        public void ResizeControls()
        {
            if (Parent == null)
                return;

            this.Left = 0;
            this.Top = 0;
            this.Width = Parent.Width;
            this.Height = Parent.Height;


            userDataGridView.Width = (Parent.Width - userDataGridView.Left) - 40;

            //searchPanel.Left = componentDataGridView.Right - searchPanel.Width;

            deleteUserButton.Left = userDataGridView.Right - deleteUserButton.Width;
            addUserButton.Left = deleteUserButton.Left - (addUserButton.Width + 6);

            // Height, Top
            userDataGridView.Height = this.Height - (userDataGridView.Top + 30);

        }
    }
}
