using YKCJViewModel.Common;

namespace YKCJViewModel.CommonCons
{
    public class MenuViewModel : ObservableObject
    {
        private readonly MenuModel Menu = new MenuModel();

        public string Menutoptile
        {
            get { return Menu._Menutoptile; }
            set
            {
                if(Menu._Menutoptile != value)
                {
                    Menu._Menutoptile = value;
                    RaisePropertyChanged(() => this.Menutoptile);
                }
            }
        }

        public string Menubottomtile
        {
            get { return Menu._Menubottomtile; }
            set
            {
                if (Menu._Menubottomtile != value)
                {
                    Menu._Menubottomtile = value;
                    RaisePropertyChanged(() => this.Menubottomtile);
                }
            }
        }

        public string Menuchangepage
        {
            get { return Menu._Menuchangepage; }
            set
            {
                if (Menu._Menuchangepage != value)
                {
                    Menu._Menuchangepage = value;
                    RaisePropertyChanged(() => this.Menuchangepage);
                }
            }
        }

        public int Menuindex
        {
            get { return Menu._Menuindex; }
            set
            {
                if (Menu._Menuindex != value)
                {
                    Menu._Menuindex = value;
                    RaisePropertyChanged(() => this.Menuindex);
                }
            }
        }

    }
}
