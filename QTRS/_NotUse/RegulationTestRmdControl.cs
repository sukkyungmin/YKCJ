using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS
{
    public partial class RegulationTestRmdControl : UserControl
    {
        private bool isLoaded = false;
        public RegulationTestRmdControl()
        {
            InitializeComponent();
        }

        private void RegulationTestRmdControl_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void addRegulationTestRmdButton_Click(object sender, EventArgs e)
        {
            AddRegulationTestRmdForm form = new AddRegulationTestRmdForm();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void InitControls()
        {
            InitMaterialTypeComboBox();
            InitDataGrid();
            isLoaded = true;
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

            SelectRegulationTestRmdList();
        }

        private void InitDataGrid()
        {
            DataGridView dataGridView = rmdDataGridView;

            
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

        private void searchButton_Click(object sender, EventArgs e)
        {
            SelectRegulationTestRmdList();
        }

        private void SelectRegulationTestRmdList()
        {
            string productAreaTypeId = ""; 
            string componentCode = "";

            productAreaTypeId = Utils.GetSelectedComboBoxItemValue(materialTypeComboBox);
            componentCode = componentCodeTextBox.Text.Trim();

            string query = string.Format("EXEC SelectRegulationTestRmdList {0}, '{1}'", productAreaTypeId, componentCode);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("법규실험 원료 기준 데이터를 가져올 수 없습니다.");
                return;
            }

            rmdDataGridView.Rows.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string idx = dataRow["idx"].ToString();
                //string iirIdx = dataRow["iirIdx"].ToString();
                //string productAreaTypeId = dataRow["productAreaTypeId"].ToString();
                string materialTypeName = dataRow["materialTypeName"].ToString();
                string warehousingDate = dataRow["warehousingDate"].ToString().Substring(0, 10);
                componentCode = dataRow["componentCode"].ToString();
                string itemDesc = dataRow["itemDesc"].ToString();
                string maker = dataRow["maker"].ToString();
                string lotNo = dataRow["lotNo"].ToString();
                /*
                string mainLotNo = dataRow["mainLotNo"].ToString();
                string baffleReport = dataRow["baffleReport"].ToString();
                string sampleOx = dataRow["sampleOx"].ToString();
                string ash = dataRow["ash"].ToString();
                string wvtr = dataRow["wvtr"].ToString();
                string basicWeight = dataRow["basicWeight"].ToString();
                string gravimeter = dataRow["gravimeter"].ToString();
                string viscosity = dataRow["viscosity"].ToString();
                string softeningPoint = dataRow["softeningPoint"].ToString();
                string ph = dataRow["ph"].ToString();
                string tensileStrength = dataRow["tensileStrength"].ToString();
                string thickness = dataRow["thickness"].ToString();
                string note = dataRow["note"].ToString();
                */

                //sampleOx = sampleOx == "1" ? "O" : "X";
                rmdDataGridView.Rows.Add(false, materialTypeName, warehousingDate, componentCode, itemDesc, maker, lotNo, idx);
                Utils.OddDataGridViewRow(rmdDataGridView);
            }

            rmdDataGridView.ClearSelection();
        }

        public void GetData()
        {
            if (isLoaded == true)
                SelectRegulationTestRmdList();
        }
    }
}
