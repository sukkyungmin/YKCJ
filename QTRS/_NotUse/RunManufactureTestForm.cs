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

namespace QTRS
{
    public partial class RunManufactureTestForm : Form
    {
        Dictionary<string, TestMethodInfo> _testMethodMap = null;
        public string _currentTestId = ""; // ProductTests.idx 
        private string _saveMode = "ADD";
        private string _serialData = "";
        enum eProductMfTestResultList
        {
            checkBox, testItem, testMethod, testScore, compatibilityOx, idx
        }

        // Image DataGridView
        enum eImageDataList { NO, CHECKBOX, IMAGE, FILE_NAME, FILE_MEMO, FILE_PATH, ID }
        enum eFileDataList { NO, CHECKBOX, FILE_NAME, FILE_MEMO, FILE_PATH, ID }

        private const int GRID_COL_WIDTH = 100;
        private const int GRID_ROW_HEIGHT = 32;
        private const int GRID_COLUMN_HEIGHT = 32;
        private const int GRID_IMAGE_ROW_HEIGHT = 100;

        private const int ROW_HEADER_CELL = 0;



        public RunManufactureTestForm(string currentTestId)
        {
            InitializeComponent();
            _currentTestId = currentTestId;
        }

        private void RunManufactureTestForm_Load(object sender, EventArgs e)
        {
            InitControls();
            if (_saveMode == "UPDATE")
                SetSelectedValues();

            if (this.ohausSerialPort.IsOpen == false)
            {
                QTRS.ProductTest.SerialPortSetup form = new ProductTest.SerialPortSetup(this.ohausSerialPort);
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog();
            }

        }
        public void SetSaveMode(string saveMode = "ADD")
        {
            _saveMode = saveMode;
        }


        private void InitControls()
        {
            InitTestMethodMap();
            InitProductTestDataGridView();
            InitImageDataGridView();
            InitFileDataGridView();

            compatibilityOxComboBox.Items.Add("부적합");
            compatibilityOxComboBox.Items.Add("적합");
        }

        private void InitTestMethodMap()
        {
            _testMethodMap = new Dictionary<string, TestMethodInfo>();

            string query = "EXEC SelectTestMethodList 1";

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
                TestMethodInfo testMethodInfo = new TestMethodInfo();

                testMethodInfo.idx = dataRow["idx"].ToString();
                testMethodInfo.testItemId = dataRow["testItemId"].ToString();
                testMethodInfo.testItem = dataRow["testItem"].ToString();
                testMethodInfo.testMethod = dataRow["testMethod"].ToString();
                testMethodInfo.testScore = dataRow["testScore"].ToString();

                _testMethodMap.Add(testMethodInfo.testItemId, testMethodInfo);

                testItemComboBox.Items.Add(new ComboBoxItem(testMethodInfo.testItem, testMethodInfo.testItemId));
            }
        }

        private void InitProductTestDataGridView()
        {
            DataGridView dataGridView = productMfDataGridView;

            string[] columnNames = { "", "시험 항목", "시험 방법", "시험 성적", "적합 여부", "IDX" };
            DataGridViewCheckBoxColumn checkboxCol = new DataGridViewCheckBoxColumn();
            checkboxCol.Name = "Column0";
            checkboxCol.HeaderText = "";
            dataGridView.Columns.Add(checkboxCol);
            for (int i = 1; i < columnNames.Length; i++)
            {
                dataGridView.Columns.Add("Column" + i.ToString(), columnNames[i]);
            }
            dataGridView.Columns[(int)eProductMfTestResultList.idx].Visible = false;

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

            // Each column style 
            dataGridView.Columns[(int)eProductMfTestResultList.checkBox].ReadOnly = false;
            for (int i = (int)eProductMfTestResultList.testItem; i <= (int)eProductMfTestResultList.compatibilityOx; i++)
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

            transferProgressBar.Minimum = 0;
            transferProgressBar.Maximum = safeFileNames.Length;

            for (int i = 0; i < safeFileNames.Length; i++)
            {
                AddFiles(dataGridView, safeFileNames[i], "", fileNames[i], id);

                Utils.OddDataGridViewRow(dataGridView);

                transferProgressBar.Value = i + 1;
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

                            transferProgressBar.Minimum = 0;
                            transferProgressBar.Maximum = 2;
                            transferProgressBar.Value = 0;

                            // 테스트 추가
                            AddTest(command);
                            transferProgressBar.Value = 1;

                            // 이미지, 파일 추가
                            AddTestFiles(command);
                            transferProgressBar.Value = 2;

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
                if (CheckRequiredTestItems() == false)
                    throw new Exception("필수 항목을 모두 입력해 주십시오.");
                ////
                // 기존 원료 검사 실행 삭제
                if (_currentTestId == "")
                    throw new Exception("완제품 검사 실행을 저장할 수 없습니다.");

                command.CommandText = string.Format("EXEC DeleteProductMfTestResults '{0}'", _currentTestId);
                if (command.ExecuteNonQuery() < 0)
                    throw new Exception("완제품 검사 실행을 저장할 수 없습니다.");
                ////


                ////
                // 새 원료 검사 실행 추가
                for (int i = 0; i < productMfDataGridView.Rows.Count; i++)
                {
                    string key = productMfDataGridView.Rows[i].Cells[(int)eProductMfTestResultList.idx].Value.ToString();

                    string testItemId = _testMethodMap[key].testItemId;
                    string testItemName = _testMethodMap[key].testItem;
                    string testMethod = _testMethodMap[key].testMethod;
                    string testScore = productMfDataGridView.Rows[i].Cells[(int)eProductMfTestResultList.testScore].Value.ToString();
                    string compatibilityOx = productMfDataGridView.Rows[i].Cells[(int)eProductMfTestResultList.compatibilityOx].Value.ToString();

                    compatibilityOx = compatibilityOx == "부적합" ? "0" : "1";

                    string query = "EXEC InsertProductMfTestResultItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ";
                    query = string.Format(query,
                        _currentTestId,
                        testItemId,
                        testItemName,
                        testMethod,
                        testScore,
                        compatibilityOx);

                    long retVal = DbHelper.ExecuteNonQuery(query);
                    if (retVal == -1)
                        throw new Exception("완제품 검사 실행을 저장할 수 없습니다.");
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
                                    _currentTestId, fileName, fileMemo, fileType, Global.FILETYPE_PRODUCT_M);
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

        private bool CheckRequiredTestItems()
        {
            for (int i = 0; i < productMfDataGridView.Rows.Count - 1; i++)
            {
                string key = Utils.GetString(productMfDataGridView.Rows[i].Cells[(int)eProductMfTestResultList.testItem].Value);
                if (key == "")
                    return false;


                string testItemId = _testMethodMap[key].testItemId;
                string testItemName = _testMethodMap[key].testItem;
                string testMethod = _testMethodMap[key].testMethod;
                string testScore = Utils.GetString(productMfDataGridView.Rows[i].Cells[(int)eProductMfTestResultList.testScore].Value);
                string compatibilityOx = Utils.GetString(productMfDataGridView.Rows[i].Cells[(int)eProductMfTestResultList.compatibilityOx].Value);

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
            transferProgressBar.Minimum = 0;
            transferProgressBar.Maximum = imageDataGridView.Rows.Count;

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

                transferProgressBar.Value = i + 1;
            }

            return true;
        }

        private bool DownloadFiles(string downloadFilePath)
        {
            transferProgressBar.Minimum = 0;
            transferProgressBar.Maximum = fileDataGridView.Rows.Count;
            transferProgressBar.Value = 0;

            for (int i = 0; i < fileDataGridView.Rows.Count; i++)
            {
                if ((bool)fileDataGridView.Rows[i].Cells[(int)eFileDataList.CHECKBOX].Value == true)
                {
                    string id = GetStringFromObject(fileDataGridView.Rows[i].Cells[(int)eFileDataList.ID].Value);
                    if (id != "") // 다운로드
                    {
                        DataSet dataSet = DbHelper.SelectQuery(string.Format("EXEC SelectTestAttachmentItem '{0}', '{1}'", id, Global.FILETYPE_PRODUCT_M));
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

                transferProgressBar.Value = i + 1;
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


            transferProgressBar.Minimum = 0;
            transferProgressBar.Maximum = dataGridView.Rows.Count;
            transferProgressBar.Value = 0;

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

            string query = string.Format("EXEC DeleteTestAttachment '{0}', '{1}'", ids, Global.FILETYPE_PRODUCT_M);
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

                transferProgressBar.Minimum = 0;
                transferProgressBar.Maximum = 3;

                int currentRow = 1;
                //for (int di = 0; di < componentTestDataGridView.Rows.Count; di++)
                //{
                // 라벨 입력
                worksheet.Cells[currentRow, 1] = "완제품 제조관리 검사";
                worksheet.Cells[currentRow, 1].Font.Bold = true;
                currentRow += 2;

                // Row 데이터 입력
                DataGridView dataGridView = productMfDataGridView;

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
                transferProgressBar.Value = 1;

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
                transferProgressBar.Value = 2;

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
                transferProgressBar.Value = 3;

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

        private void SelectProductMfTestResult()
        {
            string query = string.Format("EXEC SelectProductMfTestResultList '{0}'", _currentTestId);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("완제품 검사 실행 데이터를 가져올 수 없습니다.");
                return;
            }

            productMfDataGridView.Rows.Clear();

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
                productMfDataGridView.Rows.Add(false, testItemName, testMethod, testScore, compatibilityOx, testItemId);
            }

            productMfDataGridView.ClearSelection();
        }

        /*
        private void startMeasurementButton_Click(object sender, EventArgs e)
        {
            if (productMfDataGridView.CurrentRow.Index > -1 && productMfDataGridView.CurrentRow.Index < productMfDataGridView.Rows.Count - 1)
            {
                productMfDataGridView.Rows[productMfDataGridView.CurrentRow.Index].Cells[(int)eProductMfTestResultList.testScore].Value = measurementValueTextBox.Text;
            }
        }
        */ 

        private void captureButton_Click(object sender, EventArgs e)
        {
            CaptureForm form = new CaptureForm(imageDataGridView);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        private void SetSelectedValues()
        {
            transferProgressBar.Minimum = 0;
            transferProgressBar.Maximum = 3;
            transferProgressBar.Value = 0;

            // DataGridView data 설정 
            SelectProductMfTestResult();
            transferProgressBar.Value = 1;


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
                DataSet dataSet = DbHelper.SelectQuery(string.Format("EXEC SelectTestAttachmentList '{0}', '{1}', '{2}'", _currentTestId, fileType, Global.FILETYPE_PRODUCT_M));
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
            transferProgressBar.Value += dataGridViews.Length;
        }

        private void addTestButton_Click(object sender, EventArgs e)
        {
            if (testItemComboBox.SelectedIndex == -1 ||
              testScoreTextBox.Text.Trim() == "" ||
              compatibilityOxComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("모든 항목을 입력해 주십시오.");
                return;
            }

            string idx = (testItemComboBox.SelectedItem as ComboBoxItem).Value.ToString();

            for (int i = 0; i < productMfDataGridView.Rows.Count; i++)
            {
                string targetIdx = productMfDataGridView.Rows[i].Cells[(int)eProductMfTestResultList.idx].Value.ToString();
                if (targetIdx == idx)
                {
                    MessageBox.Show("이미 추가한 테스트 항목입니다.");
                    return;
                }
            }

            productMfDataGridView.Rows.Add(
                false,
                (testItemComboBox.SelectedItem as ComboBoxItem).Text,
                testMethodTextBox.Text.Trim(),
                testScoreTextBox.Text.Trim(),
                   compatibilityOxComboBox.SelectedIndex == 0 ? "부적합" : "적합",
                idx);
        }

        private void deleteTestButton_Click(object sender, EventArgs e)
        {
            {
                for (int i = 0; i < productMfDataGridView.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(productMfDataGridView.Rows[i].Cells[(int)eProductMfTestResultList.checkBox].Value) == true)
                    {
                        productMfDataGridView.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        private void testItemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = (testItemComboBox.SelectedItem as ComboBoxItem).Value.ToString();
            testMethodTextBox.Text = _testMethodMap[key].testMethod;

            if (_testMethodMap[key].testItemId == "3") // 질량
            {
                testScoreTextBox.Enabled = false;
            }
            else
            {
                testScoreTextBox.Enabled = true;
            }

            testScoreTextBox.Text = "";
            compatibilityOxComboBox.SelectedIndex = -1;
        }

        private void ohausSerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            this.Invoke(new EventHandler(this.ohausSerialPort_DataReceivedHandler));
        }

        private void ohausSerialPort_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {

        }

        private void ohausSerialPort_DataReceivedHandler(object sender, EventArgs e)
        {
            string data = this.ohausSerialPort.ReadExisting();
            _serialData += data;

            if (_serialData.IndexOf("\r\n") != -1)
            {
                if (testScoreTextBox.Text.Trim().Split(' ').Length == 10)
                    testScoreTextBox.Text = "";

                testScoreTextBox.Text += Utils.GetRealNumberString(_serialData) + " ";
                _serialData = "";
                Console.Beep(1000, 500);
            }
        }

        private void RunManufactureTestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.ohausSerialPort.IsOpen == true)
                this.ohausSerialPort.Close(); 
        }
    }
}
