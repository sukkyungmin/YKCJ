using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

using DMSGalaxy.Common.Utils;
using DMSGalaxy.ViewModel.Common;

namespace DMSGalaxy.ViewModel.Infos
{
     public class VersionInfos : ObservableObject
    {

         CommonUtil comutil = new CommonUtil();

         public VersionInfos()
         {
             Version_dataTable = comutil.DMSGVersionInfos(2);
         }

         public void VersionInfosReset()
         {
             Listviewreturnnumber = Version_dataTable.Rows[0]["Number"].ToString();
             Listviewreturndcs = Version_dataTable.Rows[0]["Description"].ToString();
         }
             
        /// <summary>
        /// Search Grid Data
        /// </summary>

        private DataTable dt = new DataTable();

        public DataTable Version_dataTable
        {
            get { return dt; }
            set
            {
                if (dt != value || dt != null)
                {
                    dt = value;
                    RaisePropertyChanged(() => this.Version_dataTable);
                }
            }
        }

        private string _listviewreturnnumber = "";

         public string Listviewreturnnumber
        {
            get { return _listviewreturnnumber; }
            set
            {
                if (_listviewreturnnumber != value || _listviewreturnnumber != null)
                {
                    _listviewreturnnumber = value;

                    _listviewreturnnumber = (_listviewreturnnumber != "") ? "Ver. " + value : value;

                    RaisePropertyChanged(() => this.Listviewreturnnumber);
                }
            }
        }

         private string _listviewreturndsc= "";

         public string Listviewreturndcs
         {
             get { return _listviewreturndsc; }
             set
             {
                 if (_listviewreturndsc != value || _listviewreturndsc != null)
                 {
                     _listviewreturndsc = value;
                     RaisePropertyChanged(() => this.Listviewreturndcs);
                 }
             }
         }

        #region Close Window

        private RelayCommand<Window> _CmWindowClose;

        private void WindowClose(Window wd)
        {
            wd.Close();
            Listviewreturnnumber = "";
            Listviewreturndcs = "";
        }

        bool CanWindowClose(Window wd)
        {
            return true;
        }

        public ICommand CmWindowClose
        {
            get
            {
                return _CmWindowClose ?? (this._CmWindowClose =
                    new RelayCommand<Window>(WindowClose, CanWindowClose));
            }
        }

        #endregion

        #region Return Listview 

        private RelayCommand<toString> _CmReturnText;

        private void ReturnText(toString obj)
        {
            Listviewreturnnumber = obj.Text1;
            Listviewreturndcs = obj.Text2;
        }

        bool CanReturnText(toString obj)
        {
            return true;
        }

        public ICommand CmReturnText
        {
            get
            {
                return _CmReturnText ?? (this._CmReturnText =
                    new RelayCommand<toString>(ReturnText, CanReturnText));
            }
        }

        #endregion

    }
}
