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
    public partial class RegulationTestCpControl : UserControl
    {
        private bool isLoaded = false;
        public RegulationTestCpControl()
        {
            InitializeComponent();
        }

        private void RegulationTestCpControl_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void addRegulationTestCpButton_Click(object sender, EventArgs e)
        {
            AddRegulationTestCpForm form = new AddRegulationTestCpForm();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void InitControls()
        {
            InitDataGrid();
            SelectRegulationTestCpList();
            isLoaded = true;
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
            SelectRegulationTestCpList();
        }

        private void SelectRegulationTestCpList()
        {
            string productName = "";  
            string manufactureNumber = "";

            productName = productNameTextBox.Text.Trim();
            manufactureNumber = manufactureNumberTextBox.Text.Trim();

            string query = string.Format("EXEC SelectRegulationTestCpList '{0}', '{1}'", productName, manufactureNumber);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("법규실험 완제품 기준 데이터를 가져올 수 없습니다.");
                return;
            }

            rmdDataGridView.Rows.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string idx = dataRow["idx"].ToString();
                productName = dataRow["productName"].ToString();
                manufactureNumber = dataRow["manufactureNumber"].ToString();
                string manufactureDate = dataRow["manufactureDate"].ToString().Substring(0, 10);
                string manufactureQuantity = dataRow["manufactureQuantity"].ToString();
                string dosageForm = dataRow["dosageForm"].ToString();
                string property = dataRow["property"].ToString();

                rmdDataGridView.Rows.Add(false, productName, manufactureNumber, manufactureDate, manufactureQuantity, dosageForm, property, idx);
                Utils.OddDataGridViewRow(rmdDataGridView);
            }

            rmdDataGridView.ClearSelection();
        }

        public void GetData()
        {
            if (isLoaded == true)
                SelectRegulationTestCpList();
        }
    }
}
