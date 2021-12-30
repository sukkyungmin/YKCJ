using System;
using System.Windows;
using System.Windows.Controls;
using YKCJ_EngineerReport.F_02.Cons;
using YKCJ_EngineerReport.F_02.Wds;
using YKCJViewModel.F_02;

namespace YKCJ_EngineerReport.F_02
{
    /// <summary>
    /// F_02_01.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F_02_01 : Page
    {

        private MainWindow Main = Application.Current.MainWindow as MainWindow;

        private F_02_01_ViewModel Viewmodel = new F_02_01_ViewModel();
        private F_02_01_SavePopup SavePopup = null;
        private F_02_01_ViewPopup ViewPopup = null;
        private F_02_01_ReportPopup ReportPopup = null;

        public F_02_01_PagingNumberBox[] Con_PagingNumberBox = null;
        public F_02_01()
        {
            InitializeComponent();

            Viewmodel.SendIndex += new F_02_01_ViewModel.SenIntIndex(SetUserListChange);
            DataContext = Viewmodel;
        }

        private void RowDetailsVisibleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _c1flexgrid.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.Visible;
        }

        private void RowDetailsCollapsedButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _c1flexgrid.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.Collapsed;
        }

        private async void ModifyReportButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await Dispatcher.BeginInvoke(new Action(() => SetPopup()));
        }

        private async void ViewReportButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await Dispatcher.BeginInvoke(new Action(() => SetViewPopup()));
        }

        private async void ViewerReportButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await Dispatcher.BeginInvoke(new Action(() => SetReportPopup()));
        }

        private void SetPopup()
        {
            SavePopup = new F_02_01_SavePopup(Viewmodel)
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
                Owner = this.Main
            };

            SavePopup.ShowDialog();
        }

        private void SetViewPopup()
        {
            ViewPopup = new F_02_01_ViewPopup(Viewmodel)
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
                Owner = this.Main
            };

            ViewPopup.ShowDialog();
        }

        private void SetReportPopup()
        {
            ReportPopup = new F_02_01_ReportPopup(Viewmodel)
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
                Owner = this.Main
            };

            ReportPopup.ShowDialog();
        }
        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            await Dispatcher.BeginInvoke(new Action(() => SetPageNumber()));
            //SetPageNumber();
        }

        private void SetPageNumber()
        {
            try
            {
                Viewmodel.WorkListTotalcount = Viewmodel.GetWorkListRows();

                Viewmodel.Totalpage = Viewmodel.WorkListTotalcount / 18;

                if (Viewmodel.WorkListTotalcount % 18 > 0)

                    Viewmodel.Totalpage++;

                if (Viewmodel.Totalpage > 0)
                {

                    Viewmodel.PagingStaEndmoed = (Viewmodel.Totalpage > 10) ? (Viewmodel.Totalpage <= 20) ? 1 : 2 : 0;

                    //Viewmodel.PagingStaEndlastvisible = (idx <= 10 && Viewmodel.Totalpage > 10) ? 1 : (idx > 10 && changenumberarray > (Viewmodel.Totalpage - 10)) ? 0 : 2;

                    _Wrap_pagingnumber.Children.Clear();

                    Con_PagingNumberBox = new F_02_01_PagingNumberBox[Viewmodel.Totalpage];

                    int firstnumber = (Viewmodel.Totalpage < 10) ? Viewmodel.Totalpage : 10;

                    for (int i = 0; i < Viewmodel.Totalpage; i++)
                    {
                        Con_PagingNumberBox[i] = new F_02_01_PagingNumberBox(Viewmodel, i + 1);
                        Con_PagingNumberBox[i].SendIndex += new F_02_01_PagingNumberBox.SendIntIndex(SetUserList);
                    };

                    for (int i = 0; i < firstnumber; i++)
                    {
                        _Wrap_pagingnumber.Children.Add(Con_PagingNumberBox[i]);
                    };

                    Con_PagingNumberBox[0]._LbHidden.Content = "1";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void SetUserList(int idx)
        {
            Viewmodel.Pagenumber = idx;

            for (int i = 0; i < Viewmodel.Totalpage; i++)
            {
                Con_PagingNumberBox[i]._LbHidden.Content = i == (idx - 1) ? "1" : "0";
            }
        }

        private void SetUserListChange(int idx)
        {
            try
            {

                _Wrap_pagingnumber.Children.Clear();

                float buttonarray = ((float)idx / 10);

                int changenumberarray = ((buttonarray * 10) == idx) ? ((int)Math.Truncate(buttonarray) - 1) * 10 : (int)Math.Truncate(buttonarray) * 10;

                int lastarray = (idx == Viewmodel.Totalpage || changenumberarray > (Viewmodel.Totalpage - 10)) ? Viewmodel.Totalpage : changenumberarray + 10;

                //Viewmodel.PagingStaEndlastvisible = (idx <= 10 && Viewmodel.Totalpage > 10) ? 1 : (idx > 10 && changenumberarray > (Viewmodel.Totalpage - 10)) ? 0 : 2;

                for (int i = changenumberarray; i < lastarray; i++)
                {
                    _Wrap_pagingnumber.Children.Add(Con_PagingNumberBox[i]);
                };

                SetUserList(idx);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
