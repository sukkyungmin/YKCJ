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
using System.Data;
using System.Xml;
using System.IO;
using System.Threading;
using DevExpress.Xpf.Core;
using System.Windows.Threading;
using System.Net.NetworkInformation;

using DMSGalaxy.DBConnection.CommonProvider;
using DMSGalaxy.Common.Infos;
using DMSGalaxy.Common.Helper;
using DMSGalaxy.Common.Utils;

namespace DMSGalaxy.Main.Cons
{
    /// <summary>
    /// Wd_Logins.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Wd_Logins : Window
    {

        /// <summary>
        ///  PW 암호화 Key
        /// </summary>
        const string ciperPass = "SUKMIN";

        /// <summary>
        /// 사용자테이블 Data Provider
        /// </summary>
        private Login dp_Login = null;

        /// <summary>
        /// 로그인 정상여부
        /// </summary>
        private bool m_LogInOK = false;

        /// <summary>
        /// 로그인 정상여부
        /// </summary>
        public bool LoginOK
        {
            get { return m_LogInOK; }
        }

        private CommonUtil C_util = new CommonUtil();

        private MainWindow Main = Application.Current.MainWindow as MainWindow;

        public Wd_Logins()
        {            
            InitializeComponent();

            InputMethod.SetIsInputMethodEnabled(this.txtInputID, false);
            InputMethod.SetIsInputMethodEnabled(this.PwBoxInputPW, false);
            txtInputID.Text = Properties.Settings.Default.UserID;
            FocusManager.SetFocusedElement(this, txtInputID);
            txtInputID.SelectionStart = txtInputID.Text.Length;
        }

        private void Login_Cilck()
        {
            DataTable dtList = null;
            DataRow drUser = null;

            try
            {

                if (SystemInfo.ExcMode == DMSGalaxy.Common.Utils.GlobalVar.ExecuteMODE.REAL)
                {
                    //using (Ping pingSender = new Ping())
                    //{
                    //    PingReply reply = pingSender.Send("121.137.95.29");
                    //    if (reply.Status != IPStatus.Success)
                    //    {
                    //        MessageBox.Show("현재 네트워크 또는 SERVER PC를 확인 하십시요.");
                    //        return;
                    //    }
                    //}
                    if (!C_util.IPCheck())
                    {
                        MessageBox.Show("현재 네트워크 또는 SERVER PC를 확인 하십시요.");
                        return;
                    }

                }

                dp_Login = new Login();
                dtList = dp_Login.GetUserInfoById(txtInputID.Text.Trim());
                if (txtInputID.Text == "" || PwBoxInputPW.Password.ToString().Trim() == "")
                {
                    MessageBox.Show("사용자정보를 입력해 주십시요.");
                    return;
                }

                if (null == dtList || 0 >= dtList.Rows.Count)
                {
                    MessageBox.Show("사용자정보가 존재하지 않습니다.");
                    return;
                }
                else
                {
                    if (PwBoxInputPW.Password.ToString().Trim() != StringCipher.Decrypt(dtList.Rows[0]["PW"].ToString(), ciperPass))
                    {
                        dp_Login.UserStatusReset(dtList.Rows[0]["ID"].ToString(), false);
                        MessageBox.Show("암호를 확인해주세요.");
                        return;
                    }
                }

                if (Convert.ToBoolean(dtList.Rows[0]["ContactCheck"]))
                {
                    MessageBox.Show("현재 다른 PC에서 로그인되어 있습니다.");
                    return;
                }

                XmlDocument doc = new XmlDocument();

                drUser = dtList.Rows[0];
                UserInfo.US_ID = drUser["ID"].ToString();
                UserInfo.US_PW = drUser["PW"].ToString();
                UserInfo.US_LEV = Convert.ToInt16(drUser["LEVEL"].ToString());
                UserInfo.US_NM = drUser["이름"].ToString();
                UserInfo.US_SEX = drUser["성별"].ToString();
                UserInfo.US_EPBER = drUser["사원번호"].ToString();
                UserInfo.US_JOB = drUser["Job"].ToString();
                UserInfo.US_PON = drUser["핸드폰"].ToString();
                UserInfo.US_MAIL = drUser["Email"].ToString();
                UserInfo.US_WG = drUser["소속WG"].ToString();
                UserInfo.US_IP = drUser["UserIP"].ToString();
                doc.LoadXml(drUser["Line"].ToString());
                UserInfo.US_LINE = doc;
                UserInfo.US_DAY = drUser["생일"].ToString();
                UserInfo.US_IMAGES = (Byte[])drUser["Image"];
                UserInfo.US_COUNT = Convert.ToInt16(drUser["MsCount"].ToString());


                DataSet dataSetLever = new DataSet();
                dataSetLever.ReadXml(@"XMLFile/UserMenuSet.xml");

                DataSet dataSetMenu = new DataSet();
                dataSetMenu.ReadXml(@"XMLFile/LowMenuSet.xml");

                UserInfo.USERSET = dataSetLever;
                UserInfo.MENUSET = dataSetMenu;

                UserInfo.USERINFO = drUser;
                m_LogInOK = true;

                Properties.Settings.Default.UserID = txtInputID.Text;
                Properties.Settings.Default.Save();

                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "Wd_Logins");
            }
            finally
            {
                if (null != dtList)
                {
                    dtList.Dispose();
                    dtList = null;
                }

                drUser = null;

            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login_Cilck();
        }

        private void wd_main_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DXSplashScreen.Close();

                this.Activate();
                /*
                Main.LoaderTimeCheck();
                 */ 
                if (SystemParameters.MaximizedPrimaryScreenWidth != 1920 & SystemParameters.MaximizedPrimaryScreenHeight != 1080)
                {
                    if (SystemParameters.MaximizedPrimaryScreenWidth - 16 != 1920 & SystemParameters.MaximizedPrimaryScreenHeight - 16 != 1080)
                    {
                        if (MessageBox.Show("현재 화면의 해상도는 DMSG 개발 해상도" + '\r' + '\n' +
                                                                            "(W:1920, H1080) 와 다릅니다." + '\r' + '\n' + "그대로 실행 하시겠습니까?"
                                                                             , "화면 해상도 확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.No)
                        {
                            Exit();
                        }
                    }
                }

                if (new CHK_PGM().Check_PGM_Overlap() == true)
                {
                    //m_LOG.LOG("프로그램 중복 실행", "FrmMain");
                    MessageBox.Show("프로그램이 이미 실행중 입니다.");
                    Exit();
                    return;
                }

                //con_msg = new CON_MSG(this);

                //lbl_userGrp.Content = UserInfo.GRP_ID;
                //lbl_userName.Content = UserInfo.US_NM;
                //SetFavorite(UserInfo.US_ID);
            }
            catch (Exception ex)
            {
                //m_LOG.LOG(ex.ToString(), "FrmMain");
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "Wd_Logins");
            }
        }

        //종료 함수
        private void Exit()
        {
            try
            {
                String[] args = Environment.GetCommandLineArgs();

                // args[0] is the program name and, args[1] is the first argument.
                // Test for a command-line argument.
                if (args.Length > 1)
                {

                    // Parse the argument. If successful, exit with the parsed code.
                    try
                    {
                        int exitCode = int.Parse(args[1]);

                        Environment.Exit(exitCode);
                    }
                    // If the parse fails, you fall out of the program.
                    catch
                    {
                        Environment.Exit(0);
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                        this.Close();
                    }
                }

                Environment.Exit(0);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Main.Log_Write(ex.ToString(), "Wd_Logins");
            }
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            if (!m_LogInOK)
            {
                Exit();
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
     

        private void txtpSpace_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key ==  Key.Space )
            {
                e.Handled = true;
            }
        }

        void wnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.System && e.SystemKey == Key.F4)
            {
                e.Handled = true;
            }

            if(e.Key == Key.Enter)
            {
                Login_Cilck();
            }
        }

    }
}
