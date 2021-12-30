using C1.WPF.FlexReport;
using C1.WPF.Document.Export;
using System;
using System.Windows;
using System.Data;
using YKCJViewModel.F_02;

using Common.Utils;
using System.IO;

namespace YKCJ_EngineerReport.F_02.Wds
{
    /// <summary>
    /// F_02_01_ReportPopup.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F_02_01_ReportPopup : Window
    {

        private CommonUtil utils = new CommonUtil();

        public F_02_01_ReportPopup(F_02_01_ViewModel model)
        {
            InitializeComponent();
            ReportLoad(model);
        }

        //현재 UI관련하여 BeginInvoke를 사용하였으나 나중에 속도에 별 차이가 없으면 SetReport 그대로 사용해도 무방함. 
        private async void ReportLoad(F_02_01_ViewModel Viewmodel)
        {
            await Dispatcher.BeginInvoke(new Action(() => SetReport(Viewmodel)));
        }

        private void SetReport(F_02_01_ViewModel Viewmodel)
        {
            try
            {

                //string Reportpath = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Reports\Reports.flxr";
                C1FlexReport rep = new C1FlexReport();
                rep.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Reports\Reports1.flxr", "Reports");
                rep.Parameters["CreateID"].Value = Viewmodel.Username;
                rep.Parameters["SearchDate"].Value = Viewmodel.Searchtime;

                rep.DataSource.Recordset = Viewmodel.AllWorkList;

                //var ds = new C1.WPF.FlexReport.DataSource();
                //ds.Recordset = Viewmodel.WorkList;

                //rep.DataSources.Add(ds);

               

                //MemoryStream ms = new MemoryStream();
                //using (PdfFilter pdf = new PdfFilter())
                //{
                //    pdf.Stream = ms;
                //    rep.RenderToFilter(pdf);
                //};

                
              


                fv.DocumentSource = rep;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
