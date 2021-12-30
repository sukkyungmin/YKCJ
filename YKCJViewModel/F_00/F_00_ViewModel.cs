using C1.WPF.RichTextBox.Documents;
using Common.Helper;
using Common.Infos;
using Common.Utils;
using DBConnection.F_00;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Input;
using YKCJViewModel.Common;

namespace YKCJViewModel.F_00
{
    public class F_00_ViewModel : ObservableObject
    {
        private F_00_Model Model = new F_00_Model();
        private F_00_Provider F_Provider = new F_00_Provider();
        private LOG_Write m_LOG = new LOG_Write();
        public F_00_ViewModel()
        {
            SetMainHomeData();
        }


        private async void SetMainHomeData()
        {
            try
            {
                await Task.Run(() => GetData());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F_00_ViewModel (SetMainHomeData)");
            }

        }

        private void GetData()
        {
            DateTime Cdate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 7, 0, 0);

            DateTime Sdate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 7, 0, 0);

            DateTime Edate = (DateTime.Now >= Cdate) ? Cdate : Cdate.AddDays(-1);

            GetDataTime = string.Format("{0} ~ {1}", Sdate.ToString("yyyy-MM-dd HH:mm"), Edate.ToString("yyyy-MM-dd HH:mm"));


            //DataSet dst = F_Provider.GetMainHomeData(Sdate.ToString("yyyy-MM-dd HH:mm:ss"), Edate.ToString("yyyy-MM-dd HH:mm:ss"));

            DataSet dst = F_Provider.GetMainHomeData("2019-01-01 07:00:00", "2019-02-01 07:00:00");

            MonthlyTotalData = dst.Tables[0];
            MonthlyTop5TotalData = dst.Tables[1];

            PieChartView = dst.Tables[2].AsDataView();
            LineChartView = dst.Tables[3].AsDataView();
        }


        public DataTable MonthlyTotalData
        {
            get { return Model._MonthlyTotalData; }
            set
            {
                if (Model._MonthlyTotalData != value)
                {
                    Model._MonthlyTotalData = value;
                    RaisePropertyChanged(() => this.MonthlyTotalData);
                }
            }
        }

        public DataTable MonthlyTop5TotalData
        {
            get { return Model._MonthlyTop5TotalData; }
            set
            {
                if (Model._MonthlyTop5TotalData != value)
                {
                    Model._MonthlyTop5TotalData = value;
                    RaisePropertyChanged(() => this.MonthlyTop5TotalData);
                }
            }
        }

        public DataView LineChartView
        {
            get { return Model._LineChartView; }
            set
            {
                if (Model._LineChartView != value)
                {
                    Model._LineChartView = value;
                    RaisePropertyChanged(() => this.LineChartView);
                }
            }
        }

        public DataView PieChartView
        {
            get { return Model._PieChartView; }
            set
            {
                if (Model._PieChartView != value)
                {
                    Model._PieChartView = value;
                    RaisePropertyChanged(() => this.PieChartView);
                }
            }
        }

        public string GetDataTime
        {
            get { return Model._GetDataTime; }
            set
            {
                if (Model._GetDataTime != value)
                {
                    Model._GetDataTime = value;
                    RaisePropertyChanged(() => this.GetDataTime);
                }
            }
        }

        private RelayCommand _CmdDataReset;

        public ICommand CmdDataReset => _CmdDataReset ?? (this._CmdDataReset = new RelayCommand(DataReset));

        private void DataReset()
        {
            SetMainHomeData();
        }
    }
}
