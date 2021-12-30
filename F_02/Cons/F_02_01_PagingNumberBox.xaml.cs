using System.Windows.Controls;
using System.Windows.Input;
using YKCJViewModel.F_02;

namespace YKCJ_EngineerReport.F_02.Cons
{
    /// <summary>
    /// F_02_01_PagingNumberBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F_02_01_PagingNumberBox : UserControl
    {
        public delegate void SendIntIndex(int index);
        public event SendIntIndex SendIndex;

        private readonly int pageindex;

        public F_02_01_PagingNumberBox(F_02_01_ViewModel model, int number)
        {
            InitializeComponent();
            _TbPageNo.Text = number.ToString();
            pageindex = number;
            DataContext = model;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SendIndex(pageindex);
        }
    }
}
