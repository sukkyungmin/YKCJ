using System;
using System.Data;
using System.Windows;

namespace YKCJViewModel.F_01
{
    public class F_01_01_Model
    {
        public ReportsItems _Reportitems = new ReportsItems();

        public DateTime _Searchdate;
        public string _Searchdatestring;

        public string _LoginUses;

        public bool _Searchdaily;
        public bool _Searchweekly;
        public bool _Searchmonthly;

        public DataTable _Machinelist;
        public DataTable _Modulepart1list;
        public DataTable _Modulepart2list;
        public DataTable _ClassList;
        public DataTable _ShiftList;
        public DataTable _UsernameList;


        public DataTable _Times;

        //Search Work List
        public DataTable _WorkList;
        public DataTable _WorkListidx;
        public DataTable _WorkListtimesum;
        public int _WorkListTotalcount;
        public int _WorkListcurrentidx;

        //Search Work List Filter
        //public string _Filtertext;
        //public string _Filtershift;
        //public string _Filtermachine;
        //public string _Filterclass;
        //public string _Filterpart1;
        //public string _Filterpart2;
        //public DataTable _FilterModulepart2list;

        //Report Next, Previous Button Visible Mode  0 = Total Count 1,  1 = Last Next, 2 = Next Previous, 3 = Last Previous 
        public int _ReportNxPvBtnVbMode;


        // 0 = Save , 1 = Search
        public bool _ReportSavemode;

        public bool _Reportloadprogress;

    }

    public class ReportsItems
    {
        // Save Work Item
        public DateTime _Eventdatetime;
        public string _Eventdatetimestring;

        public string _Username;
        public string _Usershiftname;
        public string _Title;
        public string _Detail;
        public string _Detailhtml;
        public string _Detailrtf;
        public string _Selectmachine;
        public string _Selectpart1;
        public string _Selectpart2;
        public string _Selectclass;
        public string _Selectshift;
        public string _Selectusername;
        public string _Filepath;

        //public string _Selectshift;

        public int _Workidx;
        public int _Checktime;
        public int _Delaytime;

        // File Save Download
        public long _Savefiletotalsize;
        public long _Savefilesize;
        public string _Savefileprogress;
        public string _Savefilepath;
        public string _Downloadfilepath;
        public bool _Downloadlock;

        public string _Savefilenamedisplay;
        public string _Savefilesizedisplay;
        public string _Savefileformdisplay;

        // File image
        //public Visibility _Image;
        public Visibility _Icon;

        // Work List Save Mode : 0 = save , 1 = modify
        public bool _Modifymode;

        // Save Lock
        public bool _Savelock;
    }
}
