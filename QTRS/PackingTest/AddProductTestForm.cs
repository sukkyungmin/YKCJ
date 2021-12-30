using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QTRS.PackingTest
{
    public partial class AddProductTestForm : Form
    {
        private PackingTestControl _parent = null;
        string _currentTestId = "";
        private string _saveMode = "ADD"; // ADD, UPDATE

        public AddProductTestForm(PackingTestControl parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void AddProductTestForm_Load(object sender, EventArgs e)
        {
            //this.Enabled = false;
            InitControls();
            ////this.Enabled = true;
        }

        private void InitControls()
        {
            InitProductCode(); 
            InitUsers();

            judgingResultComboBox.Items.Add(new ComboBoxItem("부적합", "0"));
            judgingResultComboBox.Items.Add(new ComboBoxItem("적합", "1"));
            judgingResultComboBox.SelectedIndex = 0;

            for (int i = 0; i < 24; i++)
            {
                startHourComboBox.Items.Add(string.Format("{0:D2}", i));
                endHourComboBox.Items.Add(string.Format("{0:D2}", i));
            }
            startHourComboBox.SelectedIndex = 0;
            endHourComboBox.SelectedIndex = 0;

            for (int i = 0; i < 60; i++)
            {
                startMinuteComboBox.Items.Add(string.Format("{0:D2}", i));
                endMinuteComboBox.Items.Add(string.Format("{0:D2}", i));
            }
            startMinuteComboBox.SelectedIndex = 0;
            endMinuteComboBox.SelectedIndex = 0;

            // Date
            receiptDateTimePicker.Value = DateTime.Now;
            gatherDateTimePicker.Value = DateTime.Now;
            testDateTimePicker.Value = DateTime.Now;
            judgingDateTimePicker.Value = DateTime.Now;
            startWorkDateTimePicker.Value = DateTime.Now;
            endWorkDateTimePicker.Value = DateTime.Now;


            if (_saveMode == "UPDATE")
            {
                this.Text = "완제품 품질관리 포장검사 실행";

                // 수정일 때는 수정 불가능
                productCodeComboBox.Enabled = false;

                SetSelectedValues();
            }
            else
            {
                // 신규일 때는 수정 가능
                productCodeComboBox.Enabled = true;
            }
        }

        private void SetSelectedValues()
        {
            string query = string.Format("EXEC SelectProductTestItem '{0}' ", _currentTestId);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("완제품 검사 데이터를 가져올 수 없습니다.");
                return;
            }

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            DataRow dataRow = dataSet.Tables[0].Rows[0];


            Utils.SelectComboBoxItem(productCodeComboBox, Utils.GetString(dataRow["productCode"]));

            int selectedRowIndex = _parent.productTestDataGridView.SelectedRows[0].Index;
            manufactureSerialNumberTextBox.Text = _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.manufactureSerialNumber].Value.ToString();
            manufactureDateTextBox.Text = _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.manufactureDate].Value.ToString();
            manufactureDateTextBox.Text = manufactureDateTextBox.Text.Substring(0, 10);
            manufactureQuantityTextBox.Text = _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.manufactureQuantity].Value.ToString();
            productNameTextBox.Text = _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.productName].Value.ToString();
            productDescTextBox.Text = _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.productDesc].Value.ToString();
            fdaCodeTextBox.Text = _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.fdaCode].Value.ToString();
            fdaNameTextBox.Text = _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.fdaName].Value.ToString();
            dosageFormTextBox.Text = _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.dosageForm].Value.ToString();
            propertyTextBox.Text = _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.property].Value.ToString();
            machineTextBox.Text = _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.machine].Value.ToString();
            standardOnAbsorbedAmountTextBox.Text = _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.standardOnAbsorbedAmount].Value.ToString();
            Utils.SelectComboBoxItem(judgerComboBox, Utils.GetString(dataRow["judgerId"]));
            //judgerName = Utils.GetString(dataRow["judgerName"]);
            judgingDateTimePicker.Value = Utils.GetDateTimeFormatFromObject(dataRow["judgingDate"]);
            Utils.SelectComboBoxItem(judgingResultComboBox, Utils.GetString(dataRow["judgingResult"]));
            receiptDateTimePicker.Value = Utils.GetDateTimeFormatFromObject(dataRow["receiptDate"]);
            gatherDateTimePicker.Value = Utils.GetDateTimeFormatFromObject(dataRow["gatherDate"]);
            gatherPlaceTextBox.Text = Utils.GetString(dataRow["gatherPlace"]);
            gatherQuantityTextBox.Text = Utils.GetString(dataRow["gatherQuantity"]);
            Utils.SelectComboBoxItem(gathererComboBox, Utils.GetString(dataRow["gathererId"]));
            //gathererName = Utils.GetString(dataRow["gathererName"]);
            testDateTimePicker.Value = Utils.GetDateTimeFormatFromObject(dataRow["testDate"]);
            Utils.SelectComboBoxItem(testerComboBox, Utils.GetString(dataRow["testerId"]));
            //testerName = Utils.GetString(dataRow["testerName"]);
            qyNoteTextBox.Text = Utils.GetString(dataRow["qyNote"]);
            Utils.SelectComboBoxItem(startWorkerComboBox, Utils.GetString(dataRow["startWorkerId"]));
            //startWorkerName = Utils.GetString(dataRow["startWorkerName"]);
            Utils.SelectComboBoxItem(endWorkerComboBox, Utils.GetString(dataRow["endWorkerId"]));
            //endWrokerName = Utils.GetString(dataRow["endWrokerName"]);
            Utils.SelectComboBoxItem(checkerComboBox, Utils.GetString(dataRow["checkerId"]));
            //checkerName = Utils.GetString(dataRow["checkerName"]);
            startWorkDateTimePicker.Value = Utils.GetDateTimeFormatFromObject(dataRow["startWorkDate"]);
            endWorkDateTimePicker.Value = Utils.GetDateTimeFormatFromObject(dataRow["endWorkDate"]);
            string[] startTimeArray = Utils.GetString(dataRow["startWorkTime"]).Split(':');
            if (startTimeArray.Length == 2)
            {
                startHourComboBox.SelectedIndex = startHourComboBox.FindStringExact(startTimeArray[0]);
                startMinuteComboBox.SelectedIndex = startMinuteComboBox.FindStringExact(startTimeArray[1]);
            }

            string[] endTimeArray = Utils.GetString(dataRow["endWorkTime"]).Split(':');
            if (endTimeArray.Length == 2)
            {
                endHourComboBox.SelectedIndex = endHourComboBox.FindStringExact(endTimeArray[0]);
                endMinuteComboBox.SelectedIndex = endMinuteComboBox.FindStringExact(endTimeArray[1]);
            }
            logicalProductionQuantityTextBox.Text = Utils.GetString(dataRow["logicalProductionQuantity"]);
            logicalPadQuantityTextBox.Text = Utils.GetString(dataRow["logicalPadQuantity"]);
            logicalBagQuantityTextBox.Text = Utils.GetString(dataRow["logicalBagQuantity"]);
            realProductionQuantityTextBox.Text = Utils.GetString(dataRow["realProductionQuantity"]);
            realPadQuantityTextBox.Text = Utils.GetString(dataRow["realPadQuantity"]);
            realBagQuantityTextBox.Text = Utils.GetString(dataRow["realBagQuantity"]);
            specialNoteTextBox.Text = Utils.GetString(dataRow["specialNote"]);
            correctiveMeasureTextBox.Text = Utils.GetString(dataRow["correctiveMeasure"]);
            observationNoteTextBox.Text = Utils.GetString(dataRow["observationNote"]);

            numOfBag1TextBox.Text = Utils.GetString(dataRow["numOfBag1"]);
            numOfBag2TextBox.Text = Utils.GetString(dataRow["numOfBag2"]);
            numOfBag3TextBox.Text = Utils.GetString(dataRow["numOfBag3"]);
            numOfBag4TextBox.Text = Utils.GetString(dataRow["numOfBag4"]);
            numOfBag5TextBox.Text = Utils.GetString(dataRow["numOfBag5"]);

        }

        private void InitProductCode()
        {
            string productCode = "";  //productCodeTextBox.Text.Trim();
            string productName = "";  //productNameTextBox.Text.Trim();

            string query = string.Format("EXEC SelectProductList '{0}', '{1}'", productCode, productName);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("완제품 데이터를 가져올 수 없습니다.");
                return;
            }

            productCodeComboBox.Items.Clear(); 

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            //productCodeComboBox.Items.Add(new ComboBoxItem("선택", "-1"));

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string idx = dataRow["idx"].ToString();
                productCode = dataRow["productCode"].ToString();
                productName = dataRow["productName"].ToString();
                string productDesc = dataRow["productDesc"].ToString();
                string fdaCode = dataRow["fdaCode"].ToString();
                string fdaName = dataRow["fdaName"].ToString();
                string dosageForm = dataRow["dosageForm"].ToString();
                string property = dataRow["property"].ToString();
                string machine = dataRow["machine"].ToString();
                string standardOnAbsorbedAmount = dataRow["standardOnAbsorbedAmount"].ToString();
                string note = dataRow["note"].ToString();

                productCodeComboBox.Items.Add(new ComboBoxItem(productCode + " | " + productDesc, productCode));
            }

            productCodeComboBox.SelectedIndex = -1; 
        }

        private void InitUsers()
        {
            DataSet dataSet = DbHelper.SelectQuery("EXEC SelectUserList");
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("사용자 정보를 가져올 수 없습니다.");
                return;
            }

            gathererComboBox.Items.Clear();
            testerComboBox.Items.Clear();
            judgerComboBox.Items.Clear();
            startWorkerComboBox.Items.Clear();
            endWorkerComboBox.Items.Clear();
            checkerComboBox.Items.Clear();


            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            //gathererComboBox.Items.Add(new ComboBoxItem("선택", "-1"));
            //testerComboBox.Items.Add(new ComboBoxItem("선택", "-1"));
            //judgerComboBox.Items.Add(new ComboBoxItem("선택", "-1"));
            //workerComboBox.Items.Add(new ComboBoxItem("선택", "-1"));
            //checkerComboBox.Items.Add(new ComboBoxItem("선택", "-1"));

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string id = dataRow["id"].ToString();
                string name = dataRow["name"].ToString();

                gathererComboBox.Items.Add(new ComboBoxItem(name, id));
                testerComboBox.Items.Add(new ComboBoxItem(name, id));
                judgerComboBox.Items.Add(new ComboBoxItem(name, id));
                startWorkerComboBox.Items.Add(new ComboBoxItem(name, id));
                endWorkerComboBox.Items.Add(new ComboBoxItem(name, id));
                checkerComboBox.Items.Add(new ComboBoxItem(name, id));
            }

            gathererComboBox.SelectedIndex = -1;
            testerComboBox.SelectedIndex = -1;
            judgerComboBox.SelectedIndex = -1;
            startWorkerComboBox.SelectedIndex = -1;
            endWorkerComboBox.SelectedIndex = -1;
            checkerComboBox.SelectedIndex = -1;
        }


        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void runQualitylTestButton_Click(object sender, EventArgs e)
        {
            RunQualityTestForm form = new RunQualityTestForm(_currentTestId);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();

        }

        private void productCodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (productCodeComboBox.SelectedIndex == -1)
                return;

            string productCode = Utils.GetSelectedComboBoxItemValue(productCodeComboBox); 
            string query = string.Format("EXEC SelectProductItem '{0}'", productCode);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("완제품 데이터를 가져올 수 없습니다.");
                return;
            }

            if (dataSet.Tables[0].Rows.Count == 0)
                return;


            DataRow dataRow = dataSet.Tables[0].Rows[0]; 
            productNameTextBox.Text = dataRow["productName"].ToString();
            productDescTextBox.Text = dataRow["productDesc"].ToString();
            fdaCodeTextBox.Text = dataRow["fdaCode"].ToString();
            fdaNameTextBox.Text = dataRow["fdaName"].ToString();
            dosageFormTextBox.Text = dataRow["dosageForm"].ToString();
            propertyTextBox.Text = dataRow["property"].ToString();
            machineTextBox.Text = dataRow["machine"].ToString();
            standardOnAbsorbedAmountTextBox.Text = dataRow["standardOnAbsorbedAmount"].ToString();

            gatherPlaceTextBox.Text = machineTextBox.Text; 
        }

        public void SetSaveMode(string saveMode = "ADD")
        {
            _saveMode = saveMode;
        }

        public void SetCurrentTestId(string currentTestId)
        {
            _currentTestId = currentTestId;
        }
    }
}
