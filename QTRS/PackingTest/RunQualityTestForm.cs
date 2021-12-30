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

namespace QTRS.PackingTest
{
    public partial class RunQualityTestForm : Form
    {
        Dictionary<string, TestMethodInfo> _testMethodMap = null;
        public string _currentTestId = ""; // ProductTests.idx 
        enum eProductQtTestResultList
        {
            checkBox, testItem, testMethod, testScore, compatibilityOx, idx
        }

              private const int GRID_COL_WIDTH = 100;
        private const int GRID_ROW_HEIGHT = 32;
        private const int GRID_COLUMN_HEIGHT = 32;
        private const int GRID_IMAGE_ROW_HEIGHT = 100;

        private const int ROW_HEADER_CELL = 0;


        public RunQualityTestForm(string currentTestId)
        {
            InitializeComponent();
            _currentTestId = currentTestId;
        }

        private void RunProductTestForm_Load(object sender, EventArgs e)
        {
            InitControls();
            SetSelectedValues();
        }

        private void InitControls()
        {
            //InitTestMethodMap();
            InitProductTestDataGridView();
            InitPackingTestDataGridView();

            compatibilityOxComboBox.Items.Add("부적합");
            compatibilityOxComboBox.Items.Add("적합");
        }

        private void InitProductTestDataGridView()
        {
            DataGridView dataGridView = productQtDataGridView;

            string[] columnNames = { "", "시험 항목", "시험 기준", "시험 성적", "적합 여부", "IDX" };
            DataGridViewCheckBoxColumn checkboxCol = new DataGridViewCheckBoxColumn();
            checkboxCol.Name = "Column0";
            checkboxCol.HeaderText = "";
            dataGridView.Columns.Add(checkboxCol);
            for (int i = 1; i < columnNames.Length; i++)
            {
                dataGridView.Columns.Add("Column" + i.ToString(), columnNames[i]);
            }
            dataGridView.Columns[(int)eProductQtTestResultList.checkBox].Visible = false;
            dataGridView.Columns[(int)eProductQtTestResultList.idx].Visible = false;

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


            // Each column style 
            dataGridView.Columns[(int)eProductQtTestResultList.checkBox].ReadOnly = false;
            for (int i = (int)eProductQtTestResultList.testItem; i <= (int)eProductQtTestResultList.compatibilityOx; i++)
                dataGridView.Columns[i].ReadOnly = true;


            // Common style
            dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridView.GridColor = Global.GRID_COLOR;
            dataGridView.BackgroundColor = Color.White;         // BackgroundColor 
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // SelectionMode
                                                                                  //dataGridView.ReadOnly = true;
            dataGridView.MultiSelect = false;
            dataGridView.ScrollBars = ScrollBars.Both;
        }

        private void InitPackingTestDataGridView()
        {
            DataGridView dataGridView = productQtPackingDataGridView;

            string[] columnNames = { "", "포장 항목", "포장 방법", "포장 성적", "적합 여부", "IDX" };
            DataGridViewCheckBoxColumn checkboxCol = new DataGridViewCheckBoxColumn();
            checkboxCol.Name = "Column0";
            checkboxCol.HeaderText = "";
            dataGridView.Columns.Add(checkboxCol);
            for (int i = 1; i < columnNames.Length; i++)
            {
                dataGridView.Columns.Add("Column" + i.ToString(), columnNames[i]);
            }
            dataGridView.Columns[(int)eProductQtTestResultList.idx].Visible = false;

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


            // Each column style 
            dataGridView.Columns[(int)eProductQtTestResultList.checkBox].ReadOnly = false;
            for (int i = (int)eProductQtTestResultList.testItem; i <= (int)eProductQtTestResultList.compatibilityOx; i++)
                dataGridView.Columns[i].ReadOnly = true;


            // Common style
            dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridView.GridColor = Global.GRID_COLOR;
            dataGridView.BackgroundColor = Color.White;         // BackgroundColor 
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // SelectionMode
                                                                                  //dataGridView.ReadOnly = true;
            dataGridView.MultiSelect = false;
            dataGridView.ScrollBars = ScrollBars.Both;
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

                            // Commit
                            transaction.Commit();
                            MessageBox.Show("Test를 저장했습니다.");

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
                //if (CheckRequiredTestItems() == false)
                //   throw new Exception("필수 항목을 모두 입력해 주십시오.");

                ////
                // 기존 원료 검사 실행 삭제
                if (_currentTestId == "")
                    throw new Exception("완제품 품질관리 포장검사 실행을 저장할 수 없습니다.");

                command.CommandText = string.Format("EXEC DeleteProductQtPackingTestResults '{0}'", _currentTestId);
                if (command.ExecuteNonQuery() < 0)
                    throw new Exception("완제품 품질관리 포장검사 실행을 저장할 수 없습니다.");
                ////

                ////
                for (int i = 0; i < productQtPackingDataGridView.Rows.Count; i++)
                {
                    string idx = productQtPackingDataGridView.Rows[i].Cells[(int)eProductQtTestResultList.idx].Value.ToString();
                    string testItemId = ""; 
                    string testItemName = productQtPackingDataGridView.Rows[i].Cells[(int)eProductQtTestResultList.testItem].Value.ToString();
                    string testMethod = productQtPackingDataGridView.Rows[i].Cells[(int)eProductQtTestResultList.testMethod].Value.ToString();
                    string testScore = productQtPackingDataGridView.Rows[i].Cells[(int)eProductQtTestResultList.testScore].Value.ToString();
                    string compatibilityOx = productQtPackingDataGridView.Rows[i].Cells[(int)eProductQtTestResultList.compatibilityOx].Value.ToString();

                    compatibilityOx = compatibilityOx == "부적합" ? "0" : "1";

                    string query = "EXEC InsertProductQtPackingTestResultItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ";
                    query = string.Format(query,
                        _currentTestId,
                        testItemId,
                        testItemName,
                        testMethod,
                        testScore,
                        compatibilityOx);

                    long retVal = DbHelper.ExecuteNonQuery(query);
                    if (retVal == -1)
                        throw new Exception("완제품 품질관리 포장검사 실행을 저장할 수 없습니다.");
                }
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
            for (int i = 0; i < productQtDataGridView.Rows.Count - 1; i++)
            {
                string key = Utils.GetString(productQtDataGridView.Rows[i].Cells[(int)eProductQtTestResultList.testItem].Value);
                if (key == "")
                    return false;


                string testItemId = _testMethodMap[key].testItemId;
                string testItemName = _testMethodMap[key].testItem;
                string testMethod = _testMethodMap[key].testMethod;
                string testScore = Utils.GetString(productQtDataGridView.Rows[i].Cells[(int)eProductQtTestResultList.testScore].Value);
                string compatibilityOx = Utils.GetString(productQtDataGridView.Rows[i].Cells[(int)eProductQtTestResultList.compatibilityOx].Value);

                if (testItemId == "" || testItemName == "" || testMethod == "" || testScore == "" || compatibilityOx == "")
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


        private string GetStringFromObject(object data)
        {
            if (data == null)
                return "";
            else
                return data.ToString();
        }


      

        private void SelectProductQtTestResult()
        {
            string query = string.Format("EXEC SelectProductQtTestResultList '{0}'", _currentTestId);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("완제품 품질관리 포장검사 실행 데이터를 가져올 수 없습니다.");
                return;
            }

            productQtDataGridView.Rows.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
                return;



            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string idx = dataRow["idx"].ToString();
                string testItemId = dataRow["testItemId"].ToString();
                string testItemName = dataRow["testItemName"].ToString();
                string testMethod = dataRow["testMethod"].ToString();
                //string testStandard = dataRow["testStandard"].ToString();
                string testScore = dataRow["testScore"].ToString();
                string compatibilityOx = dataRow["compatibilityOx"].ToString();

                compatibilityOx = compatibilityOx == "0" ? "부적합" : "적합"; 
                productQtDataGridView.Rows.Add(false, testItemName, testMethod, testScore, compatibilityOx, testItemId);
            }

            productQtDataGridView.ClearSelection();
        }

        private void SelectProductQtPackingTestResult()
        {
            string query = string.Format("EXEC SelectProductQtPackingTestResultList '{0}'", _currentTestId);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("완제품 품질관리 포장검사 실행 데이터를 가져올 수 없습니다.");
                return;
            }

            productQtPackingDataGridView.Rows.Clear();

            if (dataSet.Tables[0].Rows.Count == 0)
                return;



            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string idx = dataRow["idx"].ToString();
                string testItemId = dataRow["testItemId"].ToString();
                string testItemName = dataRow["testItemName"].ToString();
                string testMethod = dataRow["testMethod"].ToString();
                string testScore = dataRow["testScore"].ToString();
                string compatibilityOx = dataRow["compatibilityOx"].ToString();

                compatibilityOx = compatibilityOx == "0" ? "부적합" : "적합";
                productQtPackingDataGridView.Rows.Add(false, testItemName, testMethod, testScore, compatibilityOx, testItemId);
            }

            productQtPackingDataGridView.ClearSelection();
        }

        private void SetSelectedValues()
        {
            SelectProductQtTestResult();
            SelectProductQtPackingTestResult();
        }

        private void addTestButton_Click(object sender, EventArgs e)
        {
            if (testItemTextBox.Text.Trim() == "" ||
               testMethodTextBox.Text.Trim() == "" ||
               testScoreTextBox.Text.Trim() == "" ||
               compatibilityOxComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("모든 항목을 입력해 주십시오.");
                return;
            }

            productQtPackingDataGridView.Rows.Add(
                false,
                testItemTextBox.Text.Trim(),
                testMethodTextBox.Text.Trim(),
                testScoreTextBox.Text.Trim(),
                   compatibilityOxComboBox.SelectedIndex == 0 ? "부적합" : "적합",
                "");
        }

        private void deleteTestButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < productQtPackingDataGridView.Rows.Count; i++)
            {
                if (Convert.ToBoolean(productQtPackingDataGridView.Rows[i].Cells[(int)eProductQtTestResultList.checkBox].Value) == true)
                {
                    productQtPackingDataGridView.Rows.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
