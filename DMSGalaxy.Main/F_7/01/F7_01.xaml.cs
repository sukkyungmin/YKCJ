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
using System.Windows.Forms;
//using System.Windows
using System.IO;

using DevExpress.Xpf.Core;
using DMSGalaxy.Common.Helper;


namespace DMSGalaxy.Main.F_7._01
{
    /// <summary>
    /// F7_01.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F7_01 : Page
    {
        private LOG_Write m_LOG = new LOG_Write();

        public F7_01()
        {
            InitializeComponent();

            if (Properties.Settings.Default.MainHomeImage.Length > 10)
            {
                _ImgBackground.Source = StringToImage(Properties.Settings.Default.MainHomeImage);
            }
            else
            {
                _ImgBackground.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Images/Background0.jpg"));
            }
        }

        private void GetImage()
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog() { InitialDirectory = "c:\\", Filter = "Images Files(*.jpg;*.jpeg;*.bmp;*.png)|*.jpg;*.jpeg;*.bmp;*.png", RestoreDirectory = true };

                if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (File.Exists(openDialog.FileName))
                    {
                        _ImgBackground.Source = new BitmapImage(new Uri(openDialog.FileName, UriKind.RelativeOrAbsolute));
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F7_01");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DXSplashScreen.Close();
        }

        private string ImageToString(BitmapImage imageIn)
        {
            var base64 = string.Empty;
            using (var ms = new MemoryStream())
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imageIn));
                encoder.Save(ms);
                return base64 = Convert.ToBase64String(ms.ToArray());
            }
        }

        private BitmapImage StringToImage(string value)
        {
            byte[] bytes = Convert.FromBase64String(value);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(bytes);
            bi.EndInit();
            return bi;
        }

        private void ImageChange_Click(object sender, RoutedEventArgs e)
        {
            GetImage();
        }

        private void ImageSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (System.Windows.MessageBox.Show("메인화면 이미지를 변경 하시겠습니까?", "변경 확인",
                                                                                            MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.No)
                {
                    return;
                }

                if (_ImgBackground.Source != null)
                {
                    Properties.Settings.Default.MainHomeImage = ImageToString(_ImgBackground.Source as BitmapImage);
                    Properties.Settings.Default.Save();
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F7_01");
            }
        }

    }
}
