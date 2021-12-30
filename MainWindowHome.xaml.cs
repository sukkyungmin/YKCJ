using System.Windows;
using System.Windows.Controls;
using Common.Utils;
using YKCJ_EngineerReport.Commons.Cons;
using System.Collections.Generic;
using System.Windows.Input;

using YKCJViewModel.F_00;
using System;

namespace YKCJ_EngineerReport
{
    /// <summary>
    /// MainWindowHome.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindowHome : Page
    {

        //private Thread thrd_tempWorkDone = null;

        private MainWindow Main = Application.Current.MainWindow as MainWindow;
        private XmlUtils m_xmlUtils = new XmlUtils();
        private CommonUtil m_Utils = new CommonUtil();
        //Wd_Loader wd_loader = null; // 1번로딩화면 사용안함
        public static Point OwnerPosition;
        public static Size OwnerSize;

        public MainWindowHome(F_00_ViewModel model)
        {


            InitializeComponent();

            //Consumo consumo = new Consumo();
            //DataContext = new ConsumoViewModel(consumo);

            DataContext = model;

        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //private void SetUserMenu()
        //{
        //    try
        //    {
        //        _Im_User.Source = m_Utils.ByteArrayToBitmaplmage(UserInfo.US_IMAGES);
        //        UseName.Content = UserInfo.US_NM;
        //        UseLevel.Content = SetLevel(UserInfo.US_LEV);
        //        UseMail.Content = UserInfo.US_MAIL;
        //        UseNum.Content = UserInfo.US_EPBER;
        //        UseJob.Content = UserInfo.US_JOB;
        //        UseWG.Content = UserInfo.US_WG;
        //        UseLine.Content = m_xmlUtils.XmlUserSelectToString(UserInfo.US_LINE, "/Setting/Line", ",", "", "");
        //        UsePhone.Content = UserInfo.US_PON;
        //        UseIP.Content = UserInfo.US_IP;

        //        Main._tbUserName.Text = "User : " + UserInfo.US_NM;

        //        SetMenu();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        Main.Log_Write(ex.ToString(), "MainWindowHome");
        //    }
        //    finally
        //    {

        //    }
        //}

    }


    internal class ConsumoViewModel
    {
        public List<Consumo> Consumo { get; private set; }

        public ConsumoViewModel(Consumo consumo)
        {
            Consumo = new List<Consumo>();
            Consumo.Add(consumo);
        }
    }

    internal class Consumo
    {
        public string Titulo { get; private set; }
        public int Porcentagem { get; private set; }

        public Consumo()
        {
            Titulo = "Hangil-FA";
            Porcentagem = CalcularPorcentagem();
        }

        private int CalcularPorcentagem()
        {
            return 55; //Calculo da porcentagem de consumo
        }
    }
}
