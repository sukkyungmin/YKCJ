using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DMSGalaxy.ViewModel.Common;
using System.Collections.ObjectModel;

namespace DMSGalaxy.ViewModel.F2
{
    public class F2_02_ModelDelay : ObservableObject
    {
           public F2_02_ModelDelay()
        {

            DPersons = new ObservableCollection<Person>()
                   {
                          new Person(){ Name="발생횟수"}  
                          ,new Person(){Name="DOWNTIME(분)"}  
                   };

            DPersonsLimit = new ObservableCollection<Person>()
                   {
                           new Person(){ Name="UP"}  
                          ,new Person(){ Name="DOWN"}  
                   };

            DListboxpersons = new ObservableCollection<F2_02_CommonModel>()
            {
                new F2_02_CommonModel(){LimitCheck = false, LimitCount =0,LimitName="", PersonLimit = DPersonsLimit ,Personitem = DPersons},
                new F2_02_CommonModel(){LimitCheck = false, LimitCount =0,LimitName="", PersonLimit = DPersonsLimit,Personitem = DPersons},
                new F2_02_CommonModel(){LimitCheck = false, LimitCount =0,LimitName="", PersonLimit = DPersonsLimit ,Personitem = DPersons}
            };

            Delay = false;
            OldCheckPart = "DelayDate";
        }

        private bool _Delay;

        public bool Delay
        {
            get { return _Delay; }
            set
            {
                if (_Delay != value)
                {
                    _Delay = value;
                    RaisePropertyChanged(() => this.Delay);
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
        private ObservableCollection<Person> _Dpersons;

        public ObservableCollection<Person> DPersons
        {
            get { return _Dpersons; }
            set { _Dpersons = value; }
        }


        private ObservableCollection<Person> _DpersonsLimit;

        public ObservableCollection<Person> DPersonsLimit
        {
            get { return _DpersonsLimit; }
            set { _DpersonsLimit = value; }
        }

        private ObservableCollection<F2_02_CommonModel> _Dlistboxpersons;

        public ObservableCollection<F2_02_CommonModel> DListboxpersons
        {
            get { return _Dlistboxpersons; }
            set { _Dlistboxpersons = value; }
        }
    }
}
