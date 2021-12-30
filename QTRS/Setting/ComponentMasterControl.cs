using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.Setting
{
    public partial class ComponentMasterControl : UserControl
    {
        enum eComponentList
        {
            checkBox, componentCode, componentName, deliveryCompanyName, purpose, fdaName, property,
            confirmationTest01, confirmationTest02, confirmationTest03, confirmationTest04, confirmationTest05,
            absorbedAmount, citricAcidConfirmationTest, ph, gravity_d20, gravity_d2020,
            softeningPoint_AstmE28_67, lignin, pigment, acidAndAlkali, fluorescence,
            formaldehyde, strength, sedimentationRate, elongationRate, thickness, tensileStrength_200PM, tensileStrength,
            sulphate, heavyMetal, arsenic, residueOnMonomer, lossOnDrying, residueOnIgnition, abilityToAbsorb, waterproofTest,
            wvtr, density, thickness2, elongationRateNum, corpusAlienum_Ink, fusionPoint_Ink, refractiveIndex_n20D,
            tensileStrength_50PM, pigment1, pigment12, acidAndAlkali1, acidAndAlkali2, acidAndAlkali3, sedimentationRate1, Viscosity, SpecificGravity_d25,
            viscosity_AstmD3236_88, viscosity_AstmD3266_88, nonVolatile_Ksm0009, ashTest, heavyMetal_Ink, viscosity_Ink
        }
        private bool _isLoaded = false;
        private CheckBox _columnCheckBox = null;
        private long _checkedRowCount = 0;

        public ComponentMasterControl()
        {
            InitializeComponent();
        }

        private void ComponentMasterControl_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void InitControls()
        {
            InitRawDatas(); 
            InitDataGrid();
            _isLoaded = true;
            GetData();
        }

        private void InitRawDatas()
        {
            /*
            ////this.Enabled = false;

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

            productAreaTypeComboBox.SelectedIndex = 0;

            ////this.Enabled = true;
            */ 
        }


        private void InitDataGrid()
        {
            DataGridView dataGridView = componentDataGridView;

            string [] columnNames = { "원료 코드", "원료 이름", "남품처명", "용도", "식약처 허가명", "성상", "확인시험 1", "확인시험 2", "확인시험 3", "확인시험 4", "확인시험 5",
                                      "흡수량", "구연산 확인시험", "pH", "비중d20(측정)", "비중d2020", "연화점 (ASTM E 28_67)",
                                      "리구닌", "색소", "산 또는 알칼리", "형광", "포름알데히드", "강도", "침강속도", "신장율", "굵기", "200%M 인장강도",
                                      "인장강도", "황산염", "중금속", "비소", "잔존모노머", "건조감량", "강열잔분", "흡수능력", "방수시험", "투습도", "밀도", "두께", "신장율", "이물(잉크)",
                                      "융점(잉크)", "굴절률 n20D","50%M 인장강도","색소1","색소2","산 또는 알카리1","산 또는 알카리2","산 또는 알카리3","침강속도1","점도","비중d25",
                                      "점도 (ASTM D 3236_88)", "점도 (ASTM D 3266_88)", "비휘발성 (KSM 0009)", "회분시험", "중금속(잉크)", "점도(잉크)"};

            DataGridViewCheckBoxColumn checkboxCol = new DataGridViewCheckBoxColumn() { Name = "Column0", HeaderText = "" };
            dataGridView.Columns.Add(checkboxCol);
            for (int i=0; i<columnNames.Length; i++)
            {
                dataGridView.Columns.Add("Column" + (i+1).ToString(), columnNames[i]);
            }

            // column checkbox
            //Rectangle rect = dataGridView.GetCellDisplayRectangle((int)eProductList.checkBox, -1, true);
            _columnCheckBox = new CheckBox();
            _columnCheckBox.Size = new Size(13, 13);
            _columnCheckBox.Location = new Point(3, 3);
            _columnCheckBox.CheckedChanged += new EventHandler(checkBoxColumn_CheckedChanged);

            dataGridView.Controls.Add(_columnCheckBox);


            // Common style
            dataGridView.ReadOnly = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
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

            // Each column style 
            dataGridView.Columns[(int)eComponentList.checkBox].ReadOnly = false;
            for (int i = (int)eComponentList.componentCode; i <= (int)eComponentList.viscosity_Ink; i++)
                dataGridView.Columns[i].ReadOnly = true;


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

        void checkBoxColumn_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < componentDataGridView.RowCount; j++)
            {
                componentDataGridView[0, j].Value = _columnCheckBox.Checked;
            }

            if (_columnCheckBox.Checked == true)
                _checkedRowCount = componentDataGridView.Rows.Count;
            else
                _checkedRowCount = 0;

            componentDataGridView.EndEdit();
        }

        private void componentDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)eComponentList.checkBox)
            {
                if (Convert.ToBoolean(componentDataGridView[0, e.RowIndex].Value) == true)
                    _checkedRowCount++;
                else
                    _checkedRowCount--;

                _columnCheckBox.CheckedChanged -= new EventHandler(checkBoxColumn_CheckedChanged);
                _columnCheckBox.Checked = (_checkedRowCount == componentDataGridView.Rows.Count) ? true : false;
                _columnCheckBox.CheckedChanged += new EventHandler(checkBoxColumn_CheckedChanged);
            }
        }

        private void componentDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (componentDataGridView.IsCurrentCellDirty)
            {
                componentDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            SelectComponentList();
        }

        private void SelectComponentList()
        {
            //////this.Enabled = false;

            string componentCode = componentCodeTextBox.Text.Trim();
            string componentName = componentNameTextBox.Text.Trim();

            //string query = string.Format("EXEC SelectComponentList {0}, '{1}'", productAreaTypeId, componentCode);
            string query = string.Format("EXEC SelectComponentList '{0}', '{1}'", componentCode, componentName);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("원료 데이터를 가져올 수 없습니다.");
                return;
            }

            componentDataGridView.Rows.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string idx = dataRow["idx"].ToString();
                componentCode = dataRow["componentCode"].ToString();
                componentName = dataRow["componentName"].ToString();
                string deliveryCompanyName = dataRow["deliveryCompanyName"].ToString();
                string purpose = dataRow["purpose"].ToString();
                string fdaName = dataRow["fdaName"].ToString();
                string property = dataRow["property"].ToString();
                string confirmationTest01 = dataRow["confirmationTest01"].ToString();
                string confirmationTest02 = dataRow["confirmationTest02"].ToString();
                string confirmationTest03 = dataRow["confirmationTest03"].ToString();
                string confirmationTest04 = dataRow["confirmationTest04"].ToString();
                string confirmationTest05 = dataRow["confirmationTest05"].ToString();
                string absorbedAmount = dataRow["absorbedAmount"].ToString();
                string citricAcidConfirmationTest = dataRow["citricAcidConfirmationTest"].ToString();
                string pH_Min = dataRow["pH_Min"].ToString();
                string pH_Max = dataRow["pH_Max"].ToString();
                string gravity_d20_Min = dataRow["gravity_d20_Min"].ToString();
                string gravity_d20_Max = dataRow["gravity_d20_Max"].ToString();
                string gravity_d2020_Min = dataRow["gravity_d2020_Min"].ToString();
                string gravity_d2020_Max = dataRow["gravity_d2020_Max"].ToString();
                string softeningPoint_AstmE28_67_Min = dataRow["softeningPoint_AstmE28_67_Min"].ToString();
                string softeningPoint_AstmE28_67_Max = dataRow["softeningPoint_AstmE28_67_Max"].ToString();
                string lignin = dataRow["lignin"].ToString();
                string pigment = dataRow["pigment"].ToString();
                string acidAndAlkali = dataRow["acidAndAlkali"].ToString();
                string fluorescence = dataRow["fluorescence"].ToString();
                string formaldehyde = dataRow["formaldehyde"].ToString();
                string strength = dataRow["strength"].ToString();
                string sedimentationRate = dataRow["sedimentationRate"].ToString();
                string elongationRate = dataRow["elongationRate"].ToString();
                string thickness_Min = dataRow["thickness_Min"].ToString();
                string thickness_Max = dataRow["thickness_Max"].ToString();
                string tensileStrength_200PM_Min = dataRow["tensileStrength_200PM_Min"].ToString();
                string tensileStrength_200PM_Max = dataRow["tensileStrength_200PM_Max"].ToString();
                string tensileStrength_Min = dataRow["tensileStrength_Min"].ToString();
                string tensileStrength_Max = dataRow["tensileStrength_Max"].ToString();
                string sulphate = dataRow["sulphate"].ToString();
                string heavyMetal = dataRow["heavyMetal"].ToString();
                string arsenic = dataRow["arsenic"].ToString();
                string residueOnMonomer = dataRow["residueOnMonomer"].ToString();
                string lossOnDrying = dataRow["lossOnDrying"].ToString();
                string residueOnIgnition = dataRow["residueOnIgnition"].ToString();
                string abilityToAbsorb_Min = dataRow["abilityToAbsorb_Min"].ToString();
                string abilityToAbsorb_Max = dataRow["abilityToAbsorb_Max"].ToString();
                string waterproofTest = dataRow["waterproofTest"].ToString();
                string wvtr = dataRow["wvtr"].ToString();
                string density_Min = dataRow["density_Min"].ToString();
                string density_Max = dataRow["density_Max"].ToString();
                string thickness2_Min = dataRow["thickness2_Min"].ToString();
                string thickness2_Max = dataRow["thickness2_Max"].ToString();
                string elongationRate_Min = dataRow["elongationRate_Min"].ToString();
                string elongationRate_Max = dataRow["elongationRate_Max"].ToString();
                string corpusAlienum_Ink = dataRow["corpusAlienum_Ink"].ToString();
                string fusionPoint_Ink = dataRow["fusionPoint_Ink"].ToString();
                string refractiveIndex_n20D_Min = dataRow["refractiveIndex_n20D_Min"].ToString();
                string refractiveIndex_n20D_Max = dataRow["refractiveIndex_n20D_Max"].ToString();
                // 20.08.03 신규 추가
                string tensileStrength_50PM = dataRow["tensileStrength_50PM"].ToString();
                string pigment1 = dataRow["pigment1"].ToString();
                string pigment2 = dataRow["pigment2"].ToString();
                string acidAndAlkali1 = dataRow["acidAndAlkali1"].ToString();
                string acidAndAlkali2 = dataRow["acidAndAlkali2"].ToString();
                string acidAndAlkali3 = dataRow["acidAndAlkali3"].ToString();
                string sedimentationRate1 = dataRow["sedimentationRate1"].ToString();
                string Viscosity_Min = dataRow["Viscosity_Min"].ToString();
                string Viscosity_Max = dataRow["Viscosity_Max"].ToString();
                string SpecificGravity_d25_Min = dataRow["SpecificGravity_d25_Min"].ToString();
                string SpecificGravity_d25_Max = dataRow["SpecificGravity_d25_Max"].ToString();
                // 20.08.10 추가
                string viscosity_AstmD3236_88_Min = dataRow["viscosity_AstmD3236_88_Min"].ToString();
                string viscosity_AstmD3236_88_Max = dataRow["viscosity_AstmD3236_88_Max"].ToString();
                string viscosity_AstmD3266_88_Min = dataRow["viscosity_AstmD3266_88_Min"].ToString();
                string viscosity_AstmD3266_88_Max = dataRow["viscosity_AstmD3266_88_Max"].ToString();
                string nonVolatile_Ksm0009_Min = dataRow["nonVolatile_Ksm0009_Min"].ToString();
                string nonVolatile_Ksm0009_Max = dataRow["nonVolatile_Ksm0009_Max"].ToString();
                string ashTest_Min = dataRow["ashTest_Min"].ToString();
                string ashTest_Max = dataRow["ashTest_Max"].ToString();
                string heavyMetal_Ink_Min = dataRow["heavyMetal_Ink_Min"].ToString();
                string heavyMetal_Ink_Max = dataRow["heavyMetal_Ink_Max"].ToString();
                string viscosity_Ink_Min = dataRow["viscosity_Ink_Min"].ToString();
                string viscosity_Ink_Max = dataRow["viscosity_Ink_Max"].ToString();

                   componentDataGridView.Rows.Add(false,
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
                   string.Format("{0}-{1}", pH_Min, pH_Max),
                   string.Format("{0}-{1}", gravity_d20_Min, gravity_d20_Max),
                   string.Format("{0}-{1}", gravity_d2020_Min, gravity_d2020_Max),
                   string.Format("{0}-{1}", softeningPoint_AstmE28_67_Min, softeningPoint_AstmE28_67_Max),
                   lignin,
                   pigment,
                   acidAndAlkali,
                   fluorescence,
                   formaldehyde,
                   strength,
                   sedimentationRate,
                   elongationRate,
                   string.Format("{0}-{1}", thickness_Min, thickness_Max),
                   string.Format("{0}-{1}", tensileStrength_200PM_Min, tensileStrength_200PM_Max),
                   string.Format("{0}-{1}", tensileStrength_Min, tensileStrength_Max),
                   sulphate,
                   heavyMetal,
                   arsenic,
                   residueOnMonomer,
                   lossOnDrying,
                   residueOnIgnition,
                   string.Format("{0}-{1}", abilityToAbsorb_Min, abilityToAbsorb_Max),
                   waterproofTest,
                   wvtr,
                   string.Format("{0}-{1}", density_Min, density_Max),
                   string.Format("{0}-{1}", thickness2_Min, thickness2_Max),
                   string.Format("{0}-{1}", elongationRate_Min, elongationRate_Max),
                   corpusAlienum_Ink,
                   fusionPoint_Ink,
                   string.Format("{0}-{1}", refractiveIndex_n20D_Min, refractiveIndex_n20D_Max),
                   tensileStrength_50PM,
                   pigment1,
                   pigment2,
                   acidAndAlkali1,
                   acidAndAlkali2,
                   acidAndAlkali3,
                   sedimentationRate1,
                   string.Format("{0}-{1}", Viscosity_Min, Viscosity_Max),
                   string.Format("{0}-{1}", SpecificGravity_d25_Min, SpecificGravity_d25_Max),
                   string.Format("{0}-{1}", viscosity_AstmD3236_88_Min, viscosity_AstmD3236_88_Max),
                   string.Format("{0}-{1}", viscosity_AstmD3266_88_Min, viscosity_AstmD3266_88_Max),
                   string.Format("{0}-{1}", nonVolatile_Ksm0009_Min, nonVolatile_Ksm0009_Max),
                   string.Format("{0}-{1}", ashTest_Min, ashTest_Max),
                   string.Format("{0}-{1}", heavyMetal_Ink_Min, heavyMetal_Ink_Max),
                   string.Format("{0}-{1}", viscosity_Ink_Min, viscosity_Ink_Max));
                   Utils.OddDataGridViewRow(componentDataGridView);
            }

            componentDataGridView.ClearSelection();
            
            ////this.Enabled = true;
        }

        public void GetData()
        {
            if (_isLoaded == true)
                SelectComponentList();
        }

        private void addComponentButton_Click(object sender, EventArgs e)
        {
            AddComponentForm form = new AddComponentForm(this);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void deleteComponentButton_Click(object sender, EventArgs e)
        {
            string ids = "";
            for (int i = 0; i < componentDataGridView.Rows.Count; i++)
            {
                if (Convert.ToBoolean(componentDataGridView.Rows[i].Cells[(int)eComponentList.checkBox].Value) == true)
                {
                    ids += ("''" + componentDataGridView.Rows[i].Cells[(int)eComponentList.componentCode].Value.ToString() + "'',");
                }
            }
            if (ids != "")
                ids = ids.Substring(0, ids.Length - 1);

            if (ids == "")
            {
                MessageBox.Show("삭제할 항목을 선택해 주십시오.");
                return;
            }

            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "항목 삭제", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                long retVal = DbHelper.ExecuteNonQuery(string.Format("EXEC DeleteComponentItem '{0}'", ids));

                if (retVal != -1)
                {
                    for (int i = 0; i < componentDataGridView.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(componentDataGridView.Rows[i].Cells[(int)eComponentList.checkBox].Value) == true)
                        {
                            componentDataGridView.Rows.RemoveAt(i);
                            i--;
                        }
                    }

                    MessageBox.Show("선택한 항목을 삭제했습니다.");
                }
                else
                {
                    MessageBox.Show("선택한 항목을 삭제 할 수 없습니다.");
                }
            }
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            ImportExport.ExcelImportForm form = new ImportExport.ExcelImportForm();
            form.SetMasterFileType((int)Global.eMasterFileType.component);
            form.StartPosition = FormStartPosition.CenterParent;
            DialogResult retVal = form.ShowDialog(); 
            if(retVal == DialogResult.OK)
            {
                SelectComponentList(); 
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            ImportExport.ExcelExportForm form = new ImportExport.ExcelExportForm();
            form.SetMasterFileType((int)Global.eMasterFileType.component);
            form.SetDataGridView(componentDataGridView);
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


            componentDataGridView.Width = (Parent.Width - componentDataGridView.Left) - 40;

            //searchPanel.Left = componentDataGridView.Right - searchPanel.Width;

            deleteComponentButton.Left = componentDataGridView.Right - deleteComponentButton.Width;
            addComponentButton.Left = deleteComponentButton.Left - (addComponentButton.Width + 6);

            importButton.Left = componentDataGridView.Left;
            exportButton.Left = componentDataGridView.Right - exportButton.Width;


            // Height, Top
            componentDataGridView.Height = this.Height - (componentDataGridView.Top + importButton.Height + 40);
            importButton.Top = componentDataGridView.Bottom + 10;
            exportButton.Top = componentDataGridView.Bottom + 10;
        }

      
    }
}
