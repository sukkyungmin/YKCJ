using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.Notice
{
    public partial class NoticeControl : UserControl
    {
         bool _isLoaded = false; 
        public enum eNoticeList
        {
            checkBox,
            title,
            //content,
            authorityName,
            startPeriodDate,
            endPeriodDate,
            writerName,
            idx
        }
        public NoticeControl()
        {
            InitializeComponent();
        }

        private void NoticeControl_Load(object sender, EventArgs e)
        {
            InitControls();
            SelectNoticeList(); 
            _isLoaded = true; 
        }

        private void InitControls()
        {
            InitPeriod();
            InitDataGridView(); 
        }

        private void InitPeriod()
        {
            startDateTimeDateTimePicker.Value = DateTime.Now.AddDays(-7); 
            endDateTimeDateTimePicker.Value = DateTime.Now.AddDays(7);
        }

        private void InitDataGridView()
        {
            DataGridView dataGridView = noticeDataGridView;

            string[] columnNames = { "", "제목", "권한", "게시 시작일", "게시 종료일", "작성자", "IDX" };
            DataGridViewCheckBoxColumn checkboxCol = new DataGridViewCheckBoxColumn();
            checkboxCol.Name = "Column0";
            checkboxCol.HeaderText = "";
            dataGridView.Columns.Add(checkboxCol);
            for (int i = 1; i < columnNames.Length; i++)
            {
                dataGridView.Columns.Add("Column" + i.ToString(), columnNames[i]);
            }
            dataGridView.Columns[(int)eNoticeList.idx].Visible = false;



            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

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

            /*
            // Each column style 
            dataGridView.Columns[(int)eChangeManagementDataGridView.D3].Visible = false;
            dataGridView.Columns[(int)eChangeManagementDataGridView.TEST_REQUEST].Visible = false;
            dataGridView.Columns[(int)eChangeManagementDataGridView.ID].Visible = false;

            // Each row style
            for (int i = 0; i < (int)eChangeManagementDataGridView.ID; i++)
            {
                if (i == (int)eChangeManagementDataGridView.TITLE || i == (int)eChangeManagementDataGridView.FOCUSED_CONTROL)
                    changeManagementDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                else
                    changeManagementDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            */

            // Each column style 
            dataGridView.Columns[(int)eNoticeList.checkBox].ReadOnly = false;
            for (int i = (int)eNoticeList.title; i <= (int)eNoticeList.idx; i++)
                dataGridView.Columns[i].ReadOnly = true;

            // Common style
            dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridView.GridColor = Global.GRID_COLOR;
            dataGridView.BackgroundColor = Color.White;         // BackgroundColor 
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // SelectionMode
                                                                                  //dataGridView.ReadOnly = true;
            dataGridView.MultiSelect = false;
            dataGridView.ScrollBars = ScrollBars.Both;
        }
        private void applyPeriodCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            startDateTimeDateTimePicker.Enabled = applyPeriodCheckBox.Checked; 
            endDateTimeDateTimePicker.Enabled = applyPeriodCheckBox.Checked; 
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            SelectNoticeList(); 
        }

        public void GetData()
        {
            if (_isLoaded == true)
                SelectNoticeList();
        }

        private void SelectNoticeList()
        {
            string startDateTime = "NULL";
            string endDateTime = "NULL";
            if (applyPeriodCheckBox.Checked == true)
            {
                startDateTime = startDateTimeDateTimePicker.Value.ToString("yyyy-MM-dd");
                endDateTime = endDateTimeDateTimePicker.Value.ToString("yyyy-MM-dd");
            }

            string query = "";
            if (startDateTime == "NULL")
                query = "EXEC SelectNoticeList NULL, NULL";
            else
                query = string.Format("EXEC SelectNoticeList '{0}', '{1}'", startDateTime, endDateTime);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("공지사항을 가져올 수 없습니다.");
                return;
            }

            noticeDataGridView.Rows.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string title = dataRow["title"].ToString();
                //string content = dataRow["content"].ToString();
                string authorityName = dataRow["authorityName"].ToString();
                string startPeriodDate = dataRow["startPeriodDate"].ToString().Substring(0, 10);
                string endPeriodDate = dataRow["endPeriodDate"].ToString().Substring(0, 10);
                string writerName = dataRow["writerName"].ToString();
                string idx = dataRow["idx"].ToString();

                noticeDataGridView.Rows.Add(false,
                  title,
                  //content,
                  authorityName,
                  startPeriodDate,
                  endPeriodDate,
                  writerName,
                  idx);
                Utils.OddDataGridViewRow(noticeDataGridView);
            }

            noticeDataGridView.ClearSelection();
        }

        private void addNoticeButton_Click(object sender, EventArgs e)
        {
            AddNoticeButton(); 
        }

        public void AddNoticeButton()
        {
            AddNoticeForm form = new AddNoticeForm(this);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void deleteNoticeButton_Click(object sender, EventArgs e)
        {
            string ids = "";
            for (int i = 0; i < noticeDataGridView.Rows.Count; i++)
            {
                if (Convert.ToBoolean(noticeDataGridView.Rows[i].Cells[(int)eNoticeList.checkBox].Value) == true)
                {
                    ids += ("''" + noticeDataGridView.Rows[i].Cells[(int)eNoticeList.idx].Value.ToString() + "'',");
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
                long retVal = DbHelper.ExecuteNonQuery(string.Format("EXEC DeleteNoticeItem '{0}'", ids));

                if (retVal != -1)
                {
                    for (int i = 0; i < noticeDataGridView.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(noticeDataGridView.Rows[i].Cells[(int)eNoticeList.checkBox].Value) == true)
                        {
                            noticeDataGridView.Rows.RemoveAt(i);
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

        private void noticeDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            OpenNoticeForm();
        }

        private void OpenNoticeForm()
        {
            if (noticeDataGridView.SelectedRows.Count > 0)
            {
                string idx = noticeDataGridView.SelectedRows[0].Cells[(int)eNoticeList.idx].Value.ToString();
                AddNoticeForm form = new AddNoticeForm(this);
                form.SetSaveMode("READ");
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


            noticeDataGridView.Width = (Parent.Width - noticeDataGridView.Left) - 40;

            searchPanel.Left = noticeDataGridView.Right - searchPanel.Width;

            deleteNoticeButton.Left = noticeDataGridView.Right - deleteNoticeButton.Width;
            addNoticeButton.Left = deleteNoticeButton.Left - (addNoticeButton.Width + 6);

            //importButton.Left = importInspectionDataGridView.Left;
            //exportButton.Left = importInspectionDataGridView.Right - exportButton.Width;

            // Height, Top
            noticeDataGridView.Height = this.Height - (noticeDataGridView.Top + 30);
        }
    }
}
