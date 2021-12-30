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
    public partial class AddProductForm : Form
    {
        enum eComponentList { componentCode, componentName, mixPurpose, innerComponentName, logicalQuantity, basicWeight, area, usage, note }
        enum eTestMethod
        {
            testItemId, testItem, testMethod, testStandard, testScore, deleteButton
        }

        private ProductMasterControl _parent = null; 

        public AddProductForm(ProductMasterControl parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void AddProductForm_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void InitControls()
        {
            //this.Enabled = false;

            InitComponentDataGrid();
            InitTestMethodDataGrid();
            InitComponentList();
            SelectTestMethodList();

            ////this.Enabled = true;
        }

        private void InitComponentDataGrid()
        {
            DataGridView[] dataGridViewArray = { componentDataGridView, assignedComponentDdataGridView };
            for (int i = 0; i < dataGridViewArray.Length; i++)
            {
                DataGridView dataGridView = dataGridViewArray[i];

                string[] columnNames = { "원료 코드", "원료 이름", "배합목적", "내부원료명", "이론량", "평량", "면적", "사용량", "비고"};
                for (int ci = 0; ci < columnNames.Length; ci++)
                {
                    if (i == 0 && ci > 1)
                        continue;
                    dataGridView.Columns.Add("Column" + ci.ToString(), columnNames[ci]);
                }

                // Common style
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
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


                if(i == 1)
                {
                    dataGridView.Columns[(int)eComponentList.componentCode].ReadOnly = true; 
                    dataGridView.Columns[(int)eComponentList.componentName].ReadOnly = true; 
                }
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

                dataGridView.MultiSelect = false;
            }
        }

        private void InitTestMethodDataGrid()
        {
            DataGridView dataGridView = testMethodDataGridView;

            string[] columnNames = { "시험 항목 ID", "시험 항목", "시험 방법", "시험 기준", "시험 성적" };
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


            dataGridView.Columns[(int)eTestMethod.testMethod].Visible = false; 

            // Common style
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
            dataGridView.RowHeadersVisible = true;

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

            dataGridView.MultiSelect = false;
        }

        private void InitComponentList()
        {
            DataSet dataSet = DbHelper.SelectQuery("EXEC SelectComponentListToAddProduct");
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("원료 정보를 가져올 수 없습니다.");
                ////this.Enabled = true;
                return;
            }

            componentDataGridView.Rows.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
            {
                ////this.Enabled = true;
                return;
            }

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string componentCode = dataRow["componentCode"].ToString();
                string componentName = dataRow["componentName"].ToString();

                componentDataGridView.Rows.Add(componentCode, componentName); 
                Utils.OddDataGridViewRow(componentDataGridView);
            }

            componentDataGridView.ClearSelection();
        }

        private void SelectTestMethodList()
        {
            string query = "EXEC SelectTestMethodList 1";

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
                string testMethod = ""; // dataRow["testMethod"].ToString();
                string testStandard = dataRow["testStandard"].ToString();
                string testScore = dataRow["testScore"].ToString();

                testMethodDataGridView.Rows.Add(testItemId, testItem, testMethod, testStandard, testScore);
            }

            testMethodDataGridView.ClearSelection();
        }


        private void addComponentButton_Click(object sender, EventArgs e)
        {
            if (componentDataGridView.SelectedRows == null)
                return;

            DataGridViewSelectedRowCollection selectedRows = componentDataGridView.SelectedRows;
            int selectedRowCount = selectedRows.Count;

            for (int i = 0; i < selectedRowCount; i++)
            {

                string componentCode = selectedRows[i].Cells[(int)eComponentList.componentCode].Value.ToString();
                string componentName = selectedRows[i].Cells[(int)eComponentList.componentName].Value.ToString();

                assignedComponentDdataGridView.Rows.Add(componentCode, componentName);
                componentDataGridView.Rows.Remove(selectedRows[i]);
            }

            componentDataGridView.ClearSelection();
            assignedComponentDdataGridView.ClearSelection();
        }

        private void removeComponentButton_Click(object sender, EventArgs e)
        {
            if (assignedComponentDdataGridView.SelectedRows == null)
                return;

            DataGridViewSelectedRowCollection selectedRows = assignedComponentDdataGridView.SelectedRows;
            int selectedRowCount = selectedRows.Count;

            for (int i = 0; i < selectedRowCount; i++)
            {

                string componentCode = selectedRows[i].Cells[(int)eComponentList.componentCode].Value.ToString();
                string componentName = selectedRows[i].Cells[(int)eComponentList.componentName].Value.ToString();

                componentDataGridView.Rows.Add(componentCode, componentName);
                assignedComponentDdataGridView.Rows.Remove(selectedRows[i]);
            }

            componentDataGridView.ClearSelection();
            assignedComponentDdataGridView.ClearSelection();
        }

        private void addDataButton_Click(object sender, EventArgs e)
        {
            if (CheckRequiredItems() == false)
                return;

            string[] queryArray = new string[3];

            string productCode = productCodeTextBox.Text.Trim();
            string productName = productNameTextBox.Text.Trim();
            string productDesc = productDescTextBox.Text.Trim();
            string fdaCode = fdaCodeTextBox.Text.Trim();
            string fdaName = fdaNameTextBox.Text.Trim();
            string dosageForm = dosageFormTextBox.Text.Trim();
            string property = propertyTextBox.Text.Trim();
            string machine = machineTextBoxTextBox.Text.Trim();
            string standardOnAbsorbedAmount = standardOnAbsorbedAmountTextBox.Text.Trim();
            string note = noteTextBox.Text.Trim();

            string query = "EXEC InsertProductItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' "; 
            query = string.Format(query,
                productCode,
             productName,
             productDesc,
             fdaCode,
             fdaName,
             dosageForm,
             property,
             machine,
             standardOnAbsorbedAmount,
             note
                );

            queryArray[0] = query;
            queryArray[1] = string.Format("EXEC DeleteComponentByProduct '{0}'", productCode);
            queryArray[2] = GetAssignedComponentQuery(productCode);


            long retVal = DbHelper.ExecuteNonQueryWithTransaction(queryArray);
            if (retVal != -1)
            {
                _parent.productDataGridView.Rows.Add(false, productCode, productName, productDesc, fdaCode, fdaName, dosageForm, property, machine, standardOnAbsorbedAmount, note);
                Utils.OddDataGridViewRow(_parent.productDataGridView);
                MessageBox.Show("완제품을 추가했습니다.");
                this.Close();
            }
            else
            {
                MessageBox.Show("완제품을 추가할 수 없습니다.");
            }
        }

        private bool CheckRequiredItems()
        {
            string productCode = productCodeTextBox.Text.Trim();
            //string productName = productNameTextBox.Text.Trim();
            string productDesc = productDescTextBox.Text.Trim();
            /*
            string fdaCode = fdaCodeTextBox.Text.Trim();
            string fdaName = fdaNameTextBox.Text.Trim();
            string dosageForm = dosageFormTextBox.Text.Trim();
            string property = propertyTextBox.Text.Trim();
            */ 
            int assignedComponentCount = assignedComponentDdataGridView.Rows.Count; 

            if (productCode == "" || productDesc == "" )
            {
                MessageBox.Show("필수 항목을 모두 입력해 주십시오.");
                return false;
            }
            else if (assignedComponentCount <= 0)
            {
                MessageBox.Show("최소 1개 이상의 원료를 할당해 주십시오.");
                return false;
            }

            return true;
        }

        private string GetAssignedComponentQuery(string productCode)
        {
            // enum eComponentList { componentCode, mixPurpose, innerComponentName, logicalQuantity, basicWeight, area, usage, note }

            string query = ""; 
            for (int i=0; i< assignedComponentDdataGridView.Rows.Count; i++)
            {
                string componentCode = Utils.GetString(assignedComponentDdataGridView.Rows[i].Cells[(int)eComponentList.componentCode].Value);
                string mixPurpose = Utils.GetString(assignedComponentDdataGridView.Rows[i].Cells[(int)eComponentList.mixPurpose].Value);
                string innerComponentName = Utils.GetString(assignedComponentDdataGridView.Rows[i].Cells[(int)eComponentList.innerComponentName].Value);
                string logicalQuantity = Utils.GetString(assignedComponentDdataGridView.Rows[i].Cells[(int)eComponentList.logicalQuantity].Value);
                string basicWeight = Utils.GetString(assignedComponentDdataGridView.Rows[i].Cells[(int)eComponentList.basicWeight].Value);
                string area = Utils.GetString(assignedComponentDdataGridView.Rows[i].Cells[(int)eComponentList.area].Value);
                string usage = Utils.GetString(assignedComponentDdataGridView.Rows[i].Cells[(int)eComponentList.usage].Value);
                string note = Utils.GetString(assignedComponentDdataGridView.Rows[i].Cells[(int)eComponentList.note].Value);

                logicalQuantity = logicalQuantity == "" ? "0" : logicalQuantity;
                basicWeight = basicWeight == "" ? "0" : basicWeight;
                area = area == "" ? "0" : area;
                usage = usage == "" ? "0" : usage;

                //query += string.Format("('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'),",
                //    productCode, componentCode, mixPurpose, innerComponentName, logicalQuantity, basicWeight, area, usage, note);

                query += (string.Format("EXEC InsertComponentByProductItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'; ",
                    productCode, componentCode, mixPurpose, innerComponentName, logicalQuantity, basicWeight, area, usage, note));
            }

            /*
            if (query.Length > 0)
            {
                query = query.Substring(0, query.Length - 1);
                query = "\"" + query + "\""; 
            }

            query = "EXEC InsertComponentByProductList " + query;
            */ 

            return query; 
        }



        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addProductPage_Click(object sender, EventArgs e)
        {

        }

        private void assignedComponentDdataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(assignedComponentDdataGridView_ColumnKeyPress);

            if (assignedComponentDdataGridView.CurrentCell.ColumnIndex == (int)eComponentList.logicalQuantity ||
                assignedComponentDdataGridView.CurrentCell.ColumnIndex == (int)eComponentList.basicWeight ||
                assignedComponentDdataGridView.CurrentCell.ColumnIndex == (int)eComponentList.area ||
                assignedComponentDdataGridView.CurrentCell.ColumnIndex == (int)eComponentList.usage ) 
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(assignedComponentDdataGridView_ColumnKeyPress);
                }
            }
        }

        private void assignedComponentDdataGridView_ColumnKeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.AcceptOnlyRealNumber(sender, e);
        }

        private void saveTestMethodButton_Click(object sender, EventArgs e)
        {
            if (CheckRequiredTestMethodItems() == false)
                return;


            DataGridView dataGridView = testMethodDataGridView;
            string[] queryArray = new string[dataGridView.Rows.Count];

            queryArray[0] = "EXEC DeleteAllTestMethodItems 1";
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                string testType = "1"; // 0:원료 1:완제품
                string testItemId = Utils.GetString(dataGridView.Rows[i].Cells[(int)eTestMethod.testItemId].Value);
                string testItem = Utils.GetString(dataGridView.Rows[i].Cells[(int)eTestMethod.testItem].Value);
                string testMethod = ""; // Utils.GetString(dataGridView.Rows[i].Cells[(int)eTestMethod.testMethod].Value);
                string testStandard = Utils.GetString(dataGridView.Rows[i].Cells[(int)eTestMethod.testStandard].Value);
                string testScore = Utils.GetString(dataGridView.Rows[i].Cells[(int)eTestMethod.testScore].Value);

                string query = "EXEC InsertTestMethodItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'";
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

        private void cancelTestMethodButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void testMethodDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.ColumnIndex == (int)eTestMethod.deleteButton && e.RowIndex != senderGrid.Rows.Count - 1)
            {
                senderGrid.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void downloadSampleButton_Click(object sender, EventArgs e)
        {
            ImportExport.ExcelExportForm form = new ImportExport.ExcelExportForm();
            form.SetMasterFileType((int)Global.eMasterFileType.productTestMethod);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            ImportExport.ExcelImportForm form = new ImportExport.ExcelImportForm();
            form.SetMasterFileType((int)Global.eMasterFileType.productTestMethod);
            form.StartPosition = FormStartPosition.CenterParent;
            DialogResult retVal = form.ShowDialog();
            if (retVal == DialogResult.OK)
            {
                SelectTestMethodList();
            }
        }
    }
}
