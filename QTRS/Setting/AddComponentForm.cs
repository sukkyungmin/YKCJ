using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.Setting
{
    public partial class AddComponentForm : Form
    {
        private ComponentMasterControl _parent = null;

        enum eTestMethod
        {
            testItemId, testItem, testMethod, testStandard, testScore, deleteButton
        }

        public AddComponentForm(ComponentMasterControl parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void AddComponentForm_Load(object sender, EventArgs e)
        {
            InitControls();
            SelectTestMethodList();
        }

        private void InitControls()
        {
            InitDataGrid();
            InitRawDatas();
        }

        private void InitDataGrid()
        {
            DataGridView dataGridView = testMethodDataGridView;

            string[] columnNames = { "시험 항목 ID", "시험 항목", "시험 방법", "시험 기준", "시험 성적", "단위" };
            for (int i = 0; i < columnNames.Length; i++)
            {
                dataGridView.Columns.Add("Column" + (i + 1).ToString(), columnNames[i]);
            }
            DataGridViewButtonColumn buttonCol = new DataGridViewButtonColumn();
            buttonCol.Name = "deleteRowButton";
            buttonCol.HeaderText = "";
            buttonCol.Text = "삭제";
            buttonCol.UseColumnTextForButtonValue = true;
            dataGridView.Columns.Add(buttonCol);

            // Common style
            dataGridView.ReadOnly = false;
            //dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            dataGridView.GridColor = Global.GRID_COLOR;
            dataGridView.BackgroundColor = Color.White;         // BackgroundColor 
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // SelectionMode
            dataGridView.ScrollBars = ScrollBars.Both;


            // Default column style
            dataGridView.ColumnHeadersVisible = true;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Dotum", 11, FontStyle.Regular);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Global.GRID_COLUMN_FORE_COLOR;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Global.GRID_COLUMN_BACK_COLOR;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersHeight = Global.GRID_COLUMN_HEIGHT;
            dataGridView.AllowUserToResizeColumns = true;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            //dataGridView.AdvancedColumnHeadersBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;


            // Default row header style
            //dataGridView.RowHeadersVisible = false;

            // Default row style
            dataGridView.RowsDefaultCellStyle.Font = new Font("Dotum", 11, FontStyle.Regular);
            dataGridView.RowsDefaultCellStyle.ForeColor = Global.GRID_ROW_FORE_COLOR;
            dataGridView.RowsDefaultCellStyle.BackColor = Global.GRID_ROW_BACK_COLOR;
            dataGridView.RowTemplate.Height = Global.GRID_ROW_HEIGHT;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            //changeManagementDataGridView.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            //changeManagementDataGridView.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None; 

            /*
            // Each column style 
            dataGridView.Columns[(int)eComponentList.checkBox].ReadOnly = false;
            for (int i = (int)eComponentList.componentCode; i <= (int)eComponentList.note; i++)
                dataGridView.Columns[i].ReadOnly = true;
                */


            /*
            // Each row style
            for (int i = 0; i < (int)eChangeManagementDataGridView.ID; i++)
            {
                if (i == (int)eChangeManagementDataGridView.TITLE || i == (int)eChangeManagementDataGridView.FOCUSED_CONTROL)
                    changeManagementDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                else
                    changeManagementDataGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            */

            dataGridView.MultiSelect = false;
        }

        private void InitRawDatas()
        {
            /*
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

            ////
            // 샘플 O/X
            //sampleOxComboBox.Items.Add(new ComboBoxItem("선택", -1));
            sampleOxComboBox.Items.Add(new ComboBoxItem("O", 1));
            sampleOxComboBox.Items.Add(new ComboBoxItem("X", 0));
            sampleOxComboBox.SelectedIndex = -1;
            ////

            ////this.Enabled = true;
            */
        }

        private void SelectTestMethodList()
        {
            string query = "EXEC SelectTestMethodList 0";

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("시험방법 데이터를 가져올 수 없습니다.");
                return;
            }

            testMethodDataGridView.Rows.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string testItemId = dataRow["testItemId"].ToString();
                string testItem = dataRow["testItem"].ToString();
                string testMethod = dataRow["testMethod"].ToString();
                string testStandard = dataRow["testStandard"].ToString();
                string testScore = dataRow["testScore"].ToString();
                string unit = dataRow["unit"].ToString();

                testMethodDataGridView.Rows.Add(testItemId, testItem, testMethod, testStandard, testScore, unit);
            }

            testMethodDataGridView.ClearSelection();
        }

        private void addDataButton_Click(object sender, EventArgs e)
        {
            if (CheckRequiredComponentItems() == false)
                return;

            string componentCode = componentCodeTextBox.Text.Trim();
            string componentName = componentNameTextBox.Text.Trim();
            string deliveryCompanyName = deliveryCompanyNameTextBox.Text.Trim();
            string purpose = purposeTextBox.Text.Trim();
            string fdaName = fdaNameTextBox.Text.Trim();
            string property = propertyTextBox.Text.Trim();
            string confirmationTest01 = confirmationTest01TextBox.Text.Trim();
            string confirmationTest02 = confirmationTest02TextBox.Text.Trim();
            string confirmationTest03 = confirmationTest03TextBox.Text.Trim();
            string confirmationTest04 = confirmationTest04TextBox.Text.Trim();
            string confirmationTest05 = confirmationTest05TextBox.Text.Trim();
            string absorbedAmount = absorbedAmountTextBox.Text.Trim();
            string citricAcidConfirmationTest = citricAcidConfirmationTestTextBox.Text.Trim();
            string pH_Min = pH_MinTextBox.Text.Trim();
            string pH_Max = pH_MaxTextBox.Text.Trim();
            string gravity_d20_Min = gravity_d20_MinTextBox.Text.Trim();
            string gravity_d20_Max = gravity_d20_MaxTextBox.Text.Trim();
            string gravity_d2020_Min = gravity_d2020_MinTextBox.Text.Trim();
            string gravity_d2020_Max = gravity_d2020_MaxTextBox.Text.Trim();
            string viscosity_AstmD3236_88 = viscosity_AstmD3236_88TextBox.Text.Trim();
            string viscosity_AstmD3266_88 = viscosity_AstmD3266_88TextBox.Text.Trim();
            string softeningPoint_AstmE28_67_Min = softeningPoint_AstmE28_67_MinTextBox.Text.Trim();
            string softeningPoint_AstmE28_67_Max = softeningPoint_AstmE28_67_MaxTextBox.Text.Trim();
            string nonVolatile_Ksm0009 = nonVolatile_Ksm0009TextBox.Text.Trim();
            string lignin = ligninTextBox.Text.Trim();
            string pigment = pigmentTextBox.Text.Trim();
            string acidAndAlkali = acidAndAlkaliTextBox.Text.Trim();
            string fluorescence = fluorescenceTextBox.Text.Trim();
            string ashTest = ashTestTextBox.Text.Trim();
            string formaldehyde = formaldehydeTextBox.Text.Trim();
            string strength = strengthTextBox.Text.Trim();
            string sedimentationRate = sedimentationRateTextBox.Text.Trim();
            string elongationRate = elongationRateTextBox.Text.Trim();
            string thickness_Min = thickness_MinTextBox.Text.Trim();
            string thickness_Max = thickness_MaxTextBox.Text.Trim();
            string tensileStrength_200PM_Min = tensileStrength_200PM_MinTextBox.Text.Trim();
            string tensileStrength_200PM_Max = tensileStrength_200PM_MaxTextBox.Text.Trim();
            string tensileStrength_Min = tensileStrength_MinTextBox.Text.Trim();
            string tensileStrength_Max = tensileStrength_MaxTextBox.Text.Trim();
            string sulphate = sulphateTextBox.Text.Trim();
            string heavyMetal = heavyMetalTextBox.Text.Trim();
            string arsenic = arsenicTextBox.Text.Trim();
            string residueOnMonomer = residueOnMonomerTextBox.Text.Trim();
            string lossOnDrying = lossOnDryingTextBox.Text.Trim();
            string residueOnIgnition = residueOnIgnitionTextBox.Text.Trim();
            string abilityToAbsorb_Min = abilityToAbsorb_MinTextBox.Text.Trim();
            string abilityToAbsorb_Max = abilityToAbsorb_MaxTextBox.Text.Trim();
            string waterproofTest = waterproofTestTextBox.Text.Trim();
            string wvtr = wvtrTextBox.Text.Trim();
            string density_Min = density_MinTextBox.Text.Trim();
            string density_Max = density_MaxTextBox.Text.Trim();
            string thickness2_Min = thickness2_MinTextBox.Text.Trim();
            string thickness2_Max = thickness2_MaxTextBox.Text.Trim();
            string elongationRate_Min = elongationRate_MinTextBox.Text.Trim();
            string elongationRate_Max = elongationRate_MaxTextBox.Text.Trim();
            string corpusAlienum_Ink = corpusAlienum_InkTextBox.Text.Trim();
            string heavyMetal_Ink = heavyMetal_InkTextBox.Text.Trim();
            string viscosity_Ink = viscosity_InkTextBox.Text.Trim();
            string fusionPoint_Ink = fusionPoint_InkTextBox.Text.Trim();
            string refractiveIndex_n20D_Min = refractiveIndex_n20D_MinTextBox.Text.Trim();
            string refractiveIndex_n20D_Max = refractiveIndex_n20D_MaxTextBox.Text.Trim();


            string query = "EXEC InsertComponentItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', ";
            query += "'{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', ";
            query += "'{20}', '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', '{27}', '{28}', '{29}', ";
            query += "'{30}', '{31}', '{32}', '{33}', '{34}', '{35}', '{36}', '{37}', '{38}', '{39}', ";
            query += "'{40}', '{41}', '{42}', '{43}', '{44}', '{45}', '{46}', '{47}', '{48}', '{49}', ";
            query += "'{50}', '{51}', '{52}', '{53}', '{54}', '{55}', '{56}', '{57}', '{58}', '{59}', '{60}'; ";
            query = string.Format(query,
               componentCode,
               componentName,
               deliveryCompanyName,
               purpose,
               fdaName,
               property,
               confirmationTest01,
               confirmationTest02,
               confirmationTest03,
               confirmationTest04,
               confirmationTest05,
               absorbedAmount = (absorbedAmount == "" ? "0" : absorbedAmount),
               citricAcidConfirmationTest,
               pH_Min = (pH_Min == "" ? "0" : pH_Min),
               pH_Max = (pH_Max == "" ? "0" : pH_Max),
               gravity_d20_Min = (gravity_d20_Min == "" ? "0" : gravity_d20_Min),
               gravity_d20_Max = (gravity_d20_Max == "" ? "0" : gravity_d20_Max),
               gravity_d2020_Min = (gravity_d2020_Min == "" ? "0" : gravity_d2020_Min),
               gravity_d2020_Max = (gravity_d2020_Max == "" ? "0" : gravity_d2020_Max),
               viscosity_AstmD3236_88,
               viscosity_AstmD3266_88,
               softeningPoint_AstmE28_67_Min = (softeningPoint_AstmE28_67_Min == "" ? "0" : softeningPoint_AstmE28_67_Min),
               softeningPoint_AstmE28_67_Max = (softeningPoint_AstmE28_67_Max == "" ? "0" : softeningPoint_AstmE28_67_Max),
               nonVolatile_Ksm0009,
               lignin,
               pigment,
               acidAndAlkali,
               fluorescence,
               ashTest = (ashTest == "" ? "0" : ashTest),
               formaldehyde,
               strength,
               sedimentationRate,
               elongationRate,
               thickness_Min = (thickness_Min == "" ? "0" : thickness_Min),
               thickness_Max = (thickness_Max == "" ? "0" : thickness_Max),
               tensileStrength_200PM_Min = (tensileStrength_200PM_Min == "" ? "0" : tensileStrength_200PM_Min),
               tensileStrength_200PM_Max = (tensileStrength_200PM_Max == "" ? "0" : tensileStrength_200PM_Max),
               tensileStrength_Min = (tensileStrength_Min == "" ? "0" : tensileStrength_Min),
               tensileStrength_Max = (tensileStrength_Max == "" ? "0" : tensileStrength_Max),
               sulphate,
               heavyMetal,
               arsenic,
               residueOnMonomer = (residueOnMonomer == "" ? "0" : residueOnMonomer),
               lossOnDrying = (lossOnDrying == "" ? "0" : lossOnDrying),
               residueOnIgnition = (residueOnIgnition == "" ? "0" : residueOnIgnition),
               abilityToAbsorb_Min = (abilityToAbsorb_Min == "" ? "0" : abilityToAbsorb_Min),
               abilityToAbsorb_Max = (abilityToAbsorb_Max == "" ? "0" : abilityToAbsorb_Max),
               waterproofTest,
               wvtr,
               density_Min = (density_Min == "" ? "0" : density_Min),
               density_Max = (density_Max == "" ? "0" : density_Max),
               thickness2_Min = (thickness2_Min == "" ? "0" : thickness2_Min),
               thickness2_Max = (thickness2_Max == "" ? "0" : thickness2_Max),
               elongationRate_Min = (elongationRate_Min == "" ? "0" : elongationRate_Min),
               elongationRate_Max = (elongationRate_Max == "" ? "0" : elongationRate_Max),
               corpusAlienum_Ink,
               heavyMetal_Ink,
               viscosity_Ink,
               fusionPoint_Ink,
               refractiveIndex_n20D_Min = (refractiveIndex_n20D_Min == "" ? "0" : refractiveIndex_n20D_Min),
               refractiveIndex_n20D_Max = (refractiveIndex_n20D_Max == "" ? "0" : refractiveIndex_n20D_Max)
                    );

            long retVal = DbHelper.ExecuteNonQuery(query);
            if (retVal != -1)
            {
                _parent.componentDataGridView.Rows.Add(false,
               componentCode,
               componentName,
               deliveryCompanyName,
               purpose,
               fdaName,
               property,
               confirmationTest01,
               confirmationTest02,
               confirmationTest03,
               confirmationTest04,
               confirmationTest05,
               absorbedAmount,
               citricAcidConfirmationTest,
               pH_Min + "-" + pH_Max,
               gravity_d20_Min + "-" + gravity_d20_Max,
               gravity_d2020_Min + "-" + gravity_d2020_Max,
               viscosity_AstmD3236_88,
               viscosity_AstmD3266_88,
               softeningPoint_AstmE28_67_Min + "-" + softeningPoint_AstmE28_67_Max,
               nonVolatile_Ksm0009,
               lignin,
               pigment,
               acidAndAlkali,
               fluorescence,
               ashTest,
               formaldehyde,
               strength,
               sedimentationRate,
               elongationRate,
               thickness_Min + "-" + thickness_Max,
               tensileStrength_200PM_Min + "-" + tensileStrength_200PM_Max,
               tensileStrength_Min + "-" + tensileStrength_Max,
               sulphate,
               heavyMetal,
               arsenic,
               residueOnMonomer,
               lossOnDrying,
               residueOnIgnition,
               abilityToAbsorb_Min + "-" + abilityToAbsorb_Max,
               waterproofTest,
               wvtr,
               density_Min + "-" + density_Max,
               thickness2_Min + "-" + thickness2_Max,
               elongationRate_Min + "-" + elongationRate_Max,
               corpusAlienum_Ink,
               heavyMetal_Ink,
               viscosity_Ink,
               fusionPoint_Ink,
               refractiveIndex_n20D_Min + "-" + refractiveIndex_n20D_Max
               );

                Utils.OddDataGridViewRow(_parent.componentDataGridView);
                MessageBox.Show("원료를 추가했습니다.");
                this.Close();
            }
            else
            {
                MessageBox.Show("원료를 추가할 수 없습니다.");
            }
        }

        private bool CheckRequiredComponentItems()
        {
            string componentCode = componentCodeTextBox.Text.Trim();
            string componentName = componentNameTextBox.Text.Trim();
            /*
            string componentDesc = componentDescTextBox.Text.Trim();
            string productAreaTypeId = Utils.GetSelectedComboBoxItemValue(productAreaTypeComboBox);
            string productAreaTypeName = Utils.GetSelectedComboBoxItemText(productAreaTypeComboBox);
            string itemDesc = itemDescTextBox.Text.Trim();
            string maker = makerTextBox.Text.Trim();
            string lotNo = lotNoTextBox.Text.Trim();
            string mainLotNo = mainLotNoTextBox.Text.Trim();
            string baffleReport = baffleReportTextBox.Text.Trim();
            string sampleOx = Utils.GetSelectedComboBoxItemValue(sampleOxComboBox, "n");
            string ash = ashTextBox.Text.Trim();
            string wvtr = pH_MinTextBox.Text.Trim();
            string basicWeight = basicWeightTextBox.Text.Trim();
            string gravimeter = gravimeterTextBox.Text.Trim();
            string viscosity = viscosityTextBox.Text.Trim();
            string softeningPoint = softeningPointTextBox.Text.Trim();
            string ph = phTextBox.Text.Trim();
            string tensileStrength = tensileStrengthTextBox.Text.Trim();
            string thickness = thicknessTextBox.Text.Trim();

            if (componentCode == "" || componentName == "" || componentDesc == "" || productAreaTypeId == "0" || itemDesc == "" ||
                maker == "" || lotNo == "" || mainLotNo == "" || baffleReport == "" || //sampleOx == "-1" ||
                ash == "" || wvtr == "" || basicWeight == "" || gravimeter == "" || viscosity == "" ||
                softeningPoint == "" || ph == "" || tensileStrength == "" || thickness == "") 
            {
                MessageBox.Show("필수 항목을 모두 입력해 주십시오.");
                return false;
            }
            */

            if (componentCode == "" || componentName == "")
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

        private void saveTestMethodButton_Click(object sender, EventArgs e)
        {
            if (CheckRequiredTestMethodItems() == false)
                return;


            DataGridView dataGridView = testMethodDataGridView;
            string[] queryArray = new string[dataGridView.Rows.Count];

            queryArray[0] = "EXEC DeleteAllTestMethodItems 0";
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                string testType = "0"; // 0:원료 1:완제품
                string testItemId = Utils.GetString(dataGridView.Rows[i].Cells[(int)eTestMethod.testItemId].Value);
                string testItem = Utils.GetString(dataGridView.Rows[i].Cells[(int)eTestMethod.testItem].Value);
                string testMethod = Utils.GetString(dataGridView.Rows[i].Cells[(int)eTestMethod.testMethod].Value);
                string testStandard = Utils.GetString(dataGridView.Rows[i].Cells[(int)eTestMethod.testStandard].Value);
                string testScore = Utils.GetString(dataGridView.Rows[i].Cells[(int)eTestMethod.testScore].Value);

                string query = "EXEC InsertTestMethodItem '{0}', '{1}', '{2}', '{3}', {4}', '{5}' ";
                query = string.Format(query,
                    testType,
                    testItemId,
                    testItem,
                    testMethod,
                    testStandard,
                    testScore);

                queryArray[i + 1] = query;
            }
            long retVal = DbHelper.ExecuteNonQueryWithTransaction(queryArray);
            if (retVal != -1)
            {
                MessageBox.Show("시험방법을 저장했습니다.");
            }
        }

        private void testMethodDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            int sad = (int)eTestMethod.deleteButton;

            if (e.ColumnIndex == (int)eTestMethod.deleteButton +1 && e.RowIndex !=  senderGrid.Rows.Count -1)
            {
                senderGrid.Rows.RemoveAt(e.RowIndex);
            }
        }

        private bool CheckRequiredTestMethodItems()
        {
            DataGridView dataGridView = testMethodDataGridView;

            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                //string testType = "0"; // 0:원료 1:완제품
                string testItemId = Utils.GetString(dataGridView.Rows[i].Cells[(int)eTestMethod.testItemId].Value);
                string testItem = Utils.GetString(dataGridView.Rows[i].Cells[(int)eTestMethod.testItem].Value);
                string testMethod = Utils.GetString(dataGridView.Rows[i].Cells[(int)eTestMethod.testMethod].Value);

                //if (testItemId == "" || testItem == "" || testMethod == "")
                if (testItemId == "" || testItem == "") 
                {
                    MessageBox.Show("필수 항목을 모두 입력해 주십시오.");
                    return false;
                }
            }
            return true;
        }

        private void downloadSampleButton_Click(object sender, EventArgs e)
        {
            ImportExport.ExcelExportForm form = new ImportExport.ExcelExportForm();
            form.SetMasterFileType((int)Global.eMasterFileType.componentTestMethod);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            ImportExport.ExcelImportForm form = new ImportExport.ExcelImportForm();
            form.SetMasterFileType((int)Global.eMasterFileType.componentTestMethod);
            form.StartPosition = FormStartPosition.CenterParent;
            DialogResult retVal = form.ShowDialog();
            if (retVal == DialogResult.OK)
            {
                SelectTestMethodList();
            }
        }
    }
}
