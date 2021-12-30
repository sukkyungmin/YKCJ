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
using System.Windows.Media.Animation;

namespace DMSGalaxy.Main.Cons
{
    /// <summary>
    /// Wd_PopSlideReport.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Wd_PopSlideReport : Window
    {

        public delegate void SendPopSlideReport(bool ClosePopBool);
        public event SendPopSlideReport SendMsg;
        double GridMinSize;
        double GridMaxSize;
        double GridWidth;

        public Wd_PopSlideReport(UserControl UserControls, double _Width, double _Height, double MaxSize)
        {
            InitializeComponent();

            this.Activate();
            //this.Topmost = true;

            MainPop_UserControl.Height = _Height;  // Windows Screen Size
            MainPop_UserControl.Width = _Width; 

            grid_main.Height = _Height;  // Slide Gird Screen Height Size
            grid_main.Width = MaxSize;  // Slide Grid Max Width Size

            GridWidth = _Width;
            GridMaxSize = MaxSize;  // UserControl Grid Size Max
            GridMinSize = 0;

            grid_main.Children.Add(UserControls);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NameScope.SetNameScope(this, new NameScope());
            this.RegisterName(Canvas_Back.Name, Canvas_Back);
        }

        private void Close_Cleck(object sender, RoutedEventArgs e)
        {
            this.SendMsg(true);
            this.Close();
        }

        private void _SlideWindowAnimation(double Start, double End)
        {
            try
            {
            SlideButtonVisibility(_BtLeft, _BtRight, System.Windows.Visibility.Hidden);
            
            DoubleAnimation slideWindowAnimation = new DoubleAnimation
            {
                From = Start,
                To = End,
                Duration = new Duration(TimeSpan.FromMilliseconds(2000)),
                AccelerationRatio = 0,
                DecelerationRatio = 0.5,
                FillBehavior = FillBehavior.Stop
                //AutoReverse = true
            };

            slideWindowAnimation.Completed += ((sender, e) => { Canvas.SetLeft(Canvas_Back, End);
                                                                                                                                        SlideButtonVisibility(_BtLeft, _BtRight, System.Windows.Visibility.Visible);});

            Canvas_Back.BeginAnimation(Canvas.LeftProperty, slideWindowAnimation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SlideLeft_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GridMinSize == (-GridMaxSize + GridWidth))
                    return;
                else
                    _SlideWindowAnimation(GridMinSize, (GridMinSize - GridWidth));
                GridMinSize = GridMinSize - GridWidth;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SlideRight_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GridMinSize == 0)
                    return;
                else
                    _SlideWindowAnimation(GridMinSize, (GridMinSize + GridWidth));
                GridMinSize = GridMinSize + GridWidth;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void SlideButtonVisibility(Button Left , Button Right , Visibility Vb)
        {
            Left.Visibility = Vb;
            Right.Visibility = Vb;
        }

    }
}
