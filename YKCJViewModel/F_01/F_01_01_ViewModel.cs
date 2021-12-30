using C1.WPF.RichTextBox.Documents;
using Common.Helper;
using Common.Infos;
using Common.Utils;
using DBConnection.F_01;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using YKCJViewModel.Common;
using Fom = System.Windows.Forms;

namespace YKCJViewModel.F_01
{
    public class F_01_01_ViewModel : ObservableObject
    {
        private F_01_01_Model Model = new F_01_01_Model();
        private F_01_01_Provider F_01_Provider = new F_01_01_Provider();
        private LOG_Write m_LOG = new LOG_Write();
        private readonly CommonUtil Util = new CommonUtil();

        //private readonly string Directorypath = "\\\\HGFA-NAS2\\zZ.Temp\\석경민과장\\FileTest";
        private readonly string Directorypath = "Q:\\";
        private bool WorklistLock = true;
        private string LastSaveFilePath = "";

        public F_01_01_ViewModel()
        {
            SetinitializeDB();
        }

        private void SetinitializeDB()
        {
            try
            {

                SetComboList();

                string[] shiftarray = new string[5];
                shiftarray[0] = "없음";
                shiftarray[1] = "A";
                shiftarray[2] = "B";
                shiftarray[3] = "C";
                shiftarray[4] = "D";
                DataTable dt = new DataTable();
                dt.Columns.Add("Name");
                for (int i = 0; i < shiftarray.Length; i++)
                {
                    DataRow drow;
                    drow = dt.NewRow();
                    drow[0] = shiftarray[i];
                    dt.Rows.InsertAt(drow, i);
                }

                ShiftList = dt;

                Username = UserInfo.US_NM;
                LoginUses = UserInfo.US_NM;

                Title = "";
                Detail = "";
                Detailrtf = "";
                Filepath = "";
                Savefilepath = "";

                Searchdaily = true;
                Downloadlock = true;
                Savelock = true;

                ReportSavemode = true;

                //Image = Visibility.Hidden;
                Icons = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F1_01_ViewModel (SetinitializeDB)");
            }
        }

        private void SetDataClear()
        {
            Title = "";
            Detail = "";
            Detailrtf = "";
            Detailhtml = "";
            if (ReportSavemode)
            {
                Username = UserInfo.US_NM;
            }
            Selectmachine = null;
            Selectclass = null;
            Selectpart1 = null;
            Selectshift = null;
            Selectpart2 = null;
            Eventdatetime = DateTime.Now;
            Checktime = 0;
            Delaytime = 0;
            Filepath = "";
            Savefilepath = "";
            Savefileprogress = "";
            Downloadfilepath = "";
            Savelock = true;
            Downloadlock = true;


            WorkListTotalcount = 0;
            WorkListcurrentidx = 0;
            Savefilesize = 0;
            Savefiletotalsize = 0;
            ReportNxPvBtnVbMode = 0;

            Savefilesizedisplay = "";
            Savefilenamedisplay = "";
            Savefileformdisplay = "";
            Usershiftname = "";
            Icons = Visibility.Hidden;
        }

        private async void SetComboList()
        {
            try
            {
                await Task.Run(() =>
                {
                    DataSet dataSet = F_01_Provider.GetComboboxList();

                    Machinelist = dataSet.Tables[0];

                    ClassList = dataSet.Tables[1];

                    Modulepart1list = dataSet.Tables[2];

                    UsernameList = dataSet.Tables[3];
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F1_01_ViewModel (SetComboList)");
            }
        }

        public DateTime Eventdatetime
        {
            get
            {
                if (Model._Reportitems._Eventdatetime == DateTime.MinValue)
                    return DateTime.Now;

                return Model._Reportitems._Eventdatetime;
            }
            set
            {
                if (Model._Reportitems._Eventdatetime != value)
                {
                    Model._Reportitems._Eventdatetime = value;
                    RaisePropertyChanged(() => this.Eventdatetime);
                }
            }
        }

        public string Eventdatetimestring
        {
            get { return Model._Reportitems._Eventdatetimestring; }
            set
            {
                if (Model._Reportitems._Eventdatetimestring != value)
                {
                    Model._Reportitems._Eventdatetimestring = value;
                    RaisePropertyChanged(() => this.Eventdatetimestring);
                }
            }
        }

        public string LoginUses
        {
            get { return Model._LoginUses; }
            set
            {
                if (Model._LoginUses != value)
                {
                    Model._LoginUses = value;
                    RaisePropertyChanged(() => this.LoginUses);
                }
            }
        }

        public bool Searchdaily
        {
            get { return Model._Searchdaily; }
            set
            {
                if (Model._Searchdaily != value)
                {
                    Model._Searchdaily = value;
                    SearchDate();
                    RaisePropertyChanged(() => this.Searchdaily);
                }
            }
        }

        public bool Searchweekly
        {
            get { return Model._Searchweekly; }
            set
            {
                if (Model._Searchweekly != value)
                {
                    Model._Searchweekly = value;
                    SearchDate();
                    RaisePropertyChanged(() => this.Searchweekly);
                }
            }
        }

        public bool Searchmonthly
        {
            get { return Model._Searchmonthly; }
            set
            {
                if (Model._Searchmonthly != value)
                {
                    Model._Searchmonthly = value;
                    SearchDate();
                    RaisePropertyChanged(() => this.Searchmonthly);
                }
            }
        }

        public string Searchdatestring
        {
            get { return Model._Searchdatestring; }
            set
            {
                if (Model._Searchdatestring != value)
                {
                    Model._Searchdatestring = value;
                    RaisePropertyChanged(() => this.Searchdatestring);
                }
            }
        }

        public string Username
        {
            get { return Model._Reportitems._Username; }
            set
            {
                if (Model._Reportitems._Username != value)
                {
                    Model._Reportitems._Username = value;
                    RaisePropertyChanged(() => this.Username);
                }
            }
        }

        public string Usershiftname
        {
            get { return Model._Reportitems._Usershiftname; }
            set
            {
                if (Model._Reportitems._Usershiftname != value)
                {
                    Model._Reportitems._Usershiftname = value;
                    RaisePropertyChanged(() => this.Usershiftname);
                }
            }
        }

        public string Title
        {
            get { return Model._Reportitems._Title; }
            set
            {
                if (Model._Reportitems._Title != value)
                {
                    Model._Reportitems._Title = value;
                    RaisePropertyChanged(() => this.Title);
                }
            }
        }

        public string Detail
        {
            get { return Model._Reportitems._Detail; }
            set
            {
                if (Model._Reportitems._Detail != value)
                {
                    Model._Reportitems._Detail = value;
                    RaisePropertyChanged(() => this.Detail);
                }
            }
        }

        public string Detailhtml
        {
            get { return Model._Reportitems._Detailhtml; }
            set
            {
                if (Model._Reportitems._Detailhtml != value)
                {
                    Model._Reportitems._Detailhtml = value;
                    RaisePropertyChanged(() => this.Detailhtml);
                }
            }
        }

        public string Detailrtf
        {
            get { return Model._Reportitems._Detailrtf; }
            set
            {
                if (Model._Reportitems._Detailrtf != value)
                {
                    Model._Reportitems._Detailrtf = value;
                    RaisePropertyChanged(() => this.Detailrtf);
                }
            }
        }

        public string Selectmachine
        {
            get { return Model._Reportitems._Selectmachine; }
            set
            {
                if (Model._Reportitems._Selectmachine != value)
                {
                    Model._Reportitems._Selectmachine = value;
                    RaisePropertyChanged(() => this.Selectmachine);
                }
            }
        }

        public string Selectpart1
        {
            get { return Model._Reportitems._Selectpart1; }
            set
            {
                if (Model._Reportitems._Selectpart1 != value)
                {
                    Model._Reportitems._Selectpart1 = value;
                    Modulepart2list = SelectPart2List(value);
                    RaisePropertyChanged(() => this.Selectpart1);

                }
            }
        }

        public string Selectpart2
        {
            get { return Model._Reportitems._Selectpart2; }
            set
            {
                if (Model._Reportitems._Selectpart2 != value)
                {
                    Model._Reportitems._Selectpart2 = value;
                    RaisePropertyChanged(() => this.Selectpart2);
                }
            }
        }

        public string Selectclass
        {
            get { return Model._Reportitems._Selectclass; }
            set
            {
                if (Model._Reportitems._Selectclass != value)
                {
                    Model._Reportitems._Selectclass = value;
                    RaisePropertyChanged(() => this.Selectclass);
                }
            }
        }
            
        public string Selectshift
        {
            get { return Model._Reportitems._Selectshift; }
            set
            {
                if (Model._Reportitems._Selectshift != value)
                {
                    Model._Reportitems._Selectshift = value;
                    RaisePropertyChanged(() => this.Selectshift);
                }
            }
        }

        public string Selectusername
        {
            get { return Model._Reportitems._Selectusername; }
            set
            {
                if (Model._Reportitems._Selectusername != value)
                {
                    Model._Reportitems._Selectusername = value;
                    RaisePropertyChanged(() => this.Selectusername);
                }
            }
        }

        public string Filepath
        {
            get { return Model._Reportitems._Filepath; }
            set
            {
                if (Model._Reportitems._Filepath != value)
                {
                    Model._Reportitems._Filepath = value;
                    RaisePropertyChanged(() => this.Filepath);
                }
            }
        }

        public int Workidx
        {
            get { return Model._Reportitems._Workidx; }
            set
            {
                if (Model._Reportitems._Workidx != value)
                {
                    Model._Reportitems._Workidx = value;
                    RaisePropertyChanged(() => this.Workidx);
                }
            }
        }

        public int Checktime
        {
            get { return Model._Reportitems._Checktime; }
            set
            {
                if (Model._Reportitems._Checktime != value)
                {
                    Model._Reportitems._Checktime = value;
                    RaisePropertyChanged(() => this.Checktime);
                }
            }
        }

        public int Delaytime
        {
            get { return Model._Reportitems._Delaytime; }
            set
            {
                if (Model._Reportitems._Delaytime != value)
                {
                    Model._Reportitems._Delaytime = value;
                    RaisePropertyChanged(() => this.Delaytime);
                }
            }
        }


        public long Savefiletotalsize
        {
            get { return Model._Reportitems._Savefiletotalsize; }
            set
            {
                if (Model._Reportitems._Savefiletotalsize != value)
                {
                    Model._Reportitems._Savefiletotalsize = value;
                    RaisePropertyChanged(() => this.Savefiletotalsize);
                }
            }
        }

        public long Savefilesize
        {
            get { return Model._Reportitems._Savefilesize; }
            set
            {
                if (Model._Reportitems._Savefilesize != value)
                {
                    Model._Reportitems._Savefilesize = value;
                    RaisePropertyChanged(() => this.Savefilesize);
                }
            }
        }

        public string Savefileprogress
        {
            get { return Model._Reportitems._Savefileprogress; }
            set
            {
                if (Model._Reportitems._Savefileprogress != value)
                {
                    Model._Reportitems._Savefileprogress = value;
                    RaisePropertyChanged(() => this.Savefileprogress);
                }
            }
        }

        public string Savefilepath
        {
            get { return Model._Reportitems._Savefilepath; }
            set
            {
                if (Model._Reportitems._Savefilepath != value)
                {
                    Model._Reportitems._Savefilepath = value;
                    RaisePropertyChanged(() => this.Savefilepath);
                }
            }
        }

        public string Savefilenamedisplay
        {
            get { return Model._Reportitems._Savefilenamedisplay; }
            set
            {
                if (Model._Reportitems._Savefilenamedisplay != value)
                {
                    Model._Reportitems._Savefilenamedisplay = value;
                    RaisePropertyChanged(() => this.Savefilenamedisplay);
                }
            }
        }

        public string Savefilesizedisplay
        {
            get { return Model._Reportitems._Savefilesizedisplay; }
            set
            {
                if (Model._Reportitems._Savefilesizedisplay != value)
                {
                    Model._Reportitems._Savefilesizedisplay = value;
                    RaisePropertyChanged(() => this.Savefilesizedisplay);
                }
            }
        }

        public string Savefileformdisplay
        {
            get { return Model._Reportitems._Savefileformdisplay; }
            set
            {
                if (Model._Reportitems._Savefileformdisplay != value)
                {
                    Model._Reportitems._Savefileformdisplay = value;
                    RaisePropertyChanged(() => this.Savefileformdisplay);
                }
            }
        }

        public string Downloadfilepath
        {
            get { return Model._Reportitems._Downloadfilepath; }
            set
            {
                if (Model._Reportitems._Downloadfilepath != value)
                {
                    Model._Reportitems._Downloadfilepath = value;
                    RaisePropertyChanged(() => this.Downloadfilepath);
                }
            }
        }

        public bool Downloadlock
        {
            get { return Model._Reportitems._Downloadlock; }
            set
            {
                if (Model._Reportitems._Downloadlock != value)
                {
                    Model._Reportitems._Downloadlock = value;
                    RaisePropertyChanged(() => this.Downloadlock);
                }
            }
        }


        public bool Modifymode
        {
            get { return Model._Reportitems._Modifymode; }
            set
            {
                if (Model._Reportitems._Modifymode != value)
                {
                    Model._Reportitems._Modifymode = value;
                    RaisePropertyChanged(() => this.Modifymode);
                }
            }
        }

        public bool Savelock
        {
            get { return Model._Reportitems._Savelock; }
            set
            {
                if (Model._Reportitems._Savelock != value)
                {
                    Model._Reportitems._Savelock = value;
                    RaisePropertyChanged(() => this.Savelock);
                }
            }
        }

        //public Visibility Image
        //{
        //    get { return Model._Reportitems._Image; }
        //    set
        //    {
        //        if (Model._Reportitems._Image != value)
        //        {
        //            Model._Reportitems._Image = value;
        //            RaisePropertyChanged(() => this.Image);
        //        }
        //    }
        //}

        public Visibility Icons
        {
            get { return Model._Reportitems._Icon; }
            set
            {
                if (Model._Reportitems._Icon != value)
                {
                    Model._Reportitems._Icon = value;
                    RaisePropertyChanged(() => this.Icons);
                }
            }
        }

        public DateTime Searchdate
        {
            get
            {
                if (Model._Searchdate == DateTime.MinValue)
                    return DateTime.Now;

                return Model._Searchdate;
            }
            set
            {
                if (Model._Searchdate != value)
                {
                    Model._Searchdate = value;
                    SearchDate();
                    RaisePropertyChanged(() => this.Searchdate);
                }
            }
        }

        public DataTable Machinelist
        {
            get { return Model._Machinelist; }
            set
            {
                if (Model._Machinelist != value)
                {
                    Model._Machinelist = value;
                    RaisePropertyChanged(() => this.Machinelist);
                }
            }
        }

        public DataTable Modulepart1list
        {
            get { return Model._Modulepart1list; }
            set
            {
                if (Model._Modulepart1list != value)
                {
                    Model._Modulepart1list = value;
                    RaisePropertyChanged(() => this.Modulepart1list);
                }
            }
        }

        public DataTable Modulepart2list
        {
            get { return Model._Modulepart2list; }
            set
            {
                if (Model._Modulepart2list != value)
                {
                    Model._Modulepart2list = value;
                    RaisePropertyChanged(() => this.Modulepart2list);
                }
            }
        }

        public DataTable ClassList
        {
            get { return Model._ClassList; }
            set
            {
                if (Model._ClassList != value)
                {
                    Model._ClassList = value;
                    RaisePropertyChanged(() => this.ClassList);
                }
            }
        }

        public DataTable ShiftList
        {
            get { return Model._ShiftList; }
            set
            {
                if (Model._ShiftList != value)
                {
                    Model._ShiftList = value;
                    RaisePropertyChanged(() => this.ShiftList);
                }
            }
        }

        public DataTable UsernameList
        {
            get { return Model._UsernameList; }
            set
            {
                if (Model._UsernameList != value)
                {
                    Model._UsernameList = value;
                    RaisePropertyChanged(() => this.UsernameList);
                }
            }
        }

        public DataTable WorkList
        {
            get { return Model._WorkList; }
            set
            {
                if (Model._WorkList != value)
                {
                    Model._WorkList = value;
                    RaisePropertyChanged(() => this.WorkList);
                }
            }
        }

        public DataTable WorkListidx
        {
            get { return Model._WorkListidx; }
            set
            {
                if (Model._WorkListidx != value)
                {
                    Model._WorkListidx = value;
                    RaisePropertyChanged(() => this.WorkListidx);
                }
            }
        }

        public DataTable WorkListtimesum
        {
            get { return Model._WorkListtimesum; }
            set
            {
                if (Model._WorkListtimesum != value)
                {
                    Model._WorkListtimesum = value;
                    RaisePropertyChanged(() => this.WorkListtimesum);
                }
            }
        }

        public int WorkListTotalcount
        {
            get { return Model._WorkListTotalcount; }
            set
            {
                if (Model._WorkListTotalcount != value)
                {
                    Model._WorkListTotalcount = value;
                    RaisePropertyChanged(() => this.WorkListTotalcount);
                }
            }
        }

        public int WorkListcurrentidx
        {
            get { return Model._WorkListcurrentidx; }
            set
            {
                if (Model._WorkListcurrentidx != value)
                {
                    Model._WorkListcurrentidx = value;

                    RaisePropertyChanged(() => this.WorkListcurrentidx);
                }
            }
        }

        public int ReportNxPvBtnVbMode
        {
            get { return Model._ReportNxPvBtnVbMode; }
            set
            {
                if (Model._ReportNxPvBtnVbMode != value)
                {
                    Model._ReportNxPvBtnVbMode = value;
                    RaisePropertyChanged(() => this.ReportNxPvBtnVbMode);
                }
            }
        }

        public bool ReportSavemode
        {
            get { return Model._ReportSavemode; }
            set
            {
                if (Model._ReportSavemode != value)
                {
                    Model._ReportSavemode = value;
                    RaisePropertyChanged(() => this.ReportSavemode);
                }
            }
        }

        public bool Reportloadprogress
        {
            get { return Model._Reportloadprogress; }
            set
            {
                if (Model._Reportloadprogress != value)
                {
                    Model._Reportloadprogress = value;
                    RaisePropertyChanged(() => this.Reportloadprogress);
                }
            }
        }


        #region ICommand List

        #region GetWorkList

        private RelayCommand _CmdGetWorkList;

        public ICommand CmdGetWorkList => _CmdGetWorkList ?? (this._CmdGetWorkList = new RelayCommand(GetWorkList));

        private async void GetWorkList()
        {
            try
            {
                DataSet dset = await GetWorkListIdxAsycn();
                WorkListidx = dset.Tables[0];
                WorkListtimesum = dset.Tables[1];

                if (WorkListidx.Rows.Count > 0)
                {
                    CommonUtil.MessageAlert("I0100", WorkListidx.Rows.Count.ToString());
                    WorkListcurrentidx = 1;
                    WorkListTotalcount = WorkListidx.Rows.Count;

                    ReportNxPvBtnVbMode = (WorkListTotalcount == 1) ? 0 : (WorkListcurrentidx == 1) ? 1 : (WorkListcurrentidx == WorkListTotalcount) ? 3 : 2;

                    //WorkList = await GetWorkListAsycn(Convert.ToInt32(WorkListidx.Rows[WorkListcurrentidx - 1]["WorkListIdx"].ToString()));

                    WorkList = GetWorkListAsycn(Convert.ToInt32(WorkListidx.Rows[WorkListcurrentidx - 1]["WorkListIdx"].ToString()));

                    Savefileprogress = "";
                    //await SetWorkListModifyPopup(WorkList);

                    //await Task.Run(() =>
                    //{
                    //    SetWorkListModifyPopup(WorkList);
                    //});

                    Reportloadprogress = true;
                    SetWorkListModifyPopup(WorkList);
                }
                else
                {
                    CommonUtil.MessageAlert("I0006", "");
                    SetDataClear();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F1_01_ViewModel Command(GetWorkList)");
            }
        }

        private async Task<DataSet> GetWorkListIdxAsycn()
        {

            DataSet dt = null;

            await Task.Run(() =>
            {
                dt = F_01_Provider.GetWorkListIdx(Searchdatestring, Searchdaily);
            });

            return dt;
        }

        public async Task<DataTable> GetWorkListReportAsycn()
        {

            DataTable dt = null;

            await Task.Run(() =>
            {
                dt = F_01_Provider.GetWorkListReport(Searchdatestring, Searchdaily);
            });

            return dt;
        }

        //private async Task<DataTable> GetWorkListAsycn(int idx)
        //{

        //    DataTable dt = null;

        //    await Task.Run(() =>
        //    {
        //        dt = F_01_Provider.GetWorkList(idx);
        //    });

        //    return dt;
        //}

        private DataTable GetWorkListAsycn(int idx)
        {

            DataTable dt = null;

            dt = F_01_Provider.GetWorkList(idx);

            return dt;
        }

        private void SetWorkListModifyPopup(DataTable dt)
        {
            try
            {
                Selectpart1 = dt.Rows[0]["Part1"].ToString();

                //await Task.Run(() =>
                //{
                Workidx = Convert.ToInt32(dt.Rows[0]["WorkListIdx"].ToString());
                Selectusername = dt.Rows[0]["UserName"].ToString();
                Title = dt.Rows[0]["Title"].ToString();
                Selectshift = dt.Rows[0]["Shift"].ToString();
                Selectmachine = dt.Rows[0]["Machine"].ToString();
                Selectpart2 = dt.Rows[0]["Part2"].ToString();
                Selectclass = dt.Rows[0]["Class"].ToString();
                Eventdatetime = Convert.ToDateTime(dt.Rows[0]["EventDateTime"].ToString());
                Checktime = Convert.ToInt32(dt.Rows[0]["CheckTime"].ToString());
                Delaytime = Convert.ToInt32(dt.Rows[0]["DelayTime"].ToString());
                Detailhtml = HttpUtility.HtmlDecode(dt.Rows[0]["DetailHtml"].ToString());

                //DirectoryInfo di = new DirectoryInfo(Directorypath);
                //Filepath = (di.Exists) ? dt.Rows[0]["SaveFileDirectory"].ToString() : null;


                Filepath = dt.Rows[0]["SaveFileDirectory"].ToString();
                LastSaveFilePath = Filepath;

                Usershiftname = (Selectshift == "없음") ? Selectusername : string.Format("{0}-{1}", Selectusername, Selectshift);
                Savefilenamedisplay = (dt.Rows[0]["SaveFileName"].ToString() == "") ? "" : dt.Rows[0]["SaveFileName"].ToString();
                Savefilesizedisplay = dt.Rows[0]["FileSize"].ToString();
                Savefileformdisplay = dt.Rows[0]["Fileform"].ToString();

                //if (Savefileformdisplay == ".jpg" || Savefileformdisplay == ".jpeg" || Savefileformdisplay == ".bmp" || Savefileformdisplay == ".png")
                //{
                //    Image = Visibility.Visible;
                //    Icons = Visibility.Hidden;
                //}
                //else
                //{
                //    if (Savefileformdisplay == ".xlsx" || Savefileformdisplay == ".pdf" || Savefileformdisplay == ".docx" || Savefileformdisplay == ".pptx")
                //    {
                //        Image = Visibility.Hidden;
                //        Icons = Visibility.Visible;

                //    }
                //    else
                //    {
                //        Image = Visibility.Hidden;
                //        Icons = Visibility.Hidden;
                //    }
                //}

                Icons = (Savefileformdisplay == ".jpg" || Savefileformdisplay == ".jpeg" || Savefileformdisplay == ".bmp" || Savefileformdisplay == ".png" || Savefileformdisplay == ".xlsx" ||
                    Savefileformdisplay == ".pdf" || Savefileformdisplay == ".docx" || Savefileformdisplay == ".pptx") ? Visibility.Visible : Visibility.Hidden;


                Reportloadprogress = false;

                //});
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F1_01_ViewModel Command(SetWorkListModifyPopup)");
            }
            //Eventdatetimestring = Eventdatetime.ToString("yyyy-MM-dd hh")
        }

        #endregion

        #region SetWorkListSave OR Update

        private RelayCommand<C1.WPF.RichTextBox.C1RichTextBox> _CmdWorkListSave;

        public ICommand CmdWorkListSave => _CmdWorkListSave ?? (this._CmdWorkListSave = new RelayCommand<C1.WPF.RichTextBox.C1RichTextBox>(SetWorkListSave));

        private async void SetWorkListSave(C1.WPF.RichTextBox.C1RichTextBox obj)
        {
            try
            {
               
                int ErrorOutput = 0;
                int FileSaveCheck = 0;

                if (MessageBox.Show("현재 정보를 저장 하시겠습니까?", "저장 확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                {
                    if (Title != "" && Detailhtml != "" && Selectshift != null && Selectmachine != null && Selectclass != null && Selectpart1 != null && Selectpart2 != null && Selectusername != null)
                    {
                        Savelock = false;

                        if(Filepath != "" && LastSaveFilePath != Filepath)
                        {
                            bool directorycheck = await DirectorysCheck();

                            if (!directorycheck)
                            {
                                if (MessageBox.Show(string.Format("{0}위치의 공유폴더에 접근할수 없습니다. \r\n 저장하려는 파일이 누락되게 됩니다. 그래도 진행하시겠습니까? ", Directorypath), "확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                                {
                                    Savefilesize = 0;
                                    Savefiletotalsize = 0;
                                    Savefilepath = "";
                                }
                                else
                                {
                                    Savelock = true;
                                    Modifymode = false;
                                    return;
                                }
                            }
                            else
                            {
                                if (Modifymode)
                                {
                                    if (LastSaveFilePath != Filepath)
                                    {
                                        FileSaveCheck = await FileSave("", "", true);
                                    }
                                }
                                else
                                {
                                    if (Filepath != "")
                                    {
                                        FileSaveCheck = await FileSave("", "", true);
                                    }
                                }
                            }
                        }


                        if (FileSaveCheck == 0)
                        {
                            ErrorOutput = await SetWorkListSaveAsync(new RtfFilter().ConvertFromDocument(obj.Document).ToString());
                        }
                        else
                        {
                            Savelock = true;
                            return;
                        }
                    }
                    else
                    {
                        CommonUtil.MessageAlert("I0051", "선택 목록 ,TITLE, TEXTBOX");
                        return;
                    }
                }
                else
                {
                    return;
                }

                switch (ErrorOutput)
                {
                    case 0:
                        if (!Modifymode)
                        {
                            CommonUtil.MessageAlert("I0040", "작업일지에 대한 저장이");
                            SetDataClear();
                        }
                        else
                        {
                            CommonUtil.MessageAlert("I0040", "작업일지에 대한 변경이");
                            GetWorkList();
                        }


                        break;
                    case 1:
                        CommonUtil.MessageAlert("X0002", "(DB 저장)");

                        break;
                    case 2:
                        CommonUtil.MessageAlert("X0001", "(프로그램 함수)");
                        break;
                }

                Savelock = true;
                Modifymode = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Savelock = true;
                Modifymode = false;
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(SetWorkListSave)");
            }
        }

        private async Task<int> SetWorkListSaveAsync(string rtf)
        {
            try
            {
                int ErrorCheck = 0;

                string filepath = "";

                string filesize = "";

                if(Modifymode && Savefilepath == "")
                {
                    if(Filepath == LastSaveFilePath)
                    {
                        filepath = LastSaveFilePath;
                        filesize = Savefilesizedisplay;
                    }
                }
                else
                {
                    filepath = Savefilepath;
                    filesize = (Savefiletotalsize == 0) ? "" : string.Format("{0}KB", Savefiletotalsize / 1024);

                }

                await Task.Run(() =>
                {
                    ErrorCheck = F_01_Provider.SetWorkList(Modifymode, Workidx, Selectusername, Title, Selectshift, Selectmachine, Selectpart1, Selectpart2, Selectclass, Eventdatetime
                                             , Checktime, Delaytime, Detail, rtf, HttpUtility.HtmlEncode(Detailhtml), Path.GetExtension(filepath), Path.GetFileName(filepath), filepath, filesize);
                });


                return ErrorCheck;

                //obj.Html = HttpUtility.HtmlDecode(dcode);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(SetWorkListSaveAsync)");
                return 2;
            }
        }

        #endregion

        #region CmdGetWorkListChange

        private RelayCommand<object> _CmdGetWorkListChange;

        public ICommand CmdGetWorkListChange => _CmdGetWorkListChange ?? (this._CmdGetWorkListChange = new RelayCommand<object>(GetWorkListChange));

        private void GetWorkListChange(object obj)
        {
            try
            {

                int setnextprevious = Convert.ToInt32(obj);

                if (setnextprevious == 1)
                {
                    WorkListcurrentidx--;
                }
                else
                {
                    if (setnextprevious == 3)
                    {
                        WorkListcurrentidx = 1;
                    }
                    else
                    {
                        WorkListcurrentidx++;
                    }
                }



                ReportNxPvBtnVbMode = (WorkListTotalcount == 1) ? 0 : (WorkListcurrentidx == 1) ? 1 : (WorkListcurrentidx == WorkListTotalcount) ? 3 : 2;
                Reportloadprogress = true;
                WorkList = GetWorkListAsycn(Convert.ToInt32(WorkListidx.Rows[WorkListcurrentidx - 1]["WorkListIdx"].ToString()));
                SetWorkListModifyPopup(WorkList);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F1_01_ViewModel Command(GetWorkListChange)");
            }
        }

        #endregion


        #region SetWorkListDelete

        private RelayCommand<object> _CmdWorkListDelete;

        public ICommand CmdWorkListDelete => _CmdWorkListDelete ?? (this._CmdWorkListDelete = new RelayCommand<object>(SetWorkListDelete));

        private async void SetWorkListDelete(object obj)
        {
            try
            {
                int ErrorOutput = 0;
                int workidex = Convert.ToInt32(obj);

                if (MessageBox.Show(string.Format("현재 정보를 삭제 하시겠습니까?\n\r삭제 리스트 ID : {0}", workidex), "삭제 확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                {
                    ErrorOutput = await SetWorkListDeleteAsync(workidex);
                }
                else
                {
                    return;
                }

                switch (ErrorOutput)
                {
                    case 0:
                        CommonUtil.MessageAlert("I0040", "작업일지에 대한 삭제가");
                        GetWorkList();

                        //보류 
                        //obj.F01_Button.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                        break;
                    case 1:
                        CommonUtil.MessageAlert("X0002", "(DB 삭제)");
                        break;
                    case 2:
                        CommonUtil.MessageAlert("X0001", "(프로그램 함수)");
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F1_01_ViewModel Command(SetWorkListDelete)");
            }
        }

        private async Task<int> SetWorkListDeleteAsync(int obj)
        {
            try
            {
                int ErrorCheck = 0;

                await Task.Run(() =>
                {
                    ErrorCheck = F_01_Provider.SetWorkListDelete(obj);
                });


                return ErrorCheck;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F1_01_ViewModel Command(SetWorkListDeleteAsync)");
                return 2;
            }
        }

        #endregion

        #region WorkListModify

        private RelayCommand _CmdSetWorkListModify;

        public ICommand CmdSetWorkListModify => _CmdSetWorkListModify ?? (this._CmdSetWorkListModify = new RelayCommand(SetWorkListModify));

        private void SetWorkListModify()
        {
            Modifymode = true;
        }

        #endregion

        #region WorkListModifyCancel

        private RelayCommand _CmdSetWorkListModifyCancel;

        public ICommand CmdSetWorkListModifyCancel => _CmdSetWorkListModifyCancel ?? (this._CmdSetWorkListModifyCancel = new RelayCommand(SetWorkListModifyCancel));

        private void SetWorkListModifyCancel()
        {
            Modifymode = false;
            Reportloadprogress = true;
            SetWorkListModifyPopup(WorkList);
        }

        #endregion


        #region GetFilePath

        private RelayCommand _CmdGetFilePath;

        public ICommand CmdGetFilePath => _CmdGetFilePath ?? (this._CmdGetFilePath = new RelayCommand(GetFilePath));

        private void GetFilePath()
        {
            try
            {
                string images = Util.GetImage();

                if (!(images == ""))
                {
                    Filepath = images;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(GetFilePath)");
            }
        }

        private async Task<int> FileSave(string savepath, string downloadpath, bool savetype)
        {
            int check = 0;

            try
            {
                if (savetype)
                {
                    if (!(Filepath == "" || Filepath is null))
                    {

                        await Task.Run(() =>
                        {
                            // YKCJ Server
                            //check = FileCopy(Filepath, "Q:\\", savetype);  

                            //HGFA TEST
                            check = FileCopy(Filepath, Directorypath, savetype);
                        });

                    }
                }
                else
                {
                    await Task.Run(() =>
                    {
                        check = FileCopy(savepath, downloadpath, savetype);
                    });
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(FileSave)");
            }

            return check;
        }



        public int FileCopy(string savefile, string savepath, bool savetype)
        {
            FileInfo fi = new FileInfo(savefile);
            Savefilesize = 0;
            Savefiletotalsize = fi.Length;

            string copymody;

            byte[] bBuf = new byte[1024];

            if (savetype)
            {

                Savefilepath = string.Format("{0}\\{1}", savepath, Path.GetFileName(savefile));

                copymody = "Upload";

                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }
            }
            else
            {
                Savefilepath = savepath;

                copymody = "Download";
            }


            if (File.Exists(Savefilepath))
            {
                if (MessageBox.Show("선택된 파일이 중복입니다. 덮어쓰기 하시겠습니까?", "덮어쓰기", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                {
                    if (IsFileLocked(new FileInfo(Savefilepath)))
                    {
                        CommonUtil.MessageAlert("I0016", "");
                        return 1;
                    }
                    else
                    {
                        File.Delete(Savefilepath);
                    }
                }
                else
                {
                    return 1;
                }

            }

            // 원본 파일
            FileStream fsIn = new FileStream(savefile, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 저장 위치 
            FileStream fsOut = new FileStream(Savefilepath, FileMode.Create, FileAccess.Write);

            while (Savefilesize < Savefiletotalsize)
            {
                try
                {
                    int iLen = fsIn.Read(bBuf, 0, bBuf.Length);
                    Savefilesize += iLen;
                    fsOut.Write(bBuf, 0, iLen);

                    Savefileprogress = string.Format("{0} : {1}KB / {2}KB", copymody, Savefilesize / 1024, Savefiletotalsize / 1024);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    fsOut.Flush();
                    fsOut.Close();
                    fsIn.Close();

                    if (File.Exists(Savefilepath))
                    {
                        File.Delete(Savefilepath);
                    }
                    return 2;
                }
            }

            fsOut.Flush();
            fsOut.Close();
            fsIn.Close();

            return 0;
        }



        protected virtual bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
        }

        #endregion


        #region  SetFilePathCancel

        private RelayCommand _CmdSetFilePathCancel;

        public ICommand CmdSetFilePathCancel => _CmdSetFilePathCancel ?? (this._CmdSetFilePathCancel = new RelayCommand(SetFilePathCancel));

        private void SetFilePathCancel()
        {
            try
            {
                if (Filepath != "")
                {
                    if (MessageBox.Show("현재 파일 저장위치를 취소 하시겠습니까?", "확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                    {
                        Filepath = "";
                    }
                    else
                    {
                        return;
                    }

                }
                else
                {
                    CommonUtil.MessageAlert("I0015", "");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(GetFilePath)");
            }
        }

        #endregion



        #region  Get File Open & Download

        private RelayCommand<object> _CmdGetFileOpen;

        public ICommand CmdGetFileOpen => _CmdGetFileOpen ?? (this._CmdGetFileOpen = new RelayCommand<object>(GetFileOpen));

        private async void GetFileOpen(object obj)
        {
            try
            {
                bool directorycheck = await DirectorysCheck();

                if (!directorycheck)
                {
                    return;
                }

                await Task.Run(() =>
                {
                    System.Diagnostics.Process.Start(obj.ToString());
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(GetFilePath)");
            }
        }


        private RelayCommand<object> _CmdGetFileDownload;

        public ICommand CmdGetFileDownload => _CmdGetFileDownload ?? (this._CmdGetFileDownload = new RelayCommand<object>(GetFileDownload));

        private async void GetFileDownload(object obj)
        {
            try
            {
                bool directorycheck = await DirectorysCheck();

                if (!directorycheck)
                {
                    return;
                }

                Fom.SaveFileDialog saveFileDialog = new Fom.SaveFileDialog();
                saveFileDialog.Filter = string.Format("File (*{0})|*{0}", Path.GetExtension(obj.ToString()));
                saveFileDialog.Title = "Export File";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName.Trim() == "")
                {
                    return;
                }
                else
                {
                    if (MessageBox.Show("다운로드를 진행 하시겠습니까?", "확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                Downloadlock = false;

                Downloadfilepath = saveFileDialog.FileName.Trim();

                int errorcheck = await FileSave(obj.ToString(), Downloadfilepath, false);
                if (errorcheck == 0)
                {
                    if (MessageBox.Show("다운로드를 완료 하였습니다.\r 파일을 오픈 할까요?", "FILE OPEN", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                    {
                        GetFileOpen(Downloadfilepath);
                    }
                }
                else
                {
                    MessageBox.Show("다운로드 실패");
                    Downloadlock = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(GetFilePath)");
            }

            Downloadlock = true;
        }

        private async Task<bool> DirectorysCheck()
        {

            Downloadlock = false;
            bool directorycheck = false;

            try
            {
                await Task.Run(() =>
                {
                    DirectoryInfo di = new DirectoryInfo(Directorypath);
                    directorycheck = di.Exists;

                    if (!directorycheck)
                    {
                        MessageBox.Show(string.Format("{0}위치의 공유폴더에 접근할수 없습니다. \r\n 관리자에게 문의하십시요.", Directorypath), "확인");
                    }
                });
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(GetFilePath)");
            }

            Downloadlock = true;
            return directorycheck;
        }


        #endregion

        #region SaveReportDataClear

        private RelayCommand _CmdSetSaveReportDataClear;

        public ICommand CmdSetSaveReportDataClear => _CmdSetSaveReportDataClear ?? (this._CmdSetSaveReportDataClear = new RelayCommand(SaveReportDataClear));

        private void SaveReportDataClear()
        {
            SetDataClear();
        }


        #endregion


        #region Report Display Mode

        private RelayCommand _CmdReportSaveMode;

        public ICommand CmdReportSaveMode => _CmdReportSaveMode ?? (this._CmdReportSaveMode = new RelayCommand(ReportSaveMode));


        private RelayCommand _CmdReportViewMode;

        public ICommand CmdReportViewMode => _CmdReportViewMode ?? (this._CmdReportViewMode = new RelayCommand(ReportViewMode));

        private void ReportSaveMode()
        {
            ReportSavemode = true;
            SetDataClear();
            //Modifymode = false;
        }

        private void ReportViewMode()
        {
            ReportSavemode = false;
            Username = "";
            SetDataClear();
            //Modifymode = true;
        }

        #endregion


        #endregion


        #region F01 Call Function

        private void SearchDate()
        {
            //Searchdatestring = (!Searchdaily) ? (!Searchweekly) ? string.Format("{0} 07:00:00 ~ {1} 07:00:00", Searchdate.ToString("yyyy-MM-dd"), Searchdate.AddDays(-30).ToString("yyyy-MM-dd")) :
            //    string.Format("{0} 07:00:00 ~ {1} 07:00:00", Searchdate.ToString("yyyy-MM-dd"), Searchdate.AddDays(-7).ToString("yyyy-MM-dd")) :
            //    string.Format("{0} 07:00:00 ~ {1} 07:00:00", Searchdate.ToString("yyyy-MM-dd"), Searchdate.AddDays(-1).ToString("yyyy-MM-dd"));

            Searchdatestring = (!Searchdaily) ? (!Searchweekly) ? string.Format("{0} 07:00:00 ~ {1} 07:00:00", Searchdate.AddDays(-30).ToString("yyyy-MM-dd"), Searchdate.ToString("yyyy-MM-dd")) :
                string.Format("{0} 07:00:00 ~ {1} 07:00:00", Searchdate.AddDays(-7).ToString("yyyy-MM-dd"), Searchdate.ToString("yyyy-MM-dd")) :
                Searchdate.ToString("yyyy-MM-dd");
        }

        private DataTable SelectPart2List(string Part1)
        {
            try
            {
                DataTable dt;

                if (Part1 is null)
                {
                    return null;
                }

                return dt = F_01_Provider.GetComboboxPart2List(Part1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel (SelectPart2List)");
                return null;
            }
        }


        #endregion

    }
}
