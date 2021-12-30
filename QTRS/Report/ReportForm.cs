using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using C1report = C1.Win.FlexReport;
using C1.Win.C1Document.Export;
using QTRS.ProductTest;

namespace QTRS.Report
{
    public partial class ReportForm : Form
    {
        ReportViewer testResultReportViewer = new ReportViewer();
        int _reportType = (int)Global.eReportType.qualityManagement;

        // save report parameters
        private DataSet _savereportheadDataSet = null;
        private DataTable _mainlotcomponentDataTable = null;
        private int _savereportmode = 0; // 0 : 품질관리 기록서, 1 : 제조관리 기록서, 2 : 원료약품 시험성적서
        private string _testIdx = "";
        private string _approvalDate = "";

        C1report.C1FlexReport rep = new C1.Win.FlexReport.C1FlexReport();

        public ReportForm(int reportType)
        {
            InitializeComponent();
            _reportType = reportType;

            if (Global.loginInfo.jobId == 102)
                saveReportButton.Visible = true;

            if (_reportType == (int)Global.eReportType.qualityManagement)
                this.Text = "품질관리 기록서";
            else if (_reportType == (int)Global.eReportType.manufactureManagement)
                this.Text = "제조관리 기록서";
            else if (_reportType == (int)Global.eReportType.componentDrugTest)
            {
                testMcComboBox.Visible = false;
                textMcnumLabel.Visible = false;
                testCompanyName.Visible = true;
                testDescWherecheckbox.Visible = true;

                testProductTextCheckbox.Text = "원료명";
                this.Text = "원료약품 시험성적서";
            }
            else if (_reportType == (int)Global.eReportType.finalQualityManagement)
                this.Text = "최종포장(완제품) 품질관리 기록서";

            DataSet headerDataSet = DbHelper.SelectQuery("SelectComponentTestListForReportCombobox");

            ComponentListCombobox(testCompanyName, headerDataSet);

            testCompanyName.SelectedIndex = 0;
            testMcComboBox.SelectedIndex = 0;
        }

        private void ProductQtTestResultReportForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qtrsDataSource.ProductQtTestResultView' table. You can move, or remove it, as needed.
            //this.productQtTestResultViewTableAdapter.Fill(this.qtrsDataSource.ProductQtTestResultView);
            InitControls();
        }

        private void InitControls()
        {
            //this.Enabled = false;

            startDateTimeDateTimePicker.Value = DateTime.Now.AddDays(-7);
            endDateTimeDateTimePicker.Value = DateTime.Now;

            InitTestComboBox();

            ////this.Enabled = true; 
        }


        private void createReportButton_Click(object sender, EventArgs e)
        {
            if (_reportType == (int)Global.eReportType.componentDrugTest)
                CreateComponentDrugTestReport();
            else if (_reportType == (int)Global.eReportType.qualityManagement)
                CreateQualityManagementReport();
            else if (_reportType == (int)Global.eReportType.manufactureManagement)
                CreateManufactureManagementReport();
            else if (_reportType == (int)Global.eReportType.finalQualityManagement)
                CreateFinalQualityManagementReport();
        }

        private void CreateQualityManagementReport()
        {
            try
            {
                if (testIdComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("테스트 항목을 선택해 주십시오.");
                    testIdComboBox.Focus();
                    return;
                }

                string testId = (testIdComboBox.SelectedItem as ComboBoxItem).Value.ToString();
                string query = string.Format("EXEC SelectProductQtTestReportHeader '{0}'", testId);

                rep.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Report\ProductQtTestResultReport.flxr", "QTRS");

                DataSet headerDataSet = DbHelper.SelectQuery(query);
                if (headerDataSet == null || headerDataSet.Tables.Count == 0)
                {
                    MessageBox.Show("시험정보를 가져올 수 없습니다.");
                    return;
                }

                if (headerDataSet.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("시험정보가 아직 없습니다.");
                    return;
                }

                ReportParameter[] reportParameter = new ReportParameter[4];

                string approvalDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                //reportParameter[0] = new ReportParameter("reportTitle", "품질관리 기록서");
                //reportParameter[1] = new ReportParameter("testDate", Utils.GetString(headerDataSet.Tables[0].Rows[0]["testDate"]));
                //reportParameter[2] = new ReportParameter("testerName", Utils.GetString(headerDataSet.Tables[0].Rows[0]["testerName"]));
                //reportParameter[3] = new ReportParameter("approvalDate", " ");

                //if (Global.loginInfo.jobId == 103)
                //{
                //    reportParameter = new ReportParameter[5];
                //    reportParameter[0] = new ReportParameter("reportTitle", "품질관리 기록서");
                //    reportParameter[1] = new ReportParameter("testDate", Utils.GetString(headerDataSet.Tables[0].Rows[0]["testDate"]));
                //    reportParameter[2] = new ReportParameter("testerName", Utils.GetString(headerDataSet.Tables[0].Rows[0]["testerName"]));
                //    reportParameter[3] = new ReportParameter("sign", new Uri(System.Windows.Forms.Application.StartupPath + "\\Images\\cyk_sign.png").AbsoluteUri);
                //    reportParameter[4] = new ReportParameter("approvalDate", approvalDate);
                //}
                //else
                //{

                //    reportParameter = new ReportParameter[4];
                //    reportParameter[0] = new ReportParameter("reportTitle", "품질관리 기록서");
                //    reportParameter[1] = new ReportParameter("testDate", Utils.GetString(headerDataSet.Tables[0].Rows[0]["testDate"]));
                //    reportParameter[2] = new ReportParameter("testerName", Utils.GetString(headerDataSet.Tables[0].Rows[0]["testerName"]));
                //    reportParameter[3] = new ReportParameter("approvalDate", " ");
                //}

                string qyNote = headerDataSet.Tables[0].Rows[0]["qyNote"].ToString();
                //headerDataSet.Tables[0].Rows[0]["qyNote"] = qyNote.Replace("/0'", "");


                query = string.Format("EXEC SelectProductQtTestReportContent '{0}'", testId);
                string query2 = string.Format("EXEC SelectProductQtTestReportContentSum '{0}'", testId);

                DataSet contentDataSet = DbHelper.SelectQuery(query);
                DataSet contentDataSumSet = DbHelper.SelectQuery(query2);
                if (contentDataSet == null || contentDataSet.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("시험정보를 가져올 수 없습니다.");
                    return;
                }
                else
                {
                    for (int row = 0; row < contentDataSet.Tables[0].Rows.Count; row++)
                    {
                        DataRow dataRow = contentDataSet.Tables[0].Rows[row];

                        if (dataRow["testItemId"].ToString() == "3") // 질량
                        {
                            string testScore = dataRow["testScore"].ToString();
                            string[] testScoreArray = testScore.Split(Global.DB_VALUE_SEPARATOR.ToCharArray()[0]);
                            //testScore = testScoreArray.Length > 1 ? string.Join("g ", Utils.SubArray(testScoreArray, 0, testScoreArray.Length > 5 ? 5 : testScoreArray.Length)) + "g" : "";
                            //dataRow["testScore"] = testScore;

                            DataRow newDataRow = contentDataSet.Tables[0].NewRow();
                            newDataRow["testItemId"] = dataRow["testItemId"].ToString();
                            newDataRow["testItemName"] = "";
                            //newDataRow["testStandard"] = "기준(g) : 4.22g";
                            newDataRow["testStandard"] = string.Format("기준(g) : {0}g", contentDataSumSet.Tables[0].Rows[0]["logicalQuantity"]);
                            //newDataRow["testScore"] = testScoreArray.Length > 5 ? string.Join("g ", Utils.SubArray(testScoreArray, 5, testScoreArray.Length - 5)) + "g" : "";
                            newDataRow["testScore"] = "";
                            newDataRow["compatibilityOx"] = "";
                            //newDataRow["testerName"] = dataRow["testerName"].ToString();
                            contentDataSet.Tables[0].Rows.InsertAt(newDataRow, row + 1);

                            DataRow newDataRow2 = contentDataSet.Tables[0].NewRow();
                            newDataRow2["testItemId"] = dataRow["testItemId"].ToString();
                            newDataRow2["testItemName"] = "";
                            newDataRow2["testStandard"] = "Average";
                            //newDataRow2["testScore"] = Utils.GetAverage(testScoreArray).ToString() + "g ";
                            newDataRow2["testScore"] = string.Format("{0,10:N2}", Utils.GetAverage(testScoreArray)) + "g ";
                            newDataRow2["compatibilityOx"] = "";
                            //newDataRow2["testerName"] = dataRow["testerName"].ToString();
                            contentDataSet.Tables[0].Rows.InsertAt(newDataRow2, row + 2);

                            DataRow newDataRow3 = contentDataSet.Tables[0].NewRow();
                            newDataRow3["testItemId"] = dataRow["testItemId"].ToString();
                            newDataRow3["testItemName"] = "";
                            newDataRow3["testStandard"] = "";
                            //newDataRow3["testScore"] = string.Format("{0,10:N2}", Utils.GetAverage(testScoreArray) / 4.22) + "%";
                            newDataRow3["testScore"] = string.Format("{0,10:N2}", (Utils.GetAverage(testScoreArray) / Convert.ToDouble(contentDataSumSet.Tables[0].Rows[0]["logicalQuantity"])) * 100) + "%";
                            newDataRow3["compatibilityOx"] = "";
                            //newDataRow3["testerName"] = dataRow["testerName"].ToString();
                            contentDataSet.Tables[0].Rows.InsertAt(newDataRow3, row + 3);

                            rep.Parameters["TestStandard0302"].Value = contentDataSumSet.Tables[0].Rows[0]["logicalQuantity"].ToString().Trim();
                            rep.Parameters["TestScore0302"].Value = string.Format("{0,10:N2}", Utils.GetAverage(testScoreArray)).Trim();
                            rep.Parameters["TestScore0303"].Value = string.Format("{0,10:N2}", (Utils.GetAverage(testScoreArray) / Convert.ToDouble(contentDataSumSet.Tables[0].Rows[0]["logicalQuantity"])) * 100).Trim();

                            for (int i = 0; i < testScoreArray.Length; i++)
                            {
                                if (i < 9)
                                {
                                    rep.Parameters[string.Format("TestScore03010{0}", i + 1)].Value = testScoreArray[i].ToString();
                                }
                                else
                                {
                                    rep.Parameters["TestScore030110"].Value = testScoreArray[i].ToString();
                                }

                            }

                            break;
                        }
                    }
                }

                foreach (DataRow dataRow in contentDataSet.Tables[0].Rows)
                {
                    if (dataRow["testItemId"].ToString() == "5") // 산 및 알칼리 P.P
                    {
                        //dataRow["testItemName"] = dataRow["testItemName"].ToString().Substring(0, dataRow["testItemName"].ToString().LastIndexOf("P.P"));
                        dataRow["testItemName"] = "5.산 및 알칼리";
                    }

                    else if (dataRow["testItemId"].ToString() == "6") // 산 및 알칼리 M.O
                    {
                        dataRow["testItemName"] = "";
                    }

                    else if (dataRow["testItemId"].ToString() == "8") // 흡수량
                    {
                        string absorbedAmountData = Utils.GetString(dataRow["testScore"]);
                        if (absorbedAmountData == "")
                        {
                            dataRow["testScore"] = "0배";
                            rep.Parameters["TestScore070101"].Value = 0;
                            rep.Parameters["TestScore070102"].Value = 0;
                            rep.Parameters["TestScore070103"].Value = 0;
                            rep.Parameters["TestScore070201"].Value = 0;
                            rep.Parameters["TestScore070202"].Value = 0;
                            rep.Parameters["TestScore070203"].Value = 0;
                            rep.Parameters["TestScore070301"].Value = 0;
                        }
                        else
                        {
                            string[] testScoreArray = absorbedAmountData.Trim().Split(Global.DB_VALUE_SEPARATOR.ToCharArray()[0]);

                            // 흡수량 1
                            decimal temp1 = decimal.Parse(testScoreArray[0]);
                            decimal absorbedAmount1 = Math.Round(decimal.Parse(testScoreArray[1]) /
                                (temp1 == 0 ? 1 : temp1), 1);

                            // 흡수량 2
                            decimal temp2 = decimal.Parse(testScoreArray[2]);
                            decimal absorbedAmount2 = Math.Round(decimal.Parse(testScoreArray[3]) /
                                (temp2 == 0 ? 1 : temp2), 1);

                            // 흡수배
                            decimal absorbedAmountAverage = Math.Round((Convert.ToDecimal(absorbedAmount1) + Convert.ToDecimal(absorbedAmount2)) / 2, 1, MidpointRounding.AwayFromZero);
                            dataRow["testScore"] = string.Format("{0:0.0} ", absorbedAmountAverage) + "배";

                            rep.Parameters["TestScore070101"].Value = Convert.ToDecimal(testScoreArray[0]);
                            rep.Parameters["TestScore070102"].Value = Convert.ToDecimal(testScoreArray[1]);
                            rep.Parameters["TestScore070103"].Value = absorbedAmount1;
                            rep.Parameters["TestScore070201"].Value = Convert.ToDecimal(testScoreArray[2]);
                            rep.Parameters["TestScore070202"].Value = Convert.ToDecimal(testScoreArray[3]);
                            rep.Parameters["TestScore070203"].Value = absorbedAmount2;
                            rep.Parameters["TestScore070301"].Value = absorbedAmountAverage;

                        }
                    }


                }

                foreach (DataRow dataRow in contentDataSet.Tables[0].Rows)
                {
                    int itemnumber = 0;

                    if (dataRow["testitemName"].ToString().Replace(" ", string.Empty) != "")
                    {
                        itemnumber = Convert.ToInt16(dataRow["testitemName"].ToString().Replace(" ", string.Empty).Substring(0, dataRow["testitemName"].ToString().Replace(" ", string.Empty).LastIndexOf('.')));
                    }

                    if (dataRow["testItemId"].ToString() == "3") // 질량
                    {
                        if (dataRow["testitemName"].ToString().Replace(" ", string.Empty) == "3.질량")
                        {
                            rep.Parameters["TestStandard0303"].Value = dataRow["testStandard"].ToString().Trim();
                            rep.Parameters["CompatibilityOx03"].Value = dataRow["compatibilityOx"].ToString().Trim();
                        }
                    }

                    else if (dataRow["testItemId"].ToString() == "5") // 산 및 알칼리
                    {
                        rep.Parameters["TestStandard0501"].Value = dataRow["testStandard"].ToString().Trim();
                        rep.Parameters["TestScore0501"].Value = dataRow["testScore"].ToString().Trim();
                        rep.Parameters["CompatibilityOx05"].Value = dataRow["compatibilityOx"].ToString().Trim();
                    }

                    else if (dataRow["testItemId"].ToString() == "6") // 산 및 알칼리
                    {
                        rep.Parameters["TestStandard0502"].Value = dataRow["testStandard"].ToString().Trim();
                        rep.Parameters["TestScore0502"].Value = dataRow["testScore"].ToString().Trim();
                    }

                    else if (dataRow["testItemId"].ToString() == "8") // 흡수량
                    {
                        rep.Parameters["TestStandard07"].Value = dataRow["testStandard"].ToString().Trim();
                        rep.Parameters["CompatibilityOx07"].Value = dataRow["compatibilityOx"].ToString().Trim();
                    }

                    else
                    {
                        if (itemnumber < 10)
                        {
                            rep.Parameters[string.Format("TestStandard0{0}", itemnumber)].Value = dataRow["testStandard"].ToString().Trim();
                            rep.Parameters[string.Format("TestScore0{0}", itemnumber)].Value = dataRow["testScore"].ToString().Trim();
                            rep.Parameters[string.Format("CompatibilityOx0{0}", itemnumber)].Value = dataRow["compatibilityOx"].ToString().Trim();
                        }
                        else
                        {
                            rep.Parameters[string.Format("TestStandard{0}", itemnumber)].Value = dataRow["testStandard"].ToString().Trim();
                            rep.Parameters[string.Format("TestScore{0}", itemnumber)].Value = dataRow["testScore"].ToString().Trim();
                            rep.Parameters[string.Format("CompatibilityOx{0}", itemnumber)].Value = dataRow["compatibilityOx"].ToString().Trim();
                        }
                    }
                }

                //this.testResultReportViewer.LocalReport.EnableExternalImages = true;
                //this.testResultReportViewer.LocalReport.ReportEmbeddedResource = "QTRS.Report.ProductQtTestResultReport.rdlc";
                //this.testResultReportViewer.PageCountMode = PageCountMode.Actual;

                //ReportDataSource reportDataSourceHeader = new ReportDataSource("ProductQtTestHeaderDataSet", headerDataSet.Tables[0]);
                //ReportDataSource reportDataSourceContent = new ReportDataSource("ProductQtTestDataSet", contentDataSet.Tables[0]);
                //this.testResultReportViewer.LocalReport.DataSources.Clear();


                //this.testResultReportViewer.LocalReport.DataSources.Add(reportDataSourceHeader);
                //this.testResultReportViewer.LocalReport.DataSources.Add(reportDataSourceContent);
                //this.testResultReportViewer.LocalReport.SetParameters(reportParameter);
                //this.testResultReportViewer.RefreshReport();

                rep.DataSource.Recordset = headerDataSet.Tables[0];

                //QTRSFlexViewer.DocumentSource.ClearContent();
                QTRSFlexViewer.DocumentSource = rep;

                _savereportheadDataSet = headerDataSet;
                _testIdx = testId;
                _approvalDate = approvalDate;
                _savereportmode = 0;

                //SaveReport(
                //    headerDataSet.Tables[0].Rows[0]["fdaName"].ToString(),                  // 제품명/원료명(식약처허가명)
                //    "",                                                                     // 원료명(SAP명)
                //    headerDataSet.Tables[0].Rows[0]["manufactureSerialNumber"].ToString(),  // 제조번호
                //    headerDataSet.Tables[0].Rows[0]["manufactureDate"].ToString(),          // 제조일
                //    headerDataSet.Tables[0].Rows[0]["gatherPlace"].ToString(),              // 채취장소
                //    "",                                                                     // 납품처명
                //    testId,                                                                // TESTIDX
                //    approvalDate                                                            // 승인일
                //    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateManufactureManagementReport()
        {
            try
            {
                if (testIdComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("테스트 항목을 선택해 주십시오.");
                    testIdComboBox.Focus();
                    return;
                }

                string testId = (testIdComboBox.SelectedItem as ComboBoxItem).Value.ToString();
                string query = string.Format("EXEC SelectProductMfTestReportHeader '{0}'", testId);

                DataSet headerDataSet = DbHelper.SelectQuery(query);
                if (headerDataSet == null || headerDataSet.Tables.Count == 0)
                {
                    MessageBox.Show("시험정보를 가져올 수 없습니다.");
                    return;
                }

                if (headerDataSet.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("시험정보가 아직 없습니다.");
                    return;
                }


                //ReportParameter[] reportParameter = new ReportParameter[10];

                string approvalDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                //reportParameter[0] = new ReportParameter("reportTitle", "제조관리 기록서");
                //reportParameter[1] = new ReportParameter("specialNote", Utils.GetString(headerDataSet.Tables[0].Rows[0]["specialNote"]));
                //reportParameter[2] = new ReportParameter("checkerName", Utils.GetString(headerDataSet.Tables[0].Rows[0]["checkerName"]));
                //reportParameter[3] = new ReportParameter("correctiveMeasure", Utils.GetString(headerDataSet.Tables[0].Rows[0]["correctiveMeasure"]));
                //reportParameter[4] = new ReportParameter("observationNote", Utils.GetString(headerDataSet.Tables[0].Rows[0]["observationNote"]));
                //reportParameter[7] = new ReportParameter("approvalDate", " ");
                //reportParameter[8] = new ReportParameter("productionprocess", Utils.GetString(headerDataSet.Tables[0].Rows[0]["productionprocess"]));
                //reportParameter[9] = new ReportParameter("note", "");

                rep.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Report\ProductMfTestResultReportRow22.flxr", "QTRS");

                query = string.Format("EXEC SelectProductMfTestReportContent '{0}'", testId);

                DataSet contentDataSet = DbHelper.SelectQuery(query);
                if (contentDataSet == null || contentDataSet.Tables.Count == 0)
                {
                    MessageBox.Show("시험정보를 가져올 수 없습니다.");
                    return;
                }

                //string datesub = headerDataSet.Tables[0].Rows[0]["manufactureDate"].ToString();
                DateTime covertdate = Convert.ToDateTime(headerDataSet.Tables[0].Rows[0]["manufactureDate"].ToString().Substring(0, headerDataSet.Tables[0].Rows[0]["manufactureDate"].ToString().IndexOf(" ")));
                string componentdate = covertdate.AddYears(-3).AddDays(-1).ToString("yyyy-MM-dd");


                double totalStandardAmount = 0;
                double totalUsage = 0;
                foreach (DataRow contentDataRow in contentDataSet.Tables[0].Rows)
                {
                    string componentCode = contentDataRow["componentCode"].ToString();
                    string standardAmount = contentDataRow["standardAmount"].ToString();
                    string usage = contentDataRow["usage"].ToString();

                    //contentDataRow["standardAmount"] = string.Format("{0,10:N2}", double.Parse(standardAmount));
                    //contentDataRow["usage"] = string.Format("{0,10:N3}", double.Parse(usage));
                    //contentDataRow["standardAmount"] = Math.Round(double.Parse(standardAmount), 2);
                    //contentDataRow["usage"] = Math.Round(double.Parse(usage), 2);

                    totalStandardAmount += double.Parse(standardAmount);
                    totalUsage += double.Parse(usage);

                    query = string.Format("EXEC SelectProductMfTestReportNewSubContent '{0}','{1}'", testId, componentCode);
                    DataSet subContentDataSet = DbHelper.SelectQuery(query);

                    if (subContentDataSet.Tables[0].Rows.Count > 0)
                    {
                        for (int row = 1; row < 7; row++)
                        {
                            string lot = string.Format("Lot0{0}", row);
                            string mainLot = (row == 1) ? "mainLotNo" : string.Format("mainLotNo{0}", row);

                            string mainLotNo = subContentDataSet.Tables[0].Rows[0][lot].ToString();
                            contentDataRow[mainLot] = mainLotNo;
                        }

                    }
                    else
                    {
                        contentDataRow["mainLotNo"] = "";
                    }

                }

                _mainlotcomponentDataTable = contentDataSet.Tables[0];

                if (contentDataSet.Tables[0].Rows.Count < 22)
                {
                    for (int row = contentDataSet.Tables[0].Rows.Count; row < 22; row++)
                    {
                        contentDataSet.Tables[0].Rows.Add("", "", null, null, null, "", "", "", "", "", "");
                    }
                }
                //reportParameter[5] = new ReportParameter("totalStandardAmount", totalStandardAmount.ToString());
                //reportParameter[6] = new ReportParameter("totalUsage", totalUsage.ToString());

                ////
                double logicalPadQuantity = double.Parse(Utils.GetDigitString(headerDataSet.Tables[0].Rows[0]["logicalPadQuantity"].ToString()));
                double realPadQuantity = double.Parse(Utils.GetDigitString(headerDataSet.Tables[0].Rows[0]["realPadQuantity"].ToString()));
                double numOfBag1 = double.Parse(Utils.GetDigitString(headerDataSet.Tables[0].Rows[0]["numOfBag1"].ToString()) == "" ? "0" : Utils.GetDigitString(headerDataSet.Tables[0].Rows[0]["numOfBag1"].ToString()));
                logicalPadQuantity = logicalPadQuantity == 0 ? 1 : logicalPadQuantity;
                realPadQuantity = realPadQuantity == 0 ? 1 : realPadQuantity;
                numOfBag1 = numOfBag1 == 0 ? 1 : numOfBag1;

                // 총(이론) 이론 생산량
                //double logicalProductionQuantity = totalStandardAmount * logicalPadQuantity;
                double logicalProductionQuantity = totalUsage * logicalPadQuantity;
                // 총(실제)생산량 
                double realProductionQuantity = totalUsage * realPadQuantity;
                // 총(이론)Bag량
                double logicalBagQuantity = logicalPadQuantity / numOfBag1;
                // 총(실제)Bag량
                double realBagQuantity = realPadQuantity / numOfBag1;

                headerDataSet.Tables[0].Rows[0]["logicalProductionQuantity"] = string.Format("{0,10:N1}", logicalProductionQuantity / 1000) + " kg";
                headerDataSet.Tables[0].Rows[0]["realProductionQuantity"] = string.Format("{0,10:N1}", realProductionQuantity / 1000) + " kg";
                headerDataSet.Tables[0].Rows[0]["logicalBagQuantity"] = string.Format("{0,10:N0}", logicalBagQuantity) + " Bag";
                headerDataSet.Tables[0].Rows[0]["realBagQuantity"] = string.Format("{0,10:N0}", realBagQuantity) + " Bag";

                headerDataSet.Tables[0].Rows[0]["productionQuantityResult"] = string.Format("{0,10:N2}", (realProductionQuantity / logicalProductionQuantity) * 100) + "%";
                headerDataSet.Tables[0].Rows[0]["padQuantityResult"] = string.Format("{0,10:N2}", (realPadQuantity / logicalPadQuantity) * 100) + "%";
                headerDataSet.Tables[0].Rows[0]["bagQuantityResult"] = string.Format("{0,10:N2}", (realBagQuantity / logicalBagQuantity) * 100) + "%";

                headerDataSet.Tables[0].Rows[0]["logicalPadQuantity"] = string.Format("{0,10:N0}", logicalPadQuantity) + " 패드";
                headerDataSet.Tables[0].Rows[0]["realPadQuantity"] = string.Format("{0,10:N0}", realPadQuantity) + " 패드";

                ////
                //this.testResultReportViewer.LocalReport.EnableExternalImages = true;
                //this.testResultReportViewer.LocalReport.ReportEmbeddedResource = "QTRS.Report.ProductMfTestResultReport.rdlc";
                //this.testResultReportViewer.PageCountMode = PageCountMode.Actual;

                //ReportDataSource reportDataSourceHeader = new ReportDataSource("ProductMfTestHeaderDataSet", headerDataSet.Tables[0]);
                //ReportDataSource reportDataSourceContent = new ReportDataSource("ProductMfTestContentDataSet", contentDataSet.Tables[0]);
                //this.testResultReportViewer.LocalReport.DataSources.Clear();


                //this.testResultReportViewer.LocalReport.DataSources.Add(reportDataSourceHeader);
                //this.testResultReportViewer.LocalReport.DataSources.Add(reportDataSourceContent);
                //this.testResultReportViewer.LocalReport.SetParameters(reportParameter);
                //this.testResultReportViewer.RefreshReport();

                // QTRS New Report 
                rep.Parameters["CheckerName"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["checkerName"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["checkerName"]);
                rep.Parameters["CorrectiveMeasure"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["correctiveMeasure"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["correctiveMeasure"]);
                rep.Parameters["ObservationNote"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["observationNote"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["observationNote"]);
                rep.Parameters["Productionprocess"].Value = Utils.GetString(headerDataSet.Tables[0].Rows[0]["productionprocess"]);
                rep.Parameters["TotalStandardAmount"].Value = totalStandardAmount;
                rep.Parameters["TotalUsage"].Value = totalUsage;
                rep.Parameters["ProductName"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["fdaName"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["fdaName"]);
                rep.Parameters["DosageForm"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["dosageForm"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["dosageForm"]);
                rep.Parameters["Property"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["property"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["property"]);
                rep.Parameters["ManufactureSerialNumber"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["manufactureSerialNumber"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["manufactureSerialNumber"]);
                rep.Parameters["ManufactureDate"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["manufactureDate"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["manufactureDate"]);
                rep.Parameters["ManufactureQuantity"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["manufactureQuantity"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["manufactureQuantity"]);
                rep.Parameters["JudgingDate"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["judgingDate"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["judgingDate"]);
                rep.Parameters["StartWorkDate"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["startWorkDate"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["startWorkDate"]);
                rep.Parameters["StartWorkTime"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["startWorkTime"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["startWorkTime"]);
                rep.Parameters["StartWorkerName"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["startWorkerName"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["startWorkerName"]);
                rep.Parameters["EndWorkDate"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["endWorkDate"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["endWorkDate"]);
                rep.Parameters["EndWorkTime"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["endWorkTime"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["endWorkTime"]);
                rep.Parameters["EndWorkerName"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["endWorkerName"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["endWorkerName"]);
                rep.Parameters["LogicalProductionQuantity"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["logicalProductionQuantity"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["logicalProductionQuantity"]);
                rep.Parameters["LogicalPadQuantity"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["logicalPadQuantity"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["logicalPadQuantity"]);
                rep.Parameters["LogicalBagQuantity"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["logicalBagQuantity"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["logicalBagQuantity"]);
                rep.Parameters["RealProductionQuantity"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["realProductionQuantity"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["realProductionQuantity"]);
                rep.Parameters["RealPadQuantity"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["realPadQuantity"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["realPadQuantity"]);
                rep.Parameters["RealBagQuantity"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["realBagQuantity"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["realBagQuantity"]);
                rep.Parameters["ProductionQuantityResult"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["productionQuantityResult"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["productionQuantityResult"]);
                rep.Parameters["PadQuantityResult"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["padQuantityResult"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["padQuantityResult"]);
                rep.Parameters["BagQuantityResult"].Value = (Utils.GetString(headerDataSet.Tables[0].Rows[0]["bagQuantityResult"]) == "") ? "  " : Utils.GetString(headerDataSet.Tables[0].Rows[0]["bagQuantityResult"]);

                rep.DataSource.Recordset = contentDataSet.Tables[0];

                QTRSFlexViewer.DocumentSource = rep;

                lotModifySaveButton.Visible = true;
                _savereportheadDataSet = headerDataSet;
                _testIdx = testId;
                _approvalDate = approvalDate;
                _savereportmode = 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateComponentDrugTestReport()
        {
            try
            {
                if (testIdComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("테스트 항목을 선택해 주십시오.");
                    testIdComboBox.Focus();
                    return;
                }

                string testIdx = (testIdComboBox.SelectedItem as ComboBoxItem).Value.ToString();
                string query = string.Format("EXEC SelectComponentDrugTestReportHeader '{0}'", testIdx);

                DataSet headerDataSet = DbHelper.SelectQuery(query);
                if (headerDataSet == null || headerDataSet.Tables.Count == 0)
                {
                    MessageBox.Show("시험정보를 가져올 수 없습니다.");
                    return;
                }

                if (headerDataSet.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("시험정보가 아직 없습니다.");
                    return;
                }

                /*
                ReportParameter[] reportParameter = new ReportParameter[dataSet.Tables[0].Columns.Count];
                DataRow dataRow = dataSet.Tables[0].Rows[0];
                int col = 0; 
                reportParameter[col++] = new ReportParameter("fdaName", Utils.GetString(dataRow["fdaName"]));
                reportParameter[col++] = new ReportParameter("receiptDate", Utils.GetString(dataRow["receiptDate"]));
                reportParameter[col++] = new ReportParameter("gatherQuantity", Utils.GetString(dataRow["gatherQuantity"]));
                reportParameter[col++] = new ReportParameter("gathererName", Utils.GetString(dataRow["gathererName"]));
                reportParameter[col++] = new ReportParameter("gatherPlace", Utils.GetString(dataRow["gatherPlace"]));
                reportParameter[col++] = new ReportParameter("gatherDate", Utils.GetString(dataRow["gatherDate"]));
                reportParameter[col++] = new ReportParameter("qyNote", Utils.GetString(dataRow["qyNote"]));
                reportParameter[col++] = new ReportParameter("judgingResult", Utils.GetString(dataRow["judgingResult"]));
                reportParameter[col++] = new ReportParameter("judgerName", Utils.GetString(dataRow["judgerName"]));
                reportParameter[col++] = new ReportParameter("judgingDate", Utils.GetString(dataRow["judgingDate"]));
                reportParameter[col++] = new ReportParameter("productCode", Utils.GetString(dataRow["productCode"]));
                */
                /*
                ReportParameter[] reportParameter = new ReportParameter[1];
                reportParameter[0] = new ReportParameter("reportTitle", "제조관리 기록서");
                */
                ReportParameter[] reportParameter = new ReportParameter[2];

                string approvalDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                reportParameter[0] = new ReportParameter("reportTitle", "원료약품 시험성적서");
                reportParameter[1] = new ReportParameter("approvalDate", " ");

                //if (Global.loginInfo.jobId == 103)
                //{
                //    reportParameter = new ReportParameter[3];
                //    reportParameter[0] = new ReportParameter("reportTitle", "원료약품 시험성적서");
                //    reportParameter[1] = new ReportParameter("sign", new Uri(System.Windows.Forms.Application.StartupPath + "\\Images\\cyk_sign.png").AbsoluteUri);
                //    reportParameter[2] = new ReportParameter("approvalDate", approvalDate);
                //}
                //else
                //{

                //    reportParameter = new ReportParameter[2];
                //    reportParameter[0] = new ReportParameter("reportTitle", "원료약품 시험성적서");
                //    reportParameter[1] = new ReportParameter("approvalDate", " ");
                //}


                query = string.Format("EXEC SelectComponentDrugTestReportContent '{0}'", testIdx);

                DataSet contentDataSet = DbHelper.SelectQuery(query);
                if (contentDataSet == null || contentDataSet.Tables.Count == 0)
                {
                    MessageBox.Show("시험정보를 가져올 수 없습니다.");
                    return;
                }

                foreach (DataRow dataRow in contentDataSet.Tables[0].Rows)
                {
                    string testItemName = dataRow["testItemName"].ToString();
                    string unit = dataRow["unit"].ToString();
                    if (unit != "")
                    {
                        testItemName += string.Format("({0})", unit);
                        dataRow["testItemName"] = testItemName;
                    }
                }


                this.testResultReportViewer.LocalReport.EnableExternalImages = true;
                this.testResultReportViewer.LocalReport.ReportEmbeddedResource = "QTRS.Report.ComponentDrugTestResultReport.rdlc";
                this.testResultReportViewer.PageCountMode = PageCountMode.Estimate;

                ReportDataSource reportDataSourceHeader = new ReportDataSource("ComponentDrugTestHeaderDataSet", headerDataSet.Tables[0]);
                ReportDataSource reportDataSourceContent = new ReportDataSource("ComponentDrugTestContentDataSet", contentDataSet.Tables[0]);
                this.testResultReportViewer.LocalReport.DataSources.Clear();


                this.testResultReportViewer.LocalReport.DataSources.Add(reportDataSourceHeader);
                this.testResultReportViewer.LocalReport.DataSources.Add(reportDataSourceContent);
                this.testResultReportViewer.LocalReport.SetParameters(reportParameter);
                //this.productQtTestResultReportViewer.LocalReport.Refresh();
                this.testResultReportViewer.RefreshReport();

                _savereportheadDataSet = headerDataSet;
                _testIdx = testIdx;
                _approvalDate = approvalDate;
                _savereportmode = 2;

                //SaveReport(
                //    headerDataSet.Tables[0].Rows[0]["fdaName"].ToString(),              // 제품명/원료명(식약처허가명)
                //    headerDataSet.Tables[0].Rows[0]["componentName"].ToString(),        // 원료명(SAP명)
                //    headerDataSet.Tables[0].Rows[0]["mainLotNo"].ToString(),            // 제조번호
                //    "",                                                                 // 제조일
                //    "",                                                                 // 채취장소
                //    headerDataSet.Tables[0].Rows[0]["deliveryCompanyName"].ToString(),  // 납품처명
                //    testIdx,                                                            // TESTIDX
                //    approvalDate                                                        // 승인일
                //    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*
        private string ImageToBase64()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Image signImage = Properties.Resources.cyk_sign;
                signImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        */

        private void SaveReport(
            string productName,             // 제품명/원료명(식약처허가명)
            string componentName,           // 원료명(SAP명)
            string manufactureSerialNumber, // 제조번호
            string manufactureDate,         // 제조일
            string gatherPlace,             // 채취장소
            string deliveryCompanyName,     // 납품처명
            string testIdx,                 // TESTIDX
            string approvalDate)            // 승인일
        {
            //FileStream stream = new FileStream("D://test.pdf", FileMode.Create);

            //if (Global.loginInfo.jobId != 103)
            //    return;

            //MemoryStream ms = new MemoryStream();
            SqlParameter fileData = new SqlParameter() { SqlDbType = SqlDbType.VarBinary, ParameterName = "fileData", Size = -1 };
            //fileData.Value = this.testResultReportViewerQuick.LocalReport.Render("PDF");

            using (MemoryStream ms = new MemoryStream())
            {
                C1.Win.C1Document.Export.PdfFilter pdf = new PdfFilter() { Stream = ms };
                rep.RenderToFilter(pdf);
                fileData.Value = ms.ToArray();
            }

            if (fileData.Value != null)
            {
                string query = string.Format("EXEC InsertReportItem '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', @fileData, '{8}' ",
                    _reportType,
                    productName,
                    componentName,
                    manufactureSerialNumber,
                    manufactureDate,
                    gatherPlace,
                    deliveryCompanyName,
                    testIdx,
                    approvalDate
                    );
                int retVal = DbHelper.ExecuteNonQueryWithFileData(query, fileData);
                if (retVal < 1)
                    MessageBox.Show("리포트를 저장할 수 없습니다.");
                else
                    MessageBox.Show("리포트를 저장했습니다.");
            }
            
        }

        private void CreateFinalQualityManagementReport()
        {
            /*
            if (testIdComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("테스트 항목을 선택해 주십시오.");
                testIdComboBox.Focus();
                return;
            }

            string testId = (testIdComboBox.SelectedItem as ComboBoxItem).Value.ToString();
            string query = string.Format("EXEC SelectProductQtTestReportHeader '{0}'", testId);

            DataSet headerDataSet = DbHelper.SelectQuery(query);
            if (headerDataSet == null || headerDataSet.Tables.Count == 0)
            {
                MessageBox.Show("시험정보를 가져올 수 없습니다.");
                return;
            }

            if (headerDataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("시험정보가 아직 없습니다.");
                return;
            }

            ReportParameter[] reportParameter = null;

            if (Global.loginInfo.jobId == 103)
            {
                reportParameter = new ReportParameter[4];
                reportParameter[0] = new ReportParameter("reportTitle", "최종포장(완제품) 품질관리 기록서");
                reportParameter[1] = new ReportParameter("testDate", Utils.GetString(headerDataSet.Tables[0].Rows[0]["testDate"]));
                reportParameter[2] = new ReportParameter("testerName", Utils.GetString(headerDataSet.Tables[0].Rows[0]["testerName"]));
                reportParameter[3] = new ReportParameter("sign", new Uri(System.Windows.Forms.Application.StartupPath + "\\Images\\cyk_sign.png").AbsoluteUri);
            }
            else
            {

                reportParameter = new ReportParameter[3];
                reportParameter[0] = new ReportParameter("reportTitle", "최종포장(완제품) 품질관리 기록서");
                reportParameter[1] = new ReportParameter("testDate", Utils.GetString(headerDataSet.Tables[0].Rows[0]["testDate"]));
                reportParameter[2] = new ReportParameter("testerName", Utils.GetString(headerDataSet.Tables[0].Rows[0]["testerName"]));

            }


            query = string.Format("EXEC SelectProductQtTestReportContent '{0}'", testId);

            DataSet contentDataSet = DbHelper.SelectQuery(query);
            if (contentDataSet == null || contentDataSet.Tables.Count == 0)
            {
                MessageBox.Show("시험정보를 가져올 수 없습니다.");
                return;
            }
            else
            {
                string testItemId = "";
                for (int row = 0; row < contentDataSet.Tables[0].Rows.Count; row++)
                {
                    DataRow dataRow = contentDataSet.Tables[0].Rows[row];

                    if (dataRow["testItemId"].ToString() == "3") // 질량
                    {
                        string testScore = dataRow["testScore"].ToString();
                        string[] testScoreArray = testScore.Split(Global.DB_VALUE_SEPARATOR.ToCharArray()[0]);
                        testScore = testScoreArray.Length > 1 ? string.Join("g ", Utils.SubArray(testScoreArray, 0, testScoreArray.Length > 5 ? 5 : testScoreArray.Length)) + "g" : "";
                        dataRow["testScore"] = testScore;

                        DataRow newDataRow = contentDataSet.Tables[0].NewRow();
                        newDataRow["testItemId"] = dataRow["testItemId"].ToString();
                        newDataRow["testItemName"] = "";
                        newDataRow["testStandard"] = "기준(g) : 4.22g";
                        newDataRow["testScore"] = testScoreArray.Length > 5 ? string.Join("g ", Utils.SubArray(testScoreArray, 5, testScoreArray.Length - 5)) + "g" : "";
                        newDataRow["compatibilityOx"] = "";
                        //newDataRow["testerName"] = dataRow["testerName"].ToString();
                        contentDataSet.Tables[0].Rows.InsertAt(newDataRow, row + 1);


                        DataRow newDataRow2 = contentDataSet.Tables[0].NewRow();
                        newDataRow2["testItemId"] = dataRow["testItemId"].ToString();
                        newDataRow2["testItemName"] = "";
                        newDataRow2["testStandard"] = "Average";
                        //newDataRow2["testScore"] = Utils.GetAverage(testScoreArray).ToString() + "g ";
                        newDataRow2["testScore"] = string.Format("{0,10:N2}", Utils.GetAverage(testScoreArray)) + "g ";
                        newDataRow2["compatibilityOx"] = "";
                        //newDataRow2["testerName"] = dataRow["testerName"].ToString();
                        contentDataSet.Tables[0].Rows.InsertAt(newDataRow2, row + 2);

                        DataRow newDataRow3 = contentDataSet.Tables[0].NewRow();
                        newDataRow3["testItemId"] = dataRow["testItemId"].ToString();
                        newDataRow3["testItemName"] = "";
                        newDataRow3["testStandard"] = "";
                        newDataRow3["testScore"] = string.Format("{0,10:N2}", Utils.GetAverage(testScoreArray) / 4.22) + "%";
                        newDataRow3["compatibilityOx"] = "";
                        //newDataRow3["testerName"] = dataRow["testerName"].ToString();
                        contentDataSet.Tables[0].Rows.InsertAt(newDataRow3, row + 3);

                        break;
                    }
                }
            }

            foreach (DataRow dataRow in contentDataSet.Tables[0].Rows)
            {
                if (dataRow["testItemId"].ToString() == "5") // 산 및 알칼리 P.P
                {
                    //dataRow["testItemName"] = dataRow["testItemName"].ToString().Substring(0, dataRow["testItemName"].ToString().LastIndexOf("P.P"));
                    dataRow["testItemName"] = "산 및 알칼리";
                }

                if (dataRow["testItemId"].ToString() == "6") // 산 및 알칼리 M.O
                {
                    dataRow["testItemName"] = "";
                }
            }

            query = string.Format("EXEC SelectProductQtPackingTestReportContent '{0}'", testId);

            DataSet packingContentDataSet = DbHelper.SelectQuery(query);
            if (packingContentDataSet == null || packingContentDataSet.Tables.Count == 0)
            {
                MessageBox.Show("시험정보를 가져올 수 없습니다.");
                return;
            }
            else
            {
                for (int row = 0; row < packingContentDataSet.Tables[0].Rows.Count; row++)
                {
                    DataRow dataRow = packingContentDataSet.Tables[0].Rows[row];

                    DataRow newDataRow = contentDataSet.Tables[0].NewRow();
                    newDataRow["testItemId"] = dataRow["testItemId"].ToString();
                    newDataRow["testItemName"] = dataRow["testItemName"].ToString();
                    newDataRow["testMethod"] = dataRow["testMethod"].ToString();
                    newDataRow["testScore"] = dataRow["testScore"].ToString();
                    newDataRow["compatibilityOx"] = dataRow["compatibilityOx"].ToString();
                    contentDataSet.Tables[0].Rows.InsertAt(newDataRow, row + 1);
                }
            }

            testResultReportViewer.LocalReport.EnableExternalImages = true;
            testResultReportViewer.LocalReport.ReportEmbeddedResource = "QTRS.Report.ProductQtTestResultReport.rdlc";
            testResultReportViewer.PageCountMode = PageCountMode.Actual;

            ReportDataSource reportDataSourceHeader = new ReportDataSource("ProductQtTestHeaderDataSet", headerDataSet.Tables[0]);
            ReportDataSource reportDataSourceContent = new ReportDataSource("ProductQtTestDataSet", contentDataSet.Tables[0]);
            this.testResultReportViewer.LocalReport.DataSources.Clear();


            this.testResultReportViewer.LocalReport.DataSources.Add(reportDataSourceHeader);
            this.testResultReportViewer.LocalReport.DataSources.Add(reportDataSourceContent);
            this.testResultReportViewer.LocalReport.SetParameters(reportParameter);
            //this.productQtTestResultReportViewer.LocalReport.Refresh();
            this.testResultReportViewer.RefreshReport();

            //SaveReport("최종포장(완제품) 품질관리 기록서");
            */ 
        }


        private void InitTestComboBox()
        {
            testIdComboBox.Items.Clear();
            testIdComboBox.Text = "";

            string machinenum = "";
            string testProductTex = "";

            string query = "";
            if (_reportType == (int)Global.eReportType.componentDrugTest)
            {
                //testDescLabel.Text = "(원료코드 | 입고일자 | 식약청허가명)";
                testDescLabel.Text = "(시험일자 | 원료코드 | 식약청허가명)";

                query = "EXEC SelectComponentTestListForReport '', '', ";

                machinenum = (testDescWherecheckbox.Checked && testCompanyName.Text != "미 선택") ? testCompanyName.Text : "";
            }
            else
            {
                //testDescLabel.Text = "(제품코드 | 제조번호 | 식약청명)";
                testDescLabel.Text = "(시험일자 | 제품코드 | 식약청허가명)";

                query = "EXEC SelectProductTestListForReport '', '', ";

                machinenum = (testMcComboBox.SelectedIndex != 0) ? testMcComboBox.SelectedItem.ToString() : "";
            }

            testProductTex = (testProductTextCheckbox.Checked) ? testProductTextBox.Text : "";

            if (applyPeriodCheckBox.Checked == true)
                query += string.Format("'{0}', '{1}', '{2}', '{3}', '1', '1', {4}", 
                    startDateTimeDateTimePicker.Value.ToString("yyyy-MM-dd 00:00:00"),
                    endDateTimeDateTimePicker.Value.ToString("yyyy-MM-dd 23:59:59"),
                    machinenum,
                    testProductTex,
                    disapprovalTestCheckBox.Checked == true ? 1 : 0);
            else
                query += string.Format("NULL, NULL, '{0}', '{1}', '1', '1', {2}", machinenum, testProductTex, disapprovalTestCheckBox.Checked == true ? 1 : 0);

            if (_reportType != (int)Global.eReportType.componentDrugTest)
                query += string.Format(", {0}", _reportType);


                DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("시험정보를 가져올 수 없습니다.");
                return;
            }

          

            if (dataSet.Tables[0].Rows.Count == 0)
                return;

            if (_reportType == (int)Global.eReportType.componentDrugTest)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    string idx = Utils.GetString(dataRow["idx"]);
                    string componentCode = Utils.GetString(dataRow["componentCode"]);
                    //string warehousingDate = Utils.GetString(dataRow["warehousingDate"]);
                    string testDate = Utils.GetString(dataRow["testDate"]);
                    testDate = testDate != "" ? testDate.Substring(0, 10) : "";
                    string fdaName = Utils.GetString(dataRow["fdaName"]);

                    string testDesc = string.Format("{0} | {1} | {2}", testDate, componentCode, fdaName);

                    testIdComboBox.Items.Add(new ComboBoxItem(testDesc, idx));
                }

               
            }
            else
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    string idx = Utils.GetString(dataRow["idx"]);
                    string machine = Utils.GetString(dataRow["gatherPlace"]);
                    string productCode = Utils.GetString(dataRow["productCode"]);
                    //string manufactureSerialNumber = Utils.GetString(dataRow["manufactureSerialNumber"]);
                    string testDate = Utils.GetString(dataRow["testDate"]);
                    testDate = testDate != "" ? testDate.Substring(0, 10) : "";
                    string fdaName = Utils.GetString(dataRow["fdaName"]);

                    string testDesc = string.Format("{0} | {1} | {2} | {3}", machine, testDate, productCode, fdaName);

                    testIdComboBox.Items.Add(new ComboBoxItem(testDesc, idx));
                }
            }

            if (testIdComboBox.Items.Count > 0)
                testIdComboBox.SelectedIndex = 0;
        }

        private void ComponentListCombobox(ComboBox boxs, DataSet dst)
        {

            boxs.Items.Add("미 선택");

                foreach (DataRow dataRow in dst.Tables[0].Rows)
            {
                boxs.Items.Add(Utils.GetString(dataRow["deliveryCompanyName"]));
            }
        }

        private void ResizeControl()
        {
            int paddingSize = 40;
            testResultReportViewer.Left = paddingSize;
            testResultReportViewer.Width = contentPanel.Width - (paddingSize * 2);
            testResultReportViewer.Top = 0;
            testResultReportViewer.Height = contentPanel.Height - paddingSize; 
        }

        private void contentPanel_Resize(object sender, EventArgs e)
        {
            ResizeControl();
        }

        private void applyPeriodCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            startDateTimeDateTimePicker.Enabled = applyPeriodCheckBox.Checked;
            endDateTimeDateTimePicker.Enabled = applyPeriodCheckBox.Checked;
        }

        private void searchTestButton_Click(object sender, EventArgs e)
        {
            ////this.Enabled = false;
            InitTestComboBox();
            ////this.Enabled = true; 
        }

        private void saveReportButton_Click(object sender, EventArgs e)
        {
            switch(_savereportmode)
            {
                case 0: // 품질관리 기록서
                    SaveReport(
                                _savereportheadDataSet.Tables[0].Rows[0]["fdaName"].ToString(),                  // 제품명/원료명(식약처허가명)
                                "",                                                                     // 원료명(SAP명)
                                _savereportheadDataSet.Tables[0].Rows[0]["manufactureSerialNumber"].ToString(),  // 제조번호
                                _savereportheadDataSet.Tables[0].Rows[0]["manufactureDate"].ToString(),          // 제조일
                                _savereportheadDataSet.Tables[0].Rows[0]["gatherPlace"].ToString(),              // 채취장소
                                "",                                                                     // 납품처명
                                _testIdx,                                                                // TESTIDX
                                _approvalDate                                                            // 승인일
                                );
                    break;
                case 1: // 제조관리 기록서
                    SaveReport(
                                 _savereportheadDataSet.Tables[0].Rows[0]["fdaName"].ToString(),                  // 제품명/원료명(식약처허가명)
                                 "",                                                                     // 원료명(SAP명)
                                 _savereportheadDataSet.Tables[0].Rows[0]["manufactureSerialNumber"].ToString(),  // 제조번호
                                 _savereportheadDataSet.Tables[0].Rows[0]["manufactureDate"].ToString(),          // 제조일
                                 _savereportheadDataSet.Tables[0].Rows[0]["gatherPlace"].ToString(),              // 채취장소
                                 "",                                                                     // 납품처명
                                 _testIdx,                                                                 // TESTIDX
                                 _approvalDate                                                            // 승인일
                                 );
                    break;
                case 2: // 워뇰약품 시험성적서
                    SaveReport(
                                _savereportheadDataSet.Tables[0].Rows[0]["fdaName"].ToString(),              // 제품명/원료명(식약처허가명)
                                _savereportheadDataSet.Tables[0].Rows[0]["componentName"].ToString(),        // 원료명(SAP명)
                                _savereportheadDataSet.Tables[0].Rows[0]["mainLotNo"].ToString(),            // 제조번호
                                "",                                                                 // 제조일
                                "",                                                                 // 채취장소
                                _savereportheadDataSet.Tables[0].Rows[0]["deliveryCompanyName"].ToString(),  // 납품처명
                                _testIdx,                                                            // TESTIDX
                                _approvalDate                                                        // 승인일
                                );
                    break;
            }
        }


        /*
        private void Test1()
        {
            if (productCodeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("테스트 항목을 선택해 주십시오.");
                productCodeComboBox.Focus();
                return;
            }

            string productCode = (productCodeComboBox.SelectedItem as ComboBoxItem).Value.ToString();
            string query = string.Format("EXEC SelectProductQtTestReportHeader '{0}'", productCode);

            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                MessageBox.Show("시험정보를 가져올 수 없습니다.");
                return;
            }

            if (dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("시험정보가 아직 없습니다.");
                return;
            }


        query = string.Format("EXEC SelectProductQtTestReportContent2 '{0}'", productCode);

            DataSet dataSet2 = DbHelper.SelectQuery(query);
            if (dataSet == null || dataSet2.Tables.Count == 0)
            {
                MessageBox.Show("시험정보를 가져올 수 없습니다.");
                return;
            }

            productQtTestResultReportViewer.LocalReport.ReportEmbeddedResource = "QTRS.Report.ProductQtTestResultReportFixed.rdlc";
            productQtTestResultReportViewer.PageCountMode = PageCountMode.Actual;

            ReportDataSource reportDataSourceHeader = new ReportDataSource("ProductQtTestHeaderDataSet", dataSet.Tables[0]);
            ReportDataSource reportDataSourceContent = new ReportDataSource("ProductQtTestDataSet", dataSet2.Tables[0]);
            this.productQtTestResultReportViewer.LocalReport.DataSources.Clear();


            this.productQtTestResultReportViewer.LocalReport.DataSources.Add(reportDataSourceHeader);
            this.productQtTestResultReportViewer.LocalReport.DataSources.Add(reportDataSourceContent);
            //this.productQtTestResultReportViewer.LocalReport.SetParameters(reportParameter);
            //this.productQtTestResultReportViewer.LocalReport.Refresh();
            this.productQtTestResultReportViewer.RefreshReport();
        }
        */
        private void MianLotChange_Click(object sender, EventArgs e)
        {
            LotModifyForm form = new LotModifyForm(_testIdx, _mainlotcomponentDataTable);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }
    }
}
