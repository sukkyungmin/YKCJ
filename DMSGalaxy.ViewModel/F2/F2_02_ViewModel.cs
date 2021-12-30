using System;
using System.Data;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using ClosedXML.Excel;
using System.Threading;

using DMSGalaxy.ViewModel.Common;
using DMSGalaxy.DBConnection.F2;
using DMSGalaxy.Common.Utils;
using DMSGalaxy.Common.Helper;

namespace DMSGalaxy.ViewModel.F2
{
    public class F2_02_ViewModel : ObservableObject
    {
        private F2_02Provider F2_Provider = null;

        public F2_02_ModelProduction P { get; set; }
        private F2_02_ModelWaste W { get; set; }
        private F2_02_ModelDelay D { get; set; }
        private CommonUtil C_util = new CommonUtil();

        private LOG_Write m_LOG = new LOG_Write();


        public F2_02_ViewModel()
        {
            F2_Provider = new F2_02Provider();
            P = new F2_02_ModelProduction();
            W = new F2_02_ModelWaste();
            D = new F2_02_ModelDelay();

            St = "Production";
            MCnumber = "미 선택";
            ProductName = "ALL";
            TopCount = 99999;
            ExcelExportProgress = false;
            ExcelExportForm = false;
            ReportMode = P.OldCheckPart;
            //Persons = P.PPersons;
            Listboxpersons = P.PListboxpersons;
            Dates = string.Format("{0:yyyy-MM-dd}  to  {1:yyyy-MM-dd}", Stime, Etime);
        }


        //private ObservableCollection<Person> _persons;

        //public ObservableCollection<Person> Persons
        //{
        //    get { return _persons; }
        //    set { 
        //                if(_persons != value)
        //                {
        //                    _persons = value;
        //                    RaisePropertyChanged(() => this.Persons);
        //                }
        //            }
        //}

        private ObservableCollection<F2_02_CommonModel> _listboxpersons;

        public ObservableCollection<F2_02_CommonModel> Listboxpersons
        {
            get { return _listboxpersons; }
            set
            {
                if (_listboxpersons != value)
                {
                    _listboxpersons = value;
                    RaisePropertyChanged(() => this.Listboxpersons);
                }
            }
        }


        #region  Menu DataTime & DataTable & Grid Loader Bool

        private DateTime _Stime;

        public DateTime Stime
        {
            get
            {
                if (_Stime == DateTime.MinValue)
                    return DateTime.Now.AddDays(-1);

                return _Stime;
            }
            set
            {
                _Stime = value;
                RaisePropertyChanged(() => this.Stime);
                Dates = string.Format("{0:yyyy-MM-dd}  to  {1:yyyy-MM-dd}", Stime, Etime);
            }
        }

        private DateTime _Etime;

        public DateTime Etime
        {
            get
            {
                if (_Etime == DateTime.MinValue)
                    return DateTime.Now;

                return _Etime;
            }
            set
            {
                _Etime = value;
                RaisePropertyChanged(() => this.Etime);
                Dates = string.Format("{0:yyyy-MM-dd}  to  {1:yyyy-MM-dd}", Stime, Etime);
            }
                    
        }

        /// <summary>
        ///  Select Date
        /// </summary>
        private string _dates;

        public string Dates
        {
            get { return _dates; }
            set
            {
                if (_dates != value)
                {
                    _dates = value;
                    RaisePropertyChanged(() => this.Dates);
                }
            }
        }

        /// <summary>
        /// Search Grid Data
        /// </summary>

        private DataTable dt = new DataTable();

        public DataTable F2_dataTable
        {
            get { return dt; }
            set
            {
                if (dt != value || dt != null)
                {
                    dt = value;
                    RaisePropertyChanged(() => this.F2_dataTable);
                }
            }
        }

        /// <summary>
        /// Product List
        /// </summary>

        private DataTable dt2 = new DataTable();

        public DataTable F2_dataTable2
        {
            get { return dt2; }
            set
            {
                if (dt2 != value || dt2 != null)
                {
                    dt2 = value;
                    RaisePropertyChanged(() => this.F2_dataTable2);
                }
            }
        }
        /// <summary>
        /// Loader bool
        /// </summary>
        private bool _Show;

        public bool Show
        {
            get { return _Show; }
            set
            {
                if (_Show != value)
                {
                    _Show = value;
                    RaisePropertyChanged(() => this.Show);
                }
            }
        }

        /// <summary>
        ///  Select MC Number
        /// </summary>
        private string _mcnumber;

        public string MCnumber
        {
            get { return _mcnumber; }
            set
            {
                if (_mcnumber != value)
                {
                    _mcnumber = value;                  
                    RaisePropertyChanged(() => this.MCnumber);
                    if(_mcnumber != "미 선택")
                        DBDataSelecttoProductList(_mcnumber);
                }
            }
        }

        /// <summary>
        ///  Select Product   Name
        /// </summary>
        private string _productname;

        public string ProductName
        {
            get { return _productname; }
            set
            {
                if (_productname != value)
                {
                    _productname = value;
                    RaisePropertyChanged(() => this.ProductName);
                }
            }
        }

        /// <summary>
        ///  Select TopCount 
        /// </summary>
        private int _topcount;

        public int TopCount
        {
            get { return _topcount; }
            set
            {
                if (_topcount != value)
                {
                    _topcount = value;
                    RaisePropertyChanged(() => this.TopCount);
                }
            }
        }


        #endregion

        #region Select Mode(Production, Delay, Waste) & Grid Search Status Bool

        /// <summary>
        /// Report Mode (Production, Waste, Delay)
        /// </summary>
        private string _St;

        public string St
        {
            get {return _St;}
            set
            {
                if (_St != value)
                {
                    _St = value;
                    RaisePropertyChanged(() => this.St);
                }
            }
        }


        /// <summary>
        /// Grid Select Mode (General, Group, Code...)
        /// </summary>
        private string _ReportMode;

        public string ReportMode
        {
            get { return _ReportMode; }
            set
            {
                if (_ReportMode != value)
                {
                    _ReportMode = value;
                    RaisePropertyChanged(() => this.ReportMode);
                }
            }
        }

        /// <summary>
        /// Old Grid Select Mode (General, Group, Code...)
        /// </summary>
        private string _OldReportMode;

        public string OldReportMode
        {
            get { return _OldReportMode; }
            set
            {
                if (_OldReportMode != value)
                {
                    _OldReportMode = value;
                    RaisePropertyChanged(() => this.OldReportMode);
                }
            }
        }


        /// <summary>
        /// Grid Search Status 
        /// </summary>
        private bool _gridsearch;

        public bool GridSearch
        {
            get { return _gridsearch; }
            set
            {
                if (_gridsearch != value)
                {
                    _gridsearch = value;
                    RaisePropertyChanged(() => this.GridSearch);
                }
            }
        }

        /// <summary>
        /// Excel Export Progress Loding  Bar
        /// </summary>
        private bool _ExcelExportProgress;

        public bool ExcelExportProgress
        {
            get { return _ExcelExportProgress; }
            set
            {
                if (_ExcelExportProgress != value)
                {
                    _ExcelExportProgress = value;
                    RaisePropertyChanged(() => this.ExcelExportProgress);
                }
            }
        }

        /// <summary>
        /// Excel Export from
        /// </summary>
        private bool _ExcelExportForm;

        public bool ExcelExportForm
        {
            get { return _ExcelExportForm; }
            set
            {
                if (_ExcelExportForm != value)
                {
                    _ExcelExportForm = value;
                    RaisePropertyChanged(() => this.ExcelExportForm);
                }
            }
        }

        #endregion

        #region Report Mode Select

        private void ReportModeSelect(string Mode , bool select)
        {
            //select 1 = Report Mode Change
            //sleect 2 = Report Mode Cancel

            switch (Mode)
            {
                case "Production":
                    if (select)
                    {
                        P.Production = true;
                        W.Waste = false;
                        D.Delay = false;
                        ReportMode = P.OldCheckPart;
                        ListboxBF(P.PListboxpersons, P.PPersons, true);
                    }
                    else
                    {
                        P.OldCheckPart = ReportMode;
                        ListboxBF(P.PListboxpersons, P.PPersons, false);
                    }
                    break;

                case "Waste":
                    if (select)
                    {
                        P.Production = false;
                        W.Waste = true;
                        D.Delay = false;
                        ReportMode = W.OldCheckPart;
                        ListboxBF(W.WListboxpersons, W.WPersons, true);
                    }
                    else
                    {
                        W.OldCheckPart = ReportMode;
                        ListboxBF(W.WListboxpersons, W.WPersons, false);
                    }
                    break;

                case "Delay":
                    if (select)
                    {
                        P.Production = false;
                        W.Waste = false;
                        D.Delay = true;
                        ReportMode = D.OldCheckPart;
                        ListboxBF(D.DListboxpersons, D.DPersons, true);
                    }
                    else
                    {
                        D.OldCheckPart = ReportMode;
                        ListboxBF(D.DListboxpersons, D.DPersons, false);
                    }
                    break;
            }
        }

        
        private void ListboxBF(ObservableCollection<F2_02_CommonModel> BoxLsit, ObservableCollection<Person> orderby, bool select)
        {         
            //select 1 = Report Mode Change
            //sleect 2 = Report Mode Cancel

            if(select)
            {
                //Persons = orderby;
                Listboxpersons = BoxLsit;
            }
            else
            {
                //orderby = Persons;
                BoxLsit = Listboxpersons;
            }

        }

        void ListboxpersonsClear()
        {
            //Persons.Clear();
            Listboxpersons.Clear();
        }

        #endregion

        #region  Productin, Delay, Waste DB Select to Grid

        private void DBDataSelecttoGrid()
        {
            try
            {

                if (MCnumber == "미 선택")
                {
                    MessageBox.Show("호기를 선택해 주세요.");
                    return;
                }

                if (!C_util.IPCheck())
                {
                    MessageBox.Show("현재 네트워크 또는 SERVER PC를 확인 하십시요.");
                    return;
                }
                 
                Show = true;
                F2_dataTable = F2_Provider.GetF2_02Grid(ReportMode,Stime.ToString("yyyy-MM-dd") + " 07:00:00", Etime.ToString("yyyy-MM-dd") + " 07:00:00", SelectLimit(Listboxpersons),
                                                    SelectCode(ProductName), MCnumber.Substring(3), TopCount.ToString());

                //F2_dataTable3 = F2_dataTable.Clone();
                //F2_dataTable3.ImportRow(F2_dataTable.Rows[rowset]);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_02_ViewModel");
            }
            finally
            {
                //Login _Login = new Login();
                //_Login.UserStatusReset(UserInfo.US_ID, false);
            }
        }

        private void DBDataSelecttoProductList(string MC)
        {
            try
            {

                if (!C_util.IPCheck())
                {
                    MessageBox.Show("현재 네트워크 또는 SERVER PC를 확인 하십시요.");
                    return;
                }

                DataTable dt = new DataTable();
                dt = F2_Provider.GetF2_02ProductList(MC.Substring(3));

                DataRow totalRow = dt.NewRow();
                totalRow[0] = "ALL";

                dt.Rows.InsertAt(totalRow, 0);

                F2_dataTable2 = dt;

                ProductName = "ALL";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_02_ViewModel");
            }
            finally
            {
                //Login _Login = new Login();
                //_Login.UserStatusReset(UserInfo.US_ID, false);
            }
        }

        private string SelectLimit(ObservableCollection<F2_02_CommonModel> Limit)
        {
            string Sumlimit = "";
            string Updown = "";

            var copy = new ObservableCollection<F2_02_CommonModel>(Limit);

            foreach (var item in copy)
            {

                Updown = (item.LimitUpdown == "UP") ? " > " : " < "; 

                if (item.LimitCheck && item.LimitName != "")
                {
                    //Sumlimit += " AND " + SelectLimitNameToDBColumnName(item.LimitName) + Updown + item.LimitCount.ToString();

                    Sumlimit += (item.LimitName != "DOWNTIME(분)") ? string.Format(" AND {0}{1}{2}", SelectLimitNameToDBColumnName(item.LimitName), Updown, item.LimitCount) :
                                                                                                                                            string.Format(" AND {0}{1}{2}*60", SelectLimitNameToDBColumnName(item.LimitName), Updown, item.LimitCount);

                }
            }

            return Sumlimit;    

        }

        private string SelectCode(string Code)
        {
            string ReturnCode = "";

            ReturnCode = (Code != "" ) ?(Code != "ALL") ? Code.Substring(0, 8):"": "";

            return ReturnCode;
        }

        private string SelectLimitNameToDBColumnName(string LimitName)
        {
            string DBColumnName = "";

            switch (LimitName)
            {
                case "TOTAL COUNT":
                    DBColumnName = "A.TOTAL_COUNT";
                    break;
                case "WASTE COUNT":
                    DBColumnName = "A.WASTE_COUNT";
                    break;
                case "BOX COUNT":
                    DBColumnName = "A.BOX_COUNT";
                    break;
                case "RUNNINGTIME":
                    DBColumnName = "A.RUNNING_TIME";
                    break;
                case "DOWNTIME":
                    DBColumnName = "A.DOWN_TIME";
                    break;
                case "DOWNCOUNT":
                    DBColumnName = "A.STOP_TOTAL_COUNT";
                    break;
                case "OCCUR COUNT":
                    DBColumnName = "A.OCCUR";
                    break;
                case "DEFECT COUNT":
                    DBColumnName = "A.DEFECT";
                    break;
                case "발생횟수":
                    DBColumnName = "A.MERGE_COUNT";
                    break;
                case "DOWNTIME(분)":
                    DBColumnName = "A.DOWN_TIME";
                    break;
            }
              return DBColumnName;
        }

        #endregion

        #region Quick  Search

        private RelayCommand _CmQuickGrid;

        private void QuickGrid()
        {
            try
            {

                DBDataSelecttoGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_02_ViewModel");
            }
            finally
            {
                //Login _Login = new Login();
                //_Login.UserStatusReset(UserInfo.US_ID, false);
            }
        }

        bool CanQuickGrid()
        {
            return true;
        }

        public ICommand CmQuickGrid
        {
            get
            {
                return _CmQuickGrid ?? (this._CmQuickGrid =
                    new RelayCommand(QuickGrid, CanQuickGrid));
            }
        }

        #endregion


        #region Close Window Search

        private RelayCommand<Window> _CmWindowCloseYesGrid;

        private void WindowCloseYesGrid(Window wd)
        {
            try
            {
                GridSearch = true;
                wd.Close();
                DBDataSelecttoGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_02_ViewModel");
            }
            finally
            {
                //Login _Login = new Login();
                //_Login.UserStatusReset(UserInfo.US_ID, false);
            }
        }

        bool CanWindowCloseYesGrid(Window wd)
        {
            return true;
        }

        public ICommand CmWindowCloseYesGrid
        {
            get
            {
                return _CmWindowCloseYesGrid ?? (this._CmWindowCloseYesGrid =
                    new RelayCommand<Window>(WindowCloseYesGrid, CanWindowCloseYesGrid));
            }
        }

        #endregion

        #region Close Window

        private RelayCommand<Window> _CmWindowCloseNoGrid;

        private void WindowCloseNoGrid(Window wd)
        {
            GridSearch = false;
            wd.Close();
        }

        bool CanWindowCloseNoGrid(Window wd)
        {
            return true;
        }

        public ICommand CmWindowCloseNoGrid
        {
            get
            {
                return _CmWindowCloseNoGrid ?? (this._CmWindowCloseNoGrid =
                    new RelayCommand<Window>(WindowCloseNoGrid, CanWindowCloseNoGrid));
            }
        }

        #endregion

        #region Report Mode & Frame Close 

        public RelayCommand<StringandFrame> _CmReportModeSearch;
        public RelayCommand<string> _CmReportModeChange;

        //void ReportModeSearch(object obj)
        //{
        //    var parameter = (object[])obj;
        //    Frame Fe = (Frame)obj;

        //    if (St != (string)parameter[0])
        //    {
        //        St = (string)parameter[0];
        //        Fe.NavigationService.RemoveBackEntry();
        //    }
        //}

        private void ReportModeSearch(StringandFrame obj)
        {
            try
            {
                if (St != obj.Text)
                {
                    ReportModeSelect(St,false);
                    St = obj.Text;
                    ReportModeSelect(St,true);
                    obj.frame.Content = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_02_ViewModel");
            }
            finally
            {
                //Login _Login = new Login();
                //_Login.UserStatusReset(UserInfo.US_ID, false);
            }
        }
        
        private void ReportModeChange(string obj)
        {
            try
            {
                ReportMode = obj;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_02_ViewModel");
            }
            finally
            {
                //Login _Login = new Login();
                //_Login.UserStatusReset(UserInfo.US_ID, false);
            }
        }

        bool CanReportModeSearch(StringandFrame obj)
        {
            return  (obj != null) ? (obj.Text != St) ? true : false : false;
        }

        bool CanReportModeChange(string obj)
        {
            return true;
        }

        public ICommand CmReportModeSearch
        {
            get
            {
                return _CmReportModeSearch ?? (this._CmReportModeSearch =
                    new RelayCommand<StringandFrame>(ReportModeSearch, CanReportModeSearch));
            }
        }

        public ICommand CmReportModeChange
        {
            get
            {
                return _CmReportModeChange ?? (this._CmReportModeChange =
                    new RelayCommand<string>(ReportModeChange, CanReportModeChange));
            }
        }

        #endregion

        #region Report Export Excel

        public RelayCommand _CmExportExcel;

        private void ExportExcelThread()
        {
                if (ExcelExportForm)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ExportKYExcel));
                }
                else
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ExportExcel));
                }
        }

        private void ExportKYExcel(object oArgument)
        { 
            try
            {
                if (F2_dataTable == null || F2_dataTable.Rows.Count <= 0 )
                {
                    System.Windows.MessageBox.Show(@"Data Table에 데이터가 없습니다.", "출력 확인", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }

                if (System.Windows.MessageBox.Show(@"출력을 하시겠습니까? (저장경로 - C:\DMSG_REPORT_EXCELFILE\" + ReportMode + ")" +  '\r' + '\n' + 
                                                                                                            "(※ 특정 양식을 사용하므로 생성까지 시간이 걸립니다. 생성 및 팝업되기 전까지 다른 작업을 하십시요.)", "출력 확인", 
                                                                                                            MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.No)
                {
                    return;
                }

                ExcelExportProgress = true;

                //AppDomain.CurrentDomain.BaseDirectory.ToString() + @"";
                //XLWorkbook workbook = new XLWorkbook();
                //var worksheet = workbook.Worksheets.Add(ReportMode);


                var workbook = new XLWorkbook(string.Format(@"{0}YKCJExcelFile\{1}.xlsx", AppDomain.CurrentDomain.BaseDirectory, ReportMode));

                var worksheet = workbook.Worksheet(1);

                //ClosedXML.Excel.IXLRange exlRange;
                //exlRange = worksheet.Range(1, 1, 1, 7);
                //exlRange.Merge();
                //exlRange.Row(1).Style.Font.FontSize = 16;

                //exlRange = worksheet.Range(2, 1, 2, 7);
                //exlRange.Merge();
                //exlRange.Row(1).Style.Font.FontSize = 13;
                //exlRange.Row(1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                worksheet.Cell("A5").Value = ReportMode;

                switch (ReportMode)
                {
                    case "ProductionDate":
                                        worksheet.Cell("M5").Value = MCnumber;                       
                                        worksheet.Cell("O5").Value = Stime.ToString();
                                        worksheet.Cell("P5").Value = Etime.ToString();
                        break;
                    case "ProductionShift":
                                        worksheet.Cell("L5").Value = MCnumber;                       
                                        worksheet.Cell("N5").Value = Stime.ToString();
                                        worksheet.Cell("O5").Value = Etime.ToString();
                        break;
                    case "ChangeOver":
                                        worksheet.Cell("G5").Value = MCnumber;                       
                                        worksheet.Cell("I5").Value = Stime.ToString();
                                        worksheet.Cell("J5").Value = Etime.ToString();
                        break;
                    case "WasteDate":
                                        worksheet.Cell("H5").Value = MCnumber;                       
                                        worksheet.Cell("J5").Value = Stime.ToString();
                                        worksheet.Cell("K5").Value = Etime.ToString();
                        break;
                    case "WasteGroup":
                                        worksheet.Cell("D5").Value = MCnumber;                       
                                        worksheet.Cell("F5").Value = Stime.ToString();
                                        worksheet.Cell("G5").Value = Etime.ToString();
                        break;
                    case "WasteCode":
                                        worksheet.Cell("C5").Value = MCnumber;                       
                                        worksheet.Cell("E5").Value = Stime.ToString();
                                        worksheet.Cell("F5").Value = Etime.ToString();
                        break;
                    case "DelayDate":
                                        worksheet.Cell("H5").Value = MCnumber;                       
                                        worksheet.Cell("J5").Value = Stime.ToString();
                                        worksheet.Cell("K5").Value = Etime.ToString();
                        break;
                    case "DelayGroup":
                                        worksheet.Cell("C5").Value = MCnumber;                       
                                        worksheet.Cell("E5").Value = Stime.ToString();
                                        worksheet.Cell("F5").Value = Etime.ToString();
                        break;
                    case "DelayCode":
                                        worksheet.Cell("B5").Value = MCnumber;                       
                                        worksheet.Cell("D5").Value = Stime.ToString();
                                        worksheet.Cell("E5").Value = Etime.ToString();
                        break;
                }

                worksheet.Cell("A7").InsertTable(F2_dataTable, false);

                /* 다른방안을 검토해야됨
                for(int i = 0 ; i < F2_dataTable.Rows.Count; i++)
                {
                    //worksheet.Cell("A" + (i + 8).ToString()).Value = F2_dataTable.Rows[i]["DAYS"].ToString(); //Days
                    //worksheet.Cell("B" + (i + 8).ToString()).Value = F2_dataTable.Rows[i]["TIME"].ToString(); //Time
                    //worksheet.Cell("C" + (i + 8).ToString()).Value = F2_dataTable.Rows[i]["CODE"].ToString(); //Code
                    //worksheet.Cell("D" + (i + 8).ToString()).Value = F2_dataTable.Rows[i]["NAME"].ToString(); //Name
                    //worksheet.Cell("E" + (i + 8).ToString()).Value = F2_dataTable.Rows[i]["BOX"].ToString(); //Box
                    //worksheet.Cell("F" + (i + 8).ToString()).Value = F2_dataTable.Rows[i]["TOTAL"].ToString(); //Total
                    //worksheet.Cell("G" + (i + 8).ToString()).Value = F2_dataTable.Rows[i]["WASTE"].ToString(); //Waste
                    //worksheet.Cell("H" + (i + 8).ToString()).Value = F2_dataTable.Rows[i]["RUNNINGTIME"].ToString(); //Runningtime
                    //worksheet.Cell("I" + (i + 8).ToString()).Value = F2_dataTable.Rows[i]["DOWNTIME"].ToString(); //Downtime
                    //worksheet.Cell("J" + (i + 8).ToString()).Value = F2_dataTable.Rows[i]["STOPCOUNT"].ToString(); //StopCount
                    //worksheet.Cell("K" + (i + 8).ToString()).Value = F2_dataTable.Rows[i]["TYPE"].ToString(); //Type
                    //worksheet.Cell("L" + (i + 8).ToString()).Value = F2_dataTable.Rows[i]["PRODUCTIVITY"].ToString(); //Productivity
                    //worksheet.Cell("M" + (i + 8).ToString()).Value = F2_dataTable.Rows[i]["WASTE%"].ToString(); //Waste%
                    //worksheet.Cell("N" + (i + 8).ToString()).Value = F2_dataTable.Rows[i]["WASTEBOX%"].ToString(); //WasteBox%
                    //worksheet.Cell("O" + (i + 8).ToString()).Value = F2_dataTable.Rows[i]["OEE2"].ToString(); //OEE2
                    //worksheet.Cell("P" + (i + 8).ToString()).Value = F2_dataTable.Rows[i]["DELAY%"].ToString(); //Delay%
                    //worksheet.ActiveCell = 
                }
                 */

                //worksheet.Cell("A7").

                //worksheet.Cell("A7").insert

                //worksheet.Row(8).Delete();


                //worksheet.Columns(1, 1).Width = 50;
                //worksheet.Columns(2, 2).Width = 50;
                //worksheet.Columns(3, 3).Width = 15;


                string currentTime = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");

                workbook.SaveAs(string.Format(@"C:\DMSG_REPORT_EXCELFILE\{0}\{1}.xlsx", ReportMode, currentTime));

                string path = @"C:\DMSG_REPORT_EXCELFILE\" + ReportMode;
                System.Diagnostics.Process.Start(string.Format(@"C:\DMSG_REPORT_EXCELFILE\{0}\{1}.xlsx", ReportMode, currentTime), path);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_02_ViewModel");
            }
            finally
            {
                //Login _Login = new Login();
                //_Login.UserStatusReset(UserInfo.US_ID, false);
                ExcelExportProgress = false;
            }
        }

        private void ExportExcel(object oArgument)
        {
            try
            {
                if (F2_dataTable == null || F2_dataTable.Rows.Count <= 0)
                {
                    System.Windows.MessageBox.Show(@"Data Table에 데이터가 없습니다.", "출력 확인", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }

                if (System.Windows.MessageBox.Show(string.Format(@"출력을 하시겠습니까? (저장경로 - C:\DMSG_REPORT_EXCELFILE\{0})", ReportMode), "출력 확인",
                                                                                                            MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.No)
                {
                    return;
                }


                XLWorkbook workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add(ReportMode);

                ClosedXML.Excel.IXLRange exlRange = worksheet.Range(1, 1, 1, 7);
                exlRange.Merge();
                exlRange.Row(1).Style.Font.FontSize = 16;

                exlRange = worksheet.Range(2, 1, 2, 7);
                exlRange.Merge();
                exlRange.Row(1).Style.Font.FontSize = 13;
                exlRange.Row(1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                worksheet.Cell("A1").Value = ReportMode;
                worksheet.Cell("A2").Value = "TITLE";


                worksheet.Cell("A3").InsertTable(F2_dataTable, ReportMode, true);
                //worksheet.Columns(1, 1).Width = 50;
                //worksheet.Columns(2, 2).Width = 50;
                //worksheet.Columns(3, 3).Width = 15;


                string currentTime = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");

                workbook.SaveAs(string.Format(@"C:\DMSG_REPORT_EXCELFILE\{0}\{1}.xlsx", ReportMode, currentTime));

                string path = @"C:\DMSG_REPORT_EXCELFILE\" + ReportMode;
                System.Diagnostics.Process.Start(string.Format(@"C:\DMSG_REPORT_EXCELFILE\{0}\{1}.xlsx", ReportMode, currentTime), path);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_02_ViewModel");
            }
            finally
            {
                //Login _Login = new Login();
                //_Login.UserStatusReset(UserInfo.US_ID, false);
            }
        }

      
        bool CanExportExcel()
        {
            if (!ExcelExportProgress)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ICommand CmExportExcel
        {
            get
            {
                return _CmExportExcel ?? (this._CmExportExcel =
                     new RelayCommand(ExportExcelThread, CanExportExcel));
            }
        }

        #endregion


        #region Report Export PDF

        public RelayCommand _CmExportPDF;

        private  DataTable CopyTable(DataTable originalTable)
        {

            DataTable newTable;
            newTable = originalTable.Copy();

            return newTable;

        }

        private void ExportPDF()
        {
            try
            {
                if (F2_dataTable == null || F2_dataTable.Rows.Count <= 0)
                {
                    System.Windows.MessageBox.Show(@"Data Table에 데이터가 없습니다.", "출력 확인", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }

                if (System.Windows.MessageBox.Show(@"출력을 하시겠습니까? (저장경로 - C:\DMSG_REPORT_PDFFILE\" + ReportMode + ")", "출력 확인",
                                                                                                            MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.No)
                {
                    return;
                }

                string BatangFont = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\..\Fonts\batang.ttc";
                string GulimFont = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\..\Fonts\gulim.ttc";
                FontFactory.Register(BatangFont); FontFactory.Register(GulimFont);
                iTextSharp.text.Font HeaderFont = FontFactory.GetFont("굴림체", BaseFont.IDENTITY_H, 8);
                iTextSharp.text.Font TitleFont = FontFactory.GetFont("굴림체", BaseFont.IDENTITY_H, 9);
                iTextSharp.text.Font DataFont = FontFactory.GetFont("굴림체", BaseFont.IDENTITY_H, 7);

                DataTable dtData = null;

                dtData = CopyTable(F2_dataTable);


                //Creating iTextSharp Table from the DataTable data

                PdfPTable pdfTable = new PdfPTable(dtData.Columns.Count);

                pdfTable.DefaultCell.Padding = 1;

                pdfTable.WidthPercentage = 100;

                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                //pdfTable.DefaultCell.BorderWidth = 1;

                //Adding Header row

                foreach (DataColumn column in dtData.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName, HeaderFont));

                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);

                    pdfTable.AddCell(cell);
                }


                //Adding DataRow

                foreach (DataRow row in dtData.Rows)
                {
                    if (dtData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtData.Columns.Count; i++)
                        {
                            if (dtData.Columns[i].ColumnName == "DateStamp")
                            {
                                pdfTable.AddCell(new Phrase(row[i].ToString().Substring(0, 10), DataFont));
                            }
                            else
                            {
                                pdfTable.AddCell(new Phrase(row[i].ToString(), DataFont));
                            }
                        }
                    }
                }

                //Exporting to PDF

                string folderPath = "C:\\DMSG_REPORT_PDFFILE\\" + ReportMode+ "\\"; 

                if (!Directory.Exists(folderPath))
                {

                    Directory.CreateDirectory(folderPath);

                }

                string currentTime = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                using (FileStream stream = new FileStream(folderPath +  currentTime + ".pdf", FileMode.Create))
                {

                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

                    int[] intTblWidth = new int[dtData.Columns.Count];

                    for (int i = 0; i < dtData.Columns.Count; i++)
                    {
                        switch (dtData.Columns[i].ColumnName)
                        {
                            case "제품명":
                                intTblWidth[i] = 100;
                                break;
                            default:
                                intTblWidth[i] = 40;
                                break;
                        }
                    }

                    pdfTable.SetWidths(intTblWidth);

                    PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();

                    Paragraph para = new Paragraph("___" + "\n", TitleFont);
                    para.SpacingAfter = 10;
                    pdfDoc.Add(para);

                    pdfDoc.Add(pdfTable);

                    pdfDoc.Close();

                    stream.Close();

                }

                string path = @"C:\DMSG_REPORT_PDFFILE\" + ReportMode;
                System.Diagnostics.Process.Start(string.Format(@"{0}\{1}.pdf", folderPath, currentTime), path);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_02_ViewModel");
            }
            finally
            {
                //Login _Login = new Login();
                //_Login.UserStatusReset(UserInfo.US_ID, false);
            }
        }

        bool CanExportPDF()
        {
            return true;
        }

        public ICommand CmExportPDF
        {
            get
            {
                return _CmExportPDF ?? (this._CmExportPDF =
                     new RelayCommand(ExportPDF, CanExportPDF));
            }
        }

        #endregion
    }
}
