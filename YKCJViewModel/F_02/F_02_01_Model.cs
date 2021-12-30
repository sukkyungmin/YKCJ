using System;
using System.Data;

namespace YKCJViewModel.F_02
{
    public class F_02_01_Model
    {
        public DateTime _Stime = DateTime.Now.AddDays(-1);
        public DateTime _Etime = DateTime.Now;
        public string _Searchtime;
        public F_02_01_PopupModel _PopModel = new F_02_01_PopupModel();
        public DataTable _Machinelist;
        public DataTable _Modulepart1list;
        public DataTable _Modulepart2list;
        public DataTable _ClassList;
        public DataTable _ShiftList;
        public DataTable _WhereshiftList;

        //Search Work List
        public DataTable _WorkList;
        public DataTable _AllWorkList;
        public int _WorkListTotalcount;

        //Search Work List Modify
        public DataTable _WorklistModify;

        //Search View Work List
        public DataTable _Viewworklist;
        public DataTable _Viewworkidxlist;
        public string _ViewDetailhtml;
        public int _Viewworklistchangenumber;

        public string _F2_Test;

        //Search Work List Filter
        public string _Filtertext;
        public string _Filtershift;
        public string _Filtermachine;
        public string _Filterclass;
        public string _Filterpart1;
        public string _Filterpart2;
        public DataTable _FilterModulepart2list;

        //Search Work List Paging Number
        public int _Pagenumber;
        public int _Totalpage;

        // 0 = Page 1개 , 1 = 1 < Page >= 10 , 2 = Page 10 이상 
        public int _PagingStaEndmoed;

        // 0 = Left Visible ,  1 = Right Visible ,  2 =All Visible
        public int _PagingStaEndlastvisible;

        public string _teststring;

        public bool _Rowdetailsvisibilitymode;

    }
}
