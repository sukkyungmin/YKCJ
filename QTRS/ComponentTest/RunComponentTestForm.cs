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
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Microsoft.Office.Core;

namespace QTRS.ComponentTest
{
    public partial class RunComponentTestForm : Form
    {
        public string _currentTestId = ""; // ComponentTests.idx 
        public string _currentComponentCode = ""; 
        private string _saveMode = "ADD";
        private bool _totalCompatibilityOx = false;
        DataRow _componentDataRow = null; 
        public enum eComponentTestResultList
        {
            testItem, testMethod, testStandard, testScore, compatibilityOx, testItemId, idx
        }

        // Image DataGridView
        enum eImageDataList { NO, CHECKBOX, IMAGE, FILE_NAME, FILE_MEMO, FILE_PATH, ID }
        enum eFileDataList { NO, CHECKBOX, FILE_NAME, FILE_MEMO, FILE_PATH, ID }

        private const int GRID_COL_WIDTH = 100;
        private const int GRID_ROW_HEIGHT = 32;
        private const int GRID_COLUMN_HEIGHT = 32;
        private const int GRID_IMAGE_ROW_HEIGHT = 100;

        private const int ROW_HEADER_CELL = 0;
        private AddComponentTestForm _parent = null;

        public RunComponentTestForm(AddComponentTestForm parent, string currentTestId, string currentComponentCode)
        {
            InitializeComponent();
            _parent = parent;
            _currentTestId = currentTestId;
            _currentComponentCode = currentComponentCode;
        }

        private void RunComponentTestForm_Load(object sender, EventArgs e)
        {
            InitControls();

            if (_saveMode == "UPDATE")
                SetSelectedValues();
        }

        public void SetSaveMode(string saveMode = "ADD")
        {
            _saveMode = saveMode;
        }

        private void InitControls()
        {
            InitComponentTestDataGridView(); 
            InitTestMethodMap(); 
            InitImageDataGridView();
            InitFileDataGridView();
        }

        private void InitTestMethodMap()
        {
            string query = string.Format("EXEC SelectComponentItem '{0}'", _currentComponentCode);
            DataSet componentDataSet = DbHelper.SelectQuery(query);
            if (componentDataSet == null || componentDataSet.Tables.Count == 0)
            {
                MessageBox.Show("시험방법 데이터를 가져올 수 없습니다.");
                return;
            }

            if (componentDataSet.Tables[0].Rows.Count == 0)
                return;
            _componentDataRow = componentDataSet.Tables[0].Rows[0];


            query = "EXEC SelectTestMethodList 0";

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("시험방법 데이터를 가져올 수 없습니다.");
                return;
            }

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string testItem = dataRow["testItem"].ToString();
                string testMethod = dataRow["testMethod"].ToString();
                string testStandard = dataRow["testStandard"].ToString();
                string testScore = dataRow["testScore"].ToString();
                string testItemId = dataRow["testItemId"].ToString();
                string idx = dataRow["idx"].ToString();

                string minItemId = "";
                string maxItemId = "";
                string normalItemId = ""; 

                if (testItemId == "pH")
                {
                    minItemId = "ph_Min";
                    maxItemId = "ph_Max";
                }
                else if (testItemId == "gravity_d20")
                {
                    minItemId = "gravity_d20_Min";
                    maxItemId = "gravity_d20_Max";

                }
                else if (testItemId == "gravity_d2020")
                {
                    minItemId = "gravity_d2020_Min";
                    maxItemId = "gravity_d2020_Max";
                }
                else if (testItemId == "softeningPoint_AstmE28_67")
                {
                    minItemId = "softeningPoint_AstmE28_67_Min";
                    maxItemId = "softeningPoint_AstmE28_67_Max";
                }
                else if (testItemId == "thickness")
                {
                    minItemId = "thickness_Min";
                    maxItemId = "thickness_Max";
                }
                else if (testItemId == "tensileStrength_200PM")
                {
                    minItemId = "tensileStrength_200PM_Min";
                    maxItemId = "tensileStrength_200PM_Max";
                }
                else if (testItemId == "tensileStrength")
                {
                    minItemId = "tensileStrength_Min";
                    maxItemId = "tensileStrength_Max";
                }
                else if (testItemId == "abilityToAbsorb")
                {
                    minItemId = "abilityToAbsorb_Min";
                    maxItemId = "abilityToAbsorb_Max";
                }
                else if (testItemId == "density")
                {
                    minItemId = "density_Min";
                    maxItemId = "density_Max";
                }
                else if (testItemId == "thickness2")
                {
                    minItemId = "thickness2_Min";
                    maxItemId = "thickness2_Max";
                }
                else if (testItemId == "elongationRate")
                {
                    minItemId = "elongationRate_Min";
                    maxItemId = "elongationRate_Max";
                }
                else if (testItemId == "refractiveIndex_n20D")
                {
                    minItemId = "refractiveIndex_n20D_Min";
                    maxItemId = "refractiveIndex_n20D_Max";
                }
                else if (testItemId == "Viscosity")
                {
                    minItemId = "Viscosity_Min";
                    maxItemId = "Viscosity_Max";
                }
                else if (testItemId == "SpecificGravity_d25")
                {
                    minItemId = "SpecificGravity_d25_Min";
                    maxItemId = "SpecificGravity_d25_Max";
                }
                else if (testItemId == "viscosity_AstmD3236_88")
                {
                    minItemId = "viscosity_AstmD3236_88_Min";
                    maxItemId = "viscosity_AstmD3236_88_Max";
                }
                else if (testItemId == "viscosity_AstmD3266_88")
                {
                    minItemId = "viscosity_AstmD3266_88_Min";
                    maxItemId = "viscosity_AstmD3266_88_Max";
                }
                else if (testItemId == "nonVolatile_Ksm0009")
                {
                    minItemId = "nonVolatile_Ksm0009_Min";
                    maxItemId = "nonVolatile_Ksm0009_Max";
                }
                else if (testItemId == "ashTest")
                {
                    minItemId = "ashTest_Min";
                    maxItemId = "ashTest_Max";
                }
                else if (testItemId == "heavyMetal_Ink")
                {
                    minItemId = "heavyMetal_Ink_Min";
                    maxItemId = "heavyMetal_Ink_Max";
                }
                else if (testItemId == "viscosity_Ink")
                {
                    minItemId = "viscosity_Ink_Min";
                    maxItemId = "viscosity_Ink_Max";
                }
                else
                    normalItemId = testItemId;

                string compatibilityOx = ""; 
                if(normalItemId != "")
                {
                    string itemValue = Utils.GetString(_componentDataRow[normalItemId]);
                    if (itemValue == "" || itemValue == "0") 
                        continue;

                    else if(testItemId == "ashTest" || testItemId == "residueOnMonomer" || testItemId == "lossOnDrying" || testItemId == "sedimentationRate" || testItemId == "sedimentationRate1")
                    {
                        if(testScore == "")
                            compatibilityOx = "부적합";
                        else if (Utils.GetDoubleValue(testScore) <= Utils.GetDoubleValue(itemValue))
                            compatibilityOx = "적합";
                        else
                            compatibilityOx = "부적합";
                    }
                    else if(testItemId == "absorbedAmount" || testItemId == "tensileStrength_50PM")
                    {
                        if (testScore == "")
                            compatibilityOx = "부적합";
                        else if (Utils.GetDoubleValue(testScore) >= Utils.GetDoubleValue(itemValue))
                            compatibilityOx = "적합";
                        else
                            compatibilityOx = "부적합";
                    }
                    else
                        compatibilityOx = testScore == "" ? "부적합" : "적합"; 
                }
                else
                {
                    string minValue = Utils.GetString(_componentDataRow[minItemId]);
                    string maxValue = Utils.GetString(_componentDataRow[maxItemId]);

                    if ((minValue == "" || minValue == "0") && (maxValue == "" || maxValue == "0"))
                        continue; 
                    else
                    {
                        if (testScore == "")
                            compatibilityOx = "부적합";
                        else if (Utils.GetDoubleValue(minValue) <= Utils.GetDoubleValue(testScore) && Utils.GetDoubleValue(testScore) <= Utils.GetDoubleValue(maxValue))
                            compatibilityOx = "적합";
                        else
                            compatibilityOx = "부적합";
                    }
                }

                testStandard = GetTestStandard(_componentDataRow, testItemId);

                componentTestDataGridView.Rows.Add(
                  testItem,
                  testMethod,
                  testStandard,
                  testScore,
                  compatibilityOx,
                  testItemId,
                  idx);

            }
        }

        private string GetTestStandard(DataRow dataRow, string testItemId)
        {
            string testStandard = "";

            if (testItemId == "pH")
                testStandard = Utils.GetString(dataRow["ph_Min"]) + "이상 "; // + dataRow["ph_Max"] + "이하";
            else if (testItemId == "gravity_d20")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["gravity_d20_Min"]), dataRow["gravity_d20_Max"]);
            else if (testItemId == "gravity_d2020")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["gravity_d2020_Min"]), dataRow["gravity_d2020_Max"]);
            else if (testItemId == "softeningPoint_AstmE28_67")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["softeningPoint_AstmE28_67_Min"]), dataRow["softeningPoint_AstmE28_67_Max"]);
            else if (testItemId == "thickness")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["thickness_Min"]), dataRow["thickness_Max"]);
            else if (testItemId == "tensileStrength_200PM")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["tensileStrength_200PM_Min"]), dataRow["tensileStrength_200PM_Max"]);
            else if (testItemId == "tensileStrength")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["tensileStrength_Min"]), dataRow["tensileStrength_Max"]);
            else if (testItemId == "abilityToAbsorb")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["abilityToAbsorb_Min"]), dataRow["abilityToAbsorb_Max"]);
            else if (testItemId == "density")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["density_Min"]), dataRow["density_Max"]);
            else if (testItemId == "thickness2")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["thickness2_Min"]), dataRow["thickness2_Max"]);
            else if (testItemId == "elongationRate")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["elongationRate_Min"]), dataRow["elongationRate_Max"]);
            else if (testItemId == "refractiveIndex_n20D")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["refractiveIndex_n20D_Min"]), dataRow["refractiveIndex_n20D_Max"]);
            else if (testItemId == "Viscosity")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["Viscosity_Min"]), dataRow["Viscosity_Max"]);
            else if (testItemId == "SpecificGravity_d25")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["SpecificGravity_d25_Min"]), dataRow["SpecificGravity_d25_Max"]);
            else if (testItemId == "viscosity_AstmD3236_88")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["viscosity_AstmD3236_88_Min"]), dataRow["viscosity_AstmD3236_88_Max"]);
            else if (testItemId == "viscosity_AstmD3266_88")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["viscosity_AstmD3266_88_Min"]), dataRow["viscosity_AstmD3266_88_Max"]);
            else if (testItemId == "nonVolatile_Ksm0009")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["nonVolatile_Ksm0009_Min"]), dataRow["nonVolatile_Ksm0009_Max"]);
            else if (testItemId == "ashTest")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["ashTest_Min"]), dataRow["ashTest_Max"]);
            else if (testItemId == "heavyMetal_Ink")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["heavyMetal_Ink_Min"]), dataRow["heavyMetal_Ink_Max"]);
            else if (testItemId == "viscosity_Ink")
                testStandard = string.Format("{0}이상 {1}이하", Utils.GetString(dataRow["viscosity_Ink_Min"]), dataRow["viscosity_Ink_Max"]);
            else if (testItemId == "absorbedAmount")
                testStandard = Utils.GetString(dataRow["absorbedAmount"]) + "배 이상 ";
            else if (testItemId == "tensileStrength_50PM")
                testStandard = Utils.GetString(dataRow["tensileStrength_50PM"]) + "이상 ";
            else if (testItemId == "ashTest")
                testStandard = Utils.GetString(dataRow["ashTest"]) + "이하 ";
            else if (testItemId == "residueOnMonomer")
                testStandard = Utils.GetString(dataRow["residueOnMonomer"]) + "이하 ";
            else if (testItemId == "lossOnDrying")
                testStandard = Utils.GetString(dataRow["lossOnDrying"]) + "이하 ";
            else if (testItemId == "residueOnIgnition")
                testStandard = Utils.GetString(dataRow["residueOnIgnition"]) + "이하 ";
            else if (testItemId == "sedimentationRate")
                testStandard = Utils.GetString(dataRow["sedimentationRate"]) + "이하 ";
            else if (testItemId == "sedimentationRate1")
                testStandard = Utils.GetString(dataRow["sedimentationRate1"]) + "이하 ";
            else
                testStandard = Utils.GetString(dataRow[testItemId]);

            return testStandard; 
        }

        private void InitComponentTestDataGridView()
        {
            DataGridView dataGridView = componentTestDataGridView;

            string[] columnNames = { "시험 항목", "시험 방법", "시험 기준", "시험 성적", "적합 여부", "TestItemId", "IDX" };
            for (int i = 0; i < columnNames.Length; i++)
            {
                dataGridView.Columns.Add("Column" + i.ToString(), columnNames[i]);
            }

            dataGridView.Columns[(int)eComponentTestResultList.testItemId].Visible = false;
            dataGridView.Columns[(int)eComponentTestResultList.idx].Visible = false;

            //dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

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
            //dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            //changeManagementDataGridView.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            //changeManagementDataGridView.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None; 

            // Each column style 
            dataGridView.Columns[(int)eComponentTestResultList.testItem].Width = 150; 
            dataGridView.Columns[(int)eComponentTestResultList.testScore].Width = 150; 
            dataGridView.Columns[(int)eComponentTestResultList.compatibilityOx].Width = 80;

            int tempWidth = dataGridView.Columns[(int)eComponentTestResultList.testItem].Width +
                dataGridView.Columns[(int)eComponentTestResultList.testScore].Width +
                 dataGridView.Columns[(int)eComponentTestResultList.compatibilityOx].Width;

            dataGridView.Columns[(int)eComponentTestResultList.testMethod].Width = (dataGridView.Width - tempWidth) / 2; 
            dataGridView.Columns[(int)eComponentTestResultList.testStandard].Width = (dataGridView.Width - tempWidth) / 2; 


            // Each column style 
            for (int i = (int)eComponentTestResultList.testItem; i <= (int)eComponentTestResultList.compatibilityOx; i++)
            {
                if (i == (int)eComponentTestResultList.testScore)
                    dataGridView.Columns[i].ReadOnly = false;
                else
                    dataGridView.Columns[i].ReadOnly = true;
            }


            // Common style
            dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridView.GridColor = Global.GRID_COLOR;
            dataGridView.BackgroundColor = Color.White;         // BackgroundColor 
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // SelectionMode
                                                                                  //dataGridView.ReadOnly = true;
            dataGridView.MultiSelect = false;
            dataGridView.ScrollBars = ScrollBars.Both;
        }

        private void InitImageDataGridView()
        {
            // Default column style
            imageDataGridView.ColumnHeadersVisible = true;
            imageDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Malgun Gothic", 11, FontStyle.Regular);
            imageDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Global.GRID_COLUMN_FORE_COLOR;
            imageDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Global.GRID_COLUMN_BACK_COLOR;
            imageDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            imageDataGridView.ColumnHeadersHeight = GRID_COLUMN_HEIGHT;
            imageDataGridView.AllowUserToResizeColumns = true;
            imageDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            //imageDataGridView.AdvancedColumnHeadersBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            imageDataGridView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Default row header style
            imageDataGridView.RowHeadersVisible = false;

            // Default row style
            imageDataGridView.RowsDefaultCellStyle.Font = new Font("Malgun Gothic", 11, FontStyle.Regular);
            imageDataGridView.RowsDefaultCellStyle.ForeColor = Global.GRID_ROW_FORE_COLOR;
            imageDataGridView.RowsDefaultCellStyle.BackColor = Global.GRID_ROW_BACK_COLOR;
            //imageDataGridView.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            imageDataGridView.RowTemplate.Height = GRID_IMAGE_ROW_HEIGHT;
            imageDataGridView.AllowUserToResizeRows = false;
            imageDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            //imageDataGridView.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            //imageDataGridView.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None; 

            // Each column style 
            imageDataGridView.Columns[(int)eImageDataList.FILE_PATH].Visible = false;
            imageDataGridView.Columns[(int)eImageDataList.ID].Visible = false;
            Padding profilePicturePadding = new Padding(5, 5, 5, 5);
            imageDataGridView.Columns[(int)eImageDataList.IMAGE].DefaultCellStyle.Padding = profilePicturePadding;

            imageDataGridView.Columns[(int)eImageDataList.NO].Width = 40;
            imageDataGridView.Columns[(int)eImageDataList.CHECKBOX].Width = 40;
            imageDataGridView.Columns[(int)eImageDataList.IMAGE].Width = 100;
            imageDataGridView.Columns[(int)eImageDataList.FILE_NAME].Width = 200;
            imageDataGridView.Columns[(int)eImageDataList.FILE_MEMO].Width = imageDataGridView.Width - 382;

            imageDataGridView.Columns[(int)eImageDataList.NO].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            imageDataGridView.Columns[(int)eImageDataList.CHECKBOX].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            imageDataGridView.Columns[(int)eImageDataList.IMAGE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            imageDataGridView.Columns[(int)eImageDataList.FILE_NAME].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            // Common style
            imageDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            imageDataGridView.GridColor = Global.GRID_COLOR;
            imageDataGridView.BackgroundColor = Color.White;         // BackgroundColor 
            imageDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // SelectionMode
            //imageDataGridView.ReadOnly = true;
            imageDataGridView.ScrollBars = ScrollBars.Both;
        }

        private void InitFileDataGridView()
        {
            // Default column style
            fileDataGridView.ColumnHeadersVisible = true;
            fileDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Malgun Gothic", 11, FontStyle.Regular);
            fileDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Global.GRID_COLUMN_FORE_COLOR;
            fileDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Global.GRID_COLUMN_BACK_COLOR;
            fileDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            fileDataGridView.ColumnHeadersHeight = GRID_COLUMN_HEIGHT;
            fileDataGridView.AllowUserToResizeColumns = true;
            fileDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            //fileDataGridView.AdvancedColumnHeadersBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            fileDataGridView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Default row header style
            fileDataGridView.RowHeadersVisible = false;

            // Default row style
            fileDataGridView.RowsDefaultCellStyle.Font = new Font("Malgun Gothic", 11, FontStyle.Regular);
            fileDataGridView.RowsDefaultCellStyle.ForeColor = Global.GRID_ROW_FORE_COLOR;
            fileDataGridView.RowsDefaultCellStyle.BackColor = Global.GRID_ROW_BACK_COLOR;
            //fileDataGridView.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //fileDataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            fileDataGridView.RowTemplate.Height = GRID_ROW_HEIGHT;
            fileDataGridView.AllowUserToResizeRows = false;
            fileDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            //fileDataGridView.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            //fileDataGridView.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None; 

            // Each column style 
            fileDataGridView.Columns[(int)eFileDataList.FILE_PATH].Visible = false;
            fileDataGridView.Columns[(int)eFileDataList.ID].Visible = false;

            fileDataGridView.Columns[(int)eFileDataList.NO].Width = 40;
            fileDataGridView.Columns[(int)eFileDataList.CHECKBOX].Width = 40;
            fileDataGridView.Columns[(int)eFileDataList.FILE_NAME].Width = 200;
            fileDataGridView.Columns[(int)eFileDataList.FILE_MEMO].Width = fileDataGridView.Width - 282;

            fileDataGridView.Columns[(int)eFileDataList.NO].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            fileDataGridView.Columns[(int)eFileDataList.CHECKBOX].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            fileDataGridView.Columns[(int)eFileDataList.FILE_NAME].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Common style
            fileDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            fileDataGridView.GridColor = Global.GRID_COLOR;
            fileDataGridView.BackgroundColor = Color.White;         // BackgroundColor 
            fileDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // SelectionMode
            //fileDataGridView.ReadOnly = true;
            fileDataGridView.ScrollBars = ScrollBars.Both;
        }

        private void addFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            DataGridView dataGridView = null;
            if (fileTabControl.SelectedIndex == 0)
            {
                openFileDialog.Filter = "Image Files (*.bmp, *.jpg, *.jpeg, *.png)|*.bmp;*.jpg;*.jpeg;*.png;";
                dataGridView = imageDataGridView;
            }
            else
            {
                dataGridView = fileDataGridView;
            }

            DialogResult result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK)
                return;

            string[] safeFileNames = openFileDialog.SafeFileNames;
            string[] fileNames = openFileDialog.FileNames;

            string id = "";

            for (int i = 0; i < safeFileNames.Length; i++)
            {
                AddFiles(dataGridView, safeFileNames[i], "", fileNames[i], id);

                Utils.OddDataGridViewRow(dataGridView);
            }
            dataGridView.ClearSelection();
        }

        private void AddFiles(DataGridView dataGridView, string safeFileName, string fileMemo, string fileName, string id)
        {
            int no = dataGridView.Rows.Count + 1;
            if (dataGridView.Name == "imageDataGridView")
            {
                Image fileImage = Image.FromFile(fileName);
                dataGridView.Rows.Add(no.ToString(), false, fileImage, safeFileName, fileMemo, fileName, id);
            }
            else
            {
                dataGridView.Rows.Add(no.ToString(), false, safeFileName, fileMemo, fileName, id);
            }
        }

        private void addDataButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DbHelper._connectionString))
                {

                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;

                    // BeginTransaction
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            command.Transaction = transaction;

                            // 테스트 추가
                            AddTest(command);

                            // 이미지, 파일 추가
                            AddTestFiles(command);

                            // 최종 테스트 결과 저장
                            SetTotalCompatibilityOx(command);

                            ////
                            // 테스트 결과 공유
                            // 내부 원료명과 Main Lot이 같은 테스트가 있으면 현재 테스트로 업데이트 한다.  
                            CopyTest(command); 
                            ////

                            // Commit
                            transaction.Commit();
                            MessageBox.Show("Test를 저장했습니다.");

                            DialogResult = DialogResult.OK;
                            this.Close();

                        }
                        catch (SqlException ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                        catch (InvalidOperationException ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        } 


        private void AddTest(SqlCommand command)
        {
            try
            {
                //if(CheckRequiredTestItems() == false)
                //    throw new Exception("필수 항목을 모두 입력해 주십시오.");

                ////
                // 기존 원료 검사 실행 삭제
                if (_currentTestId == "")
                    throw new Exception("원료 검사 실행을 저장할 수 없습니다.");

                command.CommandText = string.Format("EXEC DeleteComponentTestResults '{0}'", _currentTestId); 
                if (command.ExecuteNonQuery() < 0)
                    throw new Exception("원료 검사 실행을 저장할 수 없습니다.");
                ////

                ////
                // 새 원료 검사 실행 추가
                int totalCompatibilityCount = 0;
                for (int i = 0; i < componentTestDataGridView.Rows.Count; i++)
                {
                    string key = componentTestDataGridView.Rows[i].Cells[(int)eComponentTestResultList.idx].Value.ToString();

                     string testItemId = componentTestDataGridView.Rows[i].Cells[(int)eComponentTestResultList.testItemId].Value.ToString();
                     string testItemName = componentTestDataGridView.Rows[i].Cells[(int)eComponentTestResultList.testItem].Value.ToString();
                     string testMethod = componentTestDataGridView.Rows[i].Cells[(int)eComponentTestResultList.testMethod].Value.ToString();
                     string testStandard = componentTestDataGridView.Rows[i].Cells[(int)eComponentTestResultList.testStandard].Value.ToString();
                    string testScore = componentTestDataGridView.Rows[i].Cells[(int)eComponentTestResultList.testScore].Value.ToString();
                    string compatibilityOx = componentTestDataGridView.Rows[i].Cells[(int)eComponentTestResultList.compatibilityOx].Value.ToString();

                    compatibilityOx = compatibilityOx == "부적합" ? "0" : "1";
                    if (compatibilityOx == "1")
                        totalCompatibilityCount++;


                    string query = "EXEC InsertComponentTestResultItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ";
                    query = string.Format(query,
                        _currentTestId,
                        testItemId,
                        testItemName,
                        testMethod,
                        testStandard,
                        testScore,
                        compatibilityOx);

                    long retVal = DbHelper.ExecuteNonQuery(query);
                    if (retVal == -1)
                        throw new Exception("원료 검사 실행을 저장할 수 없습니다.");
                }
                if (totalCompatibilityCount != 0 && totalCompatibilityCount == componentTestDataGridView.Rows.Count)
                    _totalCompatibilityOx = true;
                else
                    _totalCompatibilityOx = false;
                ////
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddTestFiles(SqlCommand command)
        {
            try
            {
                DataGridView[] dataGridViews = { imageDataGridView, fileDataGridView };
                for (int di = 0; di < dataGridViews.Length; di++)
                {
                    int fileNameCell = (di == 0) ? (int)eImageDataList.FILE_NAME : (int)eFileDataList.FILE_NAME;
                    int filePathCell = (di == 0) ? (int)eImageDataList.FILE_PATH : (int)eFileDataList.FILE_PATH;
                    int fileMemoCell = (di == 0) ? (int)eImageDataList.FILE_MEMO : (int)eFileDataList.FILE_MEMO;
                    int idCell = (di == 0) ? (int)eImageDataList.ID : (int)eFileDataList.ID;

                    for (int fi = 0; fi < dataGridViews[di].Rows.Count; fi++)
                    {
                        string id = GetStringFromObject(dataGridViews[di].Rows[fi].Cells[idCell].Value);
                        // 기존에 업로드 된 파일이면 설명만 업데이트한다.  
                        if (id != "")
                        {
                            string fileMemo = GetStringFromObject(dataGridViews[di].Rows[fi].Cells[fileMemoCell].Value);
                            command.CommandText = string.Format("EXEC UpdateTestAttachmentItem '{0}', '{1}', '{2}'", fileMemo, di, id);
                            int retVal = command.ExecuteNonQuery();
                            if (retVal < 1)
                                throw new Exception("Test를 저장할 수 없습니다.");
                        }
                        else
                        {
                            string filePath = GetStringFromObject(dataGridViews[di].Rows[fi].Cells[filePathCell].Value);
                            if (File.Exists(filePath) == false)
                            {
                                string message = string.Format("({0})은 없는 파일입니다.", filePath);
                                throw new Exception("Test를 저장할 수 없습니다.");
                                //continue; 
                            }

                            SqlParameter fileData = new SqlParameter();
                            fileData.SqlDbType = SqlDbType.VarBinary;
                            fileData.ParameterName = "fileData";
                            fileData.Size = -1;
                            if (di == 0)
                            {
                                Image fileImage = Image.FromFile(filePath);
                                fileData.Value = Utils.ImageToByteArray(fileImage);
                            }
                            else
                            {
                                fileData.Value = File.ReadAllBytes(filePath);
                            }

                            string fileName = GetStringFromObject(dataGridViews[di].Rows[fi].Cells[fileNameCell].Value);
                            string fileMemo = GetStringFromObject(dataGridViews[di].Rows[fi].Cells[fileMemoCell].Value);
                            string fileType = di.ToString(); // 0:image 1:file 

                            if (fileData.Value != null)
                            {
                                command.CommandText = string.Format("EXEC InsertTestAttachmentItem '{0}', '{1}', '{2}', '{3}', @fileData, '{4}' ",
                                    _currentTestId, fileName, fileMemo, fileType, Global.FILETYPE_COMPONENT);
                                command.Parameters.Clear();
                                command.Parameters.Add(fileData);
                                int retVal = command.ExecuteNonQuery();
                                if (retVal < 1)
                                    throw new Exception("Test를 저장할 수 없습니다.");
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CopyTest(SqlCommand command)
        {
            try
            {
                // 기존 원료 검사 실행 삭제
                if (_currentTestId == "")
                    throw new Exception("원료 검사 실행을 저장할 수 없습니다.");

                command.CommandText = string.Format("EXEC CopyComponentTestItemForUpdate '{0}', '{1}', '{2}'",
                               _currentTestId, _parent.innerComponentNameTextBox.Text.Trim(), _parent.mainLotNoTextBox.Text.Trim());
                if (command.ExecuteNonQuery() < 0)
                    throw new Exception("원료 검사 실행을 저장할 수 없습니다.");
                ////

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CheckRequiredTestItems()
        {
            for (int i = 0; i < componentTestDataGridView.Rows.Count - 1; i++)
            {
                string key = Utils.GetString(componentTestDataGridView.Rows[i].Cells[(int)eComponentTestResultList.testItem].Value);
                if (key == "")
                    return false; 

                string testItemId = Utils.GetString(componentTestDataGridView.Rows[i].Cells[(int)eComponentTestResultList.testItemId].Value);
                string testItemName = Utils.GetString(componentTestDataGridView.Rows[i].Cells[(int)eComponentTestResultList.testItem].Value);
                string testMethod = Utils.GetString(componentTestDataGridView.Rows[i].Cells[(int)eComponentTestResultList.testMethod].Value);
                string testStandard = Utils.GetString(componentTestDataGridView.Rows[i].Cells[(int)eComponentTestResultList.testStandard].Value);
                string testScore = Utils.GetString(componentTestDataGridView.Rows[i].Cells[(int)eComponentTestResultList.testScore].Value);
                string compatibilityOx = Utils.GetString(componentTestDataGridView.Rows[i].Cells[(int)eComponentTestResultList.compatibilityOx].Value);

                if (testItemId == "" || testItemName == "" || testMethod == "" || testStandard == "" || testScore == "" || compatibilityOx == "")

                {
                    //MessageBox.Show("필수 항목을 모두 입력해 주십시오.");
                    return false;
                }
            }
            return true; 
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; 
            this.Close();
        }

        private void downloadFileButton_Click(object sender, EventArgs e)
        {
            //DataGridView dataGridView = (fileTabControl.SelectedIndex == 0) ? imageDataGridView : fileDataGridView;
            //int CHECKBOX_CELL = (fileTabControl.SelectedIndex == 0) ? (int)eImageDataList.CHECKBOX : (int)eFileDataList.CHECKBOX;

            if (GetCheckedDataGridViewItemCount() <= 0)
            {
                MessageBox.Show("다운로드할 항목을 체크해 주십시오.");
                return;
            }

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (!string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                if (Directory.Exists(folderBrowserDialog.SelectedPath) == false)
                {
                    MessageBox.Show("잘못된 경로입니다. 다시 확인해 주십시오.");
                    return;
                }
            }

            bool success = false;
            if (fileTabControl.SelectedIndex == 0)
                success = DownloadImages(folderBrowserDialog.SelectedPath);
            else
                success = DownloadFiles(folderBrowserDialog.SelectedPath);

            if (success == true)
                MessageBox.Show("다운로드가 완료됐습니다.", "파일 다운로드");
        }

        private bool DownloadImages(string downloadFilePath)
        {
            for (int i = 0; i < imageDataGridView.Rows.Count; i++)
            {
                if ((bool)imageDataGridView.Rows[i].Cells[(int)eImageDataList.CHECKBOX].Value == true)
                {
                    Image image = (imageDataGridView.Rows[i].Cells[(int)eImageDataList.IMAGE].Value as Image);
                    if (image != null)
                    {
                        string newFileName = Utils.GetUniquFileNameByIndex(downloadFilePath, imageDataGridView.Rows[i].Cells[(int)eImageDataList.FILE_NAME].Value.ToString());
                        image.Save(newFileName);
                    }
                }
            }

            return true;
        }

        private bool DownloadFiles(string downloadFilePath)
        {
            for (int i = 0; i < fileDataGridView.Rows.Count; i++)
            {
                if ((bool)fileDataGridView.Rows[i].Cells[(int)eFileDataList.CHECKBOX].Value == true)
                {
                    string id = GetStringFromObject(fileDataGridView.Rows[i].Cells[(int)eFileDataList.ID].Value);
                    if (id != "") // 다운로드
                    {
                        DataSet dataSet = DbHelper.SelectQuery(string.Format("EXEC SelectTestAttachmentItem '{0}', '{1}'", id, Global.FILETYPE_COMPONENT));
                        if (dataSet == null || dataSet.Tables.Count == 0)
                        {
                            MessageBox.Show("파일 정보를 가져올 수 없습니다.");
                            return false;
                        }

                        if (dataSet.Tables[0].Rows.Count == 0)
                            continue;

                        string newFileName = Utils.GetUniquFileNameByIndex(downloadFilePath, fileDataGridView.Rows[i].Cells[(int)eFileDataList.FILE_NAME].Value.ToString());
                        File.WriteAllBytes(newFileName, (Byte[])dataSet.Tables[0].Rows[0]["fileData"]);
                    }
                    else
                    {
                        string newFileName = Utils.GetUniquFileNameByIndex(downloadFilePath, fileDataGridView.Rows[i].Cells[(int)eFileDataList.FILE_NAME].Value.ToString());
                        string filePath = fileDataGridView.Rows[i].Cells[(int)eFileDataList.FILE_PATH].Value.ToString();
                        File.Copy(filePath, newFileName);
                    }
                }
            }

            return true;
        }



        private void deleteFileButton_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView = (fileTabControl.SelectedIndex == 0) ? imageDataGridView : fileDataGridView;
            int CHECKBOX_CELL = (fileTabControl.SelectedIndex == 0) ? (int)eImageDataList.CHECKBOX : (int)eFileDataList.CHECKBOX;
            int ID_CELL = (fileTabControl.SelectedIndex == 0) ? (int)eImageDataList.ID : (int)eFileDataList.ID;

            if (GetCheckedDataGridViewItemCount() <= 0)
            {
                MessageBox.Show("삭제할 항목을 체크해 주십시오.");
                return;
            }

            if (DialogResult.No == MessageBox.Show("체크한 항목을 삭제하시겠습니까?", "항목 삭제", MessageBoxButtons.YesNo))
                return;

            string ids = ""; 
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if ((bool)dataGridView.Rows[i].Cells[CHECKBOX_CELL].Value == true)
                {
                    string id = GetStringFromObject(dataGridView.Rows[i].Cells[ID_CELL].Value);
                    if (id == "") // 업로드 되지 않은 것이면 패스
                        continue;

                    ids += (id + ",");
                }
            }

            if (ids != "")
                ids = ids.Substring(0, ids.Length - 1);

            string query = string.Format("EXEC DeleteTestAttachment '{0}', '{1}'", ids, Global.FILETYPE_COMPONENT);
            long retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal != -1)
            {
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    if ((bool)dataGridView.Rows[i].Cells[CHECKBOX_CELL].Value == true)
                    {
                        dataGridView.Rows.RemoveAt(i);
                        i--;
                    }

                    //transferProgressBar.Value = i;
                    Utils.ResetOddDataGridViewRow(dataGridView);
                }
                MessageBox.Show("선택한 항목을 삭제했습니다.");
            }
            else
            {
                MessageBox.Show("선택한 항목을 삭제 할 수 없습니다.");
            }
        }

        private string GetStringFromObject(object data)
        {
            if (data == null)
                return "";
            else
                return data.ToString();
        }

        private int GetCheckedDataGridViewItemCount()
        {
            DataGridView dataGridView = (fileTabControl.SelectedIndex == 0) ? imageDataGridView : fileDataGridView;
            int CHECKBOX_CELL = (fileTabControl.SelectedIndex == 0) ? (int)eImageDataList.CHECKBOX : (int)eFileDataList.CHECKBOX;

            int itemCount = 0;
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if ((bool)dataGridView.Rows[i].Cells[CHECKBOX_CELL].Value == true)
                    itemCount++;
            }
            return itemCount;
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            Excel.Application application = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            bool isSavedFile = false;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.FileName = "";
            //saveFileDialog.Filter = "엑셀 파일 (*.xlsx)|*.xlsx";
            saveFileDialog.Filter = "엑셀 파일 (*.xlsx)|*.xlsx|PDF 파일 (*.pdf)|*.pdf";
            DialogResult result = saveFileDialog.ShowDialog();
            if (result != DialogResult.OK)
                return;

            string excelFilePath = saveFileDialog.FileName;
            bool overwriteMode = true;
            const float EXCEL_ROW_HEIGHT = 16.5f;
            try
            {
                Cursor = Cursors.WaitCursor;

                // Excel 생성
                application = new Excel.Application();
                workbook = application.Workbooks.Add();
                worksheet = workbook.Worksheets.get_Item(1) as Excel.Worksheet;

                // 공통 설정
                application.get_Range("A1").EntireRow.EntireColumn.Interior.Color = Color.White;
                application.get_Range("A1").EntireRow.EntireColumn.Font.Name = "맑은 고딕";
                application.get_Range("A1").EntireRow.EntireColumn.Font.Size = 11;
                application.get_Range("A1").EntireRow.EntireColumn.RowHeight = EXCEL_ROW_HEIGHT;
                application.ActiveWindow.Zoom = 100;

                // 머지를 위한 경고 제거
                application.DisplayAlerts = false;

                // Column 너비 
                /*
                string alph = "A"; 
                for(int i=0; i<(int)eLabTestDataGridView.ID; i++)
                {
                    string column = alph + "1"; 
                    if(i == (int)eLabTestDataGridView.TITLE)
                        application.get_Range(column).ColumnWidth = Global.EXCEL_WIDE_COL_WIDTH;
                    else
                        application.get_Range(column).ColumnWidth = Global.EXCEL_BASIC_COL_WIDTH;

                    alph = Utils.IncreaseAlphabet(alph.ToCharArray()[0]); 
                }
                alph = Utils.DecreaseAlphabet(alph.ToCharArray()[0]);

                // Column 폰트
                string lastColumn = alph + "1";
                application.get_Range("A1:" + lastColumn).Font.Size = 11;
                application.get_Range("A1:" + lastColumn).Font.Bold = true;

                // Column 백컬러
                application.get_Range("A1:" + lastColumn).Interior.Color = Color.Yellow;

                // Row 폰트
                //application.get_Range("A2:E3").Font.Size = 11;
                //application.get_Range("A2:E3").Font.Bold = true;
                */

                int currentRow = 1;
                //for (int di = 0; di < componentTestDataGridView.Rows.Count; di++)
                //{
                // 라벨 입력
                worksheet.Cells[currentRow, 1] = "원료 검사";
                worksheet.Cells[currentRow, 1].Font.Bold = true;
                currentRow += 2;

                // Row 데이터 입력
                DataGridView dataGridView = componentTestDataGridView;

                Excel.Range startRange = worksheet.Cells[currentRow, 1];
                for (int ri = 0; ri < dataGridView.Rows.Count; ri++)
                {
                    for (int ci = 0; ci < dataGridView.Columns.Count; ci++)
                    {
                        int col = ci + 1;
                        worksheet.Cells[currentRow, col] = Utils.GetString(dataGridView.Rows[ri].Cells[ci].Value);
                    }

                    /*
                    if (ri == 0)
                    {
                        int mergeCount = GetMergeCount(mTestFormatDataGridViewList[di].TestFormatCode);
                        if (mergeCount != 0)
                        {
                            int mergeCol = dataGridView.Columns.Count / mergeCount;
                            int startMergeCol = 2;
                            int endMergeCol = startMergeCol + (mergeCount - 1);
                            for (int mi = 0; mi < mergeCol; mi++)
                            {
                                Excel.Range startMergeRange = worksheet.Cells[currentRow, startMergeCol];
                                Excel.Range endMergeRange = worksheet.Cells[currentRow, endMergeCol];
                                worksheet.get_Range(startMergeRange, endMergeRange).Merge();
                                startMergeCol += mergeCount;
                                endMergeCol += mergeCount;
                            }
                        }

                    }
                    */
                    currentRow++;
                }
                Excel.Range endRange = worksheet.Cells[currentRow - 1, dataGridView.Columns.Count];

                // 열 정렬
                application.get_Range(startRange, endRange).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // 보더 스타일
                application.get_Range(startRange, endRange).Borders.Color = System.Drawing.Color.Black;
                application.get_Range(startRange, endRange).Borders.LineStyle = BorderStyle.FixedSingle;
                application.get_Range(startRange, endRange).Borders.Weight = 2;

                currentRow += 2;
                //}

                ////
                // 이미지 내보내기
                for (int i = 0; i < imageDataGridView.Rows.Count; i++)
                {
                    Image image = imageDataGridView.Rows[i].Cells[(int)eImageDataList.IMAGE].Value as Image;
                    string no = Utils.GetString(imageDataGridView.Rows[i].Cells[(int)eImageDataList.NO].Value);
                    string fileName = Utils.GetString(imageDataGridView.Rows[i].Cells[(int)eImageDataList.FILE_NAME].Value);
                    string fileMemo = Utils.GetString(imageDataGridView.Rows[i].Cells[(int)eImageDataList.FILE_MEMO].Value);
                    if (image == null)
                        continue;

                    string tempFilePath = Path.GetTempPath();
                    string uniqueFileName = Utils.GetUniquFileNameByIndex(tempFilePath, fileName);
                    image.Save(uniqueFileName);

                    string imageNo = string.Format("[그림 {0}]", no);
                    string imageFileName = string.Format("- 파일 이름 : {0}", fileName);
                    string imageFileMemo = string.Format("- 파일 설명 : {0}", fileMemo);

                    worksheet.Cells[currentRow++, 1] = imageNo;
                    worksheet.Cells[currentRow++, 1] = imageFileName;
                    worksheet.Cells[currentRow++, 1] = imageFileMemo;

                    float imageTop = currentRow * EXCEL_ROW_HEIGHT;
                    worksheet.Shapes.AddPicture(uniqueFileName, MsoTriState.msoFalse, MsoTriState.msoCTrue, 0, imageTop, image.Width, image.Height);
                    File.Delete(uniqueFileName);

                    int rowCountForImage = (int)(image.Height / EXCEL_ROW_HEIGHT) + 1;
                    currentRow += (rowCountForImage + 2);
                }
                ////

                ////
                // 파일 이름 내보내기
                for (int i = 0; i < fileDataGridView.Rows.Count; i++)
                {
                    string no = Utils.GetString(imageDataGridView.Rows[i].Cells[(int)eImageDataList.NO].Value);
                    string fileName = Utils.GetString(imageDataGridView.Rows[i].Cells[(int)eImageDataList.FILE_NAME].Value);
                    string fileMemo = Utils.GetString(imageDataGridView.Rows[i].Cells[(int)eImageDataList.FILE_MEMO].Value);

                    string fileNo = string.Format("[파일 {0}]", no);
                    string fileFileName = string.Format("- 파일 이름 : {0}", fileName);
                    string fileFileMemo = string.Format("- 파일 설명 : {0}", fileMemo);

                    worksheet.Cells[currentRow++, 1] = fileNo;
                    worksheet.Cells[currentRow++, 1] = fileFileName;
                    worksheet.Cells[currentRow++, 1] = fileFileMemo;

                    currentRow += 2;
                }
                ////

                isSavedFile = SaveAsExcelFile(workbook, excelFilePath, overwriteMode);
            }
            finally
            {
                if (excelFilePath.Substring(excelFilePath.LastIndexOf('.')) == ".xlsx")
                    workbook.Close(isSavedFile);
                application.Quit();

                ReleaseExcelObject(worksheet);
                ReleaseExcelObject(workbook);
                ReleaseExcelObject(application);

                Cursor = Cursors.Default;

                if (isSavedFile == true)
                    MessageBox.Show("파일 저장을 완료했습니다.", "파일 저장");
                else
                    MessageBox.Show("파일 저장을 실패했습니다.", "파일 저장");
            }
        }

        private bool SaveAsExcelFile(Excel.Workbook workbook, string excelFilePath, bool overwriteMode = false)
        {
            // 디렉토리 경로 확인
            int pos = excelFilePath.LastIndexOf('\\');
            if (pos == -1)
                return false;

            string dirPath = excelFilePath.Substring(0, pos);
            DirectoryInfo di = new DirectoryInfo(dirPath);
            if (di.Exists == false)
                di.Create();

            // 사용중인 파일 확인
            FileInfo excelFile = new FileInfo(excelFilePath);
            while (true == Utils.IsFileLocked(excelFile))
            {
                if (DialogResult.Cancel == MessageBox.Show("저장하려는 파일이 사용중입니다. 파일을 닫고 다시 시도해 주십시오.", "오류 체크", MessageBoxButtons.RetryCancel))
                    return false;
            }

            if (overwriteMode == true)
            {
                if (excelFile.Exists == true)
                    excelFile.Delete();
            }

            if (excelFilePath.Substring(excelFilePath.LastIndexOf('.')) == ".xlsx")
                workbook.SaveAs(excelFilePath, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Excel.XlSaveConflictResolution.xlUserResolution, true, Type.Missing, Type.Missing, Type.Missing);
            else
                workbook.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, excelFilePath);

            return true;
        }

        private static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

        private void SelectComponentTestResult()
        {
            string query = string.Format("EXEC SelectComponentTestResultList '{0}'", _currentTestId);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("원료 검사 실행 데이터를 가져올 수 없습니다.");
                return;
            }

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string idx = dataRow["idx"].ToString();
                string testItemId = dataRow["testItemId"].ToString();
                string testItemName = dataRow["testItemName"].ToString();
                string testMethod = dataRow["testMethod"].ToString();
                string testStandard = dataRow["testStandard"].ToString();
                string testScore = dataRow["testScore"].ToString();
                string compatibilityOx = dataRow["compatibilityOx"].ToString();

                compatibilityOx = compatibilityOx == "0" ? "부적합" : "적합";

                for (int i = 0; i < componentTestDataGridView.Rows.Count; i++)
                {
                    if (testItemId == componentTestDataGridView.Rows[i].Cells[(int)eComponentTestResultList.testItemId].Value.ToString())
                    {
                        componentTestDataGridView.Rows[i].Cells[(int)eComponentTestResultList.testScore].Value = testScore;
                        componentTestDataGridView.Rows[i].Cells[(int)eComponentTestResultList.compatibilityOx].Value = compatibilityOx;
                    }
                }
            }
        }
        private void captureButton_Click(object sender, EventArgs e)
        {
            CaptureForm form = new CaptureForm(imageDataGridView);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog(); 
        }

        private void SetSelectedValues()
        {
            // DataGridView data 설정 
            SelectComponentTestResult(); 

            // 이미지, 파일 설정
            SetTestFormatFiles();
        }

        private void SetTestFormatFiles()
        {
            if (_currentTestId == "")
                return;

            DataGridView[] dataGridViews = { imageDataGridView, fileDataGridView };
            for (int di = 0; di < dataGridViews.Length; di++)
            {
                int fileNameCell = (di == 0) ? (int)eImageDataList.FILE_NAME : (int)eFileDataList.FILE_NAME;
                int filePathCell = (di == 0) ? (int)eImageDataList.FILE_PATH : (int)eFileDataList.FILE_PATH;
                int fileMemoCell = (di == 0) ? (int)eImageDataList.FILE_MEMO : (int)eFileDataList.FILE_MEMO;
                int idCell = (di == 0) ? (int)eImageDataList.ID : (int)eFileDataList.ID;

                int fileType = di; // 0: image 1:file
                DataSet dataSet = DbHelper.SelectQuery(string.Format("EXEC SelectTestAttachmentList '{0}', '{1}', '{2}'", _currentTestId, fileType, Global.FILETYPE_COMPONENT));
                if (dataSet == null || dataSet.Tables.Count == 0)
                {
                    MessageBox.Show("파일 정보를 가져올 수 없습니다.");
                    break;
                }

                if (dataSet.Tables[0].Rows.Count == 0)
                    continue;

                int no = 1;
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    string fileName = GetStringFromObject(dataRow["fileName"]);
                    string fileMemo = GetStringFromObject(dataRow["fileMemo"]);
                    string idx = GetStringFromObject(dataRow["idx"]);

                    if (fileType == 0)
                    {
                        Image image = Utils.ByteArrayToImage((Byte[])dataRow["fileData"]);
                        dataGridViews[di].Rows.Add(no.ToString(), false, image, fileName, fileMemo, "", idx);
                    }
                    else
                    {
                        dataGridViews[di].Rows.Add(no.ToString(), false, fileName, fileMemo, "", idx);
                    }
                    no++;

                    Utils.OddDataGridViewRow(dataGridViews[di]);
                }

            }
        }

        private void SetTotalCompatibilityOx(SqlCommand command)
        {
            command.CommandText = string.Format("EXEC UpdateComponentTestJudgingResult '{0}', '{1}'", _currentTestId, _totalCompatibilityOx == false ? 0 : 1);
            int retVal = command.ExecuteNonQuery();
            if (retVal < 1)
                throw new Exception("Test를 저장할 수 없습니다.");
            else
                _parent.judgingResultComboBox.SelectedIndex = _totalCompatibilityOx == false ? 0 : 1;

            int selectedRowIndex = _parent._parent.componentTestDataGridView.SelectedRows[0].Index;
            _parent._parent.componentTestDataGridView.Rows[selectedRowIndex].Cells[(int)QTRS.ComponentTest.ComponentTestControl.eComponentTestList.judgingResult].Value = _totalCompatibilityOx == true ? "적합" : "부적합";

        }

        private void componentTestDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != (int)eComponentTestResultList.compatibilityOx)
                return;

            if (componentTestDataGridView.SelectedRows.Count > 0)
            {
                CompatibilityForm form = new CompatibilityForm(this);
                form.StartPosition = FormStartPosition.Manual;
                form.Left = Cursor.Position.X;
                form.Top = Cursor.Position.Y;
                form.ShowDialog();
            }
        }

        private void componentTestDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != (int)eComponentTestResultList.testScore)
                return;

            string testItemId = Utils.GetString(componentTestDataGridView.Rows[e.RowIndex].Cells[(int)eComponentTestResultList.testItemId].Value);
            string testScore = Utils.GetString(componentTestDataGridView.Rows[e.RowIndex].Cells[(int)eComponentTestResultList.testScore].Value);

            string minItemId = "";
            string maxItemId = "";
            string normalItemId = "";

            if (testItemId == "pH")
            {
                minItemId = "ph_Min";
                maxItemId = "ph_Max";
            }
            else if (testItemId == "gravity_d20")
            {
                minItemId = "gravity_d20_Min";
                maxItemId = "gravity_d20_Max";

            }
            else if (testItemId == "gravity_d2020")
            {
                minItemId = "gravity_d2020_Min";
                maxItemId = "gravity_d2020_Max";
            }
            else if (testItemId == "softeningPoint_AstmE28_67")
            {
                minItemId = "softeningPoint_AstmE28_67_Min";
                maxItemId = "softeningPoint_AstmE28_67_Max";
            }
            else if (testItemId == "thickness")
            {
                minItemId = "thickness_Min";
                maxItemId = "thickness_Max";
            }
            else if (testItemId == "tensileStrength_200PM")
            {
                minItemId = "tensileStrength_200PM_Min";
                maxItemId = "tensileStrength_200PM_Max";
            }
            else if (testItemId == "tensileStrength")
            {
                minItemId = "tensileStrength_Min";
                maxItemId = "tensileStrength_Max";
            }
            else if (testItemId == "abilityToAbsorb")
            {
                minItemId = "abilityToAbsorb_Min";
                maxItemId = "abilityToAbsorb_Max";
            }
            else if (testItemId == "density")
            {
                minItemId = "density_Min";
                maxItemId = "density_Max";
            }
            else if (testItemId == "thickness2")
            {
                minItemId = "thickness2_Min";
                maxItemId = "thickness2_Max";
            }
            else if (testItemId == "elongationRate")
            {
                minItemId = "elongationRate_Min";
                maxItemId = "elongationRate_Max";
            }
            else if (testItemId == "refractiveIndex_n20D")
            {
                minItemId = "refractiveIndex_n20D_Min";
                maxItemId = "refractiveIndex_n20D_Max";
            }
            else if (testItemId == "Viscosity")
            {
                minItemId = "Viscosity_Min";
                maxItemId = "Viscosity_Max";
            }
            else if (testItemId == "SpecificGravity_d25")
            {
                minItemId = "SpecificGravity_d25_Min";
                maxItemId = "SpecificGravity_d25_Max";
            }
            else if (testItemId == "viscosity_AstmD3236_88")
            {
                minItemId = "viscosity_AstmD3236_88_Min";
                maxItemId = "viscosity_AstmD3236_88_Max";
            }
            else if (testItemId == "viscosity_AstmD3266_88")
            {
                minItemId = "viscosity_AstmD3266_88_Min";
                maxItemId = "viscosity_AstmD3266_88_Max";
            }
            else if (testItemId == "nonVolatile_Ksm0009")
            {
                minItemId = "nonVolatile_Ksm0009_Min";
                maxItemId = "nonVolatile_Ksm0009_Max";
            }
            else if (testItemId == "ashTest")
            {
                minItemId = "ashTest_Min";
                maxItemId = "ashTest_Max";
            }
            else if (testItemId == "heavyMetal_Ink")
            {
                minItemId = "heavyMetal_Ink_Min";
                maxItemId = "heavyMetal_Ink_Max";
            }
            else if (testItemId == "viscosity_Ink")
            {
                minItemId = "viscosity_Ink_Min";
                maxItemId = "viscosity_Ink_Max";
            }
            else
                normalItemId = testItemId;

            string compatibilityOx = "";
            if (normalItemId != "")
            {
                string itemValue = Utils.GetString(_componentDataRow[normalItemId]);
                string testStandard = Utils.GetString(componentTestDataGridView.Rows[e.RowIndex].Cells[(int)eComponentTestResultList.testStandard].Value);

                if (testItemId == "ashTest" || testItemId == "residueOnMonomer" || testItemId == "lossOnDrying" || testItemId == "sedimentationRate" || testItemId == "sedimentationRate1")
                {
                    if (testScore == "")
                        compatibilityOx = "부적합";
                    else if (Utils.GetDoubleValue(testScore) <= Utils.GetDoubleValue(itemValue))
                        compatibilityOx = "적합";
                    else
                        compatibilityOx = "부적합";
                }
                else if (testItemId == "absorbedAmount" || testItemId == "tensileStrength_50PM")
                {
                    if (testScore == "")
                        compatibilityOx = "부적합";
                    else if (Utils.GetDoubleValue(testScore) >= Utils.GetDoubleValue(itemValue))
                        compatibilityOx = "적합";
                    else
                        compatibilityOx = "부적합";
                }
                else
                    compatibilityOx = testScore == "" ? "부적합" : "적합";
            }
            else
            {
                string minValue = Utils.GetString(_componentDataRow[minItemId]);
                string maxValue = Utils.GetString(_componentDataRow[maxItemId]);

                if (testScore == "")
                    compatibilityOx = "부적합";
                else if (Utils.GetDoubleValue(minValue) <= Utils.GetDoubleValue(testScore) && Utils.GetDoubleValue(testScore) <= Utils.GetDoubleValue(maxValue))
                    compatibilityOx = "적합";
                else
                    compatibilityOx = "부적합";
            }

            componentTestDataGridView.Rows[e.RowIndex].Cells[(int)eComponentTestResultList.compatibilityOx].Value = compatibilityOx;

        }
    }
}
