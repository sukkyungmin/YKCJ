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
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace DMSGalaxy.Main
{
    /// <summary>
    /// MainWindowMenuFirst.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindowMenuFirst : Page
    {
        //static int MediaRepeat = 0;
        int NowRandom = 0;
        int BfRandom = 0;
        int Num = 2;

        public bool IsImage1 { get; set; }
        public List<BitmapImage> ImageList { get; set; }

        public DispatcherTimer Timer = new DispatcherTimer();
        public Random Ran = new Random();

        public MainWindowMenuFirst()
        {
            InitializeComponent();
            Timer.Interval = TimeSpan.FromSeconds(4);
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Start();
            //string filename = "/Images/Chqq/Ch1.bmp";
            //Img1.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + filename, UriKind.RelativeOrAbsolute));
            GetImage();

            //this.video.MediaEnded += new RoutedEventHandler(MediaE_MediaEnded);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //this.video.Source = new Uri(@"D:\DMSG_combine_Program\SRC\DMSGalaxy.Main\DMSGalaxy.Main\bin\Debug\Video\Choongju_ENG_0925.wmv");

            //this.video.Source =  new Uri(@"" + System.Windows.Forms.Application.StartupPath +"/Video/Choongju_ENG_0925.wmv");    // 사용

            //this.video.Source = new Uri(@"C:\Debug\Video\Choongju_ENG_0925.wmv");
            
            //this.video.Play();  // 사용
        }

        //void MediaE_MediaEnded(object sender,RoutedEventArgs e)
        //{
        //    MediaRepeat++;
        //    if (MediaRepeat < 5)
        //    {
        //        this.video.Stop();
        //        this.video.Position = TimeSpan.FromSeconds(0);
        //        this.video.Play();
        //    }
        //}

        void GetImage()
        {
            ImageList = new List<BitmapImage>();
            string[] Files = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Images/HomeSlideImage");
            foreach (string Path in Files)
            {
                try
                {
                   ImageList.Add(new BitmapImage(new Uri(Path, UriKind.Absolute)));
                }
                catch (Exception) { }
            }
            NowRandom = Ran.Next(0, ImageList.Count - 1);

            BfRandom = NowRandom;

            Img1.Source = ImageList[BfRandom];
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            NowRandom = Ran.Next(0, ImageList.Count - 1);

            if (BfRandom == NowRandom)
            {
                if (NowRandom == 0) { BfRandom = NowRandom + 1; } else if (NowRandom == (ImageList.Count - 1)) { BfRandom = NowRandom - 1; }
            }
            else
            {
                BfRandom = NowRandom;
            }

            if (IsImage1)
            {
                Img1.Source = ImageList[BfRandom];
                (Resources["Img1Animation"] as Storyboard).Begin(this);
            }
            else
            {
                Img2.Source = ImageList[BfRandom];
                (Resources["Img2Animation"] as Storyboard).Begin(this);
            }
            IsImage1 = !IsImage1;
            //string filename = "/Images/Chqq/Ch" + Num.ToString() + ".bmp";
            //Img1.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + filename, UriKind.RelativeOrAbsolute));
            //if(Num == 4)
            //{
            //    Num = 1;
            //}
            // else
            //{
            //    Num = Num + 1;
            //}
        }


    }
}
