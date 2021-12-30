using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTRS.ProductTest
{
    public partial class AbsorbedAmountTestForm : Form
    {
        enum eAbsorbedAmountList { count, before, after, absorbedAmount }; 

        public string _absorbedAmountData = "";
        private RunQualityTestForm _parent = null; 
        private string _saveMode = "ADD"; 
        //private DataGridViewCell [] _cells = new DataGridViewCell[4];

        public AbsorbedAmountTestForm(RunQualityTestForm parent)
        {
            InitializeComponent();
            _parent = parent; 
        }

        private void absorbedAmountTestForm_Load(object sender, EventArgs e)
        {
            InitControls(); 
        }

        private void InitControls()
        {
            InitProductTestDataGridView();
            if (_saveMode == "UPDATE")
            {
                SetAbsorbedAmountData();

                if(_parent.productQtDataGridView.SelectedRows.Count != 0)
                    beforeAbsorbedAmountAverageTextBox.Text =
                    _parent.productQtDataGridView.SelectedRows[0].Cells[(int)RunQualityTestForm.eProductQtTestResultList.testStandard].Value.ToString();
                //applyDataButton.Visible = false;
                //cancelButton.Text = "닫기";
            }
            else
            {
                beforeAbsorbedAmountAverageTextBox.Text  = _parent.productQtDataGridView.SelectedRows[0].Cells[(int)RunQualityTestForm.eProductQtTestResultList.testStandard].Value.ToString();
            }
        }

        private void InitProductTestDataGridView()
        {
            DataGridView dataGridView = absorbedAmountDataGridView;

            string[] columnNames = { "", "흡수전", "흡수량", "흡수배" };
            for (int i = 0; i < columnNames.Length; i++)
            {
                dataGridView.Columns.Add("Column" + i.ToString(), columnNames[i]);
            }

            //dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // Default column style
            dataGridView.ColumnHeadersVisible = true;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Dotum", 12, FontStyle.Regular);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Global.GRID_COLUMN_FORE_COLOR;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Global.GRID_COLUMN_BACK_COLOR;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersHeight = Global.GRID_COLUMN_HEIGHT * 2; 
            dataGridView.AllowUserToResizeColumns = true;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            //dataGridView.AdvancedColumnHeadersBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;


            // Default row header style
            dataGridView.RowHeadersVisible = false;

            // Default row style
            dataGridView.RowsDefaultCellStyle.Font = new Font("Dotum", 11, FontStyle.Regular);
            dataGridView.RowsDefaultCellStyle.ForeColor = Global.GRID_ROW_FORE_COLOR;
            dataGridView.RowsDefaultCellStyle.BackColor = Global.GRID_ROW_BACK_COLOR;
            dataGridView.RowTemplate.Height = (dataGridView.Height - dataGridView.ColumnHeadersHeight) / 2; // Global.GRID_ROW_HEIGHT * 2; 
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            //changeManagementDataGridView.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            //changeManagementDataGridView.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None; 

            // Common style
            dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridView.GridColor = Global.GRID_COLOR;
            dataGridView.BackgroundColor = Color.White;         // BackgroundColor 
            //dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // SelectionMode
            dataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect; // SelectionMode
                                                                                  //dataGridView.ReadOnly = true;
            dataGridView.ScrollBars = ScrollBars.None;

            int colWidth = dataGridView.Width / 4;
            dataGridView.Columns[0].Width = colWidth;
            dataGridView.Columns[1].Width = colWidth; 
            dataGridView.Columns[2].Width = colWidth; 
            dataGridView.Columns[3].Width = colWidth;

            dataGridView.Rows.Add("1회", "0", "0", "0");
            dataGridView.Rows.Add("2회", "0", "0", "0");

            dataGridView.ClearSelection();

            /*
            _cells[0] = absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.before];
            _cells[1] = absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.after];
            _cells[2] = absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.before];
            _cells[3] = absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.after];
            */

            absorbedAmountDataGridView.Columns[(int)eAbsorbedAmountList.absorbedAmount].ReadOnly = true;

            dataGridView.MultiSelect = false;
            absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.before].Selected = true; 
        }

        private void applyDataButton_Click(object sender, EventArgs e)
        {
            /*
            if(_absorbedAmountData.Trim().Split(' ').Length != 4)
            {
                MessageBox.Show("흡수량을 4번 측정해 주십시오.");
                return;
            }
            */

            _absorbedAmountData =
            absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.before].Value.ToString() + Global.DB_VALUE_SEPARATOR +
            absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.after].Value.ToString() + Global.DB_VALUE_SEPARATOR +
            absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.before].Value.ToString() + Global.DB_VALUE_SEPARATOR +
            absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.after].Value.ToString();


            _parent.productQtDataGridView.SelectedRows[0].Cells[(int)RunQualityTestForm.eProductQtTestResultList.testScore].Value = absorbedAmountAverageTextBox.Text + "배";

            _parent._absorbedAmountData = _absorbedAmountData; 
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //public void SetAbsorbedAmountData(string absorbedAmountData)
        public void SetAbsorbedAmountData()
        {
            /*
            if (_absorbedAmountData.Trim().Split(Global.DB_VALUE_SEPARATOR.ToCharArray()[0]).Length == 4)
            {
                _absorbedAmountData = "";
                absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.before].Value = "0";
                absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.after].Value = "0";
                absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.absorbedAmount].Value = "0";
                absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.before].Value = "0";
                absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.after].Value = "0";
                absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.absorbedAmount].Value = "0";
            }
            */

            absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.before].Value = 0; 
            absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.after].Value = 0; 
            absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.before].Value = 0; 
            absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.after].Value = 0; 

            //_absorbedAmountData += Utils.GetRealNumberString(absorbedAmountData) + Global.DB_VALUE_SEPARATOR;
            string[] absorbedAmountDataArray = _absorbedAmountData.Trim().Split(Global.DB_VALUE_SEPARATOR.ToCharArray()[0]);

            for(int i=0;i< absorbedAmountDataArray.Length; i++)
            {
                if (absorbedAmountDataArray[i] == "")
                    continue; 

                if (i==0)
                    absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.before].Value = absorbedAmountDataArray[i];
                else if (i == 1)
                    absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.after].Value = absorbedAmountDataArray[i];
                else if (i == 2)
                    absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.before].Value = absorbedAmountDataArray[i];
                else if (i == 3)
                    absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.after].Value = absorbedAmountDataArray[i];
            }

            ResetAbsorbedAmount();

        }

        public void AddAbsorbedAmountData(string absorbedAmountData)
        {
            /*
            if (_absorbedAmountData.Trim().Split(' ').Length == 4)
            {
                _absorbedAmountData = "";
                absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.before].Value = "0";
                absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.after].Value = "0";
                absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.absorbedAmount].Value = "0";
                absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.before].Value = "0";
                absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.after].Value = "0";
                absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.absorbedAmount].Value = "0";
            }

            _absorbedAmountData += Utils.GetRealNumberString(absorbedAmountData) + " ";
            string[] absorbedAmountDataArray = _absorbedAmountData.Trim().Split(' ');

            for (int i = 0; i < absorbedAmountDataArray.Length; i++)
            {
                if (i == 0)
                    absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.before].Value = absorbedAmountDataArray[i];
                else if (i == 1)
                    absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.after].Value = absorbedAmountDataArray[i];
                else if (i == 2)
                    absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.before].Value = absorbedAmountDataArray[i];
                else if (i == 3)
                    absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.after].Value = absorbedAmountDataArray[i];
            }
            */ 

            if(absorbedAmountDataGridView.SelectedCells.Count == 0)
            {
                absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.before].Value = absorbedAmountData;
                absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.before].Selected = true; 
            }
            else
            {
                absorbedAmountDataGridView.SelectedCells[0].Value = absorbedAmountData;
            }

            if (absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.before] == absorbedAmountDataGridView.SelectedCells[0])
                absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.after].Selected = true;
            else if (absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.after] == absorbedAmountDataGridView.SelectedCells[0])
                absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.before].Selected = true;
            else if (absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.before] == absorbedAmountDataGridView.SelectedCells[0])
                absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.after].Selected = true;
            else if (absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.after] == absorbedAmountDataGridView.SelectedCells[0])
                absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.before].Selected = true;

            ResetAbsorbedAmount();
        }

        private void ResetAbsorbedAmount()
        {
            // null 0변환
            if (absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.before].Value == null)
                absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.before].Value = 0;
            if (absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.after].Value == null)
                absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.after].Value = 0;
            if (absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.before].Value == null)
                absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.before].Value = 0;
            if (absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.after].Value == null)
                absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.after].Value = 0;

            // 흡수량 1
            decimal temp1 = decimal.Parse(absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.before].Value.ToString());
            absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.absorbedAmount].Value =
                string.Format("{0:0.0} ", Math.Round(decimal.Parse(absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.after].Value.ToString())
                /
                (temp1 == 0 ? 1 : temp1),1));


            // 흡수량 2
            decimal temp2 = decimal.Parse(absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.before].Value.ToString());
            absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.absorbedAmount].Value =
               string.Format("{0:0.0} ", Math.Round(decimal.Parse(absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.after].Value.ToString())
                /
                (temp2 == 0 ? 1 : temp2),1));


            // 흡수량 평균
            absorbedAmountAverageTextBox.Text =
                string.Format("{0:0.0} ",Math.Round(((decimal.Parse(absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.absorbedAmount].Value.ToString()) +
               decimal.Parse(absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.absorbedAmount].Value.ToString())) / 2), 1, MidpointRounding.AwayFromZero).ToString());


        }

        public void SetSaveMode(string saveMode)
        {
            _saveMode = saveMode; 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _parent.AddSerialPortData("2.2\r\n");
        }

        private void absorbedAmountDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(txtCheckNumeric_KeyPress);
        }

        private void txtCheckNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.AcceptOnlyRealNumber(sender, e);
        }

        private void absorbedAmountDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void absorbedAmountDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ResetAbsorbedAmount();
        }
    }
}
