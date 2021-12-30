using System;

namespace YKCJViewModel.F_02
{
    public class F_02_01_PopupModel
    {
        public int _Modifyworklistidx;
        public string _Username;
        public string _Modifyusername;
        public string _Title;
        public string _Detail;
        public string _DetailHtml;
        public string _Detailrtf;
        public string _Selectmachine;
        public string _Selectpart1;
        public string _Selectpart2;
        public string _Selectclass;
        public string _Selectshift;
        public string _Filepath;
        public DateTime _Eventdatetime;
        public int _Checktime;
        public int _Delaytime;

        // File Save Download
        public long _Savefiletotalsize;
        public long _Savefilesize;
        public string _Savefileprogress;
        public string _Savefilepath;
        public string _Downloadfilepath;
        public bool _Downloadlock;
        public bool _Downloadnotfile;



        // Work List Save Mode : 0 = save , 1 = modify
        public bool _Modifymode;

        // Save Lock
        public bool _Savelock;
    }
}
