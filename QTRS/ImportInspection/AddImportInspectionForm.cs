using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace QTRS.ImportInspection
{
    public partial class AddImportInspectionForm : Form
    {
        private ImportInspectionControl _parent = null;

        public AddImportInspectionForm(ImportInspectionControl parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void AddImportInspectionForm_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void InitControls()
        {
            warehousingDateTimePicker.Value = DateTime.Now;
            InitRawDatas();
        }

        private void InitRawDatas()
        {
            //this.Enabled = false;

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


            SelectComponentList();

            ////this.Enabled = true;
        }

       

        private void SelectComponentList()
        {
            //this.Enabled = false;

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

            ////this.Enabled = true;
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

        private void addDataButton_Click(object sender, EventArgs e)
        {
            if (CheckRequiredItems() == false)
                return;

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

            long retVal = DbHelper.GetLongValue(string.Format("EXEC IsExistedImportInspectionItem '{0}', '{1}', '{2}'", warehousingDate, componentCode, mainLotNo), "isExisted", -1);
            if(retVal > 0)
            {
                MessageBox.Show("수입검사에 중복된 데이터가 존재합니다.");
                return; 
            }

          
            string query = "EXEC InsertImportInspectionItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ";
            query = string.Format(query,
                warehousingDate,
                    componentCode,
                     componentName,
                     innerComponentName,
                     productAreaTypeId,
                     productAreaTypeName,
                     maker,
                     lotNo,
                     mainLotNo,
                     note);
            retVal = (long)DbHelper.ExecuteScalar(query);
            if (retVal == -1) 
            {
                MessageBox.Show("수입검사를 추가할 수 없습니다.");
                return;
            }
            else
            {
                _parent.importInspectionDataGridView.Rows.Add(false, warehousingDate, componentCode, componentName, innerComponentName, productAreaTypeName, maker, lotNo, mainLotNo, note, retVal);
                Utils.OddDataGridViewRow(_parent.importInspectionDataGridView);

                const string pattern = "[0-9][0-9]G[(]|[0-9][0-9][0-9]G[(]|[0-9][0-9].[0-9]G[(]|[0-9][0-9][0-9].[0-9]G[(]|[0-9][0-9]GSM|[0-9][0-9][0-9]GSM|[0-9][0-9].[0-9]GSM|[0-9][0-9][0-9].[0-9]GSM";
                Match match = Regex.Match(componentName, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);

                string matchcomponentName = match.ToString();

                retVal = DbHelper.GetLongValue(string.Format("EXEC IsExistedComponentTest '{0}','{1}' ,'{2}'", matchcomponentName, innerComponentName, mainLotNo), "isExisted", -1);
                if (retVal > 0)
                {
                    MessageBox.Show("원료입고 목록에는 추가 되었으나, 원료검사 목록에는 중복되어 추가되지 않습니다.");
                    return;
                }


                query = "EXEC InsertComponentTestItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '', '{11}', '', '', '{11}', '', '', 0, 0 ";
                query = string.Format(query,
                  retVal,
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
                  warehousingDate);
                retVal = (long)DbHelper.ExecuteScalar(query);
                if (retVal == -1)
                {
                    MessageBox.Show("수입검사에는 추가됐으나 원료검사 추가 중 에러가 발생했습니다.");
                }
                else
                {
                    ////
                    // 내부 원료명과 Main Lot이 같은 항목으로부터 테스트를 가져와서 현재 항목을 업데이트 한다. 
                    query = string.Format("EXEC CopyComponentTestItemForInsert '{0}', '{1}', '{2}'", retVal, innerComponentName, mainLotNo);
                    retVal = DbHelper.ExecuteNonQuery(query);
                    ////
                }
            }

            this.Close();
        }

        private bool CheckRequiredItems()
        {
            string warehousingDate = warehousingDateTimePicker.Value.ToString("yyyy-MM-dd");
            string componentCode = Utils.GetSelectedComboBoxItemValue(componentCodeComboBox);
            string mainLot = mainLotNoTextBox.Text.Trim(); 

            if (warehousingDate == "" || productAreaTypeComboBox.SelectedIndex == -1 || componentCode == "" || mainLot == "0")
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

       
    }
}
