using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System.Collections.ObjectModel;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using ClosedXML.Excel;
using System.Threading;

using DMSGalaxy.ViewModel.Common;
using DMSGalaxy.DBConnection.F2;
using DMSGalaxy.Common.Utils;
using DMSGalaxy.Common.Helper;

namespace DMSGalaxy.ViewModel.F2
{
    public class F2_01_ViewModel : ObservableObject
    {

        private F2_01Provider F2_Provider = null;

        public F2_01_ModelProduction P { get; set; }

        private CommonUtil C_util = new CommonUtil();

        private LOG_Write m_LOG = new LOG_Write();

        public F2_01_ViewModel()
        {
            F2_Provider = new F2_01Provider();
            P = new F2_01_ModelProduction() { Sqlrowperpage = 3, Sqlpagenumber = 0};
            MCnumber = "미 선택";
        }

        private string _mcnumber;

        public string MCnumber
        {
            get { return _mcnumber; }
            set
            {
                if (_mcnumber != value)
                {
                    _mcnumber = value;
                    RaisePropertyChanged(() => this.MCnumber);
                    if (_mcnumber != "미 선택")
                    {
                        P.Sqlpagenumber = 0;
                        DBDataSelectProduction();
                    }
                }
            }
        }

        private void DBDataSelectProduction()
        {
            try
            {

                if (!C_util.IPCheck())
                {
                    MessageBox.Show("현재 네트워크 또는 SERVER PC를 확인 하십시요.");
                    return;
                }

                DataTable dt = F2_Provider.GetF1_01Product("", "", "", MCnumber.Substring(3), P.Sqlrowperpage, P.Sqlpagenumber);
                if(dt.Rows.Count < 3)
                {
                    MessageBox.Show("마지막 데이터 입니다.");
                    return;
                }

                P.F2_dataTable = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel");
            }
            finally
            {
                //Login _Login = new Login();
                //_Login.UserStatusReset(UserInfo.US_ID, false);
            }
        }

        //private void capynextpage()
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (P.F2_dataTable.Rows[i + P.Dbrowindex] == null)
        //            return;

        //        switch (i)
        //        {
        //            case 0:
        //                P.DataTableCapy1 = P.F2_dataTable.Clone();
        //                P.DataTableCapy1.ImportRow(P.F2_dataTable.Rows[i + P.Dbrowindex]);
        //                P.MCnumber = string.Format("{0}      {1}", P.DataTableCapy1.Rows[0]["TOTAL_COUNT"], P.DataTableCapy1.Rows.Count);
        //                break;
        //        }

        //    }
        //}

        #region ICommand List

        private RelayCommand _Cmpreviouspage;

        private void previouspage()
        {
            try
            {
                if(P.Sqlpagenumber == 0)
                {
                    MessageBox.Show("가장 최신의 데이터 입니다.");
                    return;
                }
                    
                P.Sqlpagenumber -= 1;

                DBDataSelectProduction();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel");
            }
            finally
            {

            }
        }

        bool Canpreviouspage()
        {
            return true;
        }

        public ICommand Cmpreviouspage
        {
            get
            {
                return _Cmpreviouspage ?? (this._Cmpreviouspage =
                    new RelayCommand(previouspage, Canpreviouspage));
            }
        }


        private RelayCommand _Cmnextpage;

        private void nextpage()
        {
            try
            {

                P.Sqlpagenumber += 1;
                DBDataSelectProduction();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel");
            }
            finally
            {

            }
        }

        bool Cannextpage()
        {
            return true;
        }

        public ICommand Cmnextpage
        {
            get
            {
                return _Cmnextpage ?? (this._Cmnextpage =
                    new RelayCommand(nextpage, Cannextpage));
            }
        }


        private RelayCommand _Cmresetpage;

        private void resetpage()
        {
            try
            {

                P.Sqlpagenumber = 0;
                DBDataSelectProduction();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F2_01_ViewModel");
            }
            finally
            {

            }
        }

        bool Canresetpage()
        {
            return true;
        }

        public ICommand Cmresetpage
        {
            get
            {
                return _Cmresetpage ?? (this._Cmresetpage =
                    new RelayCommand(resetpage, Canresetpage));
            }
        }

        #endregion

    }
}
