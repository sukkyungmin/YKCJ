using System.Collections.ObjectModel;

using YKCJViewModel.Common;

namespace YKCJViewModel.F_08
{
    public class F_08_01_CommonModel : ObservableObject
{
        private bool _Useritemcheck;

        public bool Useritemcheck
        {
            get { return _Useritemcheck; }
            set
            {
                if (_Useritemcheck != value)
                {
                    _Useritemcheck = value;
                    this.RaisePropertyChanged(() => this.Useritemcheck);
                }
            }
        }


        private string _Useritemname;

        public string Useritemname
        {
            get { return _Useritemname; }
            set
            {
                if (_Useritemname != value)
                {
                    _Useritemname = value;
                    this.RaisePropertyChanged(() => this.Useritemname);
                }
            }
        }
    }
}
