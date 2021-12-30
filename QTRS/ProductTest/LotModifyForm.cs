using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System;

namespace QTRS.ProductTest
{
    public partial class LotModifyForm : Form
    {

        private string _idx = "";

        enum eMainLot
        {
            // A
            Componentcode = 0, Lot01 =2, Lot02, Lot03, Lot04, Lot05, Lot06
        }


        public LotModifyForm(string idx, DataTable dt)
        {
            InitializeComponent();
            InitProductTestDataGridView(dt);

            _idx = idx;
        }

        private void InitProductTestDataGridView(DataTable dt)
        {
            DataGridView dataGridView = LotModifyDataGridView;

            string[] columnNames = { "Code", "Name", "Lot01", "Lot02", "Lot03", "Lot04", "Lot05", "Lot06" };
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
            dataGridView.RowTemplate.Height = 30; // Global.GRID_ROW_HEIGHT * 2; 
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

            int colWidth = dataGridView.Width / 9;
            dataGridView.Columns[0].Width = colWidth;
            dataGridView.Columns[1].Width = colWidth*2;
            dataGridView.Columns[2].Width = colWidth;
            dataGridView.Columns[3].Width = colWidth;
            dataGridView.Columns[4].Width = colWidth;
            dataGridView.Columns[5].Width = colWidth;
            dataGridView.Columns[6].Width = colWidth;
            dataGridView.Columns[7].Width = colWidth;

            foreach (DataRow dataRow in dt.Rows)
            {
                if(dataRow["componentCode"].ToString() != "")
                {
                    dataGridView.Rows.Add(dataRow["componentCode"].ToString(), dataRow["fdaName"].ToString(), dataRow["mainLotNo"].ToString(), dataRow["mainLotNo2"].ToString(),
                                          dataRow["mainLotNo3"].ToString(), dataRow["mainLotNo4"].ToString(), dataRow["mainLotNo5"].ToString(), dataRow["mainLotNo6"].ToString());
                }
            }


            dataGridView.ClearSelection();

            /*
            _cells[0] = absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.before];
            _cells[1] = absorbedAmountDataGridView.Rows[0].Cells[(int)eAbsorbedAmountList.after];
            _cells[2] = absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.before];
            _cells[3] = absorbedAmountDataGridView.Rows[1].Cells[(int)eAbsorbedAmountList.after];
            */

            LotModifyDataGridView.Columns[0].ReadOnly = true;
            LotModifyDataGridView.Columns[1].ReadOnly = true;

            LotModifyDataGridView.AllowUserToAddRows = false;
            LotModifyDataGridView.MultiSelect = false;
            LotModifyDataGridView.ScrollBars = ScrollBars.Both;

            foreach (DataGridViewColumn column in LotModifyDataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


        }

        private void MainLotSaveBtn_Click(object sender, System.EventArgs e)
        {
            try
            {
                int errorCount = 0;
                int row = 0;

                DataTable dt = GetDataGridViewAsDataTable(LotModifyDataGridView);

                if (MessageBox.Show("MainLot 저장 하시겠습니까??", "MS-SQL 저장", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    foreach (DataRow drow in dt.Rows)
                    {

                        string query = "EXEC InsertUpdateComponentItemByMainLot '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ";
                        query = string.Format(query,
                        _idx,
                        drow[(int)eMainLot.Componentcode],
                        drow[(int)eMainLot.Lot01],
                        drow[(int)eMainLot.Lot02],
                        drow[(int)eMainLot.Lot03],
                        drow[(int)eMainLot.Lot04],
                        drow[(int)eMainLot.Lot05],
                        drow[(int)eMainLot.Lot06]
                       );

                        long retVal = DbHelper.ExecuteNonQuery(query);

                        row++;

                        if (retVal == -1)
                        {
                            WriteLog("MianLot 변경에서", row);
                            errorCount++;
                        }
                    }
                }
                else
                {
                    return;
                }

                if(errorCount == 0)
                {
                    MessageBox.Show("성공적으로 저장 되었습니다. \rMainLot 변경 화면이 강제로 종료되며 \r레포트 종료 후 다시 실행해 주십시요.","화면종료");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("저장에 실패 하였습니다. \r다시한번 시도하십시요.", "저장실패");
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void WriteLog(string fileName, int row)
        {
            string filePath = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                      System.Windows.Forms.Application.StartupPath,
                      Path.DirectorySeparatorChar,
                      "LOG",
                      Path.DirectorySeparatorChar,
                      "QTRS-LOG-",
                      DateTime.Now.ToString("yyyy-MM-dd"),
                      ".txt");

            string message = string.Format("-[{0}] \"{1}\" 파일의 {2}번째 행을 저장하지 못했습니다.", DateTime.Now.ToString("HH:mm:ss"), fileName, row);
            Utils.WriteLog(filePath, message);
        }

        private static DataTable GetDataGridViewAsDataTable(DataGridView _DataGridView)
        {
            try
            {
                if (_DataGridView.ColumnCount == 0)
                    return null;
                DataTable dtSource = new DataTable();
                //////create columns
                foreach (DataGridViewColumn col in _DataGridView.Columns)
                {
                    if (col.ValueType == null)
                        dtSource.Columns.Add(col.Name, typeof(string));
                    else
                        dtSource.Columns.Add(col.Name, col.ValueType);
                    dtSource.Columns[col.Name].Caption = col.HeaderText;
                }
                ///////insert row data
                foreach (DataGridViewRow row in _DataGridView.Rows)
                {
                    DataRow drNewRow = dtSource.NewRow();
                    foreach (DataColumn col in dtSource.Columns)
                    {
                        drNewRow[col.ColumnName] = row.Cells[col.ColumnName].Value;
                    }
                    dtSource.Rows.Add(drNewRow);
                }
                return dtSource;
            }
            catch
            {
                return null;
            }
        }
    }
}
