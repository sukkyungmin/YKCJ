using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;
using C1.Win.C1Document.Export;
using C1.Win.FlexViewer;
using C1report = C1.Win.FlexReport;


using QTRS.ProductTest;
using System.IO;

namespace QTRS.Report
{
    public partial class QuickReportForm : Form
    {

        ReportViewer testResultReportViewerQuick = new ReportViewer();
        private DataSet _savereportheadDataSet = null;
        private DataTable _mainlotcomponentDataTable = null;
        private int _savereportmode = 0; // 0 : 품질관리 기록서, 1 : 제조관리 기록서, 3 : 원료약품 시험성적서
        private string _testIdx = "";
        private string _approvalDate = "";

        C1report.C1FlexReport rep = new C1.Win.FlexReport.C1FlexReport();

        public QuickReportForm(int reportType, string testId , bool mnainLotCheck)
        {
            InitializeComponent();

            if (Global.loginInfo.jobId == 102)
                saveReportButton.Visible = true;

            if (reportType == (int)Global.eReportType.qualityManagement)
                this.Text = "품질관리 기록서";
            else if (reportType == (int)Global.eReportType.manufactureManagement)
                this.Text = "제조관리 기록서";
            else if (reportType == (int)Global.eReportType.componentDrugTest)
                this.Text = "원료약품 시험성적서";

            if (reportType == (int)Global.eReportType.componentDrugTest)
                CreateComponentDrugTestReport(testId);
            else if (reportType == (int)Global.eReportType.qualityManagement)
                CreateQualityManagementReport(testId);
            else if (reportType == (int)Global.eReportType.manufactureManagement)
                CreateManufactureManagementReport(testId, mnainLotCheck);
        }



        private void CreateQualityManagementReport(string testId)
        {
            try
            {
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
                            rep.Parameters["TestScore0302"].Value = string.Format("{0,10:N2}", Utils.GetAverage(testScoreArray)).Trim(); ;
                            rep.Parameters["TestScore0303"].Value = string.Format("{0,10:N2}", (Utils.GetAverage(testScoreArray) / Convert.ToDouble(contentDataSumSet.Tables[0].Rows[0]["logicalQuantity"])) * 100).Trim();

                            for (int i = 0; i < testScoreArray.Length; i++)
                            {
                                if(i < 9)
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
                                (temp1 == 0 ? 1 : temp1),1);

                            // 흡수량 2
                            decimal temp2 = decimal.Parse(testScoreArray[2]);
                            decimal absorbedAmount2 = Math.Round(decimal.Parse(testScoreArray[3]) /
                                (temp2 == 0 ? 1 : temp2),1);

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
                        if(dataRow["testitemName"].ToString().Replace(" ",string.Empty) == "3.질량")
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
                        if(itemnumber < 10)
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

                //this.testResultReportViewerQuick.LocalReport.EnableExternalImages = true;
                //this.testResultReportViewerQuick.LocalReport.ReportEmbeddedResource = "QTRS.Report.ProductQtTestResultReport.rdlc";
                //this.testResultReportViewerQuick.PageCountMode = PageCountMode.Actual;

                //ReportDataSource reportDataSourceHeader = new ReportDataSource("ProductQtTestHeaderDataSet", headerDataSet.Tables[0]);
                //ReportDataSource reportDataSourceContent = new ReportDataSource("ProductQtTestDataSet", contentDataSet.Tables[0]);
                //this.testResultReportViewerQuick.LocalReport.DataSources.Clear();


                rep.DataSource.Recordset = headerDataSet.Tables[0];

                QTRSFlexViewer.DocumentSource = rep;


                //this.testResultReportViewerQuick.LocalReport.DataSources.Add(reportDataSourceHeader);
                //this.testResultReportViewerQuick.LocalReport.DataSources.Add(reportDataSourceContent);
                //this.testResultReportViewerQuick.LocalReport.SetParameters(reportParameter);
                //this.testResultReportViewerQuick.RefreshReport();

                _savereportheadDataSet = headerDataSet;
                _testIdx = testId;
                _approvalDate = approvalDate;
                _savereportmode = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateManufactureManagementReport(string testId , bool mainlotcheck)
        {
            try
            {
                lotModifySaveButton.Visible = true;

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


                string approvalDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

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

                    totalStandardAmount += double.Parse(standardAmount);
                    totalUsage += double.Parse(usage);

                    //Old Query

                    //query = string.Format("EXEC SelectProductMfTestReportSubContent '{0}','{1}'", componentCode, componentdate);
                    //DataSet subContentDataSet = DbHelper.SelectQuery(query);
                    //if (subContentDataSet == null || subContentDataSet.Tables.Count == 0)
                    //{
                    //    MessageBox.Show("시험정보를 가져올 수 없습니다.");
                    //    return;
                    //}

                    query = string.Format("EXEC SelectProductMfTestReportNewSubContent '{0}','{1}'", testId, componentCode);
                    DataSet subContentDataSet = DbHelper.SelectQuery(query);

                    if (!mainlotcheck)
                    {
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
                    else
                    {
                        contentDataRow["mainLotNo"] = "N/A";
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
                //else
                //{
                //    for (int row = contentDataSet.Tables[0].Rows.Count; row < 25; row++)
                //    {
                //        contentDataSet.Tables[0].Rows.Add("", "", null, null, null, "", "", "", "", "", "");
                //    }
                //}

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

        private void CreateComponentDrugTestReport(string testId)
        {

            string query = string.Format("EXEC SelectComponentDrugTestReportHeader '{0}'", testId);

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


            query = string.Format("EXEC SelectComponentDrugTestReportContent '{0}'", testId);

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


            this.testResultReportViewerQuick.LocalReport.EnableExternalImages = true;
            this.testResultReportViewerQuick.LocalReport.ReportEmbeddedResource = "QTRS.Report.ComponentDrugTestResultReport.rdlc";
            this.testResultReportViewerQuick.PageCountMode = PageCountMode.Estimate;

            ReportDataSource reportDataSourceHeader = new ReportDataSource("ComponentDrugTestHeaderDataSet", headerDataSet.Tables[0]);
            ReportDataSource reportDataSourceContent = new ReportDataSource("ComponentDrugTestContentDataSet", contentDataSet.Tables[0]);
            this.testResultReportViewerQuick.LocalReport.DataSources.Clear();


            this.testResultReportViewerQuick.LocalReport.DataSources.Add(reportDataSourceHeader);
            this.testResultReportViewerQuick.LocalReport.DataSources.Add(reportDataSourceContent);
            this.testResultReportViewerQuick.LocalReport.SetParameters(reportParameter);
            //this.productQtTestResultReportViewer.LocalReport.Refresh();
            this.testResultReportViewerQuick.RefreshReport();

            _savereportheadDataSet = headerDataSet;
            _testIdx = testId;
            _approvalDate = approvalDate;
            _savereportmode = 3;

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
                        _savereportmode,
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

        private void ResizeControl()
        {
            //int paddingSize = 40;
            //testResultReportViewerQuick.Left = paddingSize;
            //testResultReportViewerQuick.Width = contentPanel.Width - (paddingSize * 2);
            //testResultReportViewerQuick.Top = 0;
            //testResultReportViewerQuick.Height = contentPanel.Height - paddingSize;
        }

        private void saveReportButton_Click(object sender, EventArgs e)
        {
            switch (_savereportmode)
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
                case 3: // 원료약품 시험성적서
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

        private void contentPanel_Resize(object sender, EventArgs e)
        {
            ResizeControl();
        }

        private void MianLotChange_Click(object sender, EventArgs e)
        {
            LotModifyForm form = new LotModifyForm(_testIdx, _mainlotcomponentDataTable);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }
    }
}
