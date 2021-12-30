using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DMSGalaxy.ViewModel.Common;
using System.Collections.ObjectModel;

namespace DMSGalaxy.ViewModel.F2
{
    public class F2_02_ModelWaste : ObservableObject
    {
       public F2_02_ModelWaste()
        {
            WPersons = new ObservableCollection<Person>()
                   {
                           new Person(){ Name="OCCUR COUNT"}  
                          ,new Person(){ Name="DEFECT COUNT"}  
                   };

            WPersonsLimit = new ObservableCollection<Person>()
                   {
                           new Person(){ Name="UP"}  
                          ,new Person(){ Name="DOWN"}  
                   };

            WListboxpersons = new ObservableCollection<F2_02_CommonModel>()
            {
                new F2_02_CommonModel(){LimitCheck = false, LimitCount =0,LimitName="", PersonLimit = WPersonsLimit ,Personitem = WPersons},
                new F2_02_CommonModel(){LimitCheck = false, LimitCount =0,LimitName="", PersonLimit = WPersonsLimit ,Personitem = WPersons},
                new F2_02_CommonModel(){LimitCheck = false, LimitCount =0,LimitName="",PersonLimit = WPersonsLimit ,Personitem = WPersons}
            };

            Waste = false;
            OldCheckPart = "WasteDate";

        }

        private bool _Waste;

        public bool Waste
        {
            get { return _Waste; }
            set
            {
                if (_Waste != value)
                {
                    _Waste = value;
                    RaisePropertyChanged(() => this.Waste);
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
        private ObservableCollection<Person> _Wpersons;

        public ObservableCollection<Person> WPersons
        {
            get { return _Wpersons; }
            set { _Wpersons = value; }
        }

        private ObservableCollection<Person> _WpersonsLimit;

        public ObservableCollection<Person> WPersonsLimit
        {
            get { return _WpersonsLimit; }
            set { _WpersonsLimit = value; }
        }

        private ObservableCollection<F2_02_CommonModel> _Wlistboxpersons;

        public ObservableCollection<F2_02_CommonModel> WListboxpersons
        {
            get { return _Wlistboxpersons; }
            set { _Wlistboxpersons = value; }
        }
    }
}
