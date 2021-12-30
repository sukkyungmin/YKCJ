using C1.WPF.RichTextBox.Documents;
using Common.Helper;
using Common.Infos;
using Common.Utils;
using DBConnection.F_02;
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

namespace YKCJViewModel.F_02
{
    public class F_02_01_ViewModel : ObservableObject
    {
        private F_02_01_Model Model = new F_02_01_Model();
        private F_02_01_Provider F_02_Provider = new F_02_01_Provider();
        private LOG_Write m_LOG = new LOG_Write();
        private readonly CommonUtil Util = new CommonUtil();


        public delegate void SenIntIndex(int index);
        public event SenIntIndex SendIndex;

        C1.WPF.RichTextBox.C1RichTextBox TextChange = new C1.WPF.RichTextBox.C1RichTextBox();

        private int SetPageChangeNumber;
        private bool WorklistLock = true;
        private string LastSaveFilePath;


        public F_02_01_ViewModel()
        {
            SetinitializeDB();
        }

        private void SetinitializeDB()
        {
            try
            {
                /* 추후 문제 있을시 이 로직으로 변경
                DataSet dataSet = F_02_Provider.GetComboboxList();
                //DataRow dr1;
                //DataRow dr2;
                //DataRow dr3;

                Machinelist = dataSet.Tables[0];
                //dr1 = Machinelist.NewRow();
                //dr1[0] = "미 선택";
                //Machinelist.Rows.InsertAt(dr1, 0);

                ClassList = dataSet.Tables[1];
                //dr2 = ClassList.NewRow();
                //dr2[0] = "미 선택";
                //ClassList.Rows.InsertAt(dr2, 0);

                Modulepart1list = dataSet.Tables[2];
                //dr3 = Modulepart1list.NewRow();
                //dr3[0] = "미 선택";
                //Modulepart1list.Rows.InsertAt(dr3, 0);
                */

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

                //Selectmachine = "미 선택";
                //Selectclass = "미 선택";
                //Selectpart1 = "미 선택";
                //Selectshift = "A";

                Title = "";
                Detail = "";
                Detailrtf = "";
                Filepath = "";
                Savefilepath = "";
                //Namefilter = "";

                SetSearchTime();

                Username = UserInfo.US_NM;

                Pagenumber = 1;
                SetPageChangeNumber = 1;

                //WorklistLock = true;
                Savelock = true;
                Downloadlock = true;
                Downloadnotfile = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel (SetinitializeDB)");
            }
        }

        private async void SetComboList()
        {
            try
            {
                await Task.Run(() =>
                {
                    DataSet dataSet = F_02_Provider.GetComboboxList();

                    Machinelist = dataSet.Tables[0];

                    ClassList = dataSet.Tables[1];

                    Modulepart1list = dataSet.Tables[2];

                    WhereshiftList = dataSet.Tables[3];
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel (SetComboList)");
            }
        }

        //private 

        private void SetSearchTime()
        {
            Searchtime = string.Format("{0} 07:00 ~ {1} 07:00", Stime.ToString("yyyy-MM-dd"), Etime.ToString("yyyy-MM-dd"));
        }

        private void SetDataClear()
        {
            Title = "";
            Detail = "";
            Detailrtf = "";
            DetailHtml = "";
            Selectmachine = null;
            Selectclass = null;
            Selectpart1 = null;
            Selectshift = null;
            Selectpart2 = null;
            Eventdatetime = DateTime.Now;
            Checktime = 0;
            Delaytime = 0;
            Savefiletotalsize = 0;
            Filepath = "";
            Savefilepath = "";
            Savefileprogress = "";
            Downloadfilepath = "";
            Savelock = true;
            Downloadlock = true;
            Downloadnotfile = true;
        }

        public string teststring
        {
            get { return Model._teststring; }
            set
            {
                if (Model._teststring != value)
                {
                    Model._teststring = value;
                    RaisePropertyChanged(() => this.teststring);
                }
            }
        }

        public bool Rowdetailsvisibilitymode
        {
            get { return Model._Rowdetailsvisibilitymode; }
            set
            {
                if (Model._Rowdetailsvisibilitymode != value)
                {
                    Model._Rowdetailsvisibilitymode = value;
                    RaisePropertyChanged(() => this.Rowdetailsvisibilitymode);
                }
            }
        }

        public bool Modifymode
        {
            get { return Model._PopModel._Modifymode; }
            set
            {
                if (Model._PopModel._Modifymode != value)
                {
                    Model._PopModel._Modifymode = value;
                    RaisePropertyChanged(() => this.Modifymode);
                }
            }
        }

        public bool Savelock
        {
            get { return Model._PopModel._Savelock; }
            set
            {
                if (Model._PopModel._Savelock != value)
                {
                    Model._PopModel._Savelock = value;
                    RaisePropertyChanged(() => this.Savelock);
                }
            }
        }
        public bool Downloadlock
        {
            get { return Model._PopModel._Downloadlock; }
            set
            {
                if (Model._PopModel._Downloadlock != value)
                {
                    Model._PopModel._Downloadlock = value;
                    RaisePropertyChanged(() => this.Downloadlock);
                }
            }
        }
        public bool Downloadnotfile
        {
            get { return Model._PopModel._Downloadnotfile; }
            set
            {
                if (Model._PopModel._Downloadnotfile != value)
                {
                    Model._PopModel._Downloadnotfile = value;
                    RaisePropertyChanged(() => this.Downloadnotfile);
                }
            }
        }

        public int Modifyworklistidx
        {
            get { return Model._PopModel._Modifyworklistidx; }
            set
            {
                if (Model._PopModel._Modifyworklistidx != value)
                {
                    Model._PopModel._Modifyworklistidx = value;
                    RaisePropertyChanged(() => this.Modifyworklistidx);
                }
            }
        }

        public int Pagenumber
        {
            get { return Model._Pagenumber; }
            set
            {
                if (Model._Pagenumber != value)
                {
                    Model._Pagenumber = (value == 0) ? 1 : value;
                    if (!WorklistLock)
                    {
                        GetWorkList();
                    }
                    RaisePropertyChanged(() => this.Pagenumber);
                }
            }
        }

        public int Totalpage
        {
            get { return Model._Totalpage; }
            set
            {
                if (Model._Totalpage != value)
                {
                    Model._Totalpage = value;
                    RaisePropertyChanged(() => this.Totalpage);
                }
            }
        }

        public int PagingStaEndmoed
        {
            get { return Model._PagingStaEndmoed; }
            set
            {
                if (Model._PagingStaEndmoed != value)
                {
                    Model._PagingStaEndmoed = value;
                    RaisePropertyChanged(() => this.PagingStaEndmoed);
                }
            }
        }
        public int PagingStaEndlastvisible
        {
            get { return Model._PagingStaEndlastvisible; }
            set
            {
                if (Model._PagingStaEndlastvisible != value)
                {
                    Model._PagingStaEndlastvisible = value;
                    RaisePropertyChanged(() => this.PagingStaEndlastvisible);
                }
            }
        }

        public int Viewworklistchangenumber
        {
            get { return Model._Viewworklistchangenumber; }
            set
            {
                if (Model._Viewworklistchangenumber != value)
                {
                    Model._Viewworklistchangenumber = value;
                    RaisePropertyChanged(() => this.Viewworklistchangenumber);
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

     
        public long Savefiletotalsize
        {
            get { return Model._PopModel._Savefiletotalsize; }
            set
            {
                if (Model._PopModel._Savefiletotalsize != value)
                {
                    Model._PopModel._Savefiletotalsize = value;
                    RaisePropertyChanged(() => this.Savefiletotalsize);
                }
            }
        }

        public long Savefilesize
        {
            get { return Model._PopModel._Savefilesize; }
            set
            {
                if (Model._PopModel._Savefilesize != value)
                {
                    Model._PopModel._Savefilesize = value;
                    RaisePropertyChanged(() => this.Savefilesize);
                }
            }
        }

        public string Savefileprogress
        {
            get { return Model._PopModel._Savefileprogress; }
            set
            {
                if (Model._PopModel._Savefileprogress != value)
                {
                    Model._PopModel._Savefileprogress = value;
                    RaisePropertyChanged(() => this.Savefileprogress);
                }
            }
        }

        public string Savefilepath
        {
            get { return Model._PopModel._Savefilepath; }
            set
            {
                if (Model._PopModel._Savefilepath != value)
                {
                    Model._PopModel._Savefilepath = value;
                    RaisePropertyChanged(() => this.Savefilepath);
                }
            }
        }

        public string Downloadfilepath
        {
            get { return Model._PopModel._Downloadfilepath; }
            set
            {
                if (Model._PopModel._Downloadfilepath != value)
                {
                    Model._PopModel._Downloadfilepath = value;
                    RaisePropertyChanged(() => this.Downloadfilepath);
                }
            }
        }


        public string Username
        {
            get { return Model._PopModel._Username; }
            set
            {
                if (Model._PopModel._Username != value)
                {
                    Model._PopModel._Username = value;
                    RaisePropertyChanged(() => this.Username);
                }
            }
        }


        public string Modifyusername
        {
            get { return Model._PopModel._Modifyusername; }
            set
            {
                if (Model._PopModel._Modifyusername != value)
                {
                    Model._PopModel._Modifyusername = value;
                    RaisePropertyChanged(() => this.Modifyusername);
                }
            }
        }

        public string Title
        {
            get { return Model._PopModel._Title; }
            set
            {
                if (Model._PopModel._Title != value)
                {
                    Model._PopModel._Title = value;
                    RaisePropertyChanged(() => this.Title);
                }
            }
        }

        public string Detail
        {
            get { return Model._PopModel._Detail; }
            set
            {
                if (Model._PopModel._Detail != value)
                {
                    Model._PopModel._Detail = value;
                    RaisePropertyChanged(() => this.Detail);
                }
            }
        }

        public string Detailrtf
        {
            get { return Model._PopModel._Detailrtf; }
            set
            {
                if (Model._PopModel._Detailrtf != value)
                {
                    Model._PopModel._Detailrtf = value;
                    RaisePropertyChanged(() => this.Detailrtf);
                }
            }
        }

        public string DetailHtml
        {
            get { return Model._PopModel._DetailHtml; }
            set
            {
                if (Model._PopModel._DetailHtml != value)
                {
                    Model._PopModel._DetailHtml = value;
                    RaisePropertyChanged(() => this.DetailHtml);
                }
            }
        }

        public string Selectmachine
        {
            get { return Model._PopModel._Selectmachine; }
            set
            {
                if (Model._PopModel._Selectmachine != value)
                {
                    Model._PopModel._Selectmachine = value;
                    RaisePropertyChanged(() => this.Selectmachine);
                }
            }
        }

        public string Selectclass
        {
            get { return Model._PopModel._Selectclass; }
            set
            {
                if (Model._PopModel._Selectclass != value)
                {
                    Model._PopModel._Selectclass = value;
                    RaisePropertyChanged(() => this.Selectclass);
                }
            }
        }

        public string Selectpart1
        {
            get { return Model._PopModel._Selectpart1; }
            set
            {
                if (Model._PopModel._Selectpart1 != value)
                {
                    Model._PopModel._Selectpart1 = value;
                    RaisePropertyChanged(() => this.Selectpart1);
                    Modulepart2list = SelectPart2List(value);
                }
            }
        }

        public string Selectpart2
        {
            get { return Model._PopModel._Selectpart2; }
            set
            {
                if (Model._PopModel._Selectpart2 != value)
                {
                    Model._PopModel._Selectpart2 = value;
                    RaisePropertyChanged(() => this.Selectpart2);
                }
            }
        }

        public string Selectshift
        {
            get { return Model._PopModel._Selectshift; }
            set
            {
                if (Model._PopModel._Selectshift != value)
                {
                    Model._PopModel._Selectshift = value;
                    RaisePropertyChanged(() => this.Selectshift);
                }
            }
        }

        public string Filepath
        {
            get { return Model._PopModel._Filepath; }
            set
            {
                if (Model._PopModel._Filepath != value)
                {
                    Model._PopModel._Filepath = value;
                    RaisePropertyChanged(() => this.Filepath);
                }
            }
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

                return dt = F_02_Provider.GetComboboxPart2List(Part1);
                //DataRow dr1;

                //dr1 = Modulepart2list.NewRow();
                //dr1[0] = "미 선택";
                //Modulepart2list.Rows.InsertAt(dr1, 0);

                //Selectpart2 = "미 선택";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel (SelectPart2List)");
                return null;
            }
        }

        public int Checktime
        {
            get { return Model._PopModel._Checktime; }
            set
            {
                if (Model._PopModel._Checktime != value)
                {
                    Model._PopModel._Checktime = value;
                    RaisePropertyChanged(() => this.Checktime);
                }
            }
        }

        public int Delaytime
        {
            get { return Model._PopModel._Delaytime; }
            set
            {
                if (Model._PopModel._Delaytime != value)
                {
                    Model._PopModel._Delaytime = value;
                    RaisePropertyChanged(() => this.Delaytime);
                }
            }
        }

        public string Searchtime
        {
            get { return Model._Searchtime; }
            set
            {
                if (Model._Searchtime != value)
                {
                    Model._Searchtime = value;
                    RaisePropertyChanged(() => this.Searchtime);
                }
            }
        }

        public string Filtertext
        {
            get { return Model._Filtertext; }
            set
            {
                if (Model._Filtertext != value)
                {
                    Model._Filtertext = value;
                    RaisePropertyChanged(() => this.Filtertext);
                }
            }
        }

        public string Filtershift
        {
            get { return Model._Filtershift; }
            set
            {
                if (Model._Filtershift != value)
                {
                    Model._Filtershift = value;
                    RaisePropertyChanged(() => this.Filtershift);
                }
            }
        }

        public string Filtermachine
        {
            get { return Model._Filtermachine; }
            set
            {
                if (Model._Filtermachine != value)
                {
                    Model._Filtermachine = value;
                    RaisePropertyChanged(() => this.Filtermachine);
                }
            }
        }


        public string Filterclass
        {
            get { return Model._Filterclass; }
            set
            {
                if (Model._Filterclass != value)
                {
                    Model._Filterclass = value;
                    RaisePropertyChanged(() => this.Filterclass);
                }
            }
        }

        public string Filterpart1
        {
            get { return Model._Filterpart1; }
            set
            {
                if (Model._Filterpart1 != value)
                {
                    Model._Filterpart1 = value;
                    RaisePropertyChanged(() => this.Filterpart1);
                    FilterModulepart2list = SelectPart2List(value);
                }
            }
        }

        public string Filterpart2
        {
            get { return Model._Filterpart2; }
            set
            {
                if (Model._Filterpart2 != value)
                {
                    Model._Filterpart2 = value;
                    RaisePropertyChanged(() => this.Filterpart2);
                }
            }
        }

        public string ViewDetailhtml
        {
            get { return Model._ViewDetailhtml; }
            set
            {
                if (Model._ViewDetailhtml != value)
                {
                    Model._ViewDetailhtml = value;
                    RaisePropertyChanged(() => this.ViewDetailhtml);
                }
            }
        }

        public DateTime Eventdatetime
        {
            get
            {
                if (Model._PopModel._Eventdatetime == DateTime.MinValue)
                    return DateTime.Now;

                return Model._PopModel._Eventdatetime;
            }
            set
            {
                if (Model._PopModel._Eventdatetime != value)
                {
                    Model._PopModel._Eventdatetime = value;
                    RaisePropertyChanged(() => this.Eventdatetime);
                }
            }
        }

        public DateTime Stime
        {
            get
            {
                if (Model._Stime == DateTime.MinValue)
                    return DateTime.Now;

                return Model._Stime;
            }
            set
            {
                if (Model._Stime != value)
                {
                    Model._Stime = value;
                    RaisePropertyChanged(() => this.Stime);
                    SetSearchTime();
                }
            }
        }

        public DateTime Etime
        {
            get
            {
                if (Model._Etime == DateTime.MinValue)
                    return DateTime.Now;

                return Model._Etime;
            }
            set
            {
                if (Model._Etime != value)
                {
                    Model._Etime = value;
                    RaisePropertyChanged(() => this.Etime);
                    SetSearchTime();
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

        public DataTable WhereshiftList
        {
            get { return Model._WhereshiftList; }
            set
            {
                if (Model._WhereshiftList != value)
                {
                    Model._WhereshiftList = value;
                    RaisePropertyChanged(() => this.WhereshiftList);
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

        public DataTable FilterModulepart2list
        {
            get { return Model._FilterModulepart2list; }
            set
            {
                if (Model._FilterModulepart2list != value)
                {
                    Model._FilterModulepart2list = value;
                    RaisePropertyChanged(() => this.FilterModulepart2list);
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

        public DataTable AllWorkList
        {
            get { return Model._AllWorkList; }
            set
            {
                if (Model._AllWorkList != value)
                {
                    Model._AllWorkList = value;
                    RaisePropertyChanged(() => this.AllWorkList);
                }
            }
        }


        public DataTable WorklistModify
        {
            get { return Model._WorklistModify; }
            set
            {
                if (Model._WorklistModify != value)
                {
                    Model._WorklistModify = value;
                    RaisePropertyChanged(() => this.WorklistModify);
                }
            }
        }

        public DataTable Viewworklist
        {
            get { return Model._Viewworklist; }
            set
            {
                if (Model._Viewworklist != value)
                {
                    Model._Viewworklist = value;
                    RaisePropertyChanged(() => this.Viewworklist);
                }
            }
        }

        public DataTable Viewworkidxlist
        {
            get { return Model._Viewworkidxlist; }
            set
            {
                if (Model._Viewworkidxlist != value)
                {
                    Model._Viewworkidxlist = value;
                    RaisePropertyChanged(() => this.Viewworkidxlist);
                }
            }
        }

        #region ICommand List

        #region SetWorkListSave OR Update

        private RelayCommand<RichtextboxModel> _CmdWorkListSave;

        public ICommand CmdWorkListSave => _CmdWorkListSave ?? (this._CmdWorkListSave = new RelayCommand<RichtextboxModel>(SetWorkListSave));

        private async void SetWorkListSave(RichtextboxModel obj)
        {
            try
            {
                int ErrorOutput = 0;
                int FileSaveCheck = 0;

                if (MessageBox.Show("현재 정보를 저장 하시겠습니까?", "저장 확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                {
                    if (Title != "" && DetailHtml != "" && Selectshift != null && Selectmachine != null && Selectclass != null && Selectpart1 != null && Selectpart2 != null)
                    {
                        Savelock = false;

                        if(Modifymode)
                        {
                            if(LastSaveFilePath != Filepath)
                            {
                                FileSaveCheck = await FileSave("","", true);
                            }
                        }
                        else
                        {
                            if (Filepath != "")
                            {
                                FileSaveCheck = await FileSave("","", true);
                            }
                        }

                        if (FileSaveCheck == 0)
                        {
                            ErrorOutput = await SetWorkListSaveAsync(new RtfFilter().ConvertFromDocument(obj.C1richtextbox.Document).ToString());
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
                        }
                        else
                        {
                            CommonUtil.MessageAlert("I0040", "작업일지에 대한 변경이");
                        }
                        SetDataClear();
                        //GetWorkList();
                        obj.WindwsPopup.Close();
                        break;
                    case 1:
                        CommonUtil.MessageAlert("X0002", "(DB 저장)");

                        break;
                    case 2:
                        CommonUtil.MessageAlert("X0001", "(프로그램 함수)");
                        break;
                }

                Savelock = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Savelock = true;
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(SetWorkListSave)");
            }
        }

        private async Task<int> SetWorkListSaveAsync(string rtf)
        {
            try
            {
                int ErrorCheck = 0;

                string name = (!Modifymode) ? Username : Modifyusername;

                string filepath = (Modifymode && Savefilepath == "") ? Filepath : Savefilepath;

                string filesize = (Savefiletotalsize == 0) ? "" : string.Format("{0}KB", Savefiletotalsize / 1024);

                await Task.Run(() =>
                {
                    ErrorCheck = F_02_Provider.SetWorkList(Modifymode, Modifyworklistidx, name, Title, Selectshift, Selectmachine, Selectpart1, Selectpart2, Selectclass, Eventdatetime
                                             , Checktime, Delaytime, Detail, rtf, HttpUtility.HtmlEncode(DetailHtml), Path.GetExtension(filepath), Path.GetFileName(filepath), filepath, filesize);
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


        #region SetWorkListDelete

        private RelayCommand<ListViewPopup> _CmdWorkListDelete;

        public ICommand CmdWorkListDelete => _CmdWorkListDelete ?? (this._CmdWorkListDelete = new RelayCommand<ListViewPopup>(SetWorkListDelete));

        private async void SetWorkListDelete(ListViewPopup obj)
        {
            try
            {
                int ErrorOutput = 0;

                if (MessageBox.Show("현재 정보를 삭제 하시겠습니까?", "삭제 확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                {
                    int workidex = Convert.ToInt32(obj.Idx);
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
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(SetWorkListDelete)");
            }
        }

        private async Task<int> SetWorkListDeleteAsync(int obj)
        {
            try
            {
                int ErrorCheck = 0;

                await Task.Run(() =>
                {
                    ErrorCheck = F_02_Provider.SetWorkListDelete(obj);
                });


                return ErrorCheck;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(SetWorkListDeleteAsync)");
                return 2;
            }
        }

        #endregion


        #region GetWorkList

        private RelayCommand _CmdGetWorkList;

        public ICommand CmdGetWorkList => _CmdGetWorkList ?? (this._CmdGetWorkList = new RelayCommand(GetWorkList));

        private async void GetWorkList()
        {
            try
            {

                WorkList = await GetWorkListAsycn();
                Viewworkidxlist = await GetWorkIdxListAsycn();
                Rowdetailsvisibilitymode = false;


                if (WorkList.Rows.Count < 1)
                {
                    if(Stime > Etime)
                    {
                        MessageBox.Show(string.Format("날짜 선택이 잘못되었습니다. \r Sdate : {0} \r Edate : {1}", Stime.ToString("yyyy-MM-dd"), Etime.ToString("yyyy-MM-dd")));
                    }
                    else
                    {
                        MessageBox.Show("데이터가 존재하지 않습니다.");
                    }
                    
                }
                Rowdetailsvisibilitymode = true;
                AllWorkList = await GetAllWorListAsycn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(GetWorkList)");
            }
        }

        private async Task<DataTable> GetWorkListAsycn()
        {

            DataTable dt = null;

            await Task.Run(() =>
                {
                    dt = F_02_Provider.GetWorkList(string.Format("{0} 07:00:00", Stime.ToString("yyyy-MM-dd")), string.Format("{0} 07:00:00", Etime.ToString("yyyy-MM-dd"))
                                       , Nullcheck(Filtertext), Nullcheck(Filtershift), Nullcheck(Filtermachine), Nullcheck(Filterclass), Nullcheck(Filterpart1), Nullcheck(Filterpart2), Pagenumber);
                });

            return dt;
        }

        private async Task<DataTable> GetWorkIdxListAsycn()
        {

            DataTable dt = null;

            await Task.Run(() =>
            {
                dt = F_02_Provider.GetWorkIdxList(string.Format("{0} 07:00:00", Stime.ToString("yyyy-MM-dd")), string.Format("{0} 07:00:00", Etime.ToString("yyyy-MM-dd"))
                                   , Nullcheck(Filtertext), Nullcheck(Filtershift), Nullcheck(Filtermachine), Nullcheck(Filterclass), Nullcheck(Filterpart1), Nullcheck(Filterpart2));
            });

            return dt;
        }

        private async Task<DataTable> GetAllWorListAsycn()
        {

            DataTable dt = null;

            await Task.Run(() =>
            {
                dt = F_02_Provider.GetAllWorkList(string.Format("{0} 07:00:00", Stime.ToString("yyyy-MM-dd")), string.Format("{0} 07:00:00", Etime.ToString("yyyy-MM-dd"))
                                   , Nullcheck(Filtertext), Nullcheck(Filtershift), Nullcheck(Filtermachine), Nullcheck(Filterclass), Nullcheck(Filterpart1), Nullcheck(Filterpart2));
            });

            return dt;
        }

        //private async void GetAllWorListAsycn()
        //{

        //    try
        //    {
        //        await Task.Run(() =>
        //        {
        //            AllWorkList = F_02_Provider.GetAllWorkList(string.Format("{0} 07:00:00", Stime.ToString("yyyy-MM-dd")), string.Format("{0} 07:00:00", Etime.ToString("yyyy-MM-dd"))
        //                               , Nullcheck(Filtertext), Nullcheck(Filtershift), Nullcheck(Filtermachine), Nullcheck(Filterclass), Nullcheck(Filterpart1), Nullcheck(Filterpart2));
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(GetWorkList)");
        //    }

        //}

        public int GetWorkListRows()
        {
            int dt = 0;

            try
            {
                dt = F_02_Provider.GetWorkListRows(string.Format("{0} 07:00:00", Stime.ToString("yyyy-MM-dd")), string.Format("{0} 07:00:00", Etime.ToString("yyyy-MM-dd"))
                                   , Nullcheck(Filtertext), Nullcheck(Filtershift), Nullcheck(Filtermachine), Nullcheck(Filterclass), Nullcheck(Filterpart1), Nullcheck(Filterpart2));

                WorklistLock = false;

                Pagenumber = 0;
                //SetPageChangeNumber = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(GetWorkList)");
            }

            return dt;
        }

        private string Nullcheck(string st)
        {
            return (st is null) ? "" : st;
        }

        #endregion

        #region GetWorkListModify

        private RelayCommand<ListViewPopup> _CmdGetWorkListModify;

        public ICommand CmdGetWorkListModify => _CmdGetWorkListModify ?? (this._CmdGetWorkListModify = new RelayCommand<ListViewPopup>(GetWorkListModify));

        private async void GetWorkListModify(ListViewPopup obj)
        {
            try
            {
                int idx = Convert.ToInt32(obj.Idx);

                WorklistModify = await GetWorkListModifyAsycn(idx);

                SetWorkListModifyPopup(WorklistModify);

                //obj.F01_Button.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));

                Modifymode = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(GetWorkListModify)");
            }
        }

        private async Task<DataTable> GetWorkListModifyAsycn(int workidx)
        {

            DataTable dt = null;

            await Task.Run(() =>
            {
                dt = F_02_Provider.GetWorkListModify(workidx);
            });

            return dt;
        }

        private void SetWorkListModifyPopup(DataTable dt)
        {
            Modifyworklistidx = Convert.ToInt32(dt.Rows[0]["WorkListIdx"].ToString());
            Modifyusername = dt.Rows[0]["UserName"].ToString();
            Title = dt.Rows[0]["Title"].ToString();
            Selectshift = dt.Rows[0]["Shift"].ToString();
            Selectmachine = dt.Rows[0]["Machine"].ToString();
            Selectpart1 = dt.Rows[0]["Part1"].ToString();
            Selectpart2 = dt.Rows[0]["PArt2"].ToString();
            Selectclass = dt.Rows[0]["Class"].ToString();
            Eventdatetime = Convert.ToDateTime(dt.Rows[0]["EventDateTime"].ToString());
            Checktime = Convert.ToInt32(dt.Rows[0]["CheckTime"].ToString());
            Delaytime = Convert.ToInt32(dt.Rows[0]["DelayTime"].ToString());
            DetailHtml = HttpUtility.HtmlDecode(dt.Rows[0]["DetailHtml"].ToString());
            Filepath = dt.Rows[0]["SaveFileDirectory"].ToString();
            LastSaveFilePath = Filepath;
        }

        #endregion


        #region GetViewWorkList  & Next Pre View Work List

        private RelayCommand<ListViewPopup> _CmdGetViewWorkList;

        public ICommand CmdGetViewWorkList => _CmdGetViewWorkList ?? (this._CmdGetViewWorkList = new RelayCommand<ListViewPopup>(GetViewWorkList));

        private async void GetViewWorkList(ListViewPopup obj)
        {
            try
            {
                int idx = Convert.ToInt32(obj.Idx);

                //foreach (DataRow drow in WorkList.Rows)
                //{
                //    if((int)drow["Idx"] == idx)
                //    {
                //        Viewworklistchangenumber = (int)drow["Number"];
                //    }
                //}

                Viewworklist = await GetWorkListModifyAsycn(idx);

                ViewDetailhtml = HttpUtility.HtmlDecode(Viewworklist.Rows[0]["DetailHtml"].ToString());

                Downloadnotfile = (Viewworklist.Rows[0]["Fileform"].ToString() == "") ? false : true;

                Savefileprogress = "";

                //obj.F01_Button.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(GetViewWorkList)");
            }
        }

        //private RelayCommand<object> _CmdGetViewWorkIdxList;

        //public ICommand CmdGetViewWorkIdxList => _CmdGetViewWorkIdxList ?? (this._CmdGetViewWorkIdxList = new RelayCommand<object>(GetViewWorkIdxList));

        //private async void GetViewWorkIdxList(object obj)
        //{
        //    try
        //    {
        //        if(Viewworklistchangenumber == 1 || Viewworklistchangenumber == WorkListTotalcount)
        //        {
        //            MessageBox.Show("마지막 데이터 입니다.");
        //            return;
        //        }
        //        else
        //        {
        //            if((int)obj == 1)
        //            {
        //                Viewworklistchangenumber++;
        //            }
        //        }



        //        int idx = Convert.ToInt32(obj.F01_Idx);

        //        Viewworklist = await GetWorkListModifyAsycn(idx);

        //        ViewDetailhtml = HttpUtility.HtmlDecode(Viewworklist.Rows[0]["DetailHtml"].ToString());

        //        Downloadnotfile = (Viewworklist.Rows[0]["Fileform"].ToString() == "") ? false : true;

        //        Savefileprogress = "";

        //        //obj.F01_Button.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(GetViewWorkList)");
        //    }
        //}

        #endregion


        #region WindwClear

        private RelayCommand<Window> _CmdWindowClear;

        public ICommand CmdWindowClear => _CmdWindowClear ?? (this._CmdWindowClear = new RelayCommand<Window>(WindwClear));

        private void WindwClear(Window obj)
        {
            if (obj.Name != "_ViewPopupWindow")
            {
                SetDataClear();
            }

            obj.Close();
        }

        #endregion

        #region SetPageNumberChange

        private RelayCommand<object> _CmdSetPageNumberChange;

        public ICommand CmdSetPageNumberChange => _CmdSetPageNumberChange ?? (this._CmdSetPageNumberChange = new RelayCommand<object>(SetPageNumberChange));

        private void SetPageNumberChange(object obj)
        {
            try
            {
                int setnextprevious = Convert.ToInt32(obj);

                SetPageChangeNumber = Pagenumber;

                if (setnextprevious == 1)
                {
                    SetPageChangeNumber = 1;
                }

                if (setnextprevious == 2)
                {
                    if (SetPageChangeNumber > 10)
                    {
                        SetPageChangeNumber -= 10;
                    }
                    else
                    {
                        return;
                    }
                }

                if (setnextprevious == 3)
                {
                    if (SetPageChangeNumber < (Totalpage - 10))
                    {
                        SetPageChangeNumber += 10;
                    }
                    else
                    {
                        SetPageChangeNumber = Totalpage;
                    }
                }

                if (setnextprevious == 4)
                {
                    SetPageChangeNumber = Totalpage;
                }

                SendIndex(SetPageChangeNumber);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(SetPageNumberChange)");
            }
        }

        #endregion


        #region TextChange

        private RelayCommand _CmdSetTextChange;

        public ICommand CmdSetTextChange => _CmdSetTextChange ?? (this._CmdSetTextChange = new RelayCommand(SetTextChange));

        private async void SetTextChange()
        {
            try
            {
                DataTable dt = F_02_Provider.GetWorkDetailid();

                int totalcount = dt.Rows.Count;

                //C1.WPF.RichTextBox.C1RichTextBox TextChange = new C1.WPF.RichTextBox.C1RichTextBox();

                await Task.Run(() =>
                {
                    foreach (DataRow drow in dt.Rows)
                    {
                        //TextChange.Html = HttpUtility.HtmlDecode(drow["Html"].ToString());

                        string rtf = "";

                        Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>

                            rtf = dd(drow)

                        )); ;

                        //string rtf = new RtfFilter().ConvertFromDocument(TextChange.Document).ToString();

                        int error = F_02_Provider.SetWorkDetailRtf(Convert.ToInt32(drow["Idx"]), rtf);

                        if (error == 1)
                        {
                            m_LOG.LOG(string.Format("Error Detail Idx : {0}", drow["Idx"].ToString()), "SetTextChange");
                        }

                        teststring = string.Format("{0} / {1}", drow["Idx"].ToString(), totalcount.ToString());
                    }

                });

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(SetTextChange)");
            }
        }

        private string dd(DataRow drow)
        {
            TextChange.Html = HttpUtility.HtmlDecode(drow["Html"].ToString());
            return new RtfFilter().ConvertFromDocument(TextChange.Document).ToString();
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

        private async Task<int> FileSave(string savepath,string downloadpath,bool savetype)
        {
            int check = 0;

            try
            {

                if(savetype)
                {
                    if (!(Filepath == "" || Filepath is null))
                    {

                        await Task.Run(() =>
                        {
                            check = FileCopy(Filepath, "\\\\HGFA-NAS2\\zZ.Temp\\석경민과장\\FileTest", savetype);
                            
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

            if(savetype)
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
                    if(IsFileLocked(new FileInfo(Savefilepath)))
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
            int errorcheck = 0;

            try
            {

                Fom.SaveFileDialog saveFileDialog = new Fom.SaveFileDialog();
                saveFileDialog.Filter = string.Format("File (*{0})|*{0}", Path.GetExtension(obj.ToString()));
                saveFileDialog.Title = "Export File";
                saveFileDialog.ShowDialog();

                if(saveFileDialog.FileName.Trim() == "")
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

                errorcheck = await FileSave(obj.ToString(), Downloadfilepath, false);

                if(errorcheck == 0)
                {
                    if (MessageBox.Show("다운로드를 완료 하였습니다.\r 파일을 오픈 할까요?", "FILE OPEN", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                    {
                        GetFileOpen(Downloadfilepath);
                    }
                }
                else
                {
                    MessageBox.Show("실패");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel Command(GetFilePath)");
            }

            Downloadlock = true;
        }


        #endregion



        #endregion
    }
}
