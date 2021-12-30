using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.IO;
using System.Data;

using YKCJViewModel.Common;
using Common.Utils;
using Common.Helper;
using DBConnection.F_08;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;

namespace YKCJViewModel.F_08
{
    public class F_08_01_ViewModel : ObservableObject
    {
        private readonly F_08_01_Model Model = new F_08_01_Model();
        private readonly CommonUtil Util = new CommonUtil();
        private readonly LOG_Write m_LOG = new LOG_Write();
        private readonly F_08_01_Provider F_08_Provider = new F_08_01_Provider();
        private const string ciperPass = "SUKMIN";

        public F_08_01_ViewModel()
        {
            Useritem = new ObservableCollection<F_08_01_CommonModel>()
            {
                new F_08_01_CommonModel(){Useritemcheck = false, Useritemname = "REPORT"},
                new F_08_01_CommonModel(){Useritemcheck = false, Useritemname = "SEARCH"},
                new F_08_01_CommonModel(){Useritemcheck = false, Useritemname = "UNDEVELOPED"},
                new F_08_01_CommonModel(){Useritemcheck = false, Useritemname = "UNDEVELOPED"},
                new F_08_01_CommonModel(){Useritemcheck = false, Useritemname = "UNDEVELOPED"},
                new F_08_01_CommonModel(){Useritemcheck = false, Useritemname = "NOTICES"},
                new F_08_01_CommonModel(){Useritemcheck = false, Useritemname = "PART LIST MODIFY"},
                new F_08_01_CommonModel(){Useritemcheck = false, Useritemname = "ACCOUNT"}
            };

            Userid = "";
            Username = "";
            Userjob = "";
            SaveMode = true;
            Userpwconfimation = "Password";
            Userimagepath = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Images\Commons\User\BasicUserPic.png";
        }

        private void Setinitialize(PasswordClear obj)
        {
            Userid = "";
            Username = "";
            Userjob = "";
            SaveMode = true;
            Userpwconfimation = "Password";
            Useridx = 0;
            Userbitmapimage = null;
            UserModifybyte = null;
            obj.F08_password.Clear();
            obj.F08_confirmpassword.Clear();
            Userimagepath = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Images\Commons\User\BasicUserPic.png";

            for(int i  = 0; i < Useritem.Count; i++)
            {
                Useritem[i].Useritemcheck = false;
            }
            
        }
        public DataTable Userlist
        {
            get { return Model._Userlist; }
            set
            {
                if (Model._Userlist != value)
                {
                    Model._Userlist = value;
                    RaisePropertyChanged(() => this.Userlist);
                }
            }
        }

        public ObservableCollection<F_08_01_CommonModel> Useritem
        {
            get { return Model._Useritem; }
            set
            {
                if (Model._Useritem != value)
                {
                    Model._Useritem = value;
                    RaisePropertyChanged(() => this.Useritem);
                }
            }
        }

        public bool SaveMode
        {
            get { return Model._SaveMode; }
            set
            {
                if (Model._SaveMode != value)
                {
                    Model._SaveMode = value;
                    RaisePropertyChanged(() => this.SaveMode);
                }
            }
        }

        public string Userid
        {
            get { return Model._Userid; }
            set
           {
                if (Model._Userid != value)
                {
                    //var regex = new Regex(@"[^a-zA-Z0-9\s]");
                    //if (regex.IsMatch(value.Substring(value.Length - 1, value.Length)))
                    //{
                        Model._Userid = value;
                        RaisePropertyChanged(() => this.Userid);
                    //}
                }
            }
        }

        public int Useridx
        {
            get { return Model._Useridx; }
            set
            {
                if (Model._Useridx != value)
                {
                    Model._Useridx = value;
                    if(value != 0)
                        SetUserModifyList(Model._Useridx);
                    RaisePropertyChanged(() => this.Useridx);
                }
            }
        }

        public string Userpw
        {
            get { return Model._Userpw; }
            set
            {
                if (Model._Userpw != value)
                {
                    Model._Userpw = value;
                    //Userpwcheck = value;
                    RaisePropertyChanged(() => this.Userpw);
                }
            }
        }
        public string Userpwcheck
        {
            get { return Model._Userpwcheck; }
            set
            {
                if (Model._Userpwcheck != value)
                {
                    Model._Userpwcheck = value;
                    RaisePropertyChanged(() => this.Userpwcheck);
                }
            }
        }

        public string Userpwconfimation
        {
            get { return Model._Userpwconfimation; }
            set
            {
                if (Model._Userpwconfimation != value)
                {
                    Model._Userpwconfimation = value;
                    RaisePropertyChanged(() => this.Userpwconfimation);
                }
            }
        }
        public string Username
        {
            get { return Model._Username; }
            set
            {
                if (Model._Username != value)
                {
                    Model._Username = value;
                    RaisePropertyChanged(() => this.Username);
                }
            }
        }
        public string Userjob
        {
            get { return Model._Userjob; }
            set
            {
                if (Model._Userjob != value)
                {
                    Model._Userjob = value;
                    RaisePropertyChanged(() => this.Userjob);
                }
            }
        }
        public string Userimagepath
        {
            get { return Model._Userimagepath; }
            set
            {
                if (Model._Userimagepath != value)
                {
                    Model._Userimagepath = value;
                    RaisePropertyChanged(() => this.Userimagepath);
                }
            }
        }

        public byte[] UserModifybyte
        {
            get { return Model._UserModifybyte; }
            set
            {
                if (Model._UserModifybyte != value)
                {
                    Model._UserModifybyte = value;
                    RaisePropertyChanged(() => this.UserModifybyte);
                }
            }
        }

        public BitmapImage Userbitmapimage
        {
            get { return Model._Userbitmapimage; }
            set
            {
                if (Model._Userbitmapimage != value)
                {
                    Model._Userbitmapimage = value;
                    RaisePropertyChanged(() => this.Userbitmapimage);
                }
            }
        }


        #region F08 Call Function
            
        public void GetUserList()
        {
            try
            {
                Userlist = F_08_Provider.GetUserList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F8_01_ViewModel Call Function(GetUserList)");
            }
        }

        private void SetUserModifyList(int idx)
        {
            try
            {
                DataSet ds = F_08_Provider.GetUser(idx);

                Userid = ds.Tables[0].Rows[0]["ID"].ToString();
                Username = ds.Tables[0].Rows[0]["NAME"].ToString();
                Userjob = ds.Tables[0].Rows[0]["JOB"].ToString();
                Userpwconfimation = string.Format("Password({0})", StringCipher.Decrypt(ds.Tables[0].Rows[0]["PW"].ToString(), ciperPass));

                UserModifybyte = (byte[])ds.Tables[0].Rows[0]["IMAGE"];
                Userbitmapimage = Util.ByteArrayToBitmaplmage((byte[])ds.Tables[0].Rows[0]["IMAGE"]);

                SetUserModifyListMenuItem(ds.Tables[1]);

                SaveMode = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F8_01_ViewModel Call Function(SetUserModifyList)");
            }
        }

        private void SetUserModifyListMenuItem(DataTable dt)
        {
            try
            {
                for (int i = 0; i < Useritem.Count; i++)
                {
                    Useritem[i].Useritemcheck = (bool)dt.Rows[0][string.Format("F_0{0}", 1 + i)];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F8_01_ViewModel Call Function(SetUserModifyListMenuItem)");
            }
        }

        private byte[] Userimagetobyte(string imagepath)
        {
            byte[] image = null;

            try
            {
                FileStream fs = new FileStream(imagepath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);

                image = br.ReadBytes((int)fs.Length);

                br.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F8_01_ViewModel Call Function(Userimagetobyte)");
            }

            return image;
        }

        private string Useritemtostring(ObservableCollection<F_08_01_CommonModel> user)
        {
            string menuitemlist = "";

            ObservableCollection<F_08_01_CommonModel> copy = new ObservableCollection<F_08_01_CommonModel>(user);

            foreach (F_08_01_CommonModel item in copy)
            {
                menuitemlist += item.Useritemcheck ? "1" : "0";
            }

            return menuitemlist;
        }

        private bool Passwordcheck(string pw, string pwre)
        {
            //if (pw is null || pwre is null)
            //{
            //    return true;
            //}
            //else
            //{
            //    if (pw.Trim() != pwre.Trim())
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            return pw is null || pwre is null || pw == "" || pwre == "" || (pw.Trim() != pwre.Trim());
        }

        #endregion



        #region ICommand List

        private RelayCommand _Cmdimagechange;

        public ICommand Cmdimagechange => _Cmdimagechange ?? (this._Cmdimagechange = new RelayCommand(Imagechange));

        private void Imagechange()
        {
            try
            {
                string images = Util.GetImage();

                if (!(images == ""))
                {
                    Userimagepath = images;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F8_01_ViewModel Command(ImageChange)");
            }
        }

        private RelayCommand<PasswordClear> _Cmdnewuser;

        public ICommand Cmdnewuser => _Cmdnewuser ?? (this._Cmdnewuser = new RelayCommand<PasswordClear>(NewUser));

        private void NewUser(PasswordClear obj)
        {
            try
            {
                Setinitialize(obj);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F8_01_ViewModel Command(NewUser)");
            }
        }

        private RelayCommand<PasswordClear> _Cmdusersave;

        public ICommand Cmdusersave => _Cmdusersave ?? (this._Cmdusersave = new RelayCommand<PasswordClear>(Usersave));

        private async void Usersave(PasswordClear obj)
        {
            try
            {
                int ErrorOutput = 0;

                if (MessageBox.Show("현재 정보를 저장 하시겠습니까?", "저장 확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                {
                    if (Passwordcheck(Userpw,Userpwcheck))
                    {
                        CommonUtil.MessageAlert("I9001", "");
                        return;
                    }
                    else
                    {
                        if (Userid.Trim() == "" || Username.Trim() == "")
                        {
                            CommonUtil.MessageAlert("I0050", "");
                            return;
                        }
                        else
                        {
                            ErrorOutput = await SetUserSave();
                        }
                    }
                }
                else
                {
                    return;
                }

                switch (ErrorOutput)
                {
                    case 0:
                        CommonUtil.MessageAlert("I0040", "사용자 정보에 대한 저장이");
                        Setinitialize(obj);
                        break;
                    case 1:
                        CommonUtil.MessageAlert("I0011", "ID가 중복 입니다. ID");
                        break;
                    case 2:
                        CommonUtil.MessageAlert("X0002", "(DB 저장)");
                        break;
                    case 3:
                        CommonUtil.MessageAlert("X0001", "(프로그램 함수)");
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F8_01_ViewModel Command(UserSave)");
            }
        }


        private async Task<int> SetUserSave()
        {
            try
            {
                int ErrorCheck = 0;

                if (Userimagepath != null || Userimagepath != "")
                {

                    await Task.Run(() =>
                    {
                        string PW = StringCipher.Encrypt(Userpw.Trim(), ciperPass);

                        ErrorCheck = F_08_Provider.SetUserSave(Userid.Trim(), PW, Username.Trim(), Userjob.Trim(), Userimagetobyte(Userimagepath), Useritemtostring(Useritem));
                    });

                }

                return ErrorCheck;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F8_01_ViewModel Command(SetUserSave)");
                return 3;
            }
        }


        private RelayCommand<PasswordClear> _Cmduserupdate;

        public ICommand Cmduserupdate => _Cmduserupdate ?? (this._Cmduserupdate = new RelayCommand<PasswordClear>(Userupdate));

        private async void Userupdate(PasswordClear obj)
        {
            try
            {
                int ErrorOutput = 0;

                if (MessageBox.Show("현재 정보를 변경 하시겠습니까?", "변경 확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                {
                    if (Passwordcheck(Userpw, Userpwcheck))
                    {
                        CommonUtil.MessageAlert("I9001", "");
                        return;
                    }
                    else
                    {
                        if (Userid.Trim() == "" || Username.Trim() == "")
                        {
                            CommonUtil.MessageAlert("I0050", "");
                            return;
                        }
                        else
                        {
                            ErrorOutput = await SetUserUpdate();
                        }
                    }
                }
                else
                {
                    return;
                }

                switch (ErrorOutput)
                {
                    case 0:
                        CommonUtil.MessageAlert("I0040", "사용자 정보에 대한 변경이");
                        Setinitialize(obj);
                        obj.F08_getuserlist.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                        break;
                    case 1:
                        CommonUtil.MessageAlert("X0002", "(DB 저장)");
                        break;
                    case 2:
                        CommonUtil.MessageAlert("X0001", "(프로그램 함수)");
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F8_01_ViewModel Command(Userupdate)");
            }
        }

        private async Task<int> SetUserUpdate()
        {
            try
            {
                int ErrorCheck = 0;


                await Task.Run(() =>
                    {
                        string PW = StringCipher.Encrypt(Userpw.Trim(), ciperPass);

                        byte[] imagebyte = null;

                        if(Userimagepath != AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Images\Commons\User\BasicUserPic.png")
                        {
                            imagebyte = Userimagetobyte(Userimagepath);
                        }
                        else
                        {
                            imagebyte = UserModifybyte;
                        }

                        ErrorCheck = F_08_Provider.SetUserUpdate(Useridx, PW, Username.Trim(), Userjob.Trim(), imagebyte, Useritemtostring(Useritem));
                    });

                return ErrorCheck;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F8_01_ViewModel Command(SetUserUpdate)");
                return 2;
            }
        }


        private RelayCommand<PasswordClear> _Cmduserdelete;

        public ICommand Cmduserdelete => _Cmduserdelete ?? (this._Cmduserdelete = new RelayCommand<PasswordClear>(UserDelete));

        private async void UserDelete(PasswordClear obj)
        {
            try
            {
                int ErrorOutput = 0;

                if (MessageBox.Show("현재 정보를 삭제 하시겠습니까?", "삭제 확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                {
                    await Task.Run(() =>
                    {
                        ErrorOutput = F_08_Provider.SetUserDelete(Useridx);
                    });
                }
                else
                {
                    return;
                }

                switch (ErrorOutput)
                {
                    case 0:
                        CommonUtil.MessageAlert("I0040", "사용자 정보에 대한 삭제가");
                        Setinitialize(obj);
                        obj.F08_getuserlist.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                        break;
                    case 1:
                        CommonUtil.MessageAlert("X0002", "(DB 저장)");
                        break;
                }

            }
            catch (Exception ex)
            {
                CommonUtil.MessageAlert("X0001", "(프로그램 함수)");
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F8_01_ViewModel Command(UserDelete)");
            }
        }

        #endregion
    }
}
