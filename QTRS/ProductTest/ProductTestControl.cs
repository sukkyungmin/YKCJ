using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.ProductTest
{
    public partial class ProductTestControl : UserControl
    {
        private string _quickReportIdx = "";
        public enum eProductTestList
        {
            checkBox,
            judgingResult,
            progressResult,
            fdaName,
            receiptDate,
            machine,
            manufactureSerialNumber,
            manufactureDate,
            manufactureQuantity,
            productCode,
            productName,
            productDesc,
            fdaCode,
            dosageForm,
            property,
            standardOnAbsorbedAmount,
            judgerName,
            judgingDate,
            gatherDate,
            gatherPlace,
            gatherQuantity,
            gathererName,
            testDate,
            testerName,
            qyNote,
            startWorkerName,
            endWorkerName,
            checkerName,
            startWorkDate,
            endWorkDate,
            startWorkTime,
            endWorkTime,
            logicalProductionQuantity,
            logicalPadQuantity,
            logicalBagQuantity,
            realProductionQuantity,
            realPadQuantity,
            realBagQuantity,
            specialNote,
            correctiveMeasure,
            observationNote,
            numOfBag1,
            numOfBag2,
            numOfBag3,
            numOfBag4,
            numOfBag5,
            idx
        }

        private CheckBox _columnCheckBox = null;
        private long _checkedRowCount = 0;

        private bool isLoaded = false;
        public ProductTestControl()
        {
            InitializeComponent();
        }

        private void ProductTestControl_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void InitControls()
        {
            InitDataGrid();
            InitPeriod();
            isLoaded = true;

            SelectProductTestList(); 
        }

        private void addProductTestButton_Click(object sender, EventArgs e)
        {
            if (CheckMenuAuth() == false)
                return;

            AddProductTestForm form = new AddProductTestForm(this);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void InitPeriod()
        {
            startDateTimeDateTimePicker.Value = DateTime.Now;
            endDateTimeDateTimePicker.Value = DateTime.Now;
        }

        private void InitDataGrid()
        {
            DataGridView dataGridView = productTestDataGridView;

            //string[] columnNames = { "", "판정 결과", "진행 여부", "제품 코드", "제조 번호", "제조일자", "제조 수량", "제품 이름", "제품 설명", "식약처 코드",
            //    "식약처 제품명", "제형", "성상", "기계(채취장소)", "흡수량 기준",
            //    "판정자 이름","판정 일자", "접수일자", "채취일자", "채취장소", "채취량", "채취자 이름", "검사 일자", "검사자 이름",
            //    "비고", "시작 작업자 이름",  "종료 작업자 이름", "확인자 이름",
            //    "작업 시작일", "작업 종료일","작업 시작시간", "작업 종료시간",  "총(이론) 생산량", "총(이론) Pad량",
            //    "총(이론) Bag량", "정품(실제) 생산량", "정품(실제) Pad량", "정품(실제) Bag량",
            //    "특기사항", "공정 조치사항", "관찰된 사항", "입수1", "입수2", "입수3", "입수4", "입수5", "IDX" };

            string[] columnNames = { "", "판정 결과", "진행 여부", "식약처 제품명", "접수일자", "기계(채취장소)", "제조 번호", "제조일자", "제조 수량", "제품 코드", "제품 이름",
                "제품 설명", "식약처 코드", "제형", "성상", "흡수량 기준",
                "판정자 이름","판정 일자", "채취일자", "채취장소", "채취량", "채취자 이름", "검사 일자", "검사자 이름",
                "비고", "시작 작업자 이름",  "종료 작업자 이름", "확인자 이름",
                "작업 시작일", "작업 종료일","작업 시작시간", "작업 종료시간",  "총(이론) 생산량", "총(이론) Pad량",
                "총(이론) Bag량", "정품(실제) 생산량", "정품(실제) Pad량", "정품(실제) Bag량",
                "특기사항", "공정 조치사항", "관찰된 사항", "입수1", "입수2", "입수3", "입수4", "입수5", "IDX" };
            DataGridViewCheckBoxColumn checkboxCol = new DataGridViewCheckBoxColumn();
            checkboxCol.Name = "Column0";
            checkboxCol.HeaderText = "";
            dataGridView.Columns.Add(checkboxCol);
            for (int i = 1; i < columnNames.Length; i++)
            {
                dataGridView.Columns.Add("Column" + i.ToString(), columnNames[i]);
            }
            dataGridView.Columns[(int)eProductTestList.idx].Visible = false;


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
            dataGridView.Columns[(int)eProductTestList.checkBox].ReadOnly = false;
            for (int i = (int)eProductTestList.judgingResult; i <= (int)eProductTestList.idx; i++)
                dataGridView.Columns[i].ReadOnly = true;
            //for (int i = (int)eProductTestList.productCode; i <= (int)eProductTestList.idx; i++)
            //    dataGridView.Columns[i].ReadOnly = true;

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
            for (int j = 0; j < productTestDataGridView.RowCount; j++)
            {
                productTestDataGridView[0, j].Value = _columnCheckBox.Checked;
            }

            if (_columnCheckBox.Checked == true)
                _checkedRowCount = productTestDataGridView.Rows.Count;
            else
                _checkedRowCount = 0;

            productTestDataGridView.EndEdit();
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
            SelectProductTestList();
        }

        private void SelectProductTestList()
        {
            ////this.Enabled = false;

            string fdaName = "";
            string productCode = "";
            string manufactureSerialNumber = "";
            string startDateTime = "NULL";
            string endDateTime = "NULL";

            fdaName = fdaNameTextBox.Text.Trim();
            productCode = productCodeTextBox.Text.Trim();
            manufactureSerialNumber = manufactureSerialNumberTextBox.Text.Trim();

            if (applyPeriodCheckBox.Checked == true)
            {
                startDateTime = startDateTimeDateTimePicker.Value.ToString("yyyy-MM-dd");
                endDateTime = endDateTimeDateTimePicker.Value.ToString("yyyy-MM-dd");
            }

            string query = "";
            if (startDateTime == "NULL")
                query = string.Format("EXEC SelectProductTestList '{0}', '{1}', '{2}', NULL, NULL, 2, 2", fdaName, productCode, manufactureSerialNumber);
            else
                query = string.Format("EXEC SelectProductTestList '{0}', '{1}', '{2}','{3}', '{4}', 2, 2", fdaName, productCode, manufactureSerialNumber, startDateTime, endDateTime);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("완제품 검사 데이터를 가져올 수 없습니다.");
                return;
            }

            productTestDataGridView.Rows.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string idx = dataRow["idx"].ToString();
                productCode = dataRow["productCode"].ToString();
                manufactureSerialNumber = dataRow["manufactureSerialNumber"].ToString();
                string manufactureDate = dataRow["manufactureDate"].ToString();
                string manufactureQuantity = dataRow["manufactureQuantity"].ToString();
                string productName = dataRow["productName"].ToString();
                string productDesc = dataRow["productDesc"].ToString();
                string fdaCode = dataRow["fdaCode"].ToString();
                fdaName = dataRow["fdaName"].ToString();
                string dosageForm = dataRow["dosageForm"].ToString();
                string property = dataRow["property"].ToString();
                string machine = dataRow["machine"].ToString();
                string standardOnAbsorbedAmount = dataRow["standardOnAbsorbedAmount"].ToString();
                string judgerName = dataRow["judgerName"].ToString();
                string judgingDate = dataRow["judgingDate"].ToString().Substring(0, 10);
                string judgingResult = dataRow["judgingResult"].ToString();
                string progressResult = dataRow["progressResult"].ToString();
                string receiptDate = dataRow["receiptDate"].ToString().Substring(0, 10);
                string gatherDate = dataRow["gatherDate"].ToString().Substring(0, 10);
                string gatherPlace = dataRow["gatherPlace"].ToString();
                string gatherQuantity = dataRow["gatherQuantity"].ToString();
                string gathererName = dataRow["gathererName"].ToString();
                string testDate = dataRow["testDate"].ToString();
                string testerName = dataRow["testerName"].ToString();
                string qyNote = dataRow["qyNote"].ToString();
                string startWorkerName = dataRow["startWorkerName"].ToString();
                string endWorkerName = dataRow["endWorkerName"].ToString();
                string checkerName = dataRow["checkerName"].ToString();
                string startWorkDate = dataRow["startWorkDate"].ToString().Substring(0, 10);
                string endWorkDate = dataRow["endWorkDate"].ToString().Substring(0, 10);
                string startWorkTime = "";
                string endWorkTime = "";
                string logicalProductionQuantity = dataRow["logicalProductionQuantity"].ToString();
                string logicalPadQuantity = dataRow["logicalPadQuantity"].ToString();
                string logicalBagQuantity = dataRow["logicalBagQuantity"].ToString();
                string realProductionQuantity = dataRow["realProductionQuantity"].ToString();
                string realPadQuantity = dataRow["realPadQuantity"].ToString();
                string realBagQuantity = dataRow["realBagQuantity"].ToString();
                string specialNote = dataRow["specialNote"].ToString();
                string correctiveMeasure = dataRow["correctiveMeasure"].ToString();
                string observationNote = dataRow["observationNote"].ToString();
                string numOfBag1 = dataRow["numOfBag1"].ToString();
                string numOfBag2 = dataRow["numOfBag2"].ToString();
                string numOfBag3 = dataRow["numOfBag3"].ToString();
                string numOfBag4 = dataRow["numOfBag4"].ToString();
                string numOfBag5 = dataRow["numOfBag5"].ToString();

                judgingResult = judgingResult == "1" ? "적합" : "부적합";
                progressResult = progressResult == "1" ? "완료" : "진행중";

                productTestDataGridView.Rows.Add(false,
                judgingResult,
                progressResult,
                fdaName,
                receiptDate,
                machine,
                manufactureSerialNumber,
                manufactureDate,
                manufactureQuantity,
                productCode,
                productName,
                productDesc,
                fdaCode,
                dosageForm,
                property,
                standardOnAbsorbedAmount,
                judgerName,
                judgingDate,
                gatherDate,
                gatherPlace,
                gatherQuantity,
                gathererName,
                testDate,
                testerName,
                qyNote,
                startWorkerName,
                endWorkerName,
                checkerName,
                startWorkDate,
                endWorkDate,
                startWorkTime,
                endWorkTime,
                logicalProductionQuantity,
                logicalPadQuantity,
                logicalBagQuantity,
                realProductionQuantity,
                realPadQuantity,
                realBagQuantity,
                specialNote,
                correctiveMeasure,
                observationNote,
                numOfBag1,
                numOfBag2,
                numOfBag3,
                numOfBag4,
                numOfBag5,
                idx);
                if (judgingResult == "부적합")
                    productTestDataGridView.Rows[productTestDataGridView.Rows.Count - 1].Cells[1].Style.ForeColor = Color.Red;

                Utils.OddDataGridViewRow(productTestDataGridView);
            }

            productTestDataGridView.ClearSelection();

            ////this.Enabled = true; 
        }

        public void GetData()
        {
            if (isLoaded == true)
                SelectProductTestList();
        }

        private void applyPeriodCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            startDateTimeDateTimePicker.Enabled = applyPeriodCheckBox.Checked;
            endDateTimeDateTimePicker.Enabled = applyPeriodCheckBox.Checked;
        }

        private void deleteProductTestButton_Click(object sender, EventArgs e)
        {
            if (CheckMenuAuth() == false)
                return;

            string ids = "";
            for (int i = 0; i < productTestDataGridView.Rows.Count; i++)
            {
                if (Convert.ToBoolean(productTestDataGridView.Rows[i].Cells[(int)eProductTestList.checkBox].Value) == true)
                {
                    ids += (productTestDataGridView.Rows[i].Cells[(int)eProductTestList.idx].Value.ToString() + ",");
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
                string[] queryArray = new string[5];
                queryArray[0] = string.Format("EXEC DeleteTestAttachment '{0}', '{1}'", ids, Global.FILETYPE_PRODUCT_M);
                queryArray[1] = string.Format("EXEC DeleteTestAttachment '{0}', '{1}'", ids, Global.FILETYPE_PRODUCT_Q);
                queryArray[2] = string.Format("EXEC DeleteProductMfTestResults '{0}'", ids);
                queryArray[3] = string.Format("EXEC DeleteProductQtTestResults '{0}'", ids);
                queryArray[4] = string.Format("EXEC DeleteProductTestItem '{0}'", ids);

                long retVal = DbHelper.ExecuteNonQueryWithTransaction(queryArray);
                if (retVal != -1)
                {
                    for (int i = 0; i < productTestDataGridView.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(productTestDataGridView.Rows[i].Cells[(int)eProductTestList.checkBox].Value) == true)
                        {
                            productTestDataGridView.Rows.RemoveAt(i);
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

        private void productTestDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            OpenProductTestForm();
        }

        private void OpenProductTestForm()
        {
            if (productTestDataGridView.SelectedRows.Count > 0)
            {
                string idx = productTestDataGridView.SelectedRows[0].Cells[(int)eProductTestList.idx].Value.ToString();
                AddProductTestForm form = new AddProductTestForm(this);
                form.SetSaveMode("UPDATE");
                form.SetCurrentTestId(idx);
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();
            }
        }


        private void productTestDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            if (productTestDataGridView.SelectedRows.Count > 0)
            {
                _quickReportIdx = productTestDataGridView.SelectedRows[0].Cells[(int)eProductTestList.idx].Value.ToString();
                quickReportIdx.Text = string.Format("(IDX : {0})", productTestDataGridView.SelectedRows[0].Cells[(int)eProductTestList.idx].Value.ToString());
                quickReportName.Text = string.Format("Code : {0} \r\nName : {1}", productTestDataGridView.SelectedRows[0].Cells[(int)eProductTestList.productCode].Value.ToString(),
                                                                                productTestDataGridView.SelectedRows[0].Cells[(int)eProductTestList.productDesc].Value.ToString());

            }
        }

        private void qualityManagementReportViewButton_Click(object sender, EventArgs e)
        {
            if (quickReportIdx.Text == "(IDX)")
            {
                MessageBox.Show("검색하실 데이터를 선택해주세요.");
                return;
            }
            Report.QuickReportForm form = new Report.QuickReportForm((int)Global.eReportType.qualityManagement, _quickReportIdx, productMainLotCheckbox.Checked) { StartPosition = FormStartPosition.CenterParent };
            form.ShowDialog();
        }

        private void manufactureManagementReportViewButton_Click(object sender, EventArgs e)
        {
            if (quickReportIdx.Text == "(IDX)")
            {
                MessageBox.Show("검색하실 데이터를 선택해주세요.");
                return;
            }
            Report.QuickReportForm form = new Report.QuickReportForm((int)Global.eReportType.manufactureManagement, _quickReportIdx, productMainLotCheckbox.Checked) { StartPosition = FormStartPosition.CenterParent };
            form.ShowDialog();
        }

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


            productTestDataGridView.Width = (Parent.Width - productTestDataGridView.Left) - 40;

            searchPanel.Left = productTestDataGridView.Right - searchPanel.Width;

            deleteProductTestButton.Left = productTestDataGridView.Right - deleteProductTestButton.Width;
            addProductTestButton.Left = deleteProductTestButton.Left - (addProductTestButton.Width + 6);

            //importButton.Left = importInspectionDataGridView.Left;
            //exportButton.Left = importInspectionDataGridView.Right - exportButton.Width;

            // Height, Top
            productTestDataGridView.Height = this.Height - (productTestDataGridView.Top + 30);
        }

    }
}
