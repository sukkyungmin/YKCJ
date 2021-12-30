using System;
using System.Data;
using YKCJViewModel.Common;
using System.Collections.ObjectModel;

namespace YKCJViewModel.F_07
{
    public class F_07_01_Model
    {

        // Select Part

        public DataTable _Machinelist;
        public DataTable _Modulepart1list;
        public DataTable _Modulepart2list;
        public DataTable _ClassList;

        public ObservableCollection<F_07_01_CommonModel> _Listitem;


        public int _Selectpart1;

        public int _Gridselectpart1idx;
        public string _Gridselectpart1topart2;

        // Modify Item

        public int _Selectmodifypart;
        public string _Selectmodifypartname;
        public string _Modifyidx;
        public string _Modifyname;

    }

    public class F_07_01_CommonModel : ObservableObject
    {
        private bool _Listitemcheck;

        public bool Listitemcheck
        {
            get { return _Listitemcheck; }
            set
            {
                if (_Listitemcheck != value)
                {
                    _Listitemcheck = value;
                    this.RaisePropertyChanged(() => this.Listitemcheck);
                }
            }
        }


        private string _Listitemname;

        public string Listitemname
        {
            get { return _Listitemname; }
            set
            {
                if (_Listitemname != value)
                {
                    _Listitemname = value;
                    this.RaisePropertyChanged(() => this.Listitemname);
                }
            }
        }

        private string _Saveitemname;

        public string Saveitemname
        {
            get { return _Saveitemname; }
            set
            {
                if (_Saveitemname != value)
                {
                    _Saveitemname = value;
                    this.RaisePropertyChanged(() => this.Saveitemname);
                }
            }
        }
    }
}
