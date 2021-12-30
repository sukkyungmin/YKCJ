using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS
{
    public partial class AddImportInspectionRmdForm : Form
    {
        private ImportInspectionRmdControl _parent = null; 
        public AddImportInspectionRmdForm(ImportInspectionRmdControl parent)
        {
            InitializeComponent();
            _parent = parent; 
        }

        private void AddImportInspectionRmdForm_Load(object sender, EventArgs e)
        {
            InitControls(); 
        }

        private void InitControls()
        {
            warehousingDateTimePicker.Value = DateTime.Now;
            InitMaterialTypeComboBox(); 
            InitSampleOxComboBox();
        }

        private void InitMaterialTypeComboBox()
        {
            string query = "EXEC SelectCodeList 1000";

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("자제코드 데이터를 가져올 수 없습니다.");
                return;
            }

            materialTypeComboBox.Items.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            //materialTypeComboBox.Items.Add(new ComboBoxItem("선택", -1));
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                materialTypeComboBox.Items.Add(new ComboBoxItem(dataRow["codeName"].ToString(), dataRow["codeId"]));
            }

            materialTypeComboBox.SelectedIndex = -1;

        }

        private void InitSampleOxComboBox()
        {
            sampleOxComboBox.Items.Add(new ComboBoxItem("O", 1));
            sampleOxComboBox.Items.Add(new ComboBoxItem("X", 0));

            sampleOxComboBox.SelectedIndex = -1; 
        }

        private void addDataButton_Click(object sender, EventArgs e)
        {
            string productAreaTypeId = Utils.GetSelectedComboBoxItemValue(materialTypeComboBox);
            string materialTypeName = Utils.GetSelectedComboBoxItemText(materialTypeComboBox); 
            string warehousingDate = warehousingDateTimePicker.Value.ToString("yyyy-MM-dd");
            string componentCode = componentCodeTextBox.Text.Trim();
            string itemDesc = itemDescTextBox.Text.Trim();
            string maker = makerTextBox.Text.Trim();
            string lotNo = lotNoTextBox.Text.Trim();
            string mainLotNo = mainLotNoTextBox.Text.Trim();
            string baffleReport = baffleReportTextBox.Text.Trim();
            string sampleOx = Utils.GetSelectedComboBoxItemValue(sampleOxComboBox, "n");
            string ash = ashTextBox.Text.Trim();
            string wvtr = wvtrTextBox.Text.Trim() + "/" + wvtr2TextBox.Text.Trim();
            string basicWeight = basicWeightTextBox.Text.Trim() + "/" + basicWeight2TextBox.Text.Trim();
            string gravimeter = gravimeterTextBox.Text.Trim();
            string viscosity = viscosityTextBox.Text.Trim();
            string softeningPoint = softeningPointTextBox.Text.Trim();
            string ph = phTextBox.Text.Trim();
            string tensileStrength = tensileStrengthTextBox.Text.Trim();
            string thickness = thicknessTextBox.Text.Trim();
            string note = noteTextBox.Text.Trim();

            string query = "EXEC InsertImportInspectionRmdItem " +
                "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9}," +
                "{10},'{11}','{12}',{13},{14},{15},{16},{17},{18},'{19}' ";
            query = string.Format(query, 
                 productAreaTypeId ,
             materialTypeName ,
             warehousingDate ,
             componentCode,
             itemDesc,
             maker,
             lotNo,
             mainLotNo,
             baffleReport,
             sampleOx,
             ash,
             wvtr,
             basicWeight,
             gravimeter,
             viscosity,
             softeningPoint,
             ph,
             tensileStrength,
             thickness,
             note);
            long retVal = DbHelper.ExecuteNonQuery(query);
            if(retVal != -1)
            {
                sampleOx = sampleOx == "1" ? "O" : "X";
                _parent.rmdDataGridView.Rows.Add(false, materialTypeName, warehousingDate, componentCode, itemDesc, maker, lotNo,
                    mainLotNo, baffleReport, sampleOx, ash, wvtr, basicWeight, gravimeter, viscosity,
                    softeningPoint, ph, tensileStrength, thickness, note, retVal);
                Utils.OddDataGridViewRow(_parent.rmdDataGridView);
            }

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
