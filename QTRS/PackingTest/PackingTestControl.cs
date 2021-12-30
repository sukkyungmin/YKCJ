using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.PackingTest
{
    public partial class PackingTestControl : UserControl
    {
        public enum eProductTestList
        {
            checkBox,
            productCode,
            manufactureSerialNumber,
            manufactureDate,
            manufactureQuantity,
            productName,
            productDesc,
            fdaCode,
            fdaName,
            dosageForm,
            property,
            machine,
            standardOnAbsorbedAmount,
            judgerName,
            judgingDate,
            judgingResult,
            receiptDate,
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

        private bool isLoaded = false;

        public PackingTestControl()
        {
            InitializeComponent();
        }

        private void PackingTestControl_Load(object sender, EventArgs e)
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

        private void InitPeriod()
        {
            startDateTimeDateTimePicker.Value = DateTime.Now;
            endDateTimeDateTimePicker.Value = DateTime.Now;
        }

        private void InitDataGrid()
        {
            DataGridView dataGridView = productTestDataGridView;

            string[] columnNames = { "", "제품 코드", "제조 번호", "제조일자", "제조 수량", "제품 이름", "제품 설명", "식약처 코드", "식약처 제품명", "제형", "성상", "기계(채취장소)", "흡수량 기준",
                "판정자 이름","판정 일자", "판정 결과", "접수일자", "채취일자", "채취장소", "채취량", "채취자 이름", "검사 일자", "검사자 이름",
                "비고",
                "시작 작업자 이름",  "종료 작업자 이름", "확인자 이름",
                "작업 시작일", "작업 종료일","작업 시작시간", "작업 종료시간",  "총(이론) 생산량", "총(이론) Pad량", "총(이론) Bag량", "정품(실제) 생산량", "정품(실제) Pad량", "정품(실제) Bag량",
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
            for (int i = (int)eProductTestList.productCode; i <= (int)eProductTestList.idx; i++)
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

            string productName = "";
            string productCode = "";
            string startDateTime = "NULL";
            string endDateTime = "NULL";

            productName = productNameTextBox.Text.Trim();
            productCode = productCodeTextBox.Text.Trim();

            if (applyPeriodCheckBox.Checked == true)
            {
                startDateTime = startDateTimeDateTimePicker.Value.ToString("yyyy-MM-dd");
                endDateTime = endDateTimeDateTimePicker.Value.ToString("yyyy-MM-dd");
            }

            string query = "";
            if (startDateTime == "NULL")
                query = string.Format("EXEC SelectProductTestList '{0}', '{1}', NULL, NULL, 2, 2", productName, productCode);
            else
                query = string.Format("EXEC SelectProductTestList '{0}', '{1}', '{2}', '{3}', 2, 2", productName, productCode, startDateTime, endDateTime);

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
                string manufactureSerialNumber = dataRow["manufactureSerialNumber"].ToString();
                string manufactureDate = dataRow["manufactureDate"].ToString();
                string manufactureQuantity = dataRow["manufactureQuantity"].ToString();
                productName = dataRow["productName"].ToString();
                string productDesc = dataRow["productDesc"].ToString();
                string fdaCode = dataRow["fdaCode"].ToString();
                string fdaName = dataRow["fdaName"].ToString();
                string dosageForm = dataRow["dosageForm"].ToString();
                string property = dataRow["property"].ToString();
                string machine = dataRow["machine"].ToString();
                string standardOnAbsorbedAmount = dataRow["standardOnAbsorbedAmount"].ToString();
                string judgerName = dataRow["judgerName"].ToString();
                string judgingDate = dataRow["judgingDate"].ToString().Substring(0, 10);
                string judgingResult = dataRow["judgingResult"].ToString();
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
                productTestDataGridView.Rows.Add(false,
                productCode,
                manufactureSerialNumber,
                manufactureDate,
                manufactureQuantity,
                productName,
                productDesc,
                fdaCode,
                fdaName,
                dosageForm,
                property,
                machine,
                standardOnAbsorbedAmount,
                judgerName,
                judgingDate,
                judgingResult,
                receiptDate,
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
    }
}
