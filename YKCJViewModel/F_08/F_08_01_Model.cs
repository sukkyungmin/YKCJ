using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Media.Imaging;

namespace YKCJViewModel.F_08
{
    public class F_08_01_Model 
    {
        //New & Modify User
        public string _Userid;
        public string _Userpw;
        public string _Userpwcheck;
        public string _Username;
        public string _Userjob;
        public string _Userimagepath;
        public ObservableCollection<F_08_01_CommonModel> _Useritem;

        public DataTable _Userlist;

        public bool _SaveMode;
        public string _Userpwconfimation;

        //Modify User 
        public int _Useridx;
        public BitmapImage _Userbitmapimage;
        public byte[] _UserModifybyte;

    }
}
