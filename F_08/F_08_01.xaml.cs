using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

using Common.Helper;
using YKCJViewModel.F_08;
using YKCJ_EngineerReport.F_08.Cons;
using YKCJViewModel.Common;

namespace YKCJ_EngineerReport.F_08
{
    /// <summary>
    /// F_08_01.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F_08_01 : Page
    {

        private readonly F_08_01_ViewModel Viewmodel = new F_08_01_ViewModel();
        private F_08_01_UserBox[] Con_Userbox = null;

        private readonly LOG_Write m_LOG = new LOG_Write();

        public F_08_01()
        {
            InitializeComponent();

            DataContext = Viewmodel;

            Setinitialize();
        }

        private void Setinitialize()
        {
            FocusManager.SetFocusedElement(this, _Userid);
        }

        private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            Viewmodel.Userpw = ((PasswordBox)sender).Password;
        }

        private void PasswordCheckBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            Viewmodel.Userpwcheck = ((PasswordBox)sender).Password;
        }

        private void ModifyUserListButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                Viewmodel.GetUserList();

                if(Viewmodel.Userlist.Rows.Count > 0)
                {
                    _Wrap_userlist.Children.Clear();

                    Con_Userbox = new F_08_01_UserBox[Viewmodel.Userlist.Rows.Count];

                    for (int i = 0; i < Viewmodel.Userlist.Rows.Count; i++)
                    {
                        Con_Userbox[i] = new F_08_01_UserBox(Viewmodel.Userlist.Rows[i]);
                        Con_Userbox[i].SendIndex += new F_08_01_UserBox.SendIntIndex(SetUserModifyList);
                        _Wrap_userlist.Children.Add(Con_Userbox[i]);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F_08_01.xaml Button Click Event(ModifyUserListButton_Click)");
            }
        }

        private void SetUserModifyList(int idx)
        {
            Viewmodel.Useridx = idx;
        }
    }
}
