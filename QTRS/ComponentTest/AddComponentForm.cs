using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.ComponentTest
{
    public partial class AddComponentTestForm : Form
    {
        public ComponentTestControl _parent = null;
        public string _currentTestId = "";
        private string _saveMode = "ADD"; // ADD, UPDATE

        public AddComponentTestForm(ComponentTestControl parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void AddComponentTestForm_Load(object sender, EventArgs e)
        {
            //this.Enabled = false;
            InitControls();
            ////this.Enabled = true;
        }

        private void InitControls()
        {
            warehousingDateTimePicker.Value = DateTime.Now;
            testDateTimePicker.Value = DateTime.Now;
            judgingDateTimePicker.Value = DateTime.Now;
            InitRawDatas();
            InitUsers();

            if (_saveMode == "UPDATE")
            {
                addDataButton.Text = "수정";
                this.Text = "원료 검사 수정";

                // 수정일 때는 수정 불가능
                warehousingDateTimePicker.Enabled = false;
                productAreaTypeComboBox.Enabled = false;
                componentCodeComboBox.Enabled = false;

                SetSelectedValues();
            }
            else
            {
                // 신규일 때는 수정 가능
                warehousingDateTimePicker.Enabled = true;
                productAreaTypeComboBox.Enabled = true;
                componentCodeComboBox.Enabled = true;
                componentNameTextBox.Enabled = true;
            }
        }

        private void SetSelectedValues()
        {
            string query = string.Format("EXEC SelectComponentTestItem '{0}' ", _currentTestId);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("원료 검사 데이터를 가져올 수 없습니다.");
                return;
            }

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            DataRow dataRow = dataSet.Tables[0].Rows[0];

            //string idx = dataRow["idx"].ToString();

            warehousingDateTimePicker.Value = Utils.GetDateTimeFormatFromObject(dataRow["warehousingDate"]);
            Utils.SelectComboBoxItem(componentCodeComboBox, dataRow["componentCode"].ToString());
            componentNameTextBox.Text = dataRow["componentName"].ToString();
            innerComponentNameTextBox.Text = dataRow["innerComponentName"].ToString();
            Utils.SelectComboBoxItem(productAreaTypeComboBox, dataRow["productAreaTypeId"].ToString());
            makerTextBox.Text = dataRow["maker"].ToString();
            lotNoTextBox.Text = dataRow["lotNo"].ToString();
            mainLotNoTextBox.Text = dataRow["mainLotNo"].ToString();
            noteTextBox.Text = dataRow["note"].ToString();
            purposeTextBox.Text = Utils.GetString(dataRow["purpose"]);
            testDateTimePicker.Value = Utils.GetDateTimeFormatFromObject(dataRow["testDate"]);
            Utils.SelectComboBoxItem(testerComboBox, Utils.GetString(dataRow["testerId"].ToString()));
            judgingDateTimePicker.Value = Utils.GetDateTimeFormatFromObject(dataRow["judgingDate"]);
            Utils.SelectComboBoxItem(judgerComboBox, Utils.GetString(dataRow["judgerId"].ToString()));
            Utils.SelectComboBoxItem(judgingResultComboBox, Utils.GetString(dataRow["judgingResult"].ToString()));
            Utils.SelectComboBoxItem(progressResultComboBox, Utils.GetString(dataRow["progressResult"].ToString()));
        }

        private void InitRawDatas()
        {

            ////
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

            productAreaTypeComboBox.SelectedIndex = -1;
            ////

            ////
            judgingResultComboBox.Items.Clear();
            judgingResultComboBox.Items.Add(new ComboBoxItem("부적합", "0"));
            judgingResultComboBox.Items.Add(new ComboBoxItem("적합", "1"));

            judgingResultComboBox.SelectedIndex = 0;
            ////

            progressResultComboBox.Items.Clear();
            progressResultComboBox.Items.Add(new ComboBoxItem("진행중", "0"));
            progressResultComboBox.Items.Add(new ComboBoxItem("완료", "1"));
            progressResultComboBox.SelectedIndex = 0;


            SelectComponentList();

        }
        private void InitUsers()
        {
            DataSet dataSet = DbHelper.SelectQuery("EXEC SelectUserList");
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("사용자 정보를 가져올 수 없습니다.");
                return;
            }

            testerComboBox.Items.Clear();
            //judgerComboBox.Items.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            //testerComboBox.Items.Add(new ComboBoxItem("선택", "-1"));
            //judgerComboBox.Items.Add(new ComboBoxItem("선택", "-1"));

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string id = dataRow["id"].ToString();
                string name = dataRow["name"].ToString();

                testerComboBox.Items.Add(new ComboBoxItem(name, id));
                //judgerComboBox.Items.Add(new ComboBoxItem(name, id));
            }

            testerComboBox.SelectedIndex = -1;
            //judgerComboBox.SelectedIndex = -1;


            // 판정자
            judgerComboBox.Items.Add(new ComboBoxItem(Global.configInfo.manufactureAdminName, Global.configInfo.manufactureAdminId));
            judgerComboBox.SelectedIndex = 0;

            // 검사자
            Utils.SelectComboBoxItem(testerComboBox, Global.loginInfo.id);
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

            if(retVal == true)
                Close();

            _parent.SelectComponentTestList();
        }

        private bool AddData(bool forTest = false)
        {
            string warehousingDate = warehousingDateTimePicker.Value.ToString("yyyy-MM-dd");
            string componentCode = Utils.GetSelectedComboBoxItemValue(componentCodeComboBox);
            string componentName = componentNameTextBox.Text.Trim();
            string innerComponentName = innerComponentNameTextBox.Text.Trim();
            string productAreaTypeId = Utils.GetSelectedComboBoxItemValue(productAreaTypeComboBox);
            string productAreaTypeName = Utils.GetSelectedComboBoxItemText(productAreaTypeComboBox);
            string maker = makerTextBox.Text.Trim();
            string lotNo = lotNoTextBox.Text.Trim();
            string mainLotNo = mainLotNoTextBox.Text.Trim();
            string note = noteTextBox.Text.Trim();
            string purpose = purposeTextBox.Text.Trim();
            string testDate = testDateTimePicker.Value.ToString("yyyy-MM-dd");
            string testerId = Utils.GetSelectedComboBoxItemValue(testerComboBox);
            string testerName = Utils.GetSelectedComboBoxItemText(testerComboBox);
            string judgingDate = judgingDateTimePicker.Value.ToString("yyyy-MM-dd");
            string judgerId = Utils.GetSelectedComboBoxItemValue(judgerComboBox);
            string judgerName = Utils.GetSelectedComboBoxItemText(judgerComboBox);
            string judgingResult = Utils.GetSelectedComboBoxItemValue(judgingResultComboBox);
            string progressResult = Utils.GetSelectedComboBoxItemValue(progressResultComboBox);

            //if("0" != DbHelper.GetValue(string.Format("EXEC IsExistedComponentTest '{0}', '{1}', '{2}'", 
                //warehousingDate, componentCode, productAreaTypeId), "isExisted", "0"))
            if(0 < DbHelper.GetLongValue(string.Format("EXEC IsExistedComponentTest '{0}', '{1}'", 
                //warehousingDate, componentCode), "isExisted", -1))
                componentCode, mainLotNo), "isExisted", -1))
            {
                MessageBox.Show("중복된 테스트가 존재합니다. 수정화면에서 진행해 주십시오.");
                return false;
            }

            string query = "EXEC InsertComponentTestItem {0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}' ";
            query = string.Format(query,
                "NULL",
                warehousingDate,
                componentCode,
                componentName,
                innerComponentName,
                productAreaTypeId,
                productAreaTypeName,
                maker,
                lotNo,
                mainLotNo,
                note,
                purpose,
                testDate,
                testerId,
                testerName,
                judgingDate,
                judgerId,
                judgerName,
                judgingResult,
                progressResult);

            long retVal = DbHelper.ExecuteScalar(query);
            if (retVal != -1)
            {
                judgingResult = judgingResult == "1" ? "적합" : "부적합";
                progressResult = progressResult == "1" ? "완료" : "진행중";

                _parent.componentTestDataGridView.Rows.Add(
                    false, judgingResult, progressResult, warehousingDate, componentCode, componentName, innerComponentName, 
                    productAreaTypeName, maker, lotNo, mainLotNo, note,
                    purpose, testDate, testerName, judgingDate, judgerName, "원료검사", retVal);

                Utils.OddDataGridViewRow(_parent.componentTestDataGridView);
                _parent.componentTestDataGridView.Rows[_parent.componentTestDataGridView.Rows.Count - 1].Selected = true;

                _currentTestId = retVal.ToString();

                ////
                // 내부 원료명과 Main Lot이 같은 항목으로부터 테스트를 가져와서 현재 항목을 업데이트 한다. 
                query = string.Format("EXEC CopyComponentTestItemForInsert '{0}', '{1}', '{2}'", retVal, innerComponentName, mainLotNo);
                retVal = DbHelper.ExecuteNonQuery(query);

                for(int i=0; i< _parent.componentTestDataGridView.Rows.Count; i++)
                {
                    if(innerComponentName == _parent.componentTestDataGridView.Rows[i].Cells[(int)ComponentTestControl.eComponentTestList.innerComponentName].Value.ToString().Trim() && 
                        mainLotNo == _parent.componentTestDataGridView.Rows[i].Cells[(int)ComponentTestControl.eComponentTestList.mainLotNo].Value.ToString().Trim())
                    {
                        _parent.componentTestDataGridView.Rows[i].Cells[(int)ComponentTestControl.eComponentTestList.testDate].Value = testDate; 
                        _parent.componentTestDataGridView.Rows[i].Cells[(int)ComponentTestControl.eComponentTestList.testerName].Value = testerName; 
                        _parent.componentTestDataGridView.Rows[i].Cells[(int)ComponentTestControl.eComponentTestList.judgingDate].Value = judgingDate; 
                        _parent.componentTestDataGridView.Rows[i].Cells[(int)ComponentTestControl.eComponentTestList.judgerName].Value = judgerName; 
                        _parent.componentTestDataGridView.Rows[i].Cells[(int)ComponentTestControl.eComponentTestList.judgingResult].Value = judgingResult; 
                        _parent.componentTestDataGridView.Rows[i].Cells[(int)ComponentTestControl.eComponentTestList.progressResult].Value = progressResult; 
                    }
                }
                ////

                if (forTest == false)
                    MessageBox.Show("원료 검사를 추가했습니다.");

                return true; 
            }
            else
            {
                MessageBox.Show("원료 검사를 추가 할 수 없습니다.");
                _currentTestId = ""; 
                return false; 
            }
        }

        private bool UpdateData(bool forTest = false)
        {
            string warehousingDate = warehousingDateTimePicker.Value.ToString("yyyy-MM-dd");
            string componentCode = Utils.GetSelectedComboBoxItemValue(componentCodeComboBox);
            string componentName = componentNameTextBox.Text.Trim();
            string innerComponentName = innerComponentNameTextBox.Text.Trim();
            string productAreaTypeId = Utils.GetSelectedComboBoxItemValue(productAreaTypeComboBox);
            string productAreaTypeName = Utils.GetSelectedComboBoxItemText(productAreaTypeComboBox);
            string maker = makerTextBox.Text.Trim();
            string lotNo = lotNoTextBox.Text.Trim();
            string mainLotNo = mainLotNoTextBox.Text.Trim();
            string note = noteTextBox.Text.Trim();
            string purpose = purposeTextBox.Text.Trim();
            string testDate = testDateTimePicker.Value.ToString("yyyy-MM-dd");
            string testerId = Utils.GetSelectedComboBoxItemValue(testerComboBox);
            string testerName = Utils.GetSelectedComboBoxItemText(testerComboBox);
            string judgingDate = judgingDateTimePicker.Value.ToString("yyyy-MM-dd");
            string judgerId = Utils.GetSelectedComboBoxItemValue(judgerComboBox);
            string judgerName = Utils.GetSelectedComboBoxItemText(judgerComboBox);
            string judgingResult = Utils.GetSelectedComboBoxItemValue(judgingResultComboBox);
            string progressResult = Utils.GetSelectedComboBoxItemValue(progressResultComboBox);

            string query = "EXEC UpdateComponentTestItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}' ";
            query = string.Format(query,
                _currentTestId,
                warehousingDate,
                componentCode,
                componentName,
                innerComponentName,
                productAreaTypeId,
                productAreaTypeName,
                maker,
                lotNo,
                mainLotNo,
                note,
                purpose,
                testDate,
                testerId,
                testerName,
                judgingDate,
                judgerId,
                judgerName,
                judgingResult,
                progressResult);

            long retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal != -1)
            {
                int selectedRowIndex = _parent.componentTestDataGridView.SelectedRows[0].Index;
                judgingResult = judgingResult == "1" ? "적합" : "부적합";
                progressResult = progressResult == "1" ? "완료" : "진행중";

                _parent.componentTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ComponentTest.ComponentTestControl.eComponentTestList.warehousingDate].Value = warehousingDate;
                _parent.componentTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ComponentTest.ComponentTestControl.eComponentTestList.componentCode].Value = componentCode;
                _parent.componentTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ComponentTest.ComponentTestControl.eComponentTestList.componentName].Value = componentName;
                _parent.componentTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ComponentTest.ComponentTestControl.eComponentTestList.innerComponentName].Value = innerComponentName;
                _parent.componentTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ComponentTest.ComponentTestControl.eComponentTestList.productAreaTypeName].Value = productAreaTypeName;
                _parent.componentTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ComponentTest.ComponentTestControl.eComponentTestList.maker].Value = maker;
                _parent.componentTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ComponentTest.ComponentTestControl.eComponentTestList.lotNo].Value = lotNo;
                _parent.componentTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ComponentTest.ComponentTestControl.eComponentTestList.mainLotNo].Value = mainLotNo;
                _parent.componentTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ComponentTest.ComponentTestControl.eComponentTestList.note].Value = note;
                _parent.componentTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ComponentTest.ComponentTestControl.eComponentTestList.judgingResult].Value = judgingResult;
                _parent.componentTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ComponentTest.ComponentTestControl.eComponentTestList.progressResult].Value = progressResult;

                if (forTest == false)
                    MessageBox.Show("원료 검사를 수정했습니다.");


                ////
                // 내부 원료명과 Main Lot이 같은 테스트가 있으면 현재 테스트로 업데이트 한다.  
                query = string.Format("EXEC CopyComponentTestItemForUpdate '{0}', '{1}', '{2}'", _currentTestId, innerComponentName, mainLotNo);
                retVal = DbHelper.ExecuteNonQuery(query);

                for (int i = 0; i < _parent.componentTestDataGridView.Rows.Count; i++)
                {
                    if (innerComponentName == _parent.componentTestDataGridView.Rows[i].Cells[(int)ComponentTestControl.eComponentTestList.innerComponentName].Value.ToString().Trim() &&
                        mainLotNo == _parent.componentTestDataGridView.Rows[i].Cells[(int)ComponentTestControl.eComponentTestList.mainLotNo].Value.ToString().Trim())
                    {
                        _parent.componentTestDataGridView.Rows[i].Cells[(int)ComponentTestControl.eComponentTestList.testDate].Value = testDate;
                        _parent.componentTestDataGridView.Rows[i].Cells[(int)ComponentTestControl.eComponentTestList.testerName].Value = testerName;
                        _parent.componentTestDataGridView.Rows[i].Cells[(int)ComponentTestControl.eComponentTestList.judgingDate].Value = judgingDate;
                        _parent.componentTestDataGridView.Rows[i].Cells[(int)ComponentTestControl.eComponentTestList.judgerName].Value = judgerName;
                        _parent.componentTestDataGridView.Rows[i].Cells[(int)ComponentTestControl.eComponentTestList.judgingResult].Value = judgingResult;
                        _parent.componentTestDataGridView.Rows[i].Cells[(int)ComponentTestControl.eComponentTestList.progressResult].Value = progressResult;
                    }
                }
                ////

                return true;
            }
            else
            {
                MessageBox.Show("원료 수정 할 수 없습니다.");
                return false;
            }
        }

        private bool CheckRequiredItems()
        {
            string componentCode = Utils.GetSelectedComboBoxItemValue(componentCodeComboBox);
            string mainLotNo = mainLotNoTextBox.Text.Trim(); 
            
            //if (warehousingDate == "" || productAreaTypeCode == "0" || componentCode == "")
            if (productAreaTypeComboBox.SelectedIndex == -1 || componentCode == "" || mainLotNo == "")
            {
                MessageBox.Show("필수 항목을 모두 입력해 주십시오.");
                return false;
            }

            return true;
        }


        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void runComponentTestButton_Click(object sender, EventArgs e)
        {
            if (_saveMode == "ADD" && DialogResult.No == MessageBox.Show("현재 원료 검사를 저장 후 원료 검사 실행을 진행할 수 있습니다. 원료 검사를 저장하시겠습니까?", 
                "원료 검사 추가", MessageBoxButtons.YesNo))
                return;

            if (CheckRequiredItems() == false)
                return;

            if (_saveMode == "ADD")
            {
                if (AddData(true) == true)
                {
                    RunComponentTestForm form = new RunComponentTestForm(this, _currentTestId, (componentCodeComboBox.SelectedItem as ComboBoxItem).Value.ToString());
                    form.SetSaveMode("ADD");
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                }
            }
            else
            {
                if (UpdateData(true) == true)
                {
                    RunComponentTestForm form = new RunComponentTestForm(this, _currentTestId, (componentCodeComboBox.SelectedItem as ComboBoxItem).Value.ToString());
                    form.SetSaveMode("UPDATE");
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                }
            }
        }

        private void SelectComponentList()
        {
            DataSet dataSet = DbHelper.SelectQuery("EXEC SelectComponentList '', ''");
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("원료 정보를 가져올 수 없습니다.");
                ////this.Enabled = true;
                return;
            }

            componentCodeComboBox.Items.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
            {
                ////this.Enabled = true;
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                componentCodeComboBox.Items.Add(new ComboBoxItem(dataRow["componentCode"] + " | " + dataRow["componentName"].ToString(), dataRow["componentCode"]));
            }

            componentCodeComboBox.SelectedIndex = -1;
            componentCodeComboBox.Enabled = true;

        }

        private void componentCodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (componentCodeComboBox.SelectedIndex == -1)
                return;

            // 원료명
            string componentCode = Utils.GetSelectedComboBoxItemValue(componentCodeComboBox);
            string componentName = Utils.GetSelectedComboBoxItemText(componentCodeComboBox);
            componentNameTextBox.Text = componentName.Split('|')[1].Trim();

            // 내부 원료명 가져오기
            innerComponentNameTextBox.Text = "";
            DataSet dataSet = DbHelper.SelectQuery(string.Format("EXEC SelectInnerComponentName '{0}'", componentCode));
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("내부원료이름을 가져올 수 없습니다.");
                ////this.Enabled = true;
                return;
            }

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            innerComponentNameTextBox.Text = dataSet.Tables[0].Rows[0]["innerComponentName"].ToString();
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
