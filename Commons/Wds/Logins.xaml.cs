using Common.Helper;
using Common.Infos;
using Common.Utils;
using DBConnection.CommonProvider;
using System;
using System.Data;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace YKCJ_EngineerReport.Commons.Wds
{
    /// <summary>
    /// Logins.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Logins : Window
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

        public Logins()
        {
            InitializeComponent();

            InputMethod.SetIsInputMethodEnabled(this.txtInputID, false);
            InputMethod.SetIsInputMethodEnabled(this.PwBoxInputPW, false);
            //txtInputID.Text = Properties.Settings.Default.UserID;
            FocusManager.SetFocusedElement(this, txtInputID);
            //txtInputID.SelectionStart = txtInputID.Text.Length;
        }

        //private async void Login_Cilck()
        private async void Login_Cilck()
        {
            DataSet dtList = null;
            DataRow drUser = null;

            try
            {

                if (SystemInfo.ExcMode == Common.Utils.GlobalVar.ExecuteMODE.REAL)
                {

                    Ping ping = new Ping();
                    var reply = await Task.Run(() => ping.Send("131.9.1.2"));

                    if (!(reply.Status == IPStatus.Success))
                    {
                        MessageBox.Show("현재 네트워크 또는 SERVER PC를 확인 하십시요.");
                        return;
                    }

                }

                dp_Login = new Login();
                //string id = txtInputID.Text.Trim();
                dtList = dp_Login.GetUserInfoById(txtInputID.Text.Trim());
                //dtList = await Task.Run(()=> dp_Login.GetUserInfoById(id));
                if (txtInputID.Text == "" || PwBoxInputPW.Password.ToString().Trim() == "")
                {
                    MessageBox.Show("사용자정보를 입력해 주십시요.");
                    return;
                }

                if (null == dtList || 0 >= dtList.Tables.Count)
                {
                    MessageBox.Show("사용자정보가 존재하지 않습니다.");
                    return;
                }
                else
                {
                    if (PwBoxInputPW.Password.ToString().Trim() != StringCipher.Decrypt(dtList.Tables[0].Rows[0]["PW"].ToString(), ciperPass))
                    {
                        //dp_Login.UserStatusReset(dtList.Tables[0].Rows[0]["ID"].ToString(), false);
                        MessageBox.Show("암호를 확인해주세요.");
                        return;
                    }
                }

                //if (Convert.ToBoolean(dtList.Rows[0]["ContactCheck"]))
                //{
                //    MessageBox.Show("현재 다른 PC에서 로그인되어 있습니다.");
                //    return;
                //}

                drUser = dtList.Tables[0].Rows[0];
                UserInfo.USERITEM = dtList.Tables[1].Rows[0];
                UserInfo.US_IDX = (int)drUser["IDX"];
                UserInfo.US_ID = drUser["ID"].ToString();
                UserInfo.US_PW = drUser["PW"].ToString();
                UserInfo.US_NM = drUser["NAME"].ToString();
                UserInfo.US_JOB = drUser["JOB"].ToString();
                UserInfo.US_IMAGES = (Byte[])drUser["IMAGE"];

                DataSet dataSetMenu = new DataSet();
                dataSetMenu.ReadXml(@"XMLFile/LowMenuSet.xml");

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
                }
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

                if (new CheckPing().Check_PGM_Overlap() == true)
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

        private void txtpSpace_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
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

            if (e.Key == Key.Enter)
            {
                Login_Cilck();
            }
        }

        private void Grid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            this.DragMove();
        }
    }
}
