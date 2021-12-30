using System.Windows.Controls;
using System.Data;
using System.Windows.Input;

namespace YKCJ_EngineerReport.F_08.Cons
{
    /// <summary>
    /// F_08_01_UserBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F_08_01_UserBox : UserControl
    {

        public delegate void SendIntIndex(int index);
        public event SendIntIndex SendIndex;

        private readonly int userindex;

        public F_08_01_UserBox(DataRow model)
        {
            InitializeComponent();
            userindex = (int)model["Idx"];
            DataContext = model;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SendIndex(userindex);
        }
    }

}
