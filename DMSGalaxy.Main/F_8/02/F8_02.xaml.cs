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
using System.Windows.Forms;
using System.Xml;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Core;

using DMSGalaxy.DBConnection.F8;
using DMSGalaxy.Common.Helper;
using DMSGalaxy.Common.Utils;

namespace DMSGalaxy.Main.F_8._02
{
    /// <summary>
    /// F8_02.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F8_02 : Page
    {

        /// <summary>
        ///  PW 암호화 Key
        /// </summary>
        const string ciperPass = "SUKMIN";

        public ObservableCollection<ItemInfo> Rows { get; set; }

        string fpath = null;
        byte[] _image = null;

        F8_02_Create_User[] F8_User = null;
        private F8_02Provider dp_F8 = null;

        private CommonUtil m_Utils = new CommonUtil();
        private LOG_Write m_LOG = new LOG_Write();

        private DataTable NowdtList = null;

        List<System.Windows.Controls.CheckBox> a_Checkbox;

        public F8_02()
        {
            InitializeComponent();

            a_Checkbox = new List<System.Windows.Controls.CheckBox>();
            a_Checkbox.Add(_Cb01);
            a_Checkbox.Add(_Cb02);
            a_Checkbox.Add(_Cb03);
            a_Checkbox.Add(_Cb04);
            a_Checkbox.Add(_Cb05);
            a_Checkbox.Add(_Cb06);
            //a_Checkbox.Add(_Cb07);
            a_Checkbox.Add(_Cb08);
            a_Checkbox.Add(_Cb09);
            a_Checkbox.Add(_Cb10);

            SetMenus();
        }

        private void SetMenus()
        {
                try
                {
                    DataTable dtList = null;

                    if (NowdtList != null)
                    {
                        NowdtList.Clear();
                    }

                    dp_F8 = new F8_02Provider();

                    Rows = new ObservableCollection<ItemInfo>();


                    if (!m_Utils.IPCheck())
                    {
                        System.Windows.MessageBox.Show("현재 네트워크 또는 SERVER PC를 확인 하십시요.");
                        return;
                    }


                    dtList = dp_F8.GetUserList();

                    NowdtList = dtList;

                    wrap_main.Children.Clear();

                    F8_User = new F8_02_Create_User[dtList.Rows.Count];

                    for (int i = 0; i < dtList.Rows.Count; i++)
                    {
                        F8_User[i] = new F8_02_Create_User();
                        F8_User[i].SendMsg += new F8_02_Create_User.SendMsgDele(child_SendMsg);
                        F8_User[i].SetMenu(dtList.Rows[i]);
                        Rows.Add(new ItemInfo() { Text = dtList.Rows[i]["이름"].ToString(), Value = i });
                        wrap_main.Children.Add(F8_User[i]);
                    }

                    _CbNameList.ItemsSource = Rows;
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                    m_LOG.LOG(ex.ToString(), "F8_02");
                }
                finally
                {
                    //Login _Login = new Login();
                    //_Login.UserStatusReset(UserInfo.US_ID, false);
                }
        }

        void child_SendMsg(DataRow Row)
        {
            try
            {
                grid_main.ColumnDefinitions[0].Width = new GridLength(390);
                grid_main.ColumnDefinitions[1].Width = new GridLength(705);
                grid_sub.Width = 380;

                UserImages.Source = m_Utils.byteArrayToBitmaplmage((Byte[])Row["Image"]);
                _image = (Byte[])Row["Image"];
                _TkIDPW.Text = string.Format("ID : {0} / PW : {1}", Row["ID"].ToString().Trim(), StringCipher.Decrypt(Row["PW"].ToString().Trim(), ciperPass));
                _TbID.Text = Row["ID"].ToString().Trim();
                _PwBoxInputPW.Text = StringCipher.Decrypt(Row["PW"].ToString().Trim(), ciperPass);
                _PwBoxInputPWre.Text = StringCipher.Decrypt(Row["PW"].ToString().Trim(), ciperPass);
                _TbName.Text = Row["이름"].ToString().Trim();
                _CbGender.Text = Row["성별"].ToString().Trim();
                _TbEnumber.Text = Row["사원번호"].ToString().Trim();
                _CbJop.Text = Row["Job"].ToString().Trim();
                _TbPhone.Text = Row["핸드폰"].ToString().Trim();
                _TbEmail.Text = Row["Email"].ToString().Trim();
                _TbWG.Text = Row["소속WG"].ToString().Trim();
                _TbDay.Text = Row["생일"].ToString().Trim();
                _TbIP.Text = Row["UserIP"].ToString().Trim();
                _CbLevel.SelectedIndex = Convert.ToInt16(Row["LEVEL"].ToString().Trim()) - 1;
                CheckboxList(Row);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F8_02");
            }
            finally
            {

            }
        }

        void child_SendMsgRefrh()
        {
            grid_main.ColumnDefinitions[0].Width = new GridLength(1090);
            grid_main.ColumnDefinitions[1].Width = new GridLength(0);
            grid_sub.Width = 800;

            UserImages.Source = null;
            _TkIDPW.Text = "";
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

        private XmlDocument XmlAdd()
        {
            XmlDocument doc = new XmlDocument();

            XmlElement inventory = doc.CreateElement("Setting");

            for (int i = 0; i < a_Checkbox.Count; i++)
            {
                if (a_Checkbox[i].IsChecked == true)
                {
                    XmlElement Line = doc.CreateElement("Line");
                    Line.InnerText = a_Checkbox[i].Content.ToString().Substring(3);
                    inventory.AppendChild(Line);
                }
            }

            doc.AppendChild(inventory);

            return doc;
        }
      

        private void CheckboxList(DataRow Row)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(Row["Line"].ToString());
                XmlElement root = doc.DocumentElement;
                XmlNodeList nodes = root.ChildNodes;

                for (int i = 0; i < a_Checkbox.Count; i++)
                {
                    a_Checkbox[i].IsChecked = false;
                }

                for (int i = 0; i < nodes.Count; i++)
                {
                    for (int j = 0; j < a_Checkbox.Count; j++)
                    {
                        if (a_Checkbox[j].Content.ToString().Substring(3) == nodes[i].InnerText)
                        {
                            a_Checkbox[j].IsChecked = (nodes[i].InnerText != "2") ? true : false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F8_02");
            }
            finally
            {

            }

        }


        private void GetImage()
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog() { InitialDirectory = "c:\\", Filter = "Images Files(*.jpg;*.jpeg;*.gif;*.bmp;*.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png", RestoreDirectory = true };

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
                m_LOG.LOG(ex.ToString(), "F8_02");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                GetImage();
        }

        private void SetUserSave(bool DBUpadteORDelete)
        {
                try
                {
                    byte[] image = null;

                    if (fpath != null)
                    {
                        FileStream fs = new FileStream(fpath, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);

                        image = br.ReadBytes((int)fs.Length);
                        br.Close();
                        fs.Close();
                    }
                    else
                    {
                        image = _image;
                    }

                    int ErrorOutput;

                    string PW = StringCipher.Encrypt(_PwBoxInputPW.Password.ToString().Trim(), ciperPass);

                    ErrorOutput = dp_F8.SetUserUpdateDelete(DBUpadteORDelete, _TbID.Text, PW, Level(_CbLevel.Text), _TbName.Text, _CbGender.Text, _TbEnumber.Text, _CbJop.Text,
                                                                                                                        _TbPhone.Text, _TbEmail.Text, _TbWG.Text, _TbIP.Text, _TbDay.Text, XmlAdd(), image);

                    child_SendMsgRefrh();
                    SetMenus();

                    switch (ErrorOutput)
                    {
                        case 0:
                            CommonUtil.MessageAlert("I0040", "사용자 정보에 대한 삭제가");
                            break;
                        case 1:
                            CommonUtil.MessageAlert("I0040", "사용자 정보 변경이");
                            break;
                        case 2:
                            CommonUtil.MessageAlert("X0002", "DB 저장");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                    m_LOG.LOG(ex.ToString(), "F8_02");
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

            return _ReturnLevel;
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
                if (LineCheck)
                {
                    break;
                }
            }
            return LineCheck;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            // if (!UsetSet)
            // {
            //     CommonUtil.MessageAlert("I0060", "");
            //    return;
            //}
             
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
                        SetUserSave(true);
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //if (!UsetSet)
            //{
            //    CommonUtil.MessageAlert("I0060", "");
            //    return;
            //}

            if (System.Windows.MessageBox.Show("현재 정보를 저장 하시겠습니까?", "저장 확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
            {
                SetUserSave(false);
            }
            else
            {
                return;
            }
        }

        private void grid_sub_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((UIElement)sender).Clip =
                new RectangleGeometry
                {
                    RadiusY =6 , RadiusX = 6,
                    Rect = new Rect(0,0,e.NewSize.Width,e.NewSize.Height)
                };
        }

        private void _CbNameList_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(Keyboard.GetKeyStates(Key.Enter) == KeyStates.Down)
            {
                child_SendMsg(NowdtList.Rows[Convert.ToInt16(_CbNameList.EditValue)]);
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

    public class ItemInfo
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }

}
