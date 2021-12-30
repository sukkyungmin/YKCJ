using DMSGalaxy.ViewModel.Common;
using System.Data;

namespace DMSGalaxy.ViewModel.F2
{
    public class F2_01_ModelProduction : ObservableObject
    {

        private int _sqlrowperpage;
        public int Sqlrowperpage
        {
            get { return _sqlrowperpage; }
            set
            {
                if (_sqlrowperpage != value)
                {
                    _sqlrowperpage = value;
                    RaisePropertyChanged(() => this.Sqlrowperpage);
                }
            }
        }

        private int _sqlpagenumber;
        public int Sqlpagenumber
        {
            get { return _sqlpagenumber; }
            set
            {
                if (_sqlpagenumber != value)
                {
                    _sqlpagenumber = value;
                    RaisePropertyChanged(() => this.Sqlpagenumber);
                }
            }
        }


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

    }
}
