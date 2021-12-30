using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QTRS.ProductTest
{
    public partial class AddProductTestForm : Form
    {
        public ProductTestControl _parent = null;
        public string _currentTestId = "";
        private string _saveMode = "ADD"; // ADD, UPDATE
        public string _productname = "";

        public AddProductTestForm(ProductTestControl parent)
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
            InitMachineComboBox();
            //ResetProductCodeComboBox(); 
            InitUsers();

            judgingResultComboBox.Items.Add(new ComboBoxItem("부적합", "0"));
            judgingResultComboBox.Items.Add(new ComboBoxItem("적합", "1"));
            judgingResultComboBox.SelectedIndex = 0;

            progressResultComboBox.Items.Add(new ComboBoxItem("진행중", "0"));
            progressResultComboBox.Items.Add(new ComboBoxItem("완료", "1"));
            progressResultComboBox.SelectedIndex = 0;


            for (int i=0; i<24; i++)
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

            // 판정자
            judgerComboBox.Items.Add(new ComboBoxItem(Global.configInfo.manufactureAdminName, Global.configInfo.manufactureAdminId));
            judgerComboBox.SelectedIndex = 0;


            if (_saveMode == "UPDATE")
            {
                addDataButton.Text = "수정";
                this.Text = "완제품 검사 수정";

                // 수정일 때는 수정 불가능
                productCodeComboBox.Enabled = false;
                SetSelectedValues();
            }
            else
            {
                Utils.SelectComboBoxItem(gathererComboBox, Global.loginInfo.id);
                Utils.SelectComboBoxItem(testerComboBox, Global.loginInfo.id);
                gatherQuantityTextBox.Text = Global.configInfo.gatherQuantity; 
                //manufactureQuantityTextBox.Text = Global.configInfo.manufactureQuantity;  
            }

            // 비고 설정
            ResetQyNote();
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

            Utils.SelectComboBoxItemByText(machineComboBox, _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.machine].Value.ToString());
            ResetProductCodeComboBox();
            Utils.SelectComboBoxItem(productCodeComboBox, Utils.GetString(dataRow["productCode"]));

            standardOnAbsorbedAmountTextBox.Text = _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.standardOnAbsorbedAmount].Value.ToString(); 
            Utils.SelectComboBoxItem(judgerComboBox, Utils.GetString(dataRow["judgerId"]));
            //judgerName = Utils.GetString(dataRow["judgerName"]);
            judgingDateTimePicker.Value = Utils.GetDateTimeFormatFromObject(dataRow["judgingDate"]);
            Utils.SelectComboBoxItem(judgingResultComboBox, Utils.GetString(dataRow["judgingResult"]));
            Utils.SelectComboBoxItem(progressResultComboBox, Utils.GetString(dataRow["progressResult"].ToString()));
            receiptDateTimePicker.Value = Utils.GetDateTimeFormatFromObject(dataRow["receiptDate"]);
            gatherDateTimePicker.Value = Utils.GetDateTimeFormatFromObject(dataRow["gatherDate"]);
            gatherPlaceTextBox.Text = Utils.GetString(dataRow["gatherPlace"]);
            gatherQuantityTextBox.Text = Utils.GetString(dataRow["gatherQuantity"]);
            Utils.SelectComboBoxItem(gathererComboBox, Utils.GetString(dataRow["gathererId"]));
            //gathererName = Utils.GetString(dataRow["gathererName"]);
            testDateTimePicker.Value = Utils.GetDateTimeFormatFromObject(dataRow["testDate"]);
            Utils.SelectComboBoxItem(testerComboBox, Utils.GetString(dataRow["testerId"]));
            //testerName = Utils.GetString(dataRow["testerName"]);
            //qyNoteTextBox.Text = Utils.GetString(dataRow["qyNote"]);
            Utils.SelectComboBoxItem(startWorkerComboBox, Utils.GetString(dataRow["startWorkerId"]));
            //startWorkerName = Utils.GetString(dataRow["startWorkerName"]);
            Utils.SelectComboBoxItem(endWorkerComboBox, Utils.GetString(dataRow["endWorkerId"]));
            //endWrokerName = Utils.GetString(dataRow["endWrokerName"]);
            Utils.SelectComboBoxItem(checkerComboBox, Utils.GetString(dataRow["checkerId"]));
            //checkerName = Utils.GetString(dataRow["checkerName"]);
            startWorkDateTimePicker.Value = Utils.GetDateTimeFormatFromObject(dataRow["startWorkDate"]);
            endWorkDateTimePicker.Value = Utils.GetDateTimeFormatFromObject(dataRow["endWorkDate"]);
            string []startTimeArray = Utils.GetString(dataRow["startWorkTime"]).Split(':'); 
            if(startTimeArray.Length == 2)
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

        private void InitMachineComboBox()
        {
            string query = "EXEC SelectCodeList 1040";

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("기계 데이터를 가져올 수 없습니다.");
                return;
            }

            machineComboBox.Items.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string codeId = dataRow["codeId"].ToString();
                string codeName = dataRow["codeName"].ToString();

                machineComboBox.Items.Add(new ComboBoxItem(codeName, codeId));
            }

            machineComboBox.SelectedIndex = -1;
        }

        private void ResetProductCodeComboBox()
        {
            string machine = (machineComboBox.SelectedItem as ComboBoxItem).Text.Trim(); 
            string productCode = "";  //productCodeTextBox.Text.Trim();
            string productName = "";  //productNameTextBox.Text.Trim();

            string ProductQuickName = "";

            productCodeComboBox.Items.Clear();

            ProductQuickName = (productQuickSearchCheckbox.Checked) ? productQuickSearchTextbox.Text : "";

            string query = string.Format("EXEC SelectProductListByMachine '{0}', '{1}'", machine, ProductQuickName);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("완제품 데이터를 가져올 수 없습니다.");
                return;
            }


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
                string standardOnAbsorbedAmount = dataRow["standardOnAbsorbedAmount"].ToString();
                string note = dataRow["note"].ToString();

                productCodeComboBox.Items.Add(new ComboBoxItem(string.Format("{0} | {1}", productCode, fdaName), productCode));
            }

            productCodeComboBox.SelectedIndex = -1;
            productCodeComboBox.Text = ""; 
        }

        private void InitUsers()
        {
            // 일반 사용자
            DataSet dataSet = DbHelper.SelectQuery("EXEC SelectUserList");
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("사용자 정보를 가져올 수 없습니다.");
                return;
            }

            gathererComboBox.Items.Clear();
            testerComboBox.Items.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string id = dataRow["id"].ToString();
                string name = dataRow["name"].ToString();

                gathererComboBox.Items.Add(new ComboBoxItem(name, id));
                testerComboBox.Items.Add(new ComboBoxItem(name, id));
            }

            gathererComboBox.SelectedIndex = -1;
            testerComboBox.SelectedIndex = -1;
        }

        private void ResetCheckerComboBox()
        {
            checkerComboBox.Items.Clear();

            if (machineComboBox.SelectedIndex == -1)
                return;

            string machineId = (machineComboBox.SelectedItem as ComboBoxItem).Value.ToString();

            // 확인자
            DataSet dataSet = DbHelper.SelectQuery(string.Format("EXEC SelectCheckerList '{0}'", machineId));
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("확인자 정보를 가져올 수 없습니다.");
                return;
            }

           

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string id = dataRow["id"].ToString();
                string name = dataRow["name"].ToString();

                checkerComboBox.Items.Add(new ComboBoxItem(name, id));
            }

            //if(checkerComboBox.Text.ToString() == "")
            //{
            //    checkerComboBox.SelectedIndex = 0;
            //}

            if(checkerComboBox.Items.Count > 0)
            {
                checkerComboBox.SelectedIndex = 0;
            }
            else
            {
                checkerComboBox.SelectedIndex = -1;
            }

        }

        private void ResetWorkerComboBox()
        {
            startWorkerComboBox.Items.Clear();
            endWorkerComboBox.Items.Clear();

            if (machineComboBox.SelectedIndex == -1)
                return; 

            string machineId = (machineComboBox.SelectedItem as ComboBoxItem).Value.ToString(); 

            // 작업자
            DataSet dataSet = DbHelper.SelectQuery(string.Format("EXEC SelectWorkerList '{0}'", machineId));
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("작업자 정보를 가져올 수 없습니다.");
                return;
            }

           

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string id = dataRow["id"].ToString();
                string name = dataRow["name"].ToString();

                startWorkerComboBox.Items.Add(new ComboBoxItem(name, id));
                endWorkerComboBox.Items.Add(new ComboBoxItem(name, id));
            }

            startWorkerComboBox.SelectedIndex = -1;
            endWorkerComboBox.SelectedIndex = -1;

        }

        private void addDataButton_Click(object sender, EventArgs e)
        {
            if (CheckRequiredItems() == false)
                return;

            bool retVal = false;
            if (_saveMode == "ADD")
                retVal = AddData();
            else
                retVal = UpdateData();

            if (retVal == true)
                Close();
        }

        private bool AddData(bool forTest = false)
        {
            string productCode = Utils.GetSelectedComboBoxItemValue(productCodeComboBox);
            string manufactureSerialNumber = manufactureSerialNumberTextBox.Text.Trim();
            string manufactureDate = manufactureDateTextBox.Text.Trim();
            string manufactureQuantity = manufactureQuantityTextBox.Text.Trim();
            string productName = productNameTextBox.Text.Trim();
            string productDesc = productDescTextBox.Text.Trim();
            string fdaCode = fdaCodeTextBox.Text.Trim();
            string fdaName = fdaNameTextBox.Text.Trim();
            string dosageForm = dosageFormTextBox.Text.Trim();
            string property = propertyTextBox.Text.Trim();
            string machine = (machineComboBox.SelectedItem as ComboBoxItem).Text.Trim();
            string standardOnAbsorbedAmount = standardOnAbsorbedAmountTextBox.Text.Trim();
            string judgerId = Utils.GetSelectedComboBoxItemValue(judgerComboBox);
            string judgerName = Utils.GetSelectedComboBoxItemText(judgerComboBox);
            string judgingDate = judgingDateTimePicker.Value.ToString("yyyy-MM-dd");
            string judgingResult = Utils.GetSelectedComboBoxItemValue(judgingResultComboBox);
            string progressResult = Utils.GetSelectedComboBoxItemValue(progressResultComboBox);
            string receiptDate = receiptDateTimePicker.Value.ToString("yyyy-MM-dd");
            string gatherDate = gatherDateTimePicker.Value.ToString("yyyy-MM-dd");
            string gatherPlace = gatherPlaceTextBox.Text.Trim();
            string gatherQuantity = gatherQuantityTextBox.Text.Trim();
            string gathererId = Utils.GetSelectedComboBoxItemValue(gathererComboBox);
            string gathererName = Utils.GetSelectedComboBoxItemText(gathererComboBox);
            string testDate = testDateTimePicker.Value.ToString("yyyy-MM-dd");
            string testerId = Utils.GetSelectedComboBoxItemValue(testerComboBox);
            string testerName = Utils.GetSelectedComboBoxItemText(testerComboBox);
            string qyNote = qyNoteTextBox.Text.Trim();
            string startWorkerId = Utils.GetSelectedComboBoxItemValue(startWorkerComboBox);
            string startWorkerName = Utils.GetSelectedComboBoxItemText(startWorkerComboBox);
            string endWorkerId = Utils.GetSelectedComboBoxItemValue(endWorkerComboBox);
            string endWorkerName = Utils.GetSelectedComboBoxItemText(endWorkerComboBox);
            string checkerId = Utils.GetSelectedComboBoxItemValue(checkerComboBox);
            string checkerName = Utils.GetSelectedComboBoxItemText(checkerComboBox);
            string startWorkDate = startWorkDateTimePicker.Value.ToString("yyyy-MM-dd");
            string endWorkDate = endWorkDateTimePicker.Value.ToString("yyyy-MM-dd");
            string startWorkTime = string.Format("{0}:{1}", startHourComboBox.SelectedItem, startMinuteComboBox.SelectedItem);
            string endWorkTime = string.Format("{0}:{1}", endHourComboBox.SelectedItem, endMinuteComboBox.SelectedItem); 
            string logicalProductionQuantity = logicalProductionQuantityTextBox.Text.Trim();
            string logicalPadQuantity = logicalPadQuantityTextBox.Text.Trim();
            string logicalBagQuantity = logicalBagQuantityTextBox.Text.Trim();
            string realProductionQuantity = realProductionQuantityTextBox.Text.Trim();
            string realPadQuantity = realPadQuantityTextBox.Text.Trim();
            string realBagQuantity = realBagQuantityTextBox.Text.Trim();
            string specialNote = specialNoteTextBox.Text.Trim();
            string correctiveMeasure = correctiveMeasureTextBox.Text.Trim();
            string observationNote = observationNoteTextBox.Text.Trim();
            string numOfBag1 = numOfBag1TextBox.Text.Trim();
            string numOfBag2 = numOfBag2TextBox.Text.Trim();
            string numOfBag3 = numOfBag3TextBox.Text.Trim();
            string numOfBag4 = numOfBag4TextBox.Text.Trim();
            string numOfBag5 = numOfBag5TextBox.Text.Trim();

            if ("0" != DbHelper.GetValue(string.Format("EXEC IsExistedProductTest '{0}', '{1}'",
               productCode, manufactureSerialNumber), "isExisted", "0"))
            {
                MessageBox.Show("중복된 테스트가 존재합니다. 수정화면에서 진행해 주십시오.");
                return false;
            }

            string query = "EXEC InsertProductTestItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', ";
            query += "'{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', ";
            query += "'{20}', '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', '{27}', '{28}', '{29}', ";
            query += "'{30}', '{31}', '{32}', '{33}', '{34}', '{35}', '{36}', '{37}', '{38}', '{39}', ";
            query += "'{40}', '{41}', '{42}'";//, '{43}', '{44}' "; 

            query = string.Format(query,
                productCode,
                manufactureSerialNumber,
                manufactureDate,
                manufactureQuantity == "" ? "0" : manufactureQuantity,
                //productName,
                //productDesc,
                //fdaCode,
                //fdaName,
                //dosageForm,
                //property,
                //machine,
                //standardOnAbsorbedAmount,
                judgerId,
                judgerName,
                judgingDate,
                judgingResult,
                progressResult,
                receiptDate,
                gatherDate,
                gatherPlace,
                gatherQuantity == "" ? "0" : gatherQuantity,
                gathererId,
                gathererName,
                testDate,
                testerId,
                testerName,
                "", //qyNote,
                startWorkerId,
                startWorkerName,
                endWorkerId,
                endWorkerName,
                checkerId,
                checkerName,
                startWorkDate,
                endWorkDate,
                startWorkTime,
                endWorkTime,
                 logicalProductionQuantity == "" ? "0" : logicalProductionQuantity,
                logicalPadQuantity == "" ? "0" : logicalPadQuantity,
                logicalBagQuantity == "" ? "0" : logicalBagQuantity,
                realProductionQuantity == "" ? "0" : realProductionQuantity,
                realPadQuantity == "" ? "0" : realPadQuantity,
                realBagQuantity == "" ? "0" : realBagQuantity, 
                specialNote,
                correctiveMeasure,
                observationNote,
                numOfBag1 == "" ? "0" : numOfBag1,
                numOfBag2 == "" ? "0" : numOfBag2,
                numOfBag3 == "" ? "0" : numOfBag3,
                numOfBag4 == "" ? "0" : numOfBag4,
                //numOfBag5 == "" ? "0" : numOfBag5
                numOfBag5
                );

            long retVal = DbHelper.ExecuteScalar(query);
            if (retVal != -1)
            {
                judgingResult = judgingResult == "1" ? "적합" : "부적합";
                progressResult = progressResult == "1" ? "완료" : "진행중";

                _parent.productTestDataGridView.Rows.Add(false,
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
                //judgerId,
                judgerName,
                judgingDate,
                gatherDate,
                gatherPlace,
                gatherQuantity,
                //gathererId,
                gathererName,
                testDate,
                //testerId,
                testerName,
                qyNote,
                //startWorkerId,
                startWorkerName,
                //endWorkerId,
                endWorkerName,
                //checkerId,
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
                retVal);
                Utils.OddDataGridViewRow(_parent.productTestDataGridView);
                _parent.productTestDataGridView.Rows[_parent.productTestDataGridView.Rows.Count - 1].Selected = true;

                if (forTest == false)
                    MessageBox.Show("완제품 검사를 추가했습니다.");

                _currentTestId = retVal.ToString();
                return true;
            }
            else
            {
                MessageBox.Show("완제품 검사를 추가할 수 없습니다.");
                _currentTestId = "";
                return false;
            }
        }

        private string WorktimeConvert(ComboBox _time)
        {
            return (_time.SelectedItem == null) ? (_time.Text.Length == 1) ? string.Format("0{0}", _time.Text) : _time.Text 
                    : _time.SelectedItem.ToString();


        }

        private bool UpdateData(bool forTest = false)
        {
            string productCode = Utils.GetSelectedComboBoxItemValue(productCodeComboBox);
            string manufactureSerialNumber = manufactureSerialNumberTextBox.Text.Trim();
            string manufactureDate = manufactureDateTextBox.Text.Trim();
            string manufactureQuantity = manufactureQuantityTextBox.Text.Trim();
            string productName = productNameTextBox.Text.Trim();
            string productDesc = productDescTextBox.Text.Trim();
            string fdaCode = fdaCodeTextBox.Text.Trim();
            string fdaName = fdaNameTextBox.Text.Trim();
            string dosageForm = dosageFormTextBox.Text.Trim();
            string property = propertyTextBox.Text.Trim();
            string machine = (machineComboBox.SelectedItem as ComboBoxItem).Text.Trim();
            string standardOnAbsorbedAmount = standardOnAbsorbedAmountTextBox.Text.Trim();
            string judgerId = Utils.GetSelectedComboBoxItemValue(judgerComboBox);
            string judgerName = Utils.GetSelectedComboBoxItemText(judgerComboBox);
            string judgingDate = judgingDateTimePicker.Value.ToString("yyyy-MM-dd");
            string judgingResult = Utils.GetSelectedComboBoxItemValue(judgingResultComboBox);
            string progressResult = Utils.GetSelectedComboBoxItemValue(progressResultComboBox);
            string receiptDate = receiptDateTimePicker.Value.ToString("yyyy-MM-dd");
            string gatherDate = gatherDateTimePicker.Value.ToString("yyyy-MM-dd");
            string gatherPlace = gatherPlaceTextBox.Text.Trim();
            string gatherQuantity = gatherQuantityTextBox.Text.Trim();
            string gathererId = Utils.GetSelectedComboBoxItemValue(gathererComboBox);
            string gathererName = Utils.GetSelectedComboBoxItemText(gathererComboBox);
            string testDate = testDateTimePicker.Value.ToString("yyyy-MM-dd");
            string testerId = Utils.GetSelectedComboBoxItemValue(testerComboBox);
            string testerName = Utils.GetSelectedComboBoxItemText(testerComboBox);
            string qyNote = qyNoteTextBox.Text.Trim();
            string startWorkerId = Utils.GetSelectedComboBoxItemValue(startWorkerComboBox);
            string startWorkerName = Utils.GetSelectedComboBoxItemText(startWorkerComboBox);
            string endWorkerId = Utils.GetSelectedComboBoxItemValue(endWorkerComboBox);
            string endWorkerName = Utils.GetSelectedComboBoxItemText(endWorkerComboBox);
            string checkerId = Utils.GetSelectedComboBoxItemValue(checkerComboBox);
            string checkerName = Utils.GetSelectedComboBoxItemText(checkerComboBox);
            string startWorkDate = startWorkDateTimePicker.Value.ToString("yyyy-MM-dd");
            string endWorkDate = endWorkDateTimePicker.Value.ToString("yyyy-MM-dd");
            string startWorkTime = string.Format("{0}:{1}", WorktimeConvert(startHourComboBox), WorktimeConvert(startMinuteComboBox));
            string endWorkTime = string.Format("{0}:{1}", WorktimeConvert(endHourComboBox), WorktimeConvert(endMinuteComboBox));
            string logicalProductionQuantity = logicalProductionQuantityTextBox.Text.Trim();
            string logicalPadQuantity = logicalPadQuantityTextBox.Text.Trim();
            string logicalBagQuantity = logicalBagQuantityTextBox.Text.Trim();
            string realProductionQuantity = realProductionQuantityTextBox.Text.Trim();
            string realPadQuantity = realPadQuantityTextBox.Text.Trim();
            string realBagQuantity = realBagQuantityTextBox.Text.Trim();
            string specialNote = specialNoteTextBox.Text.Trim();
            string correctiveMeasure = correctiveMeasureTextBox.Text.Trim();
            string observationNote = observationNoteTextBox.Text.Trim();
            string numOfBag1 = numOfBag1TextBox.Text.Trim();
            string numOfBag2 = numOfBag2TextBox.Text.Trim();
            string numOfBag3 = numOfBag3TextBox.Text.Trim();
            string numOfBag4 = numOfBag4TextBox.Text.Trim();
            string numOfBag5 = numOfBag5TextBox.Text.Trim();

            string query = "EXEC UpdateProductTestItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', ";
            query += "'{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', ";
            query += "'{20}', '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', '{27}', '{28}', '{29}', ";
            query += "'{30}', '{31}', '{32}', '{33}', '{34}', '{35}', '{36}', '{37}', '{38}', '{39}', ";
            query += "'{40}', '{41}', '{42}', '{43}'"; //, '{44}', '{45}'"; 
            query = string.Format(query,
                 productCode,
                manufactureSerialNumber,
                manufactureDate,
                manufactureQuantity == "" ? "0" : manufactureQuantity,
                //productName,
                //productDesc,
                //fdaCode,
                //fdaName,
                //dosageForm,
                //property,
                //machine,
                //standardOnAbsorbedAmount,
                judgerId,
                judgerName,
                judgingDate,
                judgingResult,
                progressResult,
                receiptDate,
                gatherDate,
                gatherPlace,
                gatherQuantity == "" ? "0" : gatherQuantity,
                gathererId,
                gathererName,
                testDate,
                testerId,
                testerName,
                "", //qyNote,
                startWorkerId,
                startWorkerName,
                endWorkerId,
                endWorkerName,
                checkerId,
                checkerName,
                startWorkDate,
                endWorkDate,
                startWorkTime,
                endWorkTime,
                 logicalProductionQuantity == "" ? "0" : logicalProductionQuantity,
                logicalPadQuantity == "" ? "0" : logicalPadQuantity,
                logicalBagQuantity == "" ? "0" : logicalBagQuantity,
                realProductionQuantity == "" ? "0" : realProductionQuantity,
                realPadQuantity == "" ? "0" : realPadQuantity,
                realBagQuantity == "" ? "0" : realBagQuantity, 
                specialNote,
                correctiveMeasure,
                observationNote,
                numOfBag1 == "" ? "0" : numOfBag1,
                numOfBag2 == "" ? "0" : numOfBag2,
                numOfBag3 == "" ? "0" : numOfBag3,
                numOfBag4 == "" ? "0" : numOfBag4,
                //numOfBag5 == "" ? "0" : numOfBag5,
                numOfBag5,
                      _currentTestId
                );

            long retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal != -1)
            {
                int selectedRowIndex = _parent.productTestDataGridView.SelectedRows[0].Index;
                judgingResult = judgingResult == "1" ? "적합" : "부적합";
                progressResult = progressResult == "1" ? "완료" : "진행중";

                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.productCode].Value = productCode;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.manufactureSerialNumber].Value = manufactureSerialNumber;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.manufactureDate].Value = manufactureDate;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.manufactureQuantity].Value = manufactureQuantity;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.productName].Value = productName;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.productDesc].Value = productDesc;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.fdaCode].Value = fdaCode;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.fdaName].Value = fdaName;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.dosageForm].Value = dosageForm;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.property].Value = property;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.machine].Value = machine;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.standardOnAbsorbedAmount].Value = standardOnAbsorbedAmount;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.judgerName].Value = judgerName;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.judgingDate].Value = judgingDate;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.judgingResult].Value = judgingResult;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.progressResult].Value = progressResult;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.receiptDate].Value = receiptDate;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.gatherDate].Value = gatherDate;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.gatherPlace].Value = gatherPlace;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.gatherQuantity].Value = gatherQuantity;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.gathererName].Value = gatherQuantity;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.testDate].Value = testDate;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.testerName].Value = testerName;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.qyNote].Value = qyNote;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.startWorkerName].Value = startWorkerName;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.endWorkerName].Value = endWorkerName;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.checkerName].Value = checkerName;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.startWorkDate].Value = startWorkDate;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.endWorkDate].Value = endWorkDate;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.startWorkTime].Value = startWorkTime;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.endWorkTime].Value = endWorkTime;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.logicalProductionQuantity].Value = logicalProductionQuantity;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.logicalPadQuantity].Value = logicalPadQuantity;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.logicalBagQuantity].Value = logicalBagQuantity;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.realProductionQuantity].Value = realProductionQuantity;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.realPadQuantity].Value = realPadQuantity;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.realBagQuantity].Value = realBagQuantity;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.specialNote].Value = specialNote;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.correctiveMeasure].Value = correctiveMeasure;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.observationNote].Value = observationNote;

                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.numOfBag1].Value = numOfBag1;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.numOfBag2].Value = numOfBag2;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.numOfBag3].Value = numOfBag3;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.numOfBag4].Value = numOfBag4;
                _parent.productTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ProductTest.ProductTestControl.eProductTestList.numOfBag5].Value = numOfBag5;

                if (forTest == false)
                    MessageBox.Show("완제품 검사를 수정했습니다.");

                return true;
            }
            else
            {
                MessageBox.Show("완제품 검사를 수정할 수 없습니다.");
                return false;
            }
        }

        private bool CheckRequiredItems()
        {
            if (machineComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("기계를 선택해 주십시오.");
                machineComboBox.Focus();
                return false;
            }


            if (productCodeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("제품코드를 선택해 주십시오.");
                productCodeComboBox.Focus(); 
                return false;
            }

            if (manufactureSerialNumberTextBox.Text.Trim() == "")
            {
                MessageBox.Show("제조번호를 입력해 주십시오.");
                manufactureSerialNumberTextBox.Focus();
                return false;
            }

            return true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void runQualitylTestButton_Click(object sender, EventArgs e)
        {
            if (_saveMode == "ADD" && DialogResult.No == MessageBox.Show("현재 완제품 검사를 저장 후 품질관리 검사 실행을 진행할 수 있습니다. 완제품 검사를 저장하시겠습니까?",
              "완제품 검사 추가", MessageBoxButtons.YesNo))
                return;

            if (CheckRequiredItems() == false)
                return;

            if (_saveMode == "ADD")
            {
                if (AddData(true) == true)
                {
                    RunQualityTestForm form = new RunQualityTestForm(this);
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog(); 
                }
            }
            else
            {
                if (UpdateData(true) == true)
                {
                    RunQualityTestForm form = new RunQualityTestForm(this);
                    form.SetSaveMode("UPDATE");
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                }
            }

        }

        private void runManufactureTestButton_Click(object sender, EventArgs e)
        {
            if (_saveMode == "ADD" && DialogResult.No == MessageBox.Show("현재 완제품 검사를 저장 후 제조관리 검사 실행을 진행할 수 있습니다. 완제품 검사를 저장하시겠습니까?",
            "완제품 검사 추가", MessageBoxButtons.YesNo))
                return;

            if (CheckRequiredItems() == false)
                return;

            if (_saveMode == "ADD")
            {
                if (AddData(true) == true)
                {
                    RunManufactureTestForm form = new RunManufactureTestForm(_currentTestId);
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                }
            }
            else
            {
                if (UpdateData(true) == true)
                {
                    RunManufactureTestForm form = new RunManufactureTestForm(_currentTestId);
                    form.SetSaveMode("UPDATE");
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                }
            }
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
            standardOnAbsorbedAmountTextBox.Text = dataRow["standardOnAbsorbedAmount"].ToString();
            _productname = productCodeComboBox.Text;

        }

        public void SetSaveMode(string saveMode = "ADD")
        {
            _saveMode = saveMode;
        }

        public void SetCurrentTestId(string currentTestId)
        {
            _currentTestId = currentTestId;
        }


        private void monthCalendarButton_Click(object sender, EventArgs e)
        {
            if (productCodeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("제품코드를 먼저 선택해 주십시오.");
                return;
            }
            DateTimePickerForm form = new DateTimePickerForm(this);
            form.StartPosition = FormStartPosition.Manual;
            form.Location = PointToScreen(new Point(manufactureSerialNumberTextBox.Left, manufactureSerialNumberTextBox.Bottom));
            form.ShowDialog();
        }

        private void machineComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetProductCodeComboBox();
            ResetWorkerComboBox();
            ResetCheckerComboBox();
            if (machineComboBox.SelectedIndex != -1)
            {
                gatherPlaceTextBox.Text = (machineComboBox.SelectedItem as ComboBoxItem).Text;
                productCodeComboBox.Enabled = true;
            }
            else
            {
                gatherPlaceTextBox.Text = ""; 
                productCodeComboBox.Enabled = false;
            }
        }

        private void realPadQuantityTextBox_TextChanged(object sender, EventArgs e)
        {
            manufactureQuantityTextBox.Text = realPadQuantityTextBox.Text.Trim();
        }

        private void numOfBagTextBox_TextChanged(object sender, EventArgs e)
        {
            ResetQyNote();
        }

        private void ResetQyNote()
        {
            //qyNoteTextBox.Text = string.Format("{0}'/{1}'/{2}'/{3}'/{4}'",
            //    numOfBag1TextBox.Text, numOfBag2TextBox.Text, numOfBag3TextBox.Text, numOfBag4TextBox.Text, numOfBag5TextBox.Text);

            string numOfBag1 = numOfBag1TextBox.Text.Trim();  
            string numOfBag2 = numOfBag2TextBox.Text.Trim();  
            string numOfBag3 = numOfBag3TextBox.Text.Trim();  
            string numOfBag4 = numOfBag4TextBox.Text.Trim();  
            string numOfBag5 = numOfBag5TextBox.Text.Trim();

            string qyNote = "";

            if (numOfBag1 != "" && numOfBag1 != "0")
                qyNote += (numOfBag1 + "'/");
            if (numOfBag2 != "" && numOfBag2 != "0")
                qyNote += (numOfBag2 + "'/");
            if (numOfBag3 != "" && numOfBag3 != "0")
                qyNote += (numOfBag3 + "'/");
            if (numOfBag4 != "" && numOfBag4 != "0")
                qyNote += (numOfBag4 + "'/");
            if (numOfBag5 != "" && numOfBag5 != "0")
                qyNote += (numOfBag5 + "/");

            if (qyNote.Length != 0)
                qyNoteTextBox.Text = qyNote.Substring(0, qyNote.Length - 1);
        }


        private void ProductCodeQuickSearch_Click(object sender, EventArgs e)
        {
            if (productCodeComboBox.Enabled & productQuickSearchCheckbox.Checked)
            {
                ResetProductCodeComboBox();
            }
        }
  
        private void judgingDateTimePicker_Validating(object sender, EventArgs e)
        {
            gatherDateTimePicker.Value = judgingDateTimePicker.Value;
            testDateTimePicker.Value = judgingDateTimePicker.Value;
            receiptDateTimePicker.Value = judgingDateTimePicker.Value;
            startWorkDateTimePicker.Value = judgingDateTimePicker.Value;
            endWorkDateTimePicker.Value = judgingDateTimePicker.Value;
        }
    }

}
