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
using DevExpress.Xpf.Core;

using DMSGalaxy.Main;
using DMSGalaxy.Main.F_1._01;
using DMSGalaxy.Common.Infos;
using DMSGalaxy.DBConnection.F1;
using DMSGalaxy.Common.Utils;
using DMSGalaxy.Common.Helper;

namespace DMSGalaxy.Main.F_1._01
{
    /// <summary>
    /// F1_01.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F1_01 : Page
    {

        private DataTable McStatus = null;
        private bool McStatusfirstCheck = false;

        //private F1_01_MCstatus[] f1_Mcstatus = null;
        private F1_01_MCstatusOn[] f1_McstatusOn = null;
        private F1_01_MCstatusOff[] f1_McstatusOff = null;
        private F1_01_MCstatusComingSoon  f1_McstatusComingSoon = null;
        private F1_01_MCstatusSub[] f1_McstatusSub = null;
        private CommonUtil C_util = new CommonUtil();
        private XmlUtils m_xmlUtils = new XmlUtils();
        private UIHelper uihelper = new UIHelper();

        private MainWindow Main = Application.Current.MainWindow as MainWindow;

        private F1_01Provider dp_F1 = null;

        private DispatcherTimer timer = new DispatcherTimer();

        int Timer = 0;

        public F1_01()
        {
            InitializeComponent();

            _TbkUpdateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            dp_F1 = new F1_01Provider();

            SetMenus();

            //_TbkUpdateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void SetMenus()
        {
        try
            {

                int McOnTotalCount = 0;

                if (!C_util.IPCheck())
                {
                    MessageBox.Show("현재 네트워크 또는 SERVER PC를 확인 하십시요.");
                    return;
                }


                DataTable dtList = dp_F1.GetMCStatus("(MachineNumber = 1 or MachineNumber = 3 or MachineNumber = 4 or MachineNumber = 5 or " +
                                                    "MachineNumber = 6 or MachineNumber = 8 or MachineNumber = 9 or MachineNumber = 10)");
                DataTable dtList2 = UserInfo.MENUSET.Tables["OverviewAddMainRow"];
                DataTable dtList3 = UserInfo.MENUSET.Tables["OverviewAddMainText"];
                DataTable dtList4 = UserInfo.MENUSET.Tables["OverviewAddMainTextRow"];

                //int McOnTotalCount = 0;
                    

                //    if (!C_util.IPCheck())
                //    {
                //        MessageBox.Show("현재 네트워크 또는 SERVER PC를 확인 하십시요.");
                //        return;
                //    }

                    
                    //dtList =  dp_F1.GetMCStatus(m_xmlUtils.XmlUserSelectToString(UserInfo.US_LINE, "/Setting/Line", " or MachineNumber = ", "(MachineNumber = ", ")"));

                    //wrap_main.Children.Clear();

                    //GridCount.Text = _MainGird.Children.Count.ToString();

                    _MainGird.Children.Clear();
                    _SubGrid.Children.Clear();

                    MainGridTextAdd(_MainGird, dtList3, dtList4);
                       
           
                    //f1_Mcstatus = new F1_01_MCstatus[dtList.Rows.Count];
                    f1_McstatusOn = new F1_01_MCstatusOn[dtList.Rows.Count];
                    f1_McstatusOff = new F1_01_MCstatusOff[dtList.Rows.Count];
                    f1_McstatusSub = new F1_01_MCstatusSub[dtList.Rows.Count];

                    //f1_Mcstatus = new F1_01_MCstatus[15];

                    for (int i = 0; i < dtList.Rows.Count; i++)
                    {
                        //f1_Mcstatus[i] = new F1_01_MCstatus(dtList ,i);
                        //f1_Mcstatus[i].SetMenu(dtList.Rows[i], dtList2.Rows[0]["MC" + dtList.Rows[i]["MCNum"].ToString()].ToString());
                        //wrap_main.Children.Add(f1_Mcstatus[i]);
                        if (dtList.Rows[i]["Status"].ToString() != "0" )
                        {
                                f1_McstatusOn[i] = new F1_01_MCstatusOn(dtList, dtList.Rows[i], i);
                                Grid.SetRow(f1_McstatusOn[i], Convert.ToInt16(dtList2.Rows[0]["MC" + dtList.Rows[i]["MCNum"].ToString()].ToString().Substring(0, 1)));
                                Grid.SetColumn(f1_McstatusOn[i], Convert.ToInt16(dtList2.Rows[0]["MC" + dtList.Rows[i]["MCNum"].ToString()].ToString().Substring(2, 1)));
                                _MainGird.Children.Add(f1_McstatusOn[i]);
                                McOnTotalCount = McOnTotalCount + 1;
                        }
                        else
                        {
                            f1_McstatusOff[i] = new F1_01_MCstatusOff(dtList, dtList.Rows[i], i);
                            Grid.SetRow(f1_McstatusOff[i], Convert.ToInt16(dtList2.Rows[0]["MC" + dtList.Rows[i]["MCNum"].ToString()].ToString().Substring(0, 1)));
                            Grid.SetColumn(f1_McstatusOff[i], Convert.ToInt16(dtList2.Rows[0]["MC" + dtList.Rows[i]["MCNum"].ToString()].ToString().Substring(2, 1)));
                            _MainGird.Children.Add(f1_McstatusOff[i]);
                            if (dtList.Rows[i]["Status"].ToString() != "0"  && dtList.Rows[i]["Name"].ToString() == "RSR")
                            {
                                McOnTotalCount = McOnTotalCount + 1;
                            }
                        }

                        f1_McstatusSub[i] = new F1_01_MCstatusSub(dtList.Rows[i]);
                        Grid.SetColumn(f1_McstatusSub[i], i);
                        _SubGrid.Children.Add(f1_McstatusSub[i]);
                    }

                    /* MC Status Coming Soon Display 
                    f1_McstatusComingSoon = new F1_01_MCstatusComingSoon();
                    Grid.SetRow(f1_McstatusComingSoon, 3);
                    Grid.SetColumn(f1_McstatusComingSoon, 7);
                    _MainGird.Children.Add(f1_McstatusComingSoon);
                    */

                    _TotalOnStatus.Text = McOnTotalCount.ToString();

                    if (!McStatusfirstCheck)
                    {
                        McStatus = dtList;
                        ThreadPool.QueueUserWorkItem(new WaitCallback(UpdateTimeCheck));
                    }
      
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "F1_01");
            }
            finally
            {
                //Login _Login = new Login();
                //_Login.UserStatusReset(UserInfo.US_ID, false);
            }
        }

        private void MainGridTextAdd(Grid grid,DataTable Text, DataTable TextRow)
        {
            TextBlock[] textblock = new TextBlock[4];

            Line[] textline = new Line[4];

            for (int i = 0; i < 4; i ++)
            {
                textblock[i] = new TextBlock();
                textblock[i].FontSize = 22;
                textblock[i].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                textblock[i].VerticalAlignment = System.Windows.VerticalAlignment.Center;
                textblock[i].FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#KoPubDotum Bold");
                textblock[i].Text = Text.Rows[0]["ROW" + i.ToString()].ToString();
                textblock[i].Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#B3B3B3")); 

                textline[i] = uihelper.CreateLine(1300, 25, 80, 25, Brushes.Gray, 0.5, new DoubleCollection { 10, 2 });

                Grid.SetRow(textblock[i], Convert.ToInt16(TextRow.Rows[0]["ROW" + i.ToString()].ToString().Substring(0, 1)));
                Grid.SetColumn(textblock[i],  Convert.ToInt16(TextRow.Rows[0]["ROW" + i.ToString()].ToString().Substring(2, 1)));
                Grid.SetRow(textline[i], Convert.ToInt16(TextRow.Rows[0]["ROW" + i.ToString()].ToString().Substring(0, 1)));
                Grid.SetColumn(textline[i], Convert.ToInt16(TextRow.Rows[0]["ROW" + i.ToString()].ToString().Substring(2, 1)));
                /* 다른 범위의 라인으로 표시하려면...
                if (i == 0 || i == 1) { if (i == 0) { Grid.SetColumnSpan(textline[i], 5); } else { Grid.SetColumnSpan(textline[i], 1); } }
                else { Grid.SetColumnSpan(textline[i], 3); }
                */
                Grid.SetColumnSpan(textline[i], 3);
                grid.Children.Add(textblock[i]);
                grid.Children.Add(textline[i]);

            }

        }

        private void UpdateTimeCheck(object oArgument)
        {
            string McNumber = "";

                      for (int i = 0; i < McStatus.Rows.Count; i++)
                    {

                        TimeSpan timeDiff = DateTime.Now - Convert.ToDateTime(McStatus.Rows[i]["UpdateTime"].ToString());

                        if(timeDiff.Days > 0 ||  timeDiff.Hours > 0)
                        {
                            McNumber += string.Format("CA#{0} ", McStatus.Rows[i]["MCNum"]);
                        }

                    }

            McStatusfirstCheck = true;
            if (McNumber != "")
            {
                            MessageBox.Show(@"" + McNumber +" 호기" + '\r' + '\n' +"서버 데이터 업데이트가 1시간 이상 지연되고 있습니다."+ '\r' + '\n'+
                                                        "전기 담당자  문의 부탁드립니다.", "업데이트 시간 확인", 
                                                            MessageBoxButton.OK, MessageBoxImage.Asterisk);            
            }
        }

        private void timer_Tick(object sender,EventArgs e)
        {
            try
            {

                Timer = Timer + 1;
                if (Timer >= 60)
                {
                    Timer = 0;
                    SetMenus();
                    _TbkUpdateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }

                _UpdateValues.EndAngle = (Timer * 6);
                //_TbkUpdatePer.Text = Convert.ToInt16((Convert.ToDecimal(Timer) / 60) * 100).ToString();
                _TbkUpdatePer.Text = Timer.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "F1_01");
            }
            finally
            {

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (DXSplashScreen.IsActive)
            {
                DXSplashScreen.Close();
            }
        }

    }
}
