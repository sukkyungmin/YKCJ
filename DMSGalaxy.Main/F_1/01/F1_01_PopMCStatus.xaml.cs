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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using System.Data;
using System.Windows.Media.Animation;

using DMSGalaxy.Common.Animators;
using DMSGalaxy.DBConnection.F1;

namespace DMSGalaxy.Main.F_1._01
{
    /// <summary>
    /// F1_01_PopMCStatus.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F1_01_PopMCStatus : Window
    {
        private MainWindow Main = Application.Current.MainWindow as MainWindow;
        private F1_01Provider dp_F1 = null;

        double orginalWidth, originalHeight;
        ScaleTransform scale = new ScaleTransform();

        bool GridPosition = false;
        int Timer = 0;
        int MCIndex;

        private  DataTable dtble = null;

        private DispatcherTimer timer = new DispatcherTimer();

        public F1_01_PopMCStatus(DataRow Row , DataTable dt , int Index)
        {
            InitializeComponent();

            //this.Topmost = true;
            this.Activate();

            MCIndex = Index;
            dtble = dt;

            SetData(Row);

            _TbUpdateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            dp_F1 = new F1_01Provider();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            //ClipBasedAnimation(grid_main, "Block");
        }

        private void ClipBasedAnimation(Grid _grid , string SelectClip)
        {
            if (SelectClip == "Circle")
            {
                CircleAnimation circleAnimationHelper = new CircleAnimation();
                //circleAnimationHelper.AnimationCompleted += new Action(circleAnimationHelper_AnimationCompleted);
                circleAnimationHelper.MakeCircleAnimation((FrameworkElement)_grid, _grid.Width, _grid.Height, new TimeSpan(0, 0, 1));
            }
            else if (SelectClip == "Radial")
            {
                RadialAnimation radialAnimationHelper = new RadialAnimation();
                //radialAnimationHelper.AnimationCompleted += new Action(circleAnimationHelper_AnimationCompleted);
                radialAnimationHelper.MakeRadiaAnimation((FrameworkElement)_grid, _grid.Width, _grid.Height, new TimeSpan(0, 0, 1));
            }
            else if (SelectClip == "Block")
            {
                BlockAnimation blockAnimationHelper = new BlockAnimation();
                //blockAnimationHelper.AnimationCompleted += new Action(circleAnimationHelper_AnimationCompleted);
                blockAnimationHelper.MakeBlockAnimation((FrameworkElement)_grid, _grid.Width, _grid.Height, new TimeSpan(0, 0, 1));
            }
            else if (SelectClip == "Interlace")
            {

                InterlacedAnimation interlacedAnimation = new InterlacedAnimation();
                //interlacedAnimation.AnimationCompleted += new Action(circleAnimationHelper_AnimationCompleted);
                interlacedAnimation.MakeInterlacedAnimation((FrameworkElement)_grid, _grid.Width, _grid.Height, new TimeSpan(0, 0, 1));
            }

            else if (SelectClip == "WaterFall")
            {

                WaterFallAnimation WaterFall = new WaterFallAnimation();
                //WaterFall.AnimationCompleted += new Action(circleAnimationHelper_AnimationCompleted);
                WaterFall.MakeWaterFallAnimation((FrameworkElement)_grid, _grid.Width, _grid.Height, new TimeSpan(0, 0, 1));
            }
        }

        private void SetData(DataRow Row)
        {
            try
            {
                _TkMCName.Text = (Row["MCNum"].ToString().TrimEnd() != "10") ? "CA 0" + Row["MCNum"].ToString().TrimEnd() : "CA " + Row["MCNum"].ToString().TrimEnd();
                _TkLine.Text = Row["Line"].ToString().TrimEnd();
                _TkStart.Text = Row["Year"].ToString().TrimEnd();
                _TkSKUs.Text = "65";            //추후 DB쿼리로 Product List를 가져와야함 임시로  65값 할당
                _TkManufacturer.Text = Row["Asset"].ToString().TrimEnd();
                _TkProductmm.Text = Row["mm"].ToString().TrimEnd();
                _TkDesignSpeed.Text = Row["DesignSpeed"].ToString().TrimEnd();
                _TkProductName.Text = Row["Name"].ToString().TrimEnd();
                _TkProductCode.Text = Row["Code"].ToString().TrimEnd();
                _TkProductPad.Text = string.Format("{0:N0}", Convert.ToInt32(Row["CPad"].ToString().TrimEnd()));
                _TkProductBox.Text = string.Format("{0:N0}", Convert.ToInt32(Row["CBox"].ToString().TrimEnd()));
                _TkTargetBox.Text = string.Format("{0:N0}", Convert.ToInt32(Row["TBox"].ToString().TrimEnd()));
                _TkfinishTime.Text = Row["Time"].ToString().TrimEnd();
                Rgbar(_RgBoxGraph, Convert.ToInt16(Row["Box%"].ToString().TrimEnd()), _TkBoxPer, _TkPer);
                _TkSafety.Text = Row["Safety"].ToString().TrimEnd();
                _TkTemp.Text = Row["Temprature"].ToString().TrimEnd();
                _TkHumidity.Text = Row["Humidity"].ToString().TrimEnd();
                _TktVOC.Text = Row["Carbon"].ToString().TrimEnd();
                _TkSpeed.Text = Row["CnSpeed"].ToString().TrimEnd();
                _TkTSpeed.Text = Row["TgSpeed"].ToString().TrimEnd();
                _SpeedValues.EndAngle = ConvertRange(0, Convert.ToInt32(Row["TgSpeed"].ToString().TrimEnd()) + 200, -140, 80,
                                                                                                                                           Convert.ToInt32(Row["CnSpeed"].ToString().TrimEnd()));
                _TgSpeedValues.EndAngle = ConvertRange(0, Convert.ToInt32(Row["TgSpeed"].ToString().TrimEnd()) + 200, -140, 80,
                                                                                                                               Convert.ToInt32(Row["TgSpeed"].ToString().TrimEnd()));
                _TkAirconditioner.Text = (Row["Airconditioner"].ToString().TrimEnd() == "1") ? "ON" : "OFF";
                _TkMCElectricPower.Text = Row["MCElectricPower"].ToString().TrimEnd();
                _TkAir.Text = Row["AIR"].ToString().TrimEnd();
                ImageChange(_IgBattery, _TkBattery, Row["OverviewStatus"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "F1_01_PopMCStatus");
            }
            finally
            {

            }     
        }

        public static int ConvertRange(int originalStart, int originalEnd, // original range
                                                                              int newStart, int newEnd, // desired range
                                                                               int value) // value to convert                                                                                
        {
            double scale = (double)(newEnd - newStart) / (originalEnd - originalStart);
            return (int)(newStart + ((value - originalStart) * scale));
        }

        private void Rgbar(Rectangle Rgbar, int Length, TextBlock BarText, TextBlock PerText)
        {
            try
            {
                var bc = new BrushConverter();
                Rgbar.Width = 80 + (Length * 9.56);
                BarText.Margin = new Thickness(Length * 9.6, 65, 0, 0);
                PerText.Margin = new Thickness(70 + (Length * 9.6), 73, 0, 0);
                BarText.Text = Length.ToString();



                //if (Length <= 25)
                //{
                //    Rgbar.Fill = (Brush)bc.ConvertFrom("#6273D1");
                //    //BarText.Foreground = (Brush)bc.ConvertFrom("#95A9F9");
                //}
                //else if (Length <= 50)
                //{
                //    Rgbar.Fill = (Brush)bc.ConvertFrom("#4B5DC6");
                //    //BarText.Foreground = (Brush)bc.ConvertFrom("#6075CC");
                //}
                //else if (Length <= 75)
                //{
                //    Rgbar.Fill = (Brush)bc.ConvertFrom("#3244AD");
                //    //BarText.Foreground = (Brush)bc.ConvertFrom("#4E60AF");
                //}
                //else
                //{
                //    Rgbar.Fill = (Brush)bc.ConvertFrom("#1D2E91");
                //    //BarText.Foreground = (Brush)bc.ConvertFrom("#223272");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "F1_01_PopMCStatus");
            }
            finally
            {

            }

        }

        private void ImageChange(Image Img ,TextBlock Statustext ,string Status)
        {
            try
            {
                switch (Status)
                {
                    case "0":
                        Statustext.Text = "Scheduled Down";
                        break;
                    case "1":
                        Statustext.Text = "Run";
                        break;
                    case "2":
                        Statustext.Text = "Ready";
                        break;
                    case "3":
                        Statustext.Text = "Stop";
                        break;
                    case "4":
                        Statustext.Text = "Scheduled Down";
                        break;
                }

                //Img.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Images/" + Status + ".png"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "F1_01_PopMCStatus");
            }
            finally
            {

            }
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                Timer = Timer + 1;
                if (Timer >= 60)
                {
                    dtble = dp_F1.GetMCStatus("(MachineNumber = 1 or MachineNumber = 3 or MachineNumber = 4 or MachineNumber = 5 or " +
                                                    "MachineNumber = 6 or MachineNumber = 8 or MachineNumber = 9 or MachineNumber = 10)");
                    SetData(dtble.Rows[MCIndex]);
                    Timer = 0;
                    //UpdateGIF();
                    _TbUpdateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                //else if (Timer == 2)
                //{
                //    this.UpdateGif.Stop();
                //    this.UpdateGif.Visibility = System.Windows.Visibility.Hidden;
                //}

                //_UpdateValues.EndAngle = (Timer * 18);
                //_TbUpdateValues.Text = Convert.ToInt16((Convert.ToDecimal(Timer) / 20) * 100).ToString();
                _UpdateValues.EndAngle = (Timer * 6);
                _TbUpdateValues.Text = Timer.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "F1_01_PopMCStatus");
            }
            finally
            {

            }

        }

        void UpdateGIF()
        {
            try
            {
                this.UpdateGif.Visibility = System.Windows.Visibility.Visible;
                this.UpdateGif.Source = new Uri(@"D:\DMSG_combine_Program\SRC\DMSGalaxy.Main\DMSGalaxy.Main\bin\Debug\Images\Update.gif");
                this.UpdateGif.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "F1_01_PopMCStatus");
            }
            finally
            {

            }
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                ((UIElement)sender).Clip =
                    new RectangleGeometry
                    {
                        RadiusY = 6,
                        RadiusX = 6,
                        Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height)
                    };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "F1_01_PopMCStatus");
            }
            finally
            {

            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void grid_Change_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!GridPosition)
                {
                    grid_Pre.SetValue(Grid.ColumnProperty, 2);
                    grid_Pre.Margin = new Thickness(0, 0, 0, 200);
                    GridPosition = true;
                }
                else
                {
                    grid_Pre.SetValue(Grid.ColumnProperty, 0);
                    grid_Pre.Margin = new Thickness(0, 0, 0, 0);
                    GridPosition = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "F1_01_PopMCStatus");
            }
            finally
            {

            }

        }

        private void grid_Next_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dtble.Rows.Count == (MCIndex + 1))
                {
                    MCIndex = 0;
                    SetData(dtble.Rows[MCIndex]);
                }
                else
                {
                    MCIndex += 1;
                    SetData(dtble.Rows[MCIndex]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "F1_01_PopMCStatus");
            }
            finally
            {

            }

        }

        private void grid_Pre_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (MCIndex == 0)
                {
                    MCIndex = (dtble.Rows.Count - 1);
                    SetData(dtble.Rows[MCIndex]);
                }
                else
                {
                    MCIndex -= 1;
                    SetData(dtble.Rows[MCIndex]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "F1_01_PopMCStatus");
            }
            finally
            {

            }
        }


        #region Form Size Change
        void Window1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChangeSize(e.NewSize.Width, e.NewSize.Height);
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {

            orginalWidth = Main.orginalWidth;
            originalHeight = Main.originalHeight;

            if (this.WindowState == WindowState.Maximized)
            {
                ChangeSize(this.ActualWidth, this.ActualHeight);
            }

            this.SizeChanged += new SizeChangedEventHandler(Window1_SizeChanged);

            this.Width = SystemParameters.MaximizedPrimaryScreenWidth - 16;
            this.Height = SystemParameters.MaximizedPrimaryScreenHeight - 16;

            this.Left = 0;
            this.Top = 0;

        }

        private void ChangeSize(double width, double height)
        {
            scale.ScaleX = width / orginalWidth;
            scale.ScaleY = height / originalHeight;

            FrameworkElement rootElement = this.Content as FrameworkElement;

            rootElement.LayoutTransform = scale;
        }
        #endregion

    }
}
