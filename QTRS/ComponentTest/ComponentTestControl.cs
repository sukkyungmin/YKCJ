using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.ComponentTest
{
    public partial class ComponentTestControl : UserControl
    {
        private string _quickReportIdx = "";
        public enum eComponentTestList
        {
            checkBox, judgingResult, progressResult, warehousingDate, componentCode, componentName, innerComponentName, productAreaTypeName, maker, lotNo, mainLotNo, note,
            purpose, testDate, testerName, judgerName, judgingDate, source, idx
        }

        private CheckBox _columnCheckBox = null;
        private long _checkedRowCount = 0;
        private bool isLoaded = false;
        public ComponentTestControl()
        {
            InitializeComponent();
        }

        private void ComponentTestControl_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void addComponentTestButton_Click(object sender, EventArgs e)
        {
            if (CheckMenuAuth() == false)
                return;

            AddComponentTestForm form = new AddComponentTestForm(this);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void InitControls()
        {
            InitRawDatas();
            InitDataGrid();
            InitPeriod();
            isLoaded = true;
            GetData();
        }

        private void InitPeriod()
        {
            startDateTimeDateTimePicker.Value = DateTime.Now;
            endDateTimeDateTimePicker.Value = DateTime.Now;
        }

        private void InitRawDatas()
        {
            //this.Enabled = false;

            // 원료 생산지 기초데이터
            string query = "EXEC SelectCodeList 1000";

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("기초 데이터를 가져올 수 없습니다.");
                ////this.Enabled = true;
                return;
            }

            productAreaTypeComboBox.Items.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
            {
                ////this.Enabled = true;
                return;
            }

            //productAreaTypeComboBox.Items.Add(new ComboBoxItem("선택", -1));
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                productAreaTypeComboBox.Items.Add(new ComboBoxItem(dataRow["codeName"].ToString(), dataRow["codeId"]));
            }

            productAreaTypeComboBox.Items.Add(new ComboBoxItem("전체", 150));

            productAreaTypeComboBox.SelectedIndex = 000000000;

            ////this.Enabled = true;
        }

        private void InitDataGrid()
        {
            DataGridView dataGridView = componentTestDataGridView;

            string[] columnNames = { "", "판정 결과", "진행 여부", "입고 일자", "원료 코드", "원료 이름", "내부원료이름", "원료 생산지 타입 이름",
                "메이커", "LOT NO", "Main LOT", "비고", "용도", "검사 일자", "검사자", "판정 일자", "판정자", "출처", "IDX" };
            DataGridViewCheckBoxColumn checkboxCol = new DataGridViewCheckBoxColumn();
            checkboxCol.Name = "Column0";
            checkboxCol.HeaderText = "";
            dataGridView.Columns.Add(checkboxCol);
            for (int i = 1; i < columnNames.Length; i++)
            {
                dataGridView.Columns.Add("Column" + i.ToString(), columnNames[i]);
            }
            dataGridView.Columns[(int)eComponentTestList.idx].Visible = false;



            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // column checkbox
            //Rectangle rect = dataGridView.GetCellDisplayRectangle((int)eProductList.checkBox, -1, true);
            _columnCheckBox = new CheckBox();
            _columnCheckBox.Size = new Size(13, 13);
            _columnCheckBox.Location = new Point(3, 3);
            _columnCheckBox.CheckedChanged += new EventHandler(checkBoxColumn_CheckedChanged);

            dataGridView.Controls.Add(_columnCheckBox);


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
            dataGridView.Columns[(int)eComponentTestList.checkBox].ReadOnly = false;
            for (int i = (int)eComponentTestList.warehousingDate; i <= (int)eComponentTestList.source; i++)
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

        void checkBoxColumn_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < componentTestDataGridView.RowCount; j++)
            {
                componentTestDataGridView[0, j].Value = _columnCheckBox.Checked;
            }

            if (_columnCheckBox.Checked == true)
                _checkedRowCount = componentTestDataGridView.Rows.Count;
            else
                _checkedRowCount = 0;

            componentTestDataGridView.EndEdit();
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

        private void searchButton_Click(object sender, EventArgs e)
        {
            SelectComponentTestList();
        }

        public void SelectComponentTestList()
        {
            ////this.Enabled = false;

            string productAreaTypeId = "";
            string componentCode = "";
            string startDateTime = "NULL";
            string endDateTime = "NULL";

            productAreaTypeId = Utils.GetSelectedComboBoxItemValue(productAreaTypeComboBox);
            componentCode = componentCodeTextBox.Text.Trim();

            if (applyPeriodCheckBox.Checked == true)
            {
                startDateTime = startDateTimeDateTimePicker.Value.ToString("yyyy-MM-dd");
                endDateTime = endDateTimeDateTimePicker.Value.ToString("yyyy-MM-dd");
            }

            string query = "";
            if (startDateTime == "NULL")
                query = string.Format("EXEC SelectComponentTestList '{0}', '{1}', NULL, NULL, 2, 2", productAreaTypeId, componentCode);
            else
                query = string.Format("EXEC SelectComponentTestList '{0}', '{1}', '{2}', '{3}', 2, 2", productAreaTypeId, componentCode, startDateTime, endDateTime);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("원료 검사 데이터를 가져올 수 없습니다.");
                return;
            }

            componentTestDataGridView.Rows.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string idx = dataRow["idx"].ToString();
                string iiIdx = DBNull.Value.Equals(dataRow["iiIdx"]) ? "NULL" : dataRow["iiIdx"].ToString();
                string warehousingDate = dataRow["warehousingDate"].ToString().Substring(0, 10);
                componentCode = dataRow["componentCode"].ToString();
                string componentName = dataRow["componentName"].ToString();
                string innerComponentName = dataRow["innerComponentName"].ToString();
                string productAreaTypeName = dataRow["productAreaTypeName"].ToString();
                string maker = dataRow["maker"].ToString();
                string lotNo = dataRow["lotNo"].ToString();
                string mainLotNo = dataRow["mainLotNo"].ToString();
                string note = dataRow["note"].ToString();
                string purpose = DBNull.Value.Equals(dataRow["purpose"]) ? "" : dataRow["purpose"].ToString();
                string testDate = DBNull.Value.Equals(dataRow["testDate"]) ? "" : dataRow["testDate"].ToString().Substring(0, 10);
                //string testerId = DBNull.Value.Equals(dataRow["testerId"]) ? "" : dataRow["testerId"].ToString();
                string testerName = DBNull.Value.Equals(dataRow["testerName"]) ? "" : dataRow["testerName"].ToString();
                //string judgerId = DBNull.Value.Equals(dataRow["judgerId"]) ? "" : dataRow["judgerId"].ToString();
                string judgingDate = DBNull.Value.Equals(dataRow["judgingDate"]) ? "" : dataRow["judgingDate"].ToString().Substring(0, 10);
                string judgerName = DBNull.Value.Equals(dataRow["judgerName"]) ? "" : dataRow["judgerName"].ToString();
                string judgingResult = DBNull.Value.Equals(dataRow["judgingResult"]) ? "" : dataRow["judgingResult"].ToString();
                string progressResult = DBNull.Value.Equals(dataRow["progressResult"]) ? "" : dataRow["progressResult"].ToString();
                string source = iiIdx == "NULL" ? "원료검사" : "수입검사";


                judgingResult = (judgingResult != "") ? (judgingResult == "1" ? "적합" : "부적합") : "";
                progressResult = (progressResult != "") ? (progressResult == "1" ? "완료" : "진행중") : "";

                componentTestDataGridView.Rows.Add(false, judgingResult, progressResult, warehousingDate, componentCode, componentName, innerComponentName, 
                    productAreaTypeName, maker, lotNo, mainLotNo, note,
                    purpose, testDate, testerName, judgingDate, judgerName, source, idx);

                if (iiIdx == "-1" || judgingResult == "부적합")
                    componentTestDataGridView.Rows[componentTestDataGridView.Rows.Count - 1].Cells[1].Style.ForeColor = Color.Red;
                //componentTestDataGridView.Rows[1].Cells[1].Style.ForeColor
                Utils.OddDataGridViewRow(componentTestDataGridView);
            }

            componentTestDataGridView.ClearSelection();

            ////this.Enabled = true; 
        }

        public void GetData()
        {
            if (isLoaded == true)
                SelectComponentTestList();
        }

        private void applyPeriodCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            startDateTimeDateTimePicker.Enabled = applyPeriodCheckBox.Checked;
            endDateTimeDateTimePicker.Enabled = applyPeriodCheckBox.Checked;
        }

        private void deleteComponentTestButton_Click(object sender, EventArgs e)
        {
            if (CheckMenuAuth() == false)
                return;

            string ids = "";
            for (int i = 0; i < componentTestDataGridView.Rows.Count; i++)
            {
                if (Convert.ToBoolean(componentTestDataGridView.Rows[i].Cells[(int)eComponentTestList.checkBox].Value) == true)
                {
                    ids += (componentTestDataGridView.Rows[i].Cells[(int)eComponentTestList.idx].Value.ToString() + ",");
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
                string[] queryArray = new string[3];
                queryArray[0] = string.Format("EXEC DeleteTestAttachment '{0}', '{1}'", ids, Global.FILETYPE_COMPONENT);
                queryArray[1] = string.Format("EXEC DeleteComponentTestResults '{0}'", ids);
                queryArray[2] = string.Format("EXEC DeleteComponentTestItem '{0}'", ids);

                long retVal = DbHelper.ExecuteNonQueryWithTransaction(queryArray);
                if (retVal != -1)
                {
                    for (int i = 0; i < componentTestDataGridView.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(componentTestDataGridView.Rows[i].Cells[(int)eComponentTestList.checkBox].Value) == true)
                        {
                            componentTestDataGridView.Rows.RemoveAt(i);
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

        private void componentTestDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            OpenComponentTestForm();
        }

        private void OpenComponentTestForm()
        {
            if (componentTestDataGridView.SelectedRows.Count > 0)
            {
                string idx = componentTestDataGridView.SelectedRows[0].Cells[(int)eComponentTestList.idx].Value.ToString();
                AddComponentTestForm form = new AddComponentTestForm(this);
                form.SetSaveMode("UPDATE");
                form.SetCurrentTestId(idx);
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();
            }
        }

        private void componentTestDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            if (componentTestDataGridView.SelectedRows.Count > 0)
            {
                _quickReportIdx = componentTestDataGridView.SelectedRows[0].Cells[(int)eComponentTestList.idx].Value.ToString();
                quickReportIdx.Text = string.Format("(IDX : {0})" ,componentTestDataGridView.SelectedRows[0].Cells[(int)eComponentTestList.idx].Value.ToString());
                quickReportName.Text = string.Format("Code : {0} \r\nName : {1}", componentTestDataGridView.SelectedRows[0].Cells[(int)eComponentTestList.componentCode].Value.ToString(),
                                                                                componentTestDataGridView.SelectedRows[0].Cells[(int)eComponentTestList.componentName].Value.ToString());

            }
        }

        private void componentDrugTestReportViewButton_Click(object sender, EventArgs e)
        {
            if (quickReportIdx.Text == "(IDX)")
            {
                MessageBox.Show("검색하실 데이터를 선택해주세요.");
                return;
            }
            Report.QuickReportForm form = new Report.QuickReportForm((int)Global.eReportType.componentDrugTest, _quickReportIdx,true) { StartPosition = FormStartPosition.CenterParent };
            form.ShowDialog();
        }

        /*
        public void UpdateComponentTestItem(string warehousingDate, string componentCode, string componentName, string productAreaTypeName, string maker,
            string lotNo, string mainLotNo, string note,
                    string purpose, string testDate, string testerName, string judgingDate, string judgerName, string judgingResult)
        {
            DataGridViewRow row = componentTestDataGridView.SelectedRows[0];
            row.Cells[(int)eComponentTestList.warehousingDate].Value = warehousingDate;
            row.Cells[(int)eComponentTestList.componentCode].Value = componentCode;
            row.Cells[(int)eComponentTestList.componentName].Value = componentName;
            row.Cells[(int)eComponentTestList.productAreaTypeName].Value = productAreaTypeName;
            row.Cells[(int)eComponentTestList.maker].Value = maker;
            row.Cells[(int)eComponentTestList.lotNo].Value = lotNo;
            row.Cells[(int)eComponentTestList.mainLotNo].Value = mainLotNo;
            row.Cells[(int)eComponentTestList.note].Value = note;
            row.Cells[(int)eComponentTestList.purpose].Value = purpose;
            row.Cells[(int)eComponentTestList.testDate].Value = testDate;
            row.Cells[(int)eComponentTestList.testerName].Value = testerName;
            row.Cells[(int)eComponentTestList.judgerName].Value = judgerName;
            row.Cells[(int)eComponentTestList.judgingDate].Value = judgingDate;
            row.Cells[(int)eComponentTestList.judgingResult].Value = judgingResult;
        }
        */

        private bool CheckMenuAuth()
        {
            if (Global.loginInfo.authorityId == 101)
            {
                MessageBox.Show("권한이 없습니다.");
                return false;
            }
            else
                return true;
        }

        public void ResizeControls()
        {
            if (Parent == null)
                return;

            this.Left = 0;
            this.Top = 0;
            this.Width = Parent.Width;
            this.Height = Parent.Height;


            componentTestDataGridView.Width = (Parent.Width - componentTestDataGridView.Left) - 40;

            searchPanel.Left = componentTestDataGridView.Right - searchPanel.Width;

            deleteComponentTestButton.Left = componentTestDataGridView.Right - deleteComponentTestButton.Width;
            addComponentTestButton.Left = deleteComponentTestButton.Left - (addComponentTestButton.Width + 6);

            //importButton.Left = importInspectionDataGridView.Left;
            //exportButton.Left = importInspectionDataGridView.Right - exportButton.Width;

            // Height, Top
            componentTestDataGridView.Height = this.Height - (componentTestDataGridView.Top + 30);
        }


    }
}
