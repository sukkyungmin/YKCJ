using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO; 

namespace QTRS.ImportExport
{
    public partial class ExcelExportForm : Form
    {
        int _masterFileType = (int)Global.eMasterFileType.component;
        string _filePath = "";
        DataGridView _dataGridView = null; 

        public ExcelExportForm()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Export File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName.Trim() == "")
                return;

            _filePath = saveFileDialog.FileName.Trim();
            fileNameTextBox.Text = _filePath.Substring(_filePath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
        }

        private void executeButton_Click(object sender, EventArgs e)
        {

            if (_masterFileType == (int)Global.eMasterFileType.componentTestMethod)
                ExportComponentTestMethodSample();
            else if (_masterFileType == (int)Global.eMasterFileType.productTestMethod)
                ExportProductTestMethodSample();
            else if (_masterFileType == (int)Global.eMasterFileType.importInspection ||
                _masterFileType == (int)Global.eMasterFileType.product ||
                _masterFileType == (int)Global.eMasterFileType.component)
                ExportDataGridData(); 

            /*

            if (_masterFileType == (int)Global.eMasterFileType.component)
                retVal = InsertUpdateComponentMasters(dataTable, ref totalCount, ref errorCount);
            else if (_masterFileType == (int)Global.eMasterFileType.product)
                retVal = InsertUpdateProductMasters(dataTable, ref totalCount, ref errorCount);
            else if (_masterFileType == (int)Global.eMasterFileType.componentTestMethod)
                ExportTestMethodSample(); 
                _masterFileType == (int)Global.eMasterFileType.productTestMethod)
                retVal = InsertUpdateTestMethods(dataTable, _masterFileType, ref totalCount, ref errorCount);
            else if (_masterFileType == (int)Global.eMasterFileType.importInspection)
                retVal = InsertUpdateImportInspections(dataTable, ref totalCount, ref errorCount);
                */
        }

        private void ExportComponentTestMethodSample()
        {
            string[] columnIds = { "property",
            "confirmationTest01", "confirmationTest02", "confirmationTest03", "confirmationTest04", "confirmationTest05",
            "absorbedAmount", "citricAcidConfirmationTest", "ph", "gravity_d20", "gravity_d2020", "viscosity_AstmD3236_88", "viscosity_AstmD3266_88",
            "softeningPoint_AstmE28_67", "nonVolatile_Ksm0009", "lignin", "pigment", "acidAndAlkali", "fluorescence", "ashTest",
            "formaldehyde", "strength", "sedimentationRate", "elongationRate", "thickness", "tensileStrength_200PM", "tensileStrength",
            "sulphate", "heavyMetal", "arsenic", "residueOnMonomer", "lossOnDrying", "residueOnIgnition", "abilityToAbsorb", "waterproofTest",
            "wvtr", "density", "thickness2", "elongationRateNum", "corpusAlienum_Ink", "heavyMetal_Ink", "viscosity_Ink", "fusionPoint_Ink", "refractiveIndex_n20D" };

            string[] columnNames = { "성상", "확인시험 1", "확인시험 2", "확인시험 3", "확인시험 4", "확인시험 5", "흡수량",
                "구연산 확인시험", "pH", "비중d20(측정)", "비중d2020", "점도 (ASTM D 3236_88)", "점도 (ASTM D 3266_88)", "연화점 (ASTM E 28_67)", "비휘발성 (KSM 0009)",
                "리구닌", "색소", "산 및 알칼리", "형광", "회분시험", "포름알데히드", "강도", "침강속도", "신장율", "굵기", "200%M 인장강도", "인장강도",
                "황산염", "중금속", "비소", "잔존모노머", "건조감량", "강열잔분", "흡수능력", "방수시험", "투습도", "밀도", "두께", "신장율", "이물(잉크)",
                "중금속(잉크)", "점도(잉크)", "융점(잉크)", "굴절률 n20D" };

            string excelFilePath = _filePath; 
            bool overwriteMode = true;

            Excel.Application application = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            bool isSavedFile = false;

            try
            {
                // Excel 생성
                application = new Excel.Application();
                workbook = application.Workbooks.Add();
                worksheet = workbook.Worksheets.get_Item(1) as Excel.Worksheet;

                // 전체 기본 폰트
                /*
                application.StandardFont = "굴림체"; 
                application.StandardFontSize = 11;
                application.ActiveWindow.Zoom = 130;
                */

                application.get_Range("A1").EntireRow.EntireColumn.Interior.Color = Color.White;
                application.get_Range("A1").EntireRow.EntireColumn.Font.Name = "맑은고딕";
                application.get_Range("A1").EntireRow.EntireColumn.Font.Size = 11;
                application.get_Range("A1").EntireRow.EntireColumn.RowHeight = 16.5;
                application.ActiveWindow.Zoom = 100;

                // 열너비 지정
                application.get_Range("A1").ColumnWidth = 5;
                application.get_Range("B1").ColumnWidth = 35;
                application.get_Range("C1").ColumnWidth = 35;
                application.get_Range("D1").ColumnWidth = 80;

                // 보더 스타일
                string range = "A1:D" + (columnIds.Length + 1);
                //application.get_Range(range).Select(); 

                application.get_Range(range).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                application.get_Range(range).Borders.Color = System.Drawing.Color.Black;
                application.get_Range(range).Borders.Weight = 2;
                /*
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeLeft].Color = System.Drawing.Color.Black;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.Color.Black;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.Color.Black;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.Color.Black;

                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = BorderStyle.FixedSingle;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = BorderStyle.FixedSingle;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = BorderStyle.FixedSingle;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = BorderStyle.FixedSingle;

                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = 1;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 1;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = 1;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 1;
            */


                // 정렬
                application.get_Range("A1:D1").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                application.get_Range("A2").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


                // 기타
                /*
                application.get_Range("C4:E4").Merge();
                application.get_Range("C5:E5").Merge();
                application.get_Range("C6:E6").Merge();
                application.get_Range("C7:E7").Merge();
                application.get_Range("C8:E8").Merge();
                application.get_Range("C10:E10").Merge();

                */

                worksheet.Cells[1, 1] = "No";
                worksheet.Cells[1, 2] = "시험 ID";
                worksheet.Cells[1, 3] = "시험 항목";
                worksheet.Cells[1, 4] = "시험 방법";
                application.get_Range("A1:D1").Interior.Color = Color.Gray;
                application.get_Range("A1:D1").Font.Bold = true;

                progressBar.Minimum = 0;
                progressBar.Maximum = columnIds.Length - 1;

                for (int row = 0; row < columnIds.Length; row++)
                {
                    progressBar.Value = row; 

                    worksheet.Cells[row + 2, 1] = (row + 1).ToString();
                    worksheet.Cells[row + 2, 2] = columnIds[row];
                    worksheet.Cells[row + 2, 3] = columnNames[row];
                }

                isSavedFile = SaveAsExcelFile(workbook, excelFilePath, overwriteMode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook.Close(isSavedFile);
                application.Quit();

                ReleaseExcelObject(worksheet);
                ReleaseExcelObject(workbook);
                ReleaseExcelObject(application);

                if (isSavedFile == true)
                {
                    MessageBox.Show("파일을 저장했습니다.");
                }
            }
        }

        private void ExportProductTestMethodSample()
        {
            string[] columnNames = { "성상","이물시험","질량","색소","산및알칼리 P.P", "산및알칼리 M.O", "형광","흡수량","삼출","강도", "포름알데히드" };

            string excelFilePath = _filePath;
            bool overwriteMode = true;

            Excel.Application application = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            bool isSavedFile = false;

            try
            {
                // Excel 생성
                application = new Excel.Application();
                workbook = application.Workbooks.Add();
                worksheet = workbook.Worksheets.get_Item(1) as Excel.Worksheet;

                // 전체 기본 폰트
                /*
                application.StandardFont = "굴림체"; 
                application.StandardFontSize = 11;
                application.ActiveWindow.Zoom = 130;
                */

                application.get_Range("A1").EntireRow.EntireColumn.Interior.Color = Color.White;
                application.get_Range("A1").EntireRow.EntireColumn.Font.Name = "맑은고딕";
                application.get_Range("A1").EntireRow.EntireColumn.Font.Size = 11;
                application.get_Range("A1").EntireRow.EntireColumn.RowHeight = 16.5;
                application.ActiveWindow.Zoom = 100;

                // 열너비 지정
                application.get_Range("A1").ColumnWidth = 5;
                application.get_Range("B1").ColumnWidth = 35;
                application.get_Range("C1").ColumnWidth = 35;
                application.get_Range("D1").ColumnWidth = 80;

                // 보더 스타일
                string range = "A1:D" + (columnNames.Length + 1);
                //application.get_Range(range).Select(); 

                application.get_Range(range).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                application.get_Range(range).Borders.Color = System.Drawing.Color.Black;
                application.get_Range(range).Borders.Weight = 2;
                
                // 정렬
                application.get_Range("A1:D1").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                application.get_Range("A2").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                worksheet.Cells[1, 1] = "No";
                worksheet.Cells[1, 2] = "시험 ID";
                worksheet.Cells[1, 3] = "시험 항목";
                worksheet.Cells[1, 4] = "시험 방법";
                application.get_Range("A1:D1").Interior.Color = Color.Gray;
                application.get_Range("A1:D1").Font.Bold = true;

                progressBar.Minimum = 0;
                progressBar.Maximum = columnNames.Length - 1;

                for (int row = 0; row < columnNames.Length; row++)
                {
                    progressBar.Value = row;

                    worksheet.Cells[row + 2, 1] = (row + 1).ToString();
                    worksheet.Cells[row + 2, 2] = (row + 1).ToString();
                    worksheet.Cells[row + 2, 3] = columnNames[row];
                }

                isSavedFile = SaveAsExcelFile(workbook, excelFilePath, overwriteMode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook.Close(isSavedFile);
                application.Quit();

                ReleaseExcelObject(worksheet);
                ReleaseExcelObject(workbook);
                ReleaseExcelObject(application);

                if (isSavedFile == true)
                {
                    MessageBox.Show("파일을 저장했습니다.");
                }
            }
        }


        private void ExportDataGridData()
        {
            string excelFilePath = _filePath;
            bool overwriteMode = true;

            Excel.Application application = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            bool isSavedFile = false;

            Excel.Range c1 = null; 
            Excel.Range c2 = null; 

            try
            {
                // Excel 생성
                application = new Excel.Application();
                workbook = application.Workbooks.Add();
                worksheet = workbook.Worksheets.get_Item(1) as Excel.Worksheet;

                // 전체 기본 폰트
                /*
                application.StandardFont = "굴림체"; 
                application.StandardFontSize = 11;
                application.ActiveWindow.Zoom = 130;
                */

                application.get_Range("A1").EntireRow.EntireColumn.Interior.Color = Color.White;
                application.get_Range("A1").EntireRow.EntireColumn.Font.Name = "맑은고딕";
                application.get_Range("A1").EntireRow.EntireColumn.Font.Size = 11;
                application.get_Range("A1").EntireRow.EntireColumn.RowHeight = 16.5;
                application.ActiveWindow.Zoom = 100;

                // 열너비 지정
                //application.get_Range("A1").ColumnWidth = 5;

                // 보더 스타일
                c1 = worksheet.Cells[1, 1];
                c2 = worksheet.Cells[_dataGridView.Rows.Count+1, _dataGridView.Columns.Count-1];
                //application.get_Range(range).Select(); 

                application.get_Range(c1, c2).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                application.get_Range(c1, c2).Borders.Color = System.Drawing.Color.Black;
                application.get_Range(c1, c2).Borders.Weight = 2;
                /*
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeLeft].Color = System.Drawing.Color.Black;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.Color.Black;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.Color.Black;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.Color.Black;

                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = BorderStyle.FixedSingle;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = BorderStyle.FixedSingle;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = BorderStyle.FixedSingle;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = BorderStyle.FixedSingle;

                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = 1;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = 1;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = 1;
                application.get_Range(range).Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = 1;
            */


                // 정렬
                //application.get_Range("A1:D1").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //application.get_Range("A2").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                //application.get_Range(worksheet.Cells[1, 1], worksheet.Cells[1, _dataGridView.Columns.Count]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


                // 기타
                /*
                application.get_Range("C4:E4").Merge();
                application.get_Range("C5:E5").Merge();
                application.get_Range("C6:E6").Merge();
                application.get_Range("C7:E7").Merge();
                application.get_Range("C8:E8").Merge();
                application.get_Range("C10:E10").Merge();

                */

                for (int col = 1; col < _dataGridView.Columns.Count; col++) // 0 체크박스
                {
                    worksheet.Cells[1, col] = _dataGridView.Columns[col].HeaderText; 
                }

                progressBar.Minimum = 0;
                progressBar.Maximum = _dataGridView.Rows.Count - 1;

                for (int row = 0; row < _dataGridView.Rows.Count; row++)
                {
                    progressBar.Value = row;

                    for (int col = 1; col < _dataGridView.Columns.Count; col++) // 0 체크박스
                    {
                        worksheet.Cells[row + 2, col] = Utils.GetString(_dataGridView.Rows[row].Cells[col].Value); 
                    }
                }

                // 헤더 설정
                ReleaseExcelObject(c2);
                c2 = worksheet.Cells[1, _dataGridView.Columns.Count - 1];
                application.get_Range(c1, c2).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                application.get_Range(c1, c2).Interior.Color = Color.Gray;
                application.get_Range(c1, c2).Font.Bold = true;


                application.get_Range(c1, c2).Columns.AutoFit();

                isSavedFile = SaveAsExcelFile(workbook, excelFilePath, overwriteMode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ReleaseExcelObject(c1);
                ReleaseExcelObject(c2);

                workbook.Close(isSavedFile);
                application.Quit();
              
                ReleaseExcelObject(worksheet);
                ReleaseExcelObject(workbook);
                ReleaseExcelObject(application);

                if (isSavedFile == true)
                {
                    MessageBox.Show("파일을 저장했습니다.");
                }
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

            // xls
            //workbook.SaveAs(excelFilePath, Excel.XlFileFormat.xlWorkbookNormal);

            // xlsx
            workbook.SaveAs(excelFilePath, Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, Excel.XlSaveAsAccessMode.xlShared, false, false, Type.Missing, Type.Missing, Type.Missing);


            return true;
        }

        private void ReleaseExcelObject(object obj)
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

        private void fileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            executeButton.Enabled = fileNameTextBox.Text.Trim() == "" ? false : true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetMasterFileType(int masterFileType)
        {
            _masterFileType = masterFileType;
        }

        public void SetDataGridView(DataGridView dataGridView)
        {
            _dataGridView = dataGridView;
        }
    }
}
