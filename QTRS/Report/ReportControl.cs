using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace QTRS.Report
{
    public partial class ReportControl : UserControl
    {
        enum eReportList { checkBox, reportType, productName, componentName, manufactureSerialNumber, manufactureDate, gatherPlace, deliveryCompanyName, approvalDate, testIdx, idx };
        private bool isLoaded = false;

        public ReportControl()
        {
            InitializeComponent();
        }

        private void ReportControl_Load(object sender, EventArgs e)
        {
            InitControls(); 
        }

        private void InitControls()
        {
            startDateTimeDateTimePicker.Value = DateTime.Now;
            endDateTimeDateTimePicker.Value = DateTime.Now;

            reportTypeComboBox.Items.Clear();
            reportTypeComboBox.Items.Add(new ComboBoxItem("전체", -1));
            reportTypeComboBox.Items.Add(new ComboBoxItem("원료약품 시험성적서", (int)Global.eReportType.componentDrugTest));
            reportTypeComboBox.Items.Add(new ComboBoxItem("품질관리 기록서", (int)Global.eReportType.qualityManagement));
            reportTypeComboBox.Items.Add(new ComboBoxItem("제조관리 기록서", (int)Global.eReportType.manufactureManagement));
            //reportTypeComboBox.Items.Add(new ComboBoxItem("최종포장(완제품) 품질관리 기록서", (int)Global.eReportType.finalQualityManagement));
            reportTypeComboBox.SelectedIndex = 0;

            InitDataGrid();

            isLoaded = true;
            GetData();
        }

        private void InitDataGrid()
        {
            DataGridView dataGridView = reportDataGridView;

            //string[] columnNames = { "", "리포트 이름", "리포트 타입", "제품명(원료명)", "제조번호", "제조일", "채취장소", "승인일", "TESTIDX", "IDX" };
            string[] columnNames = { "", "리포트 타입", "제품명/원료명(식약처허가명)", "원료명(SAP명)", "제조번호", "제조일", "채취장소", "납품처명", "승인일", "TESTIDX", "IDX" };
            DataGridViewCheckBoxColumn checkboxCol = new DataGridViewCheckBoxColumn();
            checkboxCol.Name = "Column0";
            checkboxCol.HeaderText = "";
            dataGridView.Columns.Add(checkboxCol);
            for (int i = 1; i < columnNames.Length; i++)
            {
                dataGridView.Columns.Add("Column" + i.ToString(), columnNames[i]);
            }
            dataGridView.Columns[(int)eReportList.testIdx].Visible = false;
            dataGridView.Columns[(int)eReportList.idx].Visible = false;



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
            dataGridView.Columns[(int)eReportList.checkBox].ReadOnly = false;
            for (int i = (int)eReportList.reportType; i <= (int)eReportList.approvalDate; i++)
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

        public void GetData()
        {
            if (isLoaded == true)
                SelectReportList();
        }

        private void applyPeriodCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            startDateTimeDateTimePicker.Enabled = applyPeriodCheckBox.Checked;
            endDateTimeDateTimePicker.Enabled = applyPeriodCheckBox.Checked;
        }

        private void periodRadioButton_CheckedChanged(object sender, EventArgs e)
        {

            if ((sender as RadioButton).Checked == false)
                return;

            endDateTimeDateTimePicker.ValueChanged -= endDateTimeDateTimePicker_ValueChanged;
            startDateTimeDateTimePicker.ValueChanged -= startDateTimeDateTimePicker_ValueChanged;

            endDateTimeDateTimePicker.Value = DateTime.Now;

            if (day1RadioButton.Checked == true)
                startDateTimeDateTimePicker.Value = endDateTimeDateTimePicker.Value;
            else if (day3RadioButton.Checked == true)
                startDateTimeDateTimePicker.Value = endDateTimeDateTimePicker.Value.AddDays(-3);
            else if (day7RadioButton.Checked == true)
                startDateTimeDateTimePicker.Value = endDateTimeDateTimePicker.Value.AddDays(-7);
            else if (day15RadioButton.Checked == true)
                startDateTimeDateTimePicker.Value = endDateTimeDateTimePicker.Value.AddDays(-15);
            else if (month1RadioButton.Checked == true)
                startDateTimeDateTimePicker.Value = endDateTimeDateTimePicker.Value.AddMonths(-1);
            else if (month3RadioButton.Checked == true)
                startDateTimeDateTimePicker.Value = endDateTimeDateTimePicker.Value.AddMonths(-3);
            else if (month6RadioButton.Checked == true)
                startDateTimeDateTimePicker.Value = endDateTimeDateTimePicker.Value.AddMonths(-6);
            else if (year1RadioButton.Checked == true)
                startDateTimeDateTimePicker.Value = endDateTimeDateTimePicker.Value.AddYears(-1);

            endDateTimeDateTimePicker.ValueChanged += endDateTimeDateTimePicker_ValueChanged;
            startDateTimeDateTimePicker.ValueChanged += startDateTimeDateTimePicker_ValueChanged;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            SelectReportList();
        }

        private void SelectReportList()
        {
            ////this.Enabled = false;

            string startDateTime = "NULL";
            string endDateTime = "NULL";
            string reportType = "";

            reportType = Utils.GetSelectedComboBoxItemValue(reportTypeComboBox);

            if (applyPeriodCheckBox.Checked == true)
            {
                startDateTime = startDateTimeDateTimePicker.Value.ToString("yyyy-MM-dd 00:00:00");
                endDateTime = endDateTimeDateTimePicker.Value.ToString("yyyy-MM-dd 23:59:59");
            }

            string query = "";
            if (startDateTime == "NULL")
                query = string.Format("EXEC SelectReportList NULL, NULL, '{0}'", reportType);
            else
                query = string.Format("EXEC SelectReportList '{0}', '{1}', '{2}'", startDateTime, endDateTime, reportType);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("리포트 데이터를 가져올 수 없습니다.");
                return;
            }

            reportDataGridView.Rows.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                

                string idx = dataRow["idx"].ToString();
                //string reportName = dataRow["reportName"].ToString();
                reportType = dataRow["reportType"].ToString();
                string productName = dataRow["productName"].ToString();
                string componentName = dataRow["componentName"].ToString();
                string manufactureSerialNumber = dataRow["manufactureSerialNumber"].ToString();
                string manufactureDate = dataRow["manufactureDate"].ToString();
                string gatherPlace = dataRow["gatherPlace"].ToString();
                string deliveryCompanyName = dataRow["deliveryCompanyName"].ToString();
                string testIdx = dataRow["testIdx"].ToString();
                string approvalDate = dataRow["approvalDate"].ToString().Substring(0, 16);


                if (Int32.Parse(reportType) == (int)Global.eReportType.componentDrugTest)
                    reportType = "원료약품 시험성적서";
                else if (Int32.Parse(reportType) == (int)Global.eReportType.qualityManagement)
                    reportType = "품질관리 기록서";
                else if (Int32.Parse(reportType) == (int)Global.eReportType.manufactureManagement)
                    reportType = "제조관리 기록서";
                else if (Int32.Parse(reportType) == (int)Global.eReportType.finalQualityManagement)
                    reportType = "최종포장(완제품) 품질관리 기록서";

                reportDataGridView.Rows.Add(false, reportType, productName, componentName, manufactureSerialNumber, manufactureDate, gatherPlace, deliveryCompanyName, approvalDate, testIdx, idx);
       
                Utils.OddDataGridViewRow(reportDataGridView);
            }

            reportDataGridView.ClearSelection();

            ////this.Enabled = true; 
        }

        private void startDateTimeDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            UncheckedAllPeriodRadioButtons();
        }

        private void endDateTimeDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            UncheckedAllPeriodRadioButtons();
        }

        private void UncheckedAllPeriodRadioButtons()
        {
            day1RadioButton.Checked = false;
            day3RadioButton.Checked = false;
            day7RadioButton.Checked = false;
            day15RadioButton.Checked = false;
            month1RadioButton.Checked = false;
            month3RadioButton.Checked = false;
            month6RadioButton.Checked = false;
            year1RadioButton.Checked = false;
        }

        private void downloadReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                bool isChecked = false; 
                for (int i = 0; i < reportDataGridView.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(reportDataGridView.Rows[i].Cells[(int)eReportList.checkBox].Value) == true)
                    {
                        isChecked = true;
                        break; 
                    }
                }
                if(isChecked == false)
                {
                    MessageBox.Show("다운로드 할 항목을 선택해 주십시오.");
                    return;
                }

                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (!string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    if (Directory.Exists(folderBrowserDialog.SelectedPath) == false)
                    {
                        MessageBox.Show("잘못된 경로입니다. 다시 확인해 주십시오.");
                        return;
                    }
                }

                string idx = "";
                for (int i = 0; i < reportDataGridView.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(reportDataGridView.Rows[i].Cells[(int)eReportList.checkBox].Value) == true)
                    {
                        idx = reportDataGridView.Rows[i].Cells[(int)eReportList.idx].Value.ToString();

                        DataSet dataSet = DbHelper.SelectQuery(string.Format("EXEC SelectReportItem '{0}'", idx));
                        if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("파일 정보를 가져올 수 없습니다.");
                            return;
                        }

                        string fileName = ""; 
                        string reportType = reportDataGridView.Rows[i].Cells[(int)eReportList.reportType].Value.ToString();
                        string productName = reportDataGridView.Rows[i].Cells[(int)eReportList.productName].Value.ToString();
                        string componentName = reportDataGridView.Rows[i].Cells[(int)eReportList.componentName].Value.ToString();
                        string manufactureSerialNumber = reportDataGridView.Rows[i].Cells[(int)eReportList.manufactureSerialNumber].Value.ToString();
                        string manufactureDate = reportDataGridView.Rows[i].Cells[(int)eReportList.manufactureDate].Value.ToString();
                        string gatherPlace = reportDataGridView.Rows[i].Cells[(int)eReportList.gatherPlace].Value.ToString();
                        string deliveryCompanyName = reportDataGridView.Rows[i].Cells[(int)eReportList.deliveryCompanyName].Value.ToString();
                        string approvalDate = reportDataGridView.Rows[i].Cells[(int)eReportList.approvalDate].Value.ToString().Substring(0, 16);

                        fileName = reportType;
                        if (productName != "") fileName += ("_" + productName);
                        if (componentName != "") fileName += ("_" + componentName);
                        if (manufactureSerialNumber != "") fileName += ("_" + manufactureSerialNumber);
                        if (manufactureDate != "") fileName += ("_" + manufactureDate);
                        if (gatherPlace != "") fileName += ("_" + gatherPlace);
                        if (deliveryCompanyName != "") fileName += ("_" + deliveryCompanyName);
                        fileName += ("_" + approvalDate + ".pdf");

                        fileName = fileName.Replace(":", "-");

                        string uniqueFileName = Utils.GetUniquFileNameByIndex(folderBrowserDialog.SelectedPath, fileName);
                        File.WriteAllBytes(uniqueFileName, (Byte[])dataSet.Tables[0].Rows[0]["fileData"]);
                    }
                }

                MessageBox.Show("다운로드를 완료했습니다.");
            }
            catch(Exception)
            {
                MessageBox.Show("다운로드 중 오류가 발생했습니다.");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void deleteReportButton_Click(object sender, EventArgs e)
        {
            if (CheckMenuAuth() == false)
                return;

            string ids = "";
            for (int i = 0; i < reportDataGridView.Rows.Count; i++)
            {
                if (Convert.ToBoolean(reportDataGridView.Rows[i].Cells[(int)eReportList.checkBox].Value) == true)
                {
                    ids += (reportDataGridView.Rows[i].Cells[(int)eReportList.idx].Value.ToString() + ",");
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
                // 비밀번호 체크
                PasswordForm form = new PasswordForm();
                form.StartPosition = FormStartPosition.CenterParent;
                if (form.ShowDialog() == DialogResult.Cancel)
                    return;
                ////

                string query = string.Format("EXEC DeleteReportItem '{0}'", ids);

                long retVal = DbHelper.ExecuteNonQuery(query);
                if (retVal != -1)
                {
                    for (int i = 0; i < reportDataGridView.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(reportDataGridView.Rows[i].Cells[(int)eReportList.checkBox].Value) == true)
                        {
                            reportDataGridView.Rows.RemoveAt(i);
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

        public void ResizeControls()
        {
            if (Parent == null)
                return;

            this.Left = 0;
            this.Top = 0;
            this.Width = Parent.Width;
            this.Height = Parent.Height;


            reportDataGridView.Width = (Parent.Width - reportDataGridView.Left) - 40;

            searchPanel.Left = reportDataGridView.Right - searchPanel.Width;

            deleteReportButton.Left = reportDataGridView.Right - deleteReportButton.Width;
            downloadReportButton.Left = deleteReportButton.Left - (downloadReportButton.Width + 6);

            //importButton.Left = importInspectionDataGridView.Left;
            //exportButton.Left = importInspectionDataGridView.Right - exportButton.Width;

            // Height, Top
            reportDataGridView.Height = this.Height - (reportDataGridView.Top + 30);

        }

        private bool CheckMenuAuth()
        {
            if (Global.loginInfo.authorityId == 102 || Global.loginInfo.jobId == 103)
            {
                return true; 
            }
            else
            {
                MessageBox.Show("권한이 없습니다.");
                return false;
            }
        }

        private void reportDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (reportDataGridView.SelectedRows.Count == 0)
                return;


            string idx = reportDataGridView.SelectedRows[0].Cells[(int)eReportList.idx].Value.ToString();

            DataSet dataSet = DbHelper.SelectQuery(string.Format("EXEC SelectReportItem '{0}'", idx));
            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("파일 정보를 가져올 수 없습니다.");
                return;
            }

            string tempFolderPath = System.IO.Path.GetTempPath();
            string uniquFileName = Utils.GetUniquFileNameByIndex(tempFolderPath, "qtrs_report.pdf");

            File.WriteAllBytes(uniquFileName, (Byte[])dataSet.Tables[0].Rows[0]["fileData"]);
            System.Diagnostics.Process.Start(uniquFileName);
        }
    }
}
