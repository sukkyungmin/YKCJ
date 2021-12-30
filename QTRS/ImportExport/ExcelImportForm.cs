using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace QTRS.ImportExport
{
    public partial class ExcelImportForm : Form
    {
        // 원료 마스트 엑셀 파일 컬럼

        public int Errorrow = 0;
        enum eComponentMasterXlsx
        {
            // A
            componentCode = 0, componentName, deliveryCompanyName, purpose, fdaName,
            // F
            property, confirmationTest01, confirmationTest02, confirmationTest03, confirmationTest04,
            // K
            confirmationTest05, absorbedAmount, citricAcidConfirmationTest, pH_Min, pH_Max,
            // P
            gravity_d20_Min, gravity_d20_Max, softeningPoint_AstmE28_67_Min, softeningPoint_AstmE28_67_Max, lignin, 
            // U
            pigment, acidAndAlkali, fluorescence, formaldehyde, strength,
            // Z
            sedimentationRate, elongationRate, thickness_Min, thickness_Max, tensileStrength_200PM_Min,
            // AE
            tensileStrength_200PM_Max, sulphate, heavyMetal, arsenic, residueOnMonomer,
            // AJ
            lossOnDrying, residueOnIgnition, abilityToAbsorb_Min, abilityToAbsorb_Max, waterproofTest,
            // AO
            wvtr, density_Min, density_Max, thickness2_Min, thickness2_Max,
            // AT
            elongationRate_Min, elongationRate_Max, tensileStrength_Min, tensileStrength_Max, corpusAlienum_Ink,
            // AY
            fusionPoint_Ink, gravity_d2020_Min, gravity_d2020_Max, refractiveIndex_n20D_Min, refractiveIndex_n20D_Max,
            // BE 20.08.03 신규 추가
            tensileStrength_50PM, pigment1 , pigment2, acidAndAlkali1, acidAndAlkali2, acidAndAlkali3 ,
            // BJ
            sedimentationRate1, Viscosity_Min, Viscosity_Max, SpecificGravity_d25_Min, SpecificGravity_d25_Max,
            // BO 20.08.10 추가
            viscosity_AstmD3236_88_Min, viscosity_AstmD3236_88_Max, viscosity_AstmD3266_88_Min, viscosity_AstmD3266_88_Max, nonVolatile_Ksm0009_Min,
            // BT
            nonVolatile_Ksm0009_Max, ashTest_Min, ashTest_Max, heavyMetal_Ink_Min, heavyMetal_Ink_Max,
            // BY
            viscosity_Ink_Min, viscosity_Ink_Max

        }

        // 완제품 마스터 엑셀 파일 컬럼
        // componentCode ~ usage 까지는 제품코드에 따른 원료 성분 
        enum eProductMasterXlsx
        {
            // A
            productCode = 0, productDesc, fdaCode, fdaName, dosageForm,
            // F
            property, componentCode, machine = 9, standardOnAbsorbedAmount, mixPurpose,
            // M
            innerComponentName = 8, note = 13, logicalQuantity, basicWeight, area,
            // R
            usage, productionprocess, note2 = 25 
        }

        // 테스트 방법 엑셀 파일 컬럼 (원료, 완제품 공통)
        enum eTestMethodXlsx
        {
            testItemId = 1, testItem, testMethod, testStandard, testScore, unit
        }

        // 수입검사 엑셀 파일 컬럼
        enum eImportInspectionXlsx
        {
            // A
            warehousingDate = 0, componentCode, productAreaTypeName, componentName, maker,
            // F
            lotNo, mainLotNo, note = 18
        }

        int _masterFileType = (int)Global.eMasterFileType.component; 
        string _filePath = ""; 

        public ExcelImportForm()
        {
            InitializeComponent();
        }

        private void ExcelImportForm_Load(object sender, EventArgs e)
        {

        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            ////
            // 파일 찾기
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = ConstDefine.sapSourceDir;
            openFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "파일 임포트";

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            _filePath = openFileDialog.FileName;
            fileNameTextBox.Text = openFileDialog.SafeFileName;
            ////
        }

        private void executeButton_Click(object sender, EventArgs e)
        {
            ////
            // 파일경로, 사용중인 파일 확인
            if (_filePath == "" || File.Exists(_filePath) == false)
            {
                MessageBox.Show("존재하지 않는 파일입니다.");
                return; 
            }

            if (true == Utils.IsFileLocked(new FileInfo(_filePath)))
            {
                DialogResult result = MessageBox.Show("읽으려는 파일이 사용중입니다.", "파일 임포트", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Cancel)
                    return;
            }
            ////

            ////
            // 엑셀 데이터 읽기
            bool retVal = false; 
            long totalCount = 0; 
            long errorCount = 0;                 

            //int spaceRowCount = 10; // 빈줄이 연이어 나오면 더이상 읽을 데이터가 없는 것으로 알고 빠져나간다. (잘못된 문서의 예외처리)
            int excelSheetIndex = 1;
            string excelSheetName = "";
            string version = "";
            try
            {
                GetExcelSheetName(_filePath, excelSheetIndex, ref excelSheetName, ref version);
                DataTable dataTable = OleDbHelper.ImportExcel(_filePath, excelSheetName, version);
                if (dataTable == null)
                {
                    MessageBox.Show(_filePath + " 파일을 읽을 수 없습니다.");
                    return;
                }

                if(_masterFileType == (int)Global.eMasterFileType.component)
                    retVal = InsertUpdateComponentMasters(dataTable, ref totalCount, ref errorCount);
                else if (_masterFileType == (int)Global.eMasterFileType.product)
                    retVal = InsertUpdateProductMasters(dataTable, ref totalCount, ref errorCount);
                else if (_masterFileType == (int)Global.eMasterFileType.componentTestMethod || 
                    _masterFileType == (int)Global.eMasterFileType.productTestMethod)
                    retVal = InsertUpdateTestMethods(dataTable, _masterFileType, ref totalCount, ref errorCount);
                else if (_masterFileType == (int)Global.eMasterFileType.importInspection)
                    retVal = InsertUpdateImportInspections(dataTable, ref totalCount, ref errorCount);

                if(retVal == false)
                {
                    DialogResult = DialogResult.Cancel;
                }
                else
                {
                    string message = string.Format("임포트를 완료했습니다. 전체:{0}개, 성공:{1}개, 에러:{2}개",
                        totalCount, totalCount - errorCount, errorCount);
                    if (errorCount > 0)
                        message += "\nLOG 경로에서 에러내용을 확인해 주십시오."; 
                    MessageBox.Show(message);

                    DialogResult = DialogResult.OK;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

        private void GetExcelSheetName(string excelFilePath, int excelSheetIndex, ref string excelSheetName, ref string version)
        {
            Excel.Application application = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                // Excel 파일 열기
                application = new Excel.Application();
                application.Visible = false;
                workbook = application.Workbooks.Open(excelFilePath);
                worksheet = workbook.Worksheets.get_Item(excelSheetIndex) as Excel.Worksheet;
                worksheet.Select();
                version = application.Version;
                excelSheetName = worksheet.Name;
            }
            finally
            {
                workbook.Close(false);
                application.Quit();
                ReleaseExcelObject(worksheet);
                ReleaseExcelObject(workbook);
                ReleaseExcelObject(application);
            }
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

        //private string GetExcelData(object dataObject, string dataType = "string")
        private string GetExcelData(object dataObject)
        {
            /*
            string defaultValue = "";
            if (dataType == "string")
                defaultValue = "";
            else if (dataType == "number")
                defaultValue = "0";

            return dataObject == null ? defaultValue : dataObject.ToString().Trim();
            */ 
            return dataObject == null || dataObject.ToString().Trim() == "n/a" ? "" : dataObject.ToString().Trim();
        }

        public void SetMasterFileType(int masterFileType)
        {
            _masterFileType = masterFileType; 
        }

        private bool InsertUpdateComponentMasters(DataTable dataTable, ref long totalCount, ref long errorCount)
        {
            if(dataTable.Columns.Count < (int)eComponentMasterXlsx.SpecificGravity_d25_Max)
            {
                MessageBox.Show("실제 데이터 항목수보다 엑셀의 컬럼수가 적습니다.");
                return false; 
            }

            DbHelper.ExecuteNonQuery("EXEC DeleteAllComponentItem");

            progressBar.Minimum = 0; 
            progressBar.Maximum = dataTable.Rows.Count - 1;
            totalCount = dataTable.Rows.Count - 1;

            for (int row = 1; row < dataTable.Rows.Count; row++) // 0 행은 타이틀
            {
                progressBar.Value = row;

                DataRow dataRow = dataTable.Rows[row];

                string componentCode = GetExcelData(dataRow[(int)eComponentMasterXlsx.componentCode]);
                string componentName = GetExcelData(dataRow[(int)eComponentMasterXlsx.componentName]);
                string deliveryCompanyName = GetExcelData(dataRow[(int)eComponentMasterXlsx.deliveryCompanyName]).Replace("'","");
                string purpose = GetExcelData(dataRow[(int)eComponentMasterXlsx.purpose]);
                string fdaName = GetExcelData(dataRow[(int)eComponentMasterXlsx.fdaName]);
                string property = GetExcelData(dataRow[(int)eComponentMasterXlsx.property]);
                string confirmationTest01 = GetExcelData(dataRow[(int)eComponentMasterXlsx.confirmationTest01]);
                string confirmationTest02 = GetExcelData(dataRow[(int)eComponentMasterXlsx.confirmationTest02]);
                string confirmationTest03 = GetExcelData(dataRow[(int)eComponentMasterXlsx.confirmationTest03]);
                string confirmationTest04 = GetExcelData(dataRow[(int)eComponentMasterXlsx.confirmationTest04]);
                string confirmationTest05 = GetExcelData(dataRow[(int)eComponentMasterXlsx.confirmationTest05]);
                string absorbedAmount = GetExcelData(dataRow[(int)eComponentMasterXlsx.absorbedAmount]);
                string citricAcidConfirmationTest = GetExcelData(dataRow[(int)eComponentMasterXlsx.citricAcidConfirmationTest]);
                string pH_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.pH_Min]);
                string pH_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.pH_Max]);
                string gravity_d20_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.gravity_d20_Min]);
                string gravity_d20_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.gravity_d20_Max]);
                string gravity_d2020_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.gravity_d2020_Min]);
                string gravity_d2020_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.gravity_d2020_Max]);
                string softeningPoint_AstmE28_67_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.softeningPoint_AstmE28_67_Min]);
                string softeningPoint_AstmE28_67_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.softeningPoint_AstmE28_67_Max]);
                string lignin = GetExcelData(dataRow[(int)eComponentMasterXlsx.lignin]);
                string pigment = GetExcelData(dataRow[(int)eComponentMasterXlsx.pigment]);
                string acidAndAlkali = GetExcelData(dataRow[(int)eComponentMasterXlsx.acidAndAlkali]);
                string fluorescence = GetExcelData(dataRow[(int)eComponentMasterXlsx.fluorescence]);
                string formaldehyde = GetExcelData(dataRow[(int)eComponentMasterXlsx.formaldehyde]);
                string strength = GetExcelData(dataRow[(int)eComponentMasterXlsx.strength]);
                string sedimentationRate = GetExcelData(dataRow[(int)eComponentMasterXlsx.sedimentationRate]);
                string elongationRate = GetExcelData(dataRow[(int)eComponentMasterXlsx.elongationRate]);
                string thickness_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.thickness_Min]);
                string thickness_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.thickness_Max]);
                string tensileStrength_200PM_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.tensileStrength_200PM_Min]);
                string tensileStrength_200PM_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.tensileStrength_200PM_Max]);
                string tensileStrength_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.tensileStrength_Min]);
                string tensileStrength_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.tensileStrength_Max]);
                string sulphate = GetExcelData(dataRow[(int)eComponentMasterXlsx.sulphate]);
                string heavyMetal = GetExcelData(dataRow[(int)eComponentMasterXlsx.heavyMetal]);
                string arsenic = GetExcelData(dataRow[(int)eComponentMasterXlsx.arsenic]);
                string residueOnMonomer = GetExcelData(dataRow[(int)eComponentMasterXlsx.residueOnMonomer]);
                string lossOnDrying = GetExcelData(dataRow[(int)eComponentMasterXlsx.lossOnDrying]);
                string residueOnIgnition = GetExcelData(dataRow[(int)eComponentMasterXlsx.residueOnIgnition]);
                string abilityToAbsorb_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.abilityToAbsorb_Min]);
                string abilityToAbsorb_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.abilityToAbsorb_Max]);
                string waterproofTest = GetExcelData(dataRow[(int)eComponentMasterXlsx.waterproofTest]);
                string wvtr = GetExcelData(dataRow[(int)eComponentMasterXlsx.wvtr]);
                string density_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.density_Min]);
                string density_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.density_Max]);
                string thickness2_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.thickness2_Min]);
                string thickness2_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.thickness2_Max]);
                string elongationRate_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.elongationRate_Min]);
                string elongationRate_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.elongationRate_Max]);
                string corpusAlienum_Ink = GetExcelData(dataRow[(int)eComponentMasterXlsx.corpusAlienum_Ink]);
                string fusionPoint_Ink = GetExcelData(dataRow[(int)eComponentMasterXlsx.fusionPoint_Ink]);
                string refractiveIndex_n20D_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.refractiveIndex_n20D_Min]);
                string refractiveIndex_n20D_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.refractiveIndex_n20D_Max]);
                // 20.08.03 신규 추가
                string tensileStrength_50PM = GetExcelData(dataRow[(int)eComponentMasterXlsx.tensileStrength_50PM]);
                string pigment1 = GetExcelData(dataRow[(int)eComponentMasterXlsx.pigment1]);
                string pigment2 = GetExcelData(dataRow[(int)eComponentMasterXlsx.pigment2]);
                string acidAndAlkali1 = GetExcelData(dataRow[(int)eComponentMasterXlsx.acidAndAlkali1]);
                string acidAndAlkali2 = GetExcelData(dataRow[(int)eComponentMasterXlsx.acidAndAlkali2]);
                string acidAndAlkali3 = GetExcelData(dataRow[(int)eComponentMasterXlsx.acidAndAlkali3]);
                string sedimentationRate1 = GetExcelData(dataRow[(int)eComponentMasterXlsx.sedimentationRate1]);
                string Viscosity_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.Viscosity_Min]);
                string Viscosity_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.Viscosity_Max]);
                string SpecificGravity_d25_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.SpecificGravity_d25_Min]);
                string SpecificGravity_d25_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.SpecificGravity_d25_Max]);
                // 20.08.10 추가
                string viscosity_AstmD3236_88_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.viscosity_AstmD3236_88_Min]);
                string viscosity_AstmD3236_88_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.viscosity_AstmD3236_88_Max]);
                string viscosity_AstmD3266_88_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.viscosity_AstmD3266_88_Min]);
                string viscosity_AstmD3266_88_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.viscosity_AstmD3266_88_Max]);
                string nonVolatile_Ksm0009_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.nonVolatile_Ksm0009_Min]);
                string nonVolatile_Ksm0009_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.nonVolatile_Ksm0009_Max]);
                string ashTest_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.ashTest_Min]);
                string ashTest_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.ashTest_Max]);
                string heavyMetal_Ink_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.heavyMetal_Ink_Min]);
                string heavyMetal_Ink_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.heavyMetal_Ink_Max]);
                string viscosity_Ink_Min = GetExcelData(dataRow[(int)eComponentMasterXlsx.viscosity_Ink_Min]);
                string viscosity_Ink_Max = GetExcelData(dataRow[(int)eComponentMasterXlsx.viscosity_Ink_Max]);

                // 필수 항목
                if (componentCode == "" || componentName == "")
                {
                    WriteLog(fileNameTextBox.Text, row + 1);
                    errorCount++; 
                    continue;
                }

                string query = "EXEC InsertUpdateComponentItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', ";
                query += "'{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', ";
                query += "'{20}', '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', '{27}', '{28}', '{29}', ";
                query += "'{30}', '{31}', '{32}', '{33}', '{34}', '{35}', '{36}', '{37}', '{38}', '{39}', ";
                query += "'{40}', '{41}', '{42}', '{43}', '{44}', '{45}', '{46}', '{47}', '{48}', '{49}', ";
                query += "'{50}', '{51}', '{52}', '{53}', '{54}', '{55}', '{56}', '{57}', '{58}', '{59}', '{60}', ";
                query += "'{61}', '{62}', '{63}', '{64}', '{65}', '{66}', '{67}', '{68}', '{69}', '{70}', ";
                query += "'{71}', '{72}', '{73}', '{74}', '{75}', '{76}', '{77}'; ";
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
                   softeningPoint_AstmE28_67_Min = (softeningPoint_AstmE28_67_Min == "" ? "0" : softeningPoint_AstmE28_67_Min),
                   softeningPoint_AstmE28_67_Max = (softeningPoint_AstmE28_67_Max == "" ? "0" : softeningPoint_AstmE28_67_Max),
                   lignin,
                   pigment,
                   acidAndAlkali,
                   fluorescence,
                   formaldehyde,
                   strength,
                   sedimentationRate = (sedimentationRate == "" ? "0" : sedimentationRate),
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
                   fusionPoint_Ink,
                   refractiveIndex_n20D_Min = (refractiveIndex_n20D_Min == "" ? "0" : refractiveIndex_n20D_Min),
                   refractiveIndex_n20D_Max = (refractiveIndex_n20D_Max == "" ? "0" : refractiveIndex_n20D_Max),
                   tensileStrength_50PM = (tensileStrength_50PM == "" ? "0" : tensileStrength_50PM),
                   pigment1,
                   pigment2,
                   acidAndAlkali1,
                   acidAndAlkali2,
                   acidAndAlkali3,
                   sedimentationRate1 = (sedimentationRate1 == "" ? "0" : sedimentationRate1.Replace(",","")),
                   Viscosity_Min = (Viscosity_Min == "" ? "0" : Viscosity_Min.Replace(",", "")),
                   Viscosity_Max = (Viscosity_Max == "" ? "0" : Viscosity_Max.Replace(",", "")),
                   SpecificGravity_d25_Min = (SpecificGravity_d25_Min == "" ? "0" : SpecificGravity_d25_Min.Replace(",", "")),
                   SpecificGravity_d25_Max = (SpecificGravity_d25_Max == "" ? "0" : SpecificGravity_d25_Max.Replace(",", "")),
                   viscosity_AstmD3236_88_Min = (viscosity_AstmD3236_88_Min == "" ? "0" : viscosity_AstmD3236_88_Min.Replace(",", "")),
                   viscosity_AstmD3236_88_Max = (viscosity_AstmD3236_88_Max == "" ? "0" : viscosity_AstmD3236_88_Max.Replace(",", "")),
                   viscosity_AstmD3266_88_Min = (viscosity_AstmD3266_88_Min == "" ? "0" : viscosity_AstmD3266_88_Min.Replace(",", "")),
                   viscosity_AstmD3266_88_Max = (viscosity_AstmD3266_88_Max == "" ? "0" : viscosity_AstmD3266_88_Max.Replace(",", "")),
                   nonVolatile_Ksm0009_Min = (nonVolatile_Ksm0009_Min == "" ? "0" : nonVolatile_Ksm0009_Min.Replace(",", "")),
                   nonVolatile_Ksm0009_Max = (nonVolatile_Ksm0009_Max == "" ? "0" : nonVolatile_Ksm0009_Max.Replace(",", "")),
                   ashTest_Min = (ashTest_Min == "" ? "0" : ashTest_Min.Replace(",", "")),
                   ashTest_Max = (ashTest_Max == "" ? "0" : ashTest_Max.Replace(",", "")),
                   heavyMetal_Ink_Min = (heavyMetal_Ink_Min == "" ? "0" : heavyMetal_Ink_Min.Replace(",", "")),
                   heavyMetal_Ink_Max = (heavyMetal_Ink_Max == "" ? "0" : heavyMetal_Ink_Max.Replace(",", "")),
                   viscosity_Ink_Min = (viscosity_Ink_Min == "" ? "0" : viscosity_Ink_Min.Replace(",", "")),
                   viscosity_Ink_Max = (viscosity_Ink_Max == "" ? "0" : viscosity_Ink_Max.Replace(",", ""))
                        );

                long retVal = DbHelper.ExecuteNonQuery(query);
                if (retVal == -1)
                {
                    WriteLog(fileNameTextBox.Text, row + 1);
                    errorCount++;
                }
            }

            return true; 
        }

        private bool InsertUpdateProductMasters(DataTable dataTable, ref long totalCount, ref long errorCount)
        {

            if (dataTable.Columns.Count < (int)eProductMasterXlsx.note2)
            {
                MessageBox.Show("실제 데이터 항목수보다 엑셀의 컬럼수가 적습니다.");
                return false;
            }

            DbHelper.ExecuteNonQuery("EXEC DeleteAllProduct");

            progressBar.Minimum = 0;
            progressBar.Maximum = dataTable.Rows.Count - 1;
            totalCount = dataTable.Rows.Count - 1;

            string prevProductCode = "";
            string prevmachine = "";
            for (int row = 1; row < dataTable.Rows.Count; row++)
            {
                progressBar.Value = row;

                DataRow dataRow = dataTable.Rows[row];

                string productCode = GetExcelData(dataRow[(int)eProductMasterXlsx.productCode]);
                string productName = ""; // 현재 없다.  
                string productDesc = GetExcelData(dataRow[(int)eProductMasterXlsx.productDesc]);
                string fdaCode = GetExcelData(dataRow[(int)eProductMasterXlsx.fdaCode]);
                string fdaName = GetExcelData(dataRow[(int)eProductMasterXlsx.fdaName]);
                string dosageForm = GetExcelData(dataRow[(int)eProductMasterXlsx.dosageForm]);
                string property = GetExcelData(dataRow[(int)eProductMasterXlsx.property]);
                string machine = GetExcelData(dataRow[(int)eProductMasterXlsx.machine]);
                string standardOnAbsorbedAmount = GetExcelData(dataRow[(int)eProductMasterXlsx.standardOnAbsorbedAmount]);
                string note = GetExcelData(dataRow[(int)eProductMasterXlsx.note]);
                string componentCode = GetExcelData(dataRow[(int)eProductMasterXlsx.componentCode]);
                string mixPurpose = GetExcelData(dataRow[(int)eProductMasterXlsx.mixPurpose]);
                string innerComponentName = GetExcelData(dataRow[(int)eProductMasterXlsx.innerComponentName]);
                string logicalQuantity = GetExcelData(dataRow[(int)eProductMasterXlsx.logicalQuantity]);
                string basicWeight = GetExcelData(dataRow[(int)eProductMasterXlsx.basicWeight]);
                string area = GetExcelData(dataRow[(int)eProductMasterXlsx.area]);
                string usage = GetExcelData(dataRow[(int)eProductMasterXlsx.usage]);
                string productionprocess = GetExcelData(dataRow[(int)eProductMasterXlsx.productionprocess]);
                string note2 = GetExcelData(dataRow[(int)eProductMasterXlsx.note2]); // 원료 속성의 노트

                // 필수 항목
                if (productCode == "" || productDesc == "" || componentCode == "")
                {
                    WriteLog(fileNameTextBox.Text, row + 1);
                    errorCount++;
                    continue;
                }

                if ((prevProductCode != productCode || prevmachine != machine))
                {
                    string[] queryArray = new string[2];
                    queryArray[0] = "EXEC InsertUpdateProductItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}' ";
                    queryArray[0] = string.Format(queryArray[0],
                        productCode,
                         productName,
                         productDesc,
                         fdaCode,
                         fdaName,
                         dosageForm,
                         property,
                         machine,
                         standardOnAbsorbedAmount,
                         note,
                         productionprocess
                        );

                    queryArray[1] = "EXEC InsertUpdateComponentItemByProduct '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ";
                    queryArray[1] = string.Format(queryArray[1],
                        productCode,
                         componentCode,
                         machine,
                         mixPurpose,
                         innerComponentName,
                         logicalQuantity = (logicalQuantity == "" ? "0" : logicalQuantity),
                         basicWeight = (basicWeight == "" ? "0" : basicWeight),
                         area = (area == "" ? "0" : area),
                         usage = (usage == "" ? "0" : usage),
                         note2
                        );

                    long retVal = DbHelper.ExecuteNonQueryWithTransaction(queryArray);
                    if (retVal == -1)
                    {
                        WriteLog(fileNameTextBox.Text, row + 1);
                        errorCount++;
                    }

                    prevProductCode = productCode;
                    prevmachine = machine;
                }
                else
                {
                    string query = "EXEC InsertUpdateComponentItemByProduct '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' , '{9}' ";
                    query = string.Format(query,
                        productCode,
                         componentCode,
                         machine,
                         mixPurpose,
                         innerComponentName,
                         logicalQuantity = (logicalQuantity == "" ? "0" : logicalQuantity),
                         basicWeight = (basicWeight == "" ? "0" : basicWeight),
                         area = (area == "" ? "0" : area),
                         usage = (usage == "" ? "0" : usage),
                         note2
                        );

                    long retVal = DbHelper.ExecuteNonQuery(query);
                    if (retVal == -1)
                    {
                        WriteLog(fileNameTextBox.Text, row + 1);
                        errorCount++;
                    }
                }
            }

            return true; 
        }

        private bool InsertUpdateTestMethods(DataTable dataTable, int testType, ref long totalCount, ref long errorCount)
        {

            if (dataTable.Columns.Count < (int)eTestMethodXlsx.testMethod)
            {
                MessageBox.Show("실제 데이터 항목수보다 엑셀의 컬럼수가 적습니다.");
                return false;
            }

            progressBar.Minimum = 0;
            progressBar.Maximum = dataTable.Rows.Count - 1;
            totalCount = dataTable.Rows.Count - 1;

            testType = testType == (int)Global.eMasterFileType.componentTestMethod ? 0 : 1;

            if(testType == 0)
                DbHelper.ExecuteNonQuery("EXEC DeleteTestMethodsType0");

            for (int row = 1; row < dataTable.Rows.Count; row++)
            {
                progressBar.Value = row;

                DataRow dataRow = dataTable.Rows[row];

                string testItemId = GetExcelData(dataRow[(int)eTestMethodXlsx.testItemId]);
                string testItem = GetExcelData(dataRow[(int)eTestMethodXlsx.testItem]);
                string testMethod = GetExcelData(dataRow[(int)eTestMethodXlsx.testMethod]);
                string testStandard = GetExcelData(dataRow[(int)eTestMethodXlsx.testStandard]);
                string testScore = GetExcelData(dataRow[(int)eTestMethodXlsx.testScore]);
                string unit = ""; 
                if(testType == 0)
                    unit = GetExcelData(dataRow[(int)eTestMethodXlsx.unit]);

                // 필수 항목
                //if (testItemId == "" || testItem == "" || testMethod == "")
                if (testItemId == "" || testItem == "")
                {
                    WriteLog(fileNameTextBox.Text, row + 1);
                    errorCount++;
                    continue;
                }

                string query = "EXEC InsertUpdateTestMethodItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ";
                query = string.Format(query,
                    testType,
                    testItemId,
                    testItem,
                    testMethod,
                    testStandard,
                    testScore,
                    unit);

                long retVal = DbHelper.ExecuteNonQuery(query);
                if (retVal == -1)
                {
                    WriteLog(fileNameTextBox.Text, row + 1);
                    errorCount++;
                }
            }

            return true; 
        }

        private bool InsertUpdateImportInspections(DataTable dataTable, ref long totalCount, ref long errorCount)
        {
            //try
            //{
            //    Errorrow = 0;

                if (dataTable.Columns.Count < (int)eImportInspectionXlsx.note)
                {
                    MessageBox.Show("실제 데이터 항목수보다 엑셀의 컬럼수가 적습니다.");
                    return false;
                }

                progressBar.Minimum = 0;
                progressBar.Maximum = dataTable.Rows.Count - 1;
                totalCount = dataTable.Rows.Count - 1;

                for (int row = 1; row < dataTable.Rows.Count; row++)
                {
                    Errorrow = row;

                    progressBar.Value = row;

                    DataRow dataRow = dataTable.Rows[row];

                    string warehousingDate = "";
                    if (dataRow[(int)eImportInspectionXlsx.warehousingDate] != null)
                    {
                        //warehousingDate = GetExcelData(dataRow[(int)eImportInspectionXlsx.warehousingDate]);

                        DateTime res;
                        if (DateTime.TryParse(dataRow[(int)eImportInspectionXlsx.warehousingDate].ToString(), out res))
                        {
                            warehousingDate = res.ToString("yyyy-MM-dd");
                        }

                        //DateTime newDate = Convert.ToDateTime(dataRow[(int)eImportInspectionXlsx.warehousingDate]).Date;
                        //warehousingDate = newDate.ToString("yyyy-MM-dd");
                    }
                    string componentCode = GetExcelData(dataRow[(int)eImportInspectionXlsx.componentCode]);
                    string componentName = GetExcelData(dataRow[(int)eImportInspectionXlsx.componentName]);
                    string productAreaTypeName = GetExcelData(dataRow[(int)eImportInspectionXlsx.productAreaTypeName]);

                    string productAreaTypeId = "102";
                    if (productAreaTypeName == "국내")
                        productAreaTypeId = "100";
                    else if (productAreaTypeName == "수입" || productAreaTypeName == "국외")
                        productAreaTypeId = "101";

                    string maker = GetExcelData(dataRow[(int)eImportInspectionXlsx.maker]);
                    string lotNo = GetExcelData(dataRow[(int)eImportInspectionXlsx.lotNo]);
                    string mainLotNo = GetExcelData(dataRow[(int)eImportInspectionXlsx.mainLotNo]);
                    string note = "";

                    // 필수 항목
                    //if (warehousingDate == "" || productAreaTypeId == "" || productAreaTypeName == "" || componentCode == "" || componentName == "")
                    if (warehousingDate == "" || productAreaTypeId == "" || componentCode == "" || mainLotNo == "")
                    {
                        WriteLog(fileNameTextBox.Text, row + 1);
                        errorCount++;
                        continue;
                    }

                    long retVal = -1;

                    // 내부 원료명 가져오기
                    DataSet dataSet = DbHelper.SelectQuery(string.Format("EXEC SelectInnerComponentName '{0}'", componentCode));
                    if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
                    {
                        WriteLog(fileNameTextBox.Text, row + 1);
                        errorCount++;
                        continue;
                    }

                    string innerComponentName = dataSet.Tables[0].Rows[0]["innerComponentName"].ToString();

                    string query = "EXEC InsertUpdateImportInspectionItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ";
                    query = string.Format(query,
                        warehousingDate,
                            componentCode,
                             componentName,
                             innerComponentName,
                             productAreaTypeId,
                             productAreaTypeName,
                             maker,
                             lotNo,
                             mainLotNo,
                             note);

                    retVal = (long)DbHelper.ExecuteScalar(query);
                    if (retVal == -1) // 어떤 에러든 발생되면 이 절까지 오지 않고 catch 로 빠질 것 같다.
                    {
                        WriteLog(fileNameTextBox.Text, row + 1);
                        errorCount++;
                        //throw new Exception("Occurs an error");
                    }
                    else
                    {
                        const string pattern = "[0-9][0-9]G[(]|[0-9][0-9][0-9]G[(]|[0-9][0-9].[0-9]G[(]|[0-9][0-9][0-9].[0-9]G[(]|[0-9][0-9]GSM|[0-9][0-9][0-9]GSM|[0-9][0-9].[0-9]GSM|[0-9][0-9][0-9].[0-9]GSM";
                        Match match = Regex.Match(componentName, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);

                        string matchcomponentName = match.ToString();

                        retVal = DbHelper.GetLongValue(string.Format("EXEC IsExistedComponentTest '{0}','{1}' ,'{2}'", matchcomponentName, innerComponentName, mainLotNo), "isExisted", -1);
                        if (retVal == 0)
                        {
                            query = "EXEC InsertUpdateComponentTestItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '', '{11}', '', '', '{11}', '', '', 0, 0 ";
                            query = string.Format(query,
                              retVal,
                              warehousingDate,
                              componentCode,
                              componentName,
                              innerComponentName,
                              productAreaTypeId,
                              productAreaTypeName,
                              maker,
                              lotNo,
                              mainLotNo,
                              note,
                                DateTime.Now.ToString("yyyy-MM-dd"));
                            retVal = (long)DbHelper.ExecuteScalar(query);
                            if (retVal == -1)
                            {
                                WriteLog(fileNameTextBox.Text, row + 1);
                                errorCount++;
                                //throw new Exception("Occurs an error");
                            }
                            else
                            {
                                ////
                                // 내부 원료명과 Main Lot이 같은 항목으로부터 테스트를 가져와서 현재 항목을 업데이트 한다. 
                                query = string.Format("EXEC CopyComponentTestItemForInsert '{0}', '{1}', '{2}'", retVal, innerComponentName, mainLotNo);
                                retVal = DbHelper.ExecuteNonQuery(query);
                                ////
                            }
                        }

                    }
                }
                return true;
            //}
            //catch (Exception ex)
            //{
            //    WriteLog(fileNameTextBox.Text, Errorrow + 1);
            //    MessageBox.Show(ex.Message);
            //    return true;
            //}
        }

        private void fileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            executeButton.Enabled = fileNameTextBox.Text.Trim() == "" ? false : true;
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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close(); 
        }
    }
}
