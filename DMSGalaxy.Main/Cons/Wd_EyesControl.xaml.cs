using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DMSGalaxy.Main.Cons
{
    /// <summary>
    /// Wd_EyesControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Wd_EyesControl : Window
    {
        private MainWindow Main = Application.Current.MainWindow as MainWindow;

        public Wd_EyesControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Eye.PupilSize = sliPupil.Value;
            Main.Eye.IrisSize = sliIris.Value;
            this.Close();
        }

    }
}
