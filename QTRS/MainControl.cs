using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace QTRS
{
    public partial class MainControl : UserControl
    {
        private MainForm _parent = null;
        private bool _isLoad = false; 
        enum eComponentTestList
        {
            warehousingDate, componentCode, componentName, productAreaTypeName, maker, lotNo, mainLotNo, note,
            purpose, testDate, testerName, judgerName, judgingDate, judgingResult, source, idx
        }

        enum eProductTestList
        {
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
            idx
        }

        public MainControl(MainForm parent)
        {
            InitializeComponent();
            _parent = parent; 
        }

        private void MainControl_Load(object sender, EventArgs e)
        {
            InitControls();
            _isLoad = true;
            GetData();
        }

        private void InitControls()
        {
            InitNoticeDataGridView();
            InitComponentTestDataGridView();
            InitProductTestDataGridView();
        }

        private void InitNoticeDataGridView()
        {
            DataGridView dataGridView = noticeDataGridView;

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

            // Common style
            dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridView.GridColor = Global.GRID_COLOR;
            dataGridView.BackgroundColor = Color.White;         // BackgroundColor 
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // SelectionMode
                                                                                  //dataGridView.ReadOnly = true;
            dataGridView.MultiSelect = false;
            dataGridView.ScrollBars = ScrollBars.Both;
        }

        private void InitComponentTestDataGridView()
        {
            DataGridView dataGridView = componentTestDataGridView;

            string[] columnNames = { "입고 일자", "원료 코드", "원료 이름", "원료 생산지 타입 이름", "메이커", "LOT NO", "Main LOT", "비고", "용도", "검사 일자", "검사자", "판정 일자", "판정자", "판정 결과", "출처", "IDX" };
            for (int i = 0; i < columnNames.Length; i++)
            {
                dataGridView.Columns.Add("Column" + i.ToString(), columnNames[i]);
            }
            dataGridView.Columns[(int)eComponentTestList.idx].Visible = false;



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
            //dataGridView.Columns[(int)eComponentTestList.checkBox].ReadOnly = false;
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

        private void InitProductTestDataGridView()
        {
            DataGridView dataGridView = productTestDataGridView;

            string[] columnNames = {  "제품 코드", "제조 번호", "제조일자", "제조 수량", "제품 이름", "제품 설명", "식약처 코드", "식약처 제품명", "제형", "성상", "기계(채취장소)", "흡수량 기준",
                "판정자 이름","판정 일자", "판정 결과", "접수일자", "채취일자", "채취장소", "채취량", "채취자 이름", "검사 일자", "검사자 이름",
                "비고",
                "시작 작업자 이름",  "종료 작업자 이름", "확인자 이름",
                "작업 시작일", "작업 종료일","작업 시작시간", "작업 종료시간",  "총(이론) 생산량", "총(이론) Pad량", "총(이론) Bag량", "정품(실제) 생산량", "정품(실제) Pad량", "정품(실제) Bag량",
                "특기사항", "공정 조치사항", "관찰된 사항", "IDX" };
            for (int i = 0; i < columnNames.Length; i++)
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
            //dataGridView.Columns[(int)eProductTestList.checkBox].ReadOnly = false;
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

        public void GetData()
        {
            if (_isLoad == false)
                return; 

            SelectNoticeList();
            SelectComponentTestList();
            SelectProductTestList();
        }

        private void SelectNoticeList()
        {

            DataSet dataSet = DbHelper.SelectQuery("EXEC SelectNoticeListForMain");
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
                string startPeriodDate = dataRow["startPeriodDate"].ToString().Substring(0, 10);
                string endPeriodDate = dataRow["endPeriodDate"].ToString().Substring(0, 10);
                string authorityName = dataRow["authorityName"].ToString();
                string title = dataRow["title"].ToString();
                string writerName = dataRow["writerName"].ToString();
                string idx = dataRow["idx"].ToString();

                noticeDataGridView.Rows.Add(startPeriodDate.Substring(0, 10) + " ~ " + endPeriodDate.Substring(0, 10),
                    authorityName, title, writerName, idx);
                Utils.OddDataGridViewRow(noticeDataGridView);
            }

            noticeDataGridView.ClearSelection();
        }

        private void SelectComponentTestList()
        {
            DataSet dataSet = DbHelper.SelectQuery("EXEC SelectComponentTestListForMain");
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("완제품 검사 데이터를 가져올 수 없습니다.");
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
                string componentCode = dataRow["componentCode"].ToString();
                string componentName = dataRow["componentName"].ToString();
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
                string source = iiIdx == "NULL" ? "원료검사" : "수입검사";


                judgingResult = (judgingResult != "") ? (judgingResult == "1" ? "적합" : "부적합") : "";
                componentTestDataGridView.Rows.Add(warehousingDate, componentCode, componentName, productAreaTypeName, maker, lotNo, mainLotNo, note,
                    purpose, testDate, testerName, judgingDate, judgerName, judgingResult, source, idx);

                if (iiIdx == "-1")
                    componentTestDataGridView.Rows[componentTestDataGridView.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                else
                    Utils.OddDataGridViewRow(componentTestDataGridView);
            }

            componentTestDataGridView.ClearSelection();
        }

        private void SelectProductTestList()
        { 
            DataSet dataSet = DbHelper.SelectQuery("EXEC SelectProductTestListForMain");
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
                string productCode = dataRow["productCode"].ToString();
                string manufactureSerialNumber = dataRow["manufactureSerialNumber"].ToString();
                string manufactureDate = dataRow["manufactureDate"].ToString();
                string manufactureQuantity = dataRow["manufactureQuantity"].ToString();
                string productName = dataRow["productName"].ToString();
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

                judgingResult = judgingResult == "1" ? "적합" : "부적합";
                productTestDataGridView.Rows.Add(
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
                idx);
                Utils.OddDataGridViewRow(productTestDataGridView);
            }

            productTestDataGridView.ClearSelection();
        }

        private void viewAllNoticeButton_Click(object sender, EventArgs e)
        {
            _parent.SetNoticeMenuButtonStatus(true); 
            string menuName = "noticeMenuButton";
            //ResetMenuButtonStatus(menuName);
            _parent.ResetContent(menuName);
            _parent.SetCurrentMenuName(menuName); 
        }

        private void writeNoticeButton_Click(object sender, EventArgs e)
        {
            _parent.SetNoticeMenuButtonStatus(true);
            string menuName = "noticeMenuButton";
            //ResetMenuButtonStatus(menuName);
            _parent.ResetContent(menuName);
            _parent.SetCurrentMenuName(menuName);

            _parent.AddNoticeButton(); 
        }

        private void homeTimer_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Timer--");
            GetData(); 
        }

        public void StartHomeTimer()
        {
            homeTimer.Start(); 
        }

        public void StopHomeTimer()
        {
            homeTimer.Stop(); 
        }

        private void qualityManagementReportViewButton_Click(object sender, EventArgs e)
        {
            Report.ReportForm form = new Report.ReportForm((int)Global.eReportType.qualityManagement);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog(); 
        }

        /*
        private void qualityManagementReportViewButton2_Click(object sender, EventArgs e)
        {
            Report.ProductQtTestResultReportForm form = new Report.ProductQtTestResultReportForm(1);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        private void qualityManagementReportViewButton3_Click(object sender, EventArgs e)
        {
            Report.ProductQtTestResultXlsxReportForm form = new Report.ProductQtTestResultXlsxReportForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }
        */ 

        private void manufactureManagementReportViewButton_Click(object sender, EventArgs e)
        {
            Report.ReportForm form = new Report.ReportForm((int)Global.eReportType.manufactureManagement);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        private void finalQualityManagementReportViewButton_Click(object sender, EventArgs e)
        {
            Report.ReportForm form = new Report.ReportForm((int)Global.eReportType.finalQualityManagement);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        private void componentDrugTestReportViewButton_Click(object sender, EventArgs e)
        {
            Report.ReportForm form = new Report.ReportForm((int)Global.eReportType.componentDrugTest);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        public void ResizeControls()
        {
            if (Parent == null)
                return; 

            this.Left = 0;
            this.Top = 0;
            this.Width = Parent.Width;
            this.Height = Parent.Height;

            int gapSize = 40; 

            noticeDataGridView.Width = (Parent.Width - noticeDataGridView.Left) - gapSize;
            componentTestDataGridView.Width = (Parent.Width - componentTestDataGridView.Left) - gapSize;
            productTestDataGridView.Width = (Parent.Width - productTestDataGridView.Left) - gapSize;

            writeNoticeButton.Left = noticeDataGridView.Right - writeNoticeButton.Width;
            viewAllNoticeButton.Left = writeNoticeButton.Left - (viewAllNoticeButton.Width + 6);


            // Height, Top
            int contentSize = this.Height - componentTestDataGridView.Top;

            int gridSize = contentSize - (productTextLabel.Height + reportGroupBox.Height + 70);
            componentTestDataGridView.Height = gridSize / 2;
            productTestDataGridView.Height = gridSize / 2;

            productTextPictureBox.Top = componentTestDataGridView.Bottom + 20;
            productTextLabel.Top = componentTestDataGridView.Bottom + 20;
            productTestDataGridView.Top = productTextLabel.Bottom + 10;
            reportGroupBox.Top = productTestDataGridView.Bottom + 20;

        }
    }
}
