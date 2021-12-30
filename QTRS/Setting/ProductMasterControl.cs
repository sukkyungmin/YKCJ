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
    public partial class ProductMasterControl : UserControl
    {
        enum eProductList { checkBox, productCode, productName, productDesc, fdaCode, fdaName, dosageForm, property, machine, standardOnAbsorbedAmount, note }
        private bool _isLoaded = false;
        private CheckBox _columnCheckBox = null;
        private long _checkedRowCount = 0;

        public ProductMasterControl()
        {
            InitializeComponent();
        }

        private void ProductMasterControl_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void InitControls()
        {
            //InitRawDatas();
            InitDataGrid();
            _isLoaded = true;
            GetData();
        }

        private void InitDataGrid()
        {
            DataGridView dataGridView = productDataGridView;

            string[] columnNames = { "", "제품 코드", "제품 이름", "제품 설명", "식약처 코드", "식약처 제품명", "제형", "성상", "기계", "흡수량 기준", "비고" };
            DataGridViewCheckBoxColumn checkboxCol = new DataGridViewCheckBoxColumn();
            checkboxCol.Name = "Column0";
            checkboxCol.HeaderText = "";
            dataGridView.Columns.Add(checkboxCol);
            for (int i = 1; i < columnNames.Length; i++)
            {
                dataGridView.Columns.Add("Column" + i.ToString(), columnNames[i]);
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
            dataGridView.Columns[(int)eProductList.checkBox].ReadOnly = false;
            for (int i = (int)eProductList.productCode; i <= (int)eProductList.property; i++)
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
            for (int j = 0; j < productDataGridView.RowCount; j++)
            {
                productDataGridView[0, j].Value = _columnCheckBox.Checked;
            }

            if (_columnCheckBox.Checked == true)
                _checkedRowCount = productDataGridView.Rows.Count;
            else
                _checkedRowCount = 0;

            productDataGridView.EndEdit();
        }

        private void productDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == (int)eProductList.checkBox)
            {
                if (Convert.ToBoolean(productDataGridView[0, e.RowIndex].Value) == true)
                    _checkedRowCount++;
                else
                    _checkedRowCount--;

                _columnCheckBox.CheckedChanged -= new EventHandler(checkBoxColumn_CheckedChanged);
                _columnCheckBox.Checked = (_checkedRowCount == productDataGridView.Rows.Count) ? true : false;
                _columnCheckBox.CheckedChanged += new EventHandler(checkBoxColumn_CheckedChanged);
            }
        }

        private void productDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (productDataGridView.IsCurrentCellDirty)
            {
                productDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            SelectProductList();
        }

        private void SelectProductList()
        {
            ////this.Enabled = false;

            string productCode = productCodeTextBox.Text.Trim();
            string productName = productNameTextBox.Text.Trim();

            string query = string.Format("EXEC SelectProductList '{0}', '{1}'", productCode, productName);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("완제품 데이터를 가져올 수 없습니다.");
                return;
            }

            productDataGridView.Rows.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

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
                string machine = dataRow["machine"].ToString();
                string standardOnAbsorbedAmount = dataRow["standardOnAbsorbedAmount"].ToString();
                string note = dataRow["note"].ToString();

                productDataGridView.Rows.Add(false, productCode, productName, productDesc, fdaCode, fdaName, dosageForm, property, machine, standardOnAbsorbedAmount, note);
                Utils.OddDataGridViewRow(productDataGridView);
            }

            productDataGridView.ClearSelection();

            ////this.Enabled = true; 
        }

        public void GetData()
        {
            if (_isLoaded == true)
                SelectProductList();
        }

        private void addProductButton_Click(object sender, EventArgs e)
        {
            AddProductForm form = new AddProductForm(this);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void deleteProductButton_Click(object sender, EventArgs e)
        {
            string ids = ""; 
            for (int i = 0; i < productDataGridView.Rows.Count; i++)
            {
                if (Convert.ToBoolean(productDataGridView.Rows[i].Cells[(int)eProductList.checkBox].Value) == true)
                {
                    ids += ("''" + productDataGridView.Rows[i].Cells[(int)eProductList.productCode].Value.ToString() + "'',");
                }
            }
            if(ids != "")
                ids = ids.Substring(0, ids.Length - 1);

            if (ids == "")
            {
                MessageBox.Show("삭제할 항목을 선택해 주십시오.");
                return;
            }

            if (MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "항목 삭제", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                long retVal = DbHelper.ExecuteNonQuery(string.Format("EXEC DeleteProductItem '{0}'", ids));

                if (retVal != -1)
                {
                    for (int i = 0; i < productDataGridView.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(productDataGridView.Rows[i].Cells[(int)eProductList.checkBox].Value) == true)
                        {
                            productDataGridView.Rows.RemoveAt(i);
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
            form.SetMasterFileType((int)Global.eMasterFileType.product);
            form.StartPosition = FormStartPosition.CenterParent;
            DialogResult retVal = form.ShowDialog();
            if (retVal == DialogResult.OK)
            {
                SelectProductList();
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            ImportExport.ExcelExportForm form = new ImportExport.ExcelExportForm();
            form.SetMasterFileType((int)Global.eMasterFileType.product);
            form.SetDataGridView(productDataGridView);
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


            productDataGridView.Width = (Parent.Width - productDataGridView.Left) - 40;

            //searchPanel.Left = componentDataGridView.Right - searchPanel.Width;

            deleteProductButton.Left = productDataGridView.Right - deleteProductButton.Width;
            addProductButton.Left = deleteProductButton.Left - (addProductButton.Width + 6);

            importButton.Left = productDataGridView.Left;
            exportButton.Left = productDataGridView.Right - exportButton.Width;

            // Height, Top
            productDataGridView.Height = this.Height - (productDataGridView.Top + importButton.Height + 40);
            importButton.Top = productDataGridView.Bottom + 10;
            exportButton.Top = productDataGridView.Bottom + 10;
        }
    }
}
