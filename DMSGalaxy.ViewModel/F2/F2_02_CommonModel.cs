using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMSGalaxy.ViewModel.Common;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System.Collections.ObjectModel;

namespace DMSGalaxy.ViewModel.F2
{
    public class F2_02_CommonModel : ObservableObject
    {
        #region Report Mode Combobox List

        private int _limitCount;

        public int LimitCount
        {
            get { return _limitCount; }
            set
            {
                if (_limitCount != value)
                {
                    _limitCount = value;
                    this.RaisePropertyChanged(() => this.LimitCount);
                }
            }
        }

        private string _limitname;

        public string LimitName
        {
            get { return _limitname; }
            set
            {
                if (_limitname != value)
                {
                    _limitname = value;
                    this.RaisePropertyChanged(() => this.LimitName);
                }
            }
        }

        private string _limitUpdown;

        public string LimitUpdown
        {
            get { return _limitUpdown; }
            set
            {
                if (_limitUpdown != value)
                {
                    _limitUpdown = value;
                    this.RaisePropertyChanged(() => this.LimitUpdown);
                }
            }
        }

        private bool _limitcheck;

        public bool LimitCheck
        {
            get { return _limitcheck; }
            set
            {
                if (_limitcheck != value)
                {
                    _limitcheck = value;
                    this.RaisePropertyChanged(() => this.LimitCheck);
                }
            }
        }

        /// <summary>
        /// Limit Name
        /// </summary>

        private ObservableCollection<Person> _personitem;

        public ObservableCollection<Person> Personitem
        {
            get { return _personitem; }
            set { _personitem = value; }
        }

        private Person _spersonitem;

        public Person Spersonitem
        {
            get { return _spersonitem; }
            set
            {
                this._spersonitem = value;
                this.RaisePropertyChanged(() => this.Spersonitem);
                LimitName = this._spersonitem.Name;
            }
        }

        /// <summary>
        /// UpDown Limit
        /// </summary>

        private ObservableCollection<Person> _personLimit;

        public ObservableCollection<Person> PersonLimit
        {
            get { return _personLimit; }
            set { _personLimit = value; }
        }

        private Person _spersonLimit;

        public Person SpersonLimit
        {
            get { return _spersonLimit; }
            set
            {
                this._spersonLimit = value;
                this.RaisePropertyChanged(() => this.SpersonLimit);
                LimitUpdown = this._spersonLimit.Name;
            }
        }
        #endregion

        #region Report Limit Check Return  // Unused  Code

        public RelayCommand _CmReporLimitCheckReturn;

        private void LimitCheckReturn()
        {
            try
            {
                LimitCheck = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Login _Login = new Login();
                //_Login.UserStatusReset(UserInfo.US_ID, false);
            }
        }

        bool CanReporLimitCheckReturn()
        {
            return true;
        }


        public ICommand CmReporLimitCheckReturn
        {
            get
            {
                return _CmReporLimitCheckReturn ?? (this._CmReporLimitCheckReturn =
                    new RelayCommand(LimitCheckReturn, CanReporLimitCheckReturn));
            }
        }


        #endregion


    }

    public class OrderbyPerson : ObservableObject
    {
        private string _orderbyname;

        public string OrderbyName
        {
            get { return _orderbyname; }
            set
            {
                if (_orderbyname != value)
                {
                    _orderbyname = value;
                    this.RaisePropertyChanged(() => this.OrderbyName);
                }
            }
        }

        private bool _orderbysort;

        public bool OrderbySort
        {
            get { return _orderbysort; }
            set { _orderbysort = value; }
        }

        private ObservableCollection<Person> _orderbypersonitem;

        public ObservableCollection<Person> OrderbyPersonitem
        {
            get { return _orderbypersonitem; }
            set { _orderbypersonitem = value; }
        }

        private Person _orderbyspersonitem;

        public Person OrderbySpersonitem
        {
            get { return _orderbyspersonitem; }
            set
            {
                this._orderbyspersonitem = value;
                OrderbyName = this._orderbyspersonitem.Name;
            }
        }
    }

    public class Person
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }

}
