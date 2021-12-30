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
using System.Windows.Forms;
using System.IO;
using System.Xml;
using DevExpress.Xpf.Core;

using DMSGalaxy.DBConnection.CommonProvider;
using DMSGalaxy.Common.Infos;
using DMSGalaxy.Common.Helper;
using DMSGalaxy.Common.Utils;

namespace DMSGalaxy.Main.F_8._01
{
    /// <summary>
    /// F8_01.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F8_01 : Page
    {

        /// <summary>
        ///  PW 암호화 Key
        /// </summary>
        const string ciperPass = "SUKMIN";

        string fpath = null;

        private Login Lg = null;
        private LOG_Write m_LOG = new LOG_Write();
        private CommonUtil C_util = new CommonUtil();

        List<System.Windows.Controls.CheckBox> a_Checkbox;


        public F8_01()
        {
            InitializeComponent();

            fpath = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Images\BasicUserPic.png";

            UserImages.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Images/BasicUserPic.png"));

            Lg = new Login();

            a_Checkbox = new List<System.Windows.Controls.CheckBox>();
            a_Checkbox.Add(_Cb01);
            a_Checkbox.Add(_Cb02);
            a_Checkbox.Add(_Cb03);
            a_Checkbox.Add(_Cb04);
            a_Checkbox.Add(_Cb05);
            a_Checkbox.Add(_Cb06);
            a_Checkbox.Add(_Cb08);
            a_Checkbox.Add(_Cb09);
            a_Checkbox.Add(_Cb10);

        }

        void child_SendMsgRefrh()
        {
            UserImages.Source = null;
            _TbID.Text = "";
            _PwBoxInputPW.Text = "";
            _PwBoxInputPWre.Text = "";
            _TbName.Text = "";
            _CbGender.Text = "";
            _TbEnumber.Text = "";
            _CbJop.Text = "";
            _TbPhone.Text = "";
            _TbEmail.Text = "";
            _TbWG.Text = "";
            _TbDay.Text = "";
            _TbIP.Text = "";
            _CbLevel.Text = "";

            for (int i = 0; i < a_Checkbox.Count; i++)
            {
                a_Checkbox[i].IsChecked = false;
            }
        }

        private void GetImage()
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();

                openDialog.InitialDirectory = "c:\\";
                openDialog.Filter = "Images Files(*.jpg;*.jpeg;*.gif;*.bmp;*.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png";
                openDialog.RestoreDirectory = true;

                if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (File.Exists(openDialog.FileName))
                    {
                        UserImages.Source = new BitmapImage(new Uri(openDialog.FileName, UriKind.RelativeOrAbsolute));
                        fpath = openDialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                m_LOG.LOG(ex.ToString(), "F8_01");
            }
        }

        private void SetUserSave()
        {
            try
            {
                if (fpath != null)
                {
                    int ErrorOutput;

                    FileStream fs = new FileStream(fpath, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);

                    byte[] image = br.ReadBytes((int)fs.Length);
                    br.Close();
                    fs.Close();

                    string PW = StringCipher.Encrypt(_PwBoxInputPW.Password.ToString().Trim(), ciperPass);


                    if (!C_util.IPCheck())
                    {
                        System.Windows.MessageBox.Show("현재 네트워크 또는 SERVER PC를 확인 하십시요.");
                        return;
                    }


                    ErrorOutput = Lg.SetUserSave(_TbID.Text, PW, Level(_CbLevel.Text), _TbName.Text, _CbGender.Text, _TbEnumber.Text, _CbJop.Text, _TbPhone.Text, _TbEmail.Text, _TbWG.Text,
                                                                                            _TbIP.Text, _TbDay.Text, XmlTest(), image);

                    switch (ErrorOutput)
                    {
                        case 0:
                            CommonUtil.MessageAlert("I0040", "사용자 정보에 대한 저장이");
                            break;
                        case 1:
                            CommonUtil.MessageAlert("I0011", "ID가 중복 입니다. ID");
                            break;
                        case 2:
                            child_SendMsgRefrh();
                            CommonUtil.MessageAlert("X0002", "DB 저장");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F8_01");
            }
            finally
            {
                //Login _Login = new Login();
                //_Login.UserStatusReset(UserInfo.US_ID, false);
            }
        }

        private int Level(string _level)
        {
            int _ReturnLevel = 0;

            switch (_level)
            {
                case "Level 1 : General":
                    _ReturnLevel = 1;
                    break;
                case "Level 2 : Specialist":
                    _ReturnLevel = 2;
                    break;
                case "Level 3 : Asset Leader":
                    _ReturnLevel = 3;
                    break;
                case "Level 4 : Engineer":
                    _ReturnLevel = 4;
                    break;
                case "Level 5 : Master":
                    _ReturnLevel = 5;
                    break;
            }

            return  _ReturnLevel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetImage();
        }

        private XmlDocument XmlTest()
        {
            XmlDocument doc = new XmlDocument();

            XmlElement inventory = doc.CreateElement("Setting");

            for (int i = 0; i < a_Checkbox.Count ; i++)
            {
                if(a_Checkbox[i].IsChecked == true)
                {
                    XmlElement Line = doc.CreateElement("Line");
                    Line.InnerText = a_Checkbox[i].Content.ToString().Substring(3);
                    inventory.AppendChild(Line);
                }
            }

            doc.AppendChild(inventory);

            return doc;
        }

        private bool Passwordcheck()
        {
            if (_PwBoxInputPW.Password.ToString().Trim() != _PwBoxInputPWre.Password.ToString().Trim() || _PwBoxInputPW.Password.ToString().Trim() == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool LineCheck()
        {
            bool LineCheck = false;

            for (int i = 0; i < a_Checkbox.Count; i++)
            {
                LineCheck = a_Checkbox[i].IsChecked.Value;
                if(LineCheck)
                {
                    break;
                }                
            }
            return LineCheck;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("현재 정보를 저장 하시겠습니까?", "저장 확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
            {
                if (Passwordcheck())
                {
                    CommonUtil.MessageAlert("I9001", "");
                    return;
                }
                else
                {
                    if (_TbID.Text == "" || _TbEnumber.Text == "" || _TbWG.Text == "" || _TbIP.Text == "" || _CbLevel.Text == "" || !LineCheck())
                    {
                        CommonUtil.MessageAlert("I0050", "");
                    }
                    else
                    {
                        SetUserSave();
                    }
                }
            }
            else
            {
                return;
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
