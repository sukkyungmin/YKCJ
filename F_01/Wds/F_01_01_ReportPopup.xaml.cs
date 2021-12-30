using C1.WPF.FlexReport;
using C1.WPF.Document.Export;
using System;
using System.Windows;
using System.Data;
using YKCJViewModel.F_01;

using Common.Utils;
using System.IO;
using Common.Infos;

namespace YKCJ_EngineerReport.F_01.Wds
{
    /// <summary>
    /// F_01_01_ReportPopup.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F_01_01_ReportPopup : Window
    {

        private CommonUtil utils = new CommonUtil();

        public F_01_01_ReportPopup(F_01_01_ViewModel model)
        {
            InitializeComponent();
            ReportLoad(model);
        }

        //현재 UI관련하여 BeginInvoke를 사용하였으나 나중에 속도에 별 차이가 없으면 SetReport 그대로 사용해도 무방함. 
        private async void ReportLoad(F_01_01_ViewModel Viewmodel)
        {
            await Dispatcher.BeginInvoke(new Action(() => SetReport(Viewmodel)));
        }

        private async void SetReport(F_01_01_ViewModel Viewmodel)
        {
            try
            {
                DataTable dt = await Viewmodel.GetWorkListReportAsycn();
                //string Reportpath = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Reports\Reports.flxr";
                C1FlexReport rep = new C1FlexReport();
                rep.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Reports\Reports1.flxr", "Reports");
                rep.Parameters["CreateID"].Value = UserInfo.US_NM;
                rep.Parameters["SearchDate"].Value = Viewmodel.Searchdatestring;

                rep.DataSource.Recordset = dt;
                //rep.DataSource.Recordset.add

                fv.DocumentSource = rep;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
