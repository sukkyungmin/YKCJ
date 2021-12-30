using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.Notice
{
    public partial class AddNoticeForm : Form
    {
        private NoticeControl _parent = null; 
        private string _saveMode = "ADD";

        public AddNoticeForm(NoticeControl parent)
        {
            InitializeComponent();
            _parent = parent; 
        }

        private void AddNoticeForm_Load(object sender, EventArgs e)
        {
            InitControls(); 
        }

        private void InitControls()
        {
            startPeriodDateTimePicker.Value = DateTime.Now;
            endPeriodDateTimePicker.Value = DateTime.Now.AddDays(7);
            InitRawDatas();


            if (_saveMode == "READ")
            {
                addDataButton.Visible = false;
                cancelButton.Text = "닫기";
                this.Text = "공지사항";

                // 읽기일 때는 수정 불가능
                startPeriodDateTimePicker.Enabled = false;
                endPeriodDateTimePicker.Enabled = false;
                groupComboBox.Enabled = false;
                titleTextBox.Enabled = false;
                contentTextBox.Enabled = false;

                SetSelectedValues();
            }
            else
            {
                addDataButton.Visible = true;
                // 신규일 때는 수정 가능
                startPeriodDateTimePicker.Enabled = true;
                endPeriodDateTimePicker.Enabled = true;
                groupComboBox.Enabled = true;
                titleTextBox.Enabled = true;
                contentTextBox.Enabled = true;
            }
        }

        private void InitRawDatas()
        {
            //this.Enabled = false;

                string query = "EXEC SelectCodeList 1010"; 

                DataSet dataSet = DbHelper.SelectQuery(query);
                if (dataSet == null || dataSet.Tables.Count == 0)
                {
                    MessageBox.Show("기초 데이터를 가져올 수 없습니다.");
                    ////this.Enabled = true;
                    return;
                }

            groupComboBox.Items.Clear();

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    ////this.Enabled = true;
                    return;
                }

            //groupComboBox.Items.Add(new ComboBoxItem("선택", -1));
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                groupComboBox.Items.Add(new ComboBoxItem(dataRow["codeName"].ToString(), dataRow["codeId"]));
                }

            groupComboBox.SelectedIndex = -1;


            ////this.Enabled = true;
        }

        private void addDataButton_Click(object sender, EventArgs e)
        {
            if (CheckRequiredItems() == false)
                return;

            string title = titleTextBox.Text.Trim();
            string content = contentTextBox.Text.Trim();
            string authorityId = Utils.GetSelectedComboBoxItemValue(groupComboBox);
            string authorityName = Utils.GetSelectedComboBoxItemText(groupComboBox);
            string startPeriodDate = startPeriodDateTimePicker.Value.ToString("yyyy-MM-dd"); 
            string endPeriodDate = endPeriodDateTimePicker.Value.ToString("yyyy-MM-dd");
            string writerId = Global.loginInfo.id;
            string writerName = Global.loginInfo.name;

            if (title == "")
                title = content.Substring(0, content.Length > 9 ? 10 : content.Length);

            string query = "EXEC InsertNoticeItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ";
            query = string.Format(query,
                    title,
                content,
                authorityId,
                authorityName,
                startPeriodDate,
                endPeriodDate,
                writerId,
                writerName
                );

            long retVal = DbHelper.ExecuteScalar(query);
            if (retVal != -1)
            {
                _parent.noticeDataGridView.Rows.Add(false,
                title,
                //content,
                authorityName,
                startPeriodDate,
                endPeriodDate,
                writerName,
                retVal);

                Utils.OddDataGridViewRow(_parent.noticeDataGridView);
                MessageBox.Show("공지사항을 추가했습니다.");
                this.Close();
            }
            else
            {
                MessageBox.Show("공지사항을 추가할 수 없습니다.");
            }

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private bool CheckRequiredItems()
        {
            string title = titleTextBox.Text.Trim();
            string content = contentTextBox.Text.Trim();
            string authorityId = Utils.GetSelectedComboBoxItemValue(groupComboBox);
            string authorityName = Utils.GetSelectedComboBoxItemText(groupComboBox);
            string startPeriodDate = startPeriodDateTimePicker.Value.ToString("yyyy-MM-dd");
            string endPeriodDate = endPeriodDateTimePicker.Value.ToString("yyyy-MM-dd");
            string writerId = Global.loginInfo.id;
            string writerName = Global.loginInfo.name;

            if(startPeriodDate == "" || endPeriodDate == "" || authorityId == "" || content == "" )
            {
                MessageBox.Show("필수 항목을 입력해 주십시오.");
                return false;
            }

            return true; 
        }

        public void SetSaveMode(string saveMode = "ADD")
        {
            _saveMode = saveMode;
        }

        private void SetSelectedValues()
        {
            string idx = _parent.noticeDataGridView.SelectedRows[0].Cells[(int)QTRS.Notice.NoticeControl.eNoticeList.idx].Value.ToString();

            DataSet dataSet = DbHelper.SelectQuery(string.Format("EXEC SelectNoticeItem '{0}'", idx));
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("공지사항을 가져올 수 없습니다.");
                return;
            }

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            DataRow dataRow = dataSet.Tables[0].Rows[0];
            //string startPeriodDate = dataRow["startPeriodDate"].ToString();
            //string endPeriodDate = dataRow["endPeriodDate"].ToString();
            string authorityId = dataRow["authorityId"].ToString();
            string title = dataRow["title"].ToString();
            string content = dataRow["content"].ToString();

            startPeriodDateTimePicker.Value = Utils.GetDateTimeFormatFromObject(dataRow["startPeriodDate"]);
            endPeriodDateTimePicker.Value = Utils.GetDateTimeFormatFromObject(dataRow["endPeriodDate"]);
            Utils.SelectComboBoxItem(groupComboBox, authorityId);
            titleTextBox.Text = title;
            contentTextBox.Text = content;
    }
    }
}
