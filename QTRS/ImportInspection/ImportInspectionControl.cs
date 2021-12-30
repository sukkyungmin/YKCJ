using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.ImportInspection
{
    public partial class ImportInspectionControl : UserControl
    {
        enum eImportInspectionList
        {
            checkBox, warehousingDate, componentCode, componentName, innerComponentName, productAreaTypeName, maker, lotNo, mainLotNo, note, idx
        }

        private CheckBox _columnCheckBox = null;
        private long _checkedRowCount = 0;
        private bool _isLoaded = false;

        public ImportInspectionControl()
        {
            InitializeComponent();
        }

        private void ImportInspectionControl_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void InitControls()
        {
            InitPeriod();
            InitRawDatas();
            InitDataGrid();
            _isLoaded = true;
            GetData();
        }

        private bool CheckMenuAuth()
        {
            if (Global.loginInfo.authorityId == 100)
            {
                MessageBox.Show("권한이 없습니다.");
                return false;
            }
            else
                return true; 
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

            productAreaTypeComboBox.SelectedIndex = 0;

            ////this.Enabled = true;
        }

        private void InitDataGrid()
        {
            DataGridView dataGridView = importInspectionDataGridView;

            string[] columnNames = { "", "입고 일자", "원료 코드", "원료 이름", "내부원료이름", "원료 생산지 타입 이름", "메이커", "LOT NO", "Main LOT", "비고", "IDX" };
            DataGridViewCheckBoxColumn checkboxCol = new DataGridViewCheckBoxColumn();
            checkboxCol.Name = "Column0";
            checkboxCol.HeaderText = "";
            dataGridView.Columns.Add(checkboxCol);
            for (int i = 1; i < columnNames.Length; i++)
            {
                dataGridView.Columns.Add("Column" + i.ToString(), columnNames[i]);
            }
            dataGridView.Columns[(int)eImportInspectionList.idx].Visible = false;

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
            dataGridView.Columns[(int)eImportInspectionList.checkBox].ReadOnly = false;
            for (int i = (int)eImportInspectionList.warehousingDate; i <= (int)eImportInspectionList.idx; i++)
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

        void checkBoxColumn_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < importInspectionDataGridView.RowCount; j++)
            {
                importInspectionDataGridView[0, j].Value = _columnCheckBox.Checked;
            }

            if (_columnCheckBox.Checked == true)
                _checkedRowCount = importInspectionDataGridView.Rows.Count;
            else
                _checkedRowCount = 0;

            importInspectionDataGridView.EndEdit();
        }


        private void searchButton_Click(object sender, EventArgs e)
        {
            SelectImportInspectionList();
        }

        private void SelectImportInspectionList()
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
                query = string.Format("EXEC SelectImportInspectionList '{0}', '{1}', NULL, NULL", productAreaTypeId, componentCode);
            else
                query = string.Format("EXEC SelectImportInspectionList '{0}', '{1}', '{2}', '{3}'", productAreaTypeId, componentCode, startDateTime, endDateTime);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("수입검사 데이터를 가져올 수 없습니다.");
                return;
            }

            importInspectionDataGridView.Rows.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string idx = dataRow["idx"].ToString();
                string warehousingDate = dataRow["warehousingDate"].ToString().Substring(0, 10);
                componentCode = dataRow["componentCode"].ToString();
                string componentName = dataRow["componentName"].ToString();
                string innerComponentName = dataRow["innerComponentName"].ToString();
                string productAreaTypeName = dataRow["productAreaTypeName"].ToString();
                string maker = dataRow["maker"].ToString();
                string lotNo = dataRow["lotNo"].ToString();
                string mainLotNo = dataRow["mainLotNo"].ToString();
                string note = dataRow["note"].ToString();

                importInspectionDataGridView.Rows.Add(false, warehousingDate, componentCode, componentName, innerComponentName, productAreaTypeName, maker, lotNo, mainLotNo, note, idx);
                Utils.OddDataGridViewRow(importInspectionDataGridView);
            }

            importInspectionDataGridView.ClearSelection();

            ////this.Enabled = true; 
        }

        public void GetData()
        {
            if (_isLoaded == true)
                SelectImportInspectionList();
        }

        private void addImportInspectionButton_Click(object sender, EventArgs e)
        {
            if (CheckMenuAuth() == false)
                return; 

            AddImportInspectionForm form = new AddImportInspectionForm(this);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void deleteImportInspectionButton_Click(object sender, EventArgs e)
        {
            if (CheckMenuAuth() == false)
                return;

            string ids = "";
            for (int i = 0; i < importInspectionDataGridView.Rows.Count; i++)
            {
                if (Convert.ToBoolean(importInspectionDataGridView.Rows[i].Cells[(int)eImportInspectionList.checkBox].Value) == true)
                {
                    ids += (importInspectionDataGridView.Rows[i].Cells[(int)eImportInspectionList.idx].Value.ToString() + ",");
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
                string []queryArray = new string[2];
                queryArray[0] = string.Format("EXEC DeleteImportInspectionItem '{0}'", ids);
                queryArray[1] = string.Format("EXEC UpdateComponentTestDeleteStatus '{0}'", ids);
                long retVal = DbHelper.ExecuteNonQueryWithTransaction(queryArray);
                if (retVal != -1)
                {
                    for (int i = 0; i < importInspectionDataGridView.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(importInspectionDataGridView.Rows[i].Cells[(int)eImportInspectionList.checkBox].Value) == true)
                        {
                            importInspectionDataGridView.Rows.RemoveAt(i);
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

        private void importButton_Click(object sender, EventArgs e)
        {
            if (CheckMenuAuth() == false)
                return;

            ImportExport.ExcelImportForm form = new ImportExport.ExcelImportForm();
            form.SetMasterFileType((int)Global.eMasterFileType.importInspection);
            form.StartPosition = FormStartPosition.CenterParent;
            DialogResult retVal = form.ShowDialog();
            if (retVal == DialogResult.OK)
            {
                SelectImportInspectionList();
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            ImportExport.ExcelExportForm form = new ImportExport.ExcelExportForm();
            form.SetMasterFileType((int)Global.eMasterFileType.importInspection);
            form.SetDataGridView(importInspectionDataGridView); 
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
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

        private void applyPeriodCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            startDateTimeDateTimePicker.Enabled = applyPeriodCheckBox.Checked;
            endDateTimeDateTimePicker.Enabled = applyPeriodCheckBox.Checked;
        }

        private void importInspectionDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void ResizeControls()
        {
            if (Parent == null)
                return;

            this.Left = 0;
            this.Top = 0;
            this.Width = Parent.Width;
            this.Height = Parent.Height;


            importInspectionDataGridView.Width = (Parent.Width - importInspectionDataGridView.Left) - 40;

            searchPanel.Left = importInspectionDataGridView.Right - searchPanel.Width;

            deleteImportInspectionButton.Left = importInspectionDataGridView.Right - deleteImportInspectionButton.Width;
            addImportInspectionButton.Left = deleteImportInspectionButton.Left - (addImportInspectionButton.Width + 6);

            importButton.Left = importInspectionDataGridView.Left;
            exportButton.Left = importInspectionDataGridView.Right - exportButton.Width;


            // Height, Top
            importInspectionDataGridView.Height = this.Height - (importInspectionDataGridView.Top + importButton.Height + 40);
            importButton.Top = importInspectionDataGridView.Bottom + 10;
            exportButton.Top = importInspectionDataGridView.Bottom + 10;
        }
    }
}
