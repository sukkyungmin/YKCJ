using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections;
using System.ComponentModel;
using DMSGalaxy.ViewModel.Common;

namespace DMSGalaxy.ViewModel.F2
{
    public class F2_02_ModelProduction : ObservableObject 
    {
        public F2_02_ModelProduction()
        {

            PPersons = new ObservableCollection<Person>()
                   {
                           new Person(){ Name="TOTAL COUNT"}  
                          ,new Person(){ Name="WASTE COUNT"}  
                          ,new Person(){Name="BOX COUNT"}  
                          ,new Person(){Name="RUNNINGTIME"}  
                          ,new Person(){Name="DOWNTIME"}
                          ,new Person(){Name="DOWNCOUNT"}
                   };


            PPersonsLimit = new ObservableCollection<Person>()
                   {
                           new Person(){ Name="UP"}  
                          ,new Person(){ Name="DOWN"}  
                   };

            PListboxpersons = new ObservableCollection<F2_02_CommonModel>()
            {
                new F2_02_CommonModel(){LimitCheck = false, LimitCount =0,LimitName="", PersonLimit = PPersonsLimit,Personitem = PPersons},
                new F2_02_CommonModel(){LimitCheck = false, LimitCount =0,LimitName="", PersonLimit = PPersonsLimit,Personitem = PPersons},
                new F2_02_CommonModel(){LimitCheck = false, LimitCount =0,LimitName="", PersonLimit = PPersonsLimit ,Personitem = PPersons}
            };

            Production = true;
            OldCheckPart = "ProductionDate";

        }

        private bool _Production;

        public bool Production
        {
            get { return _Production; }
            set
            {
                if(_Production != value)
                {
                    _Production = value;
                    RaisePropertyChanged(() => this.Production);
                }
            }
        }

        private string _oldCheckPart;

        public string OldCheckPart
        {
            get { return _oldCheckPart; }
            set
            {
                if (_oldCheckPart != value)
                {
                    _oldCheckPart = value;
                }
            }
        }


        /// <summary>
        /// Combobox Item List
        /// </summary>
        private ObservableCollection<Person> _Ppersons;

        public ObservableCollection<Person> PPersons
        {
            get { return _Ppersons; }
            set { _Ppersons = value; }
        }

        private ObservableCollection<Person> _PpersonsLimit;

        public ObservableCollection<Person> PPersonsLimit
        {
            get { return _PpersonsLimit; }
            set { _PpersonsLimit = value; }
        }

        private ObservableCollection<F2_02_CommonModel> _Plistboxpersons;

        public ObservableCollection<F2_02_CommonModel> PListboxpersons
        {
            get { return _Plistboxpersons; }
            set { _Plistboxpersons = value; }
        }

        #region Orderby  & Part  View model 옮긴 후추후 삭제

        /// <summary>
        /// Combobox Orderby
        /// </summary>
        private Person _orderby;

        public Person Orderby
        {
            get { return _orderby; }
            set
            {
                if(_orderby != value)
                {
                    this._orderby = value;
                    Selectoderby = _orderby.Name;
                }
            }
        }

        /// <summary>
        /// Combobox Orderby Select item
        /// </summary>
        private string _selectorderby;

        public string Selectoderby
        {
            get { return _selectorderby; }
            set
            {
                if(_selectorderby != value)
                {
                    _selectorderby = value;
                    RaisePropertyChanged(() => this.Selectoderby);
                }
            }
        }


        /// <summary>
        ///  Search Part1 Production Grid Select
        /// </summary>
        private bool _Part1Production;

        public bool Part1Production
        {
            get { return _Part1Production; }
            set
            {
                if(_Part1Production != value)
                {
                    if(_Part1Production)
                    {
                        _Part1Production = value;
                        _Part1Changeover = false;
                        RaisePropertyChanged(() => this.Part1Production);
                        RaisePropertyChanged(() => this.Part1ChangeOver);
                    }
                }
            }
        }

        /// <summary>
        ///  Search Part1 Production Grid Select
        /// </summary>
        private bool _Part1Changeover;

        public bool Part1ChangeOver
        {
            get { return _Part1Changeover; }
            set
            {
                if (_Part1Changeover != value)
                {
                    if (_Part1Changeover)
                    {
                        _Part1Changeover = value;
                        _Part1Production = false;
                        RaisePropertyChanged(() => this.Part1ChangeOver);
                        RaisePropertyChanged(() => this.Part1Production);
                    }
                }
            }
        }

        #endregion
    }
}
