using System;
using System.Windows;
using System.Data;
using System.Windows.Input;
using System.Web;
using System.Collections.ObjectModel;

using YKCJViewModel.Common;
using Common.Utils;
using Common.Infos;
using Common.Helper;
using DBConnection.F_07;
using System.Threading.Tasks;

namespace YKCJViewModel.F_07
{
    public class F_07_01_ViewModel : ObservableObject
    {
        private F_07_01_Model Model = new F_07_01_Model();
        private F_07_01_Provider F_07_Provider = new F_07_01_Provider();
        private LOG_Write m_LOG = new LOG_Write();

        public F_07_01_ViewModel()
        {
            //SetinitializeDB();
        }

        private void SetinitialzeModel()
        {
            Listitem = new ObservableCollection<F_07_01_CommonModel>()
            {
                new F_07_01_CommonModel(){Listitemcheck = true,  Listitemname = "MACHINE", Saveitemname = ""},
                new F_07_01_CommonModel(){Listitemcheck = false, Listitemname = "CLASS", Saveitemname = ""},
                new F_07_01_CommonModel(){Listitemcheck = false, Listitemname = "PART1", Saveitemname = ""},
                new F_07_01_CommonModel(){Listitemcheck = false, Listitemname = "PART2", Saveitemname = ""}
            };
        }

        private async void SetinitializeDB(int partidx)
        {
            try
            {
                DataSet dataSet = await SetPartListSaveAsync();
                //DataRow dr;

                Machinelist = dataSet.Tables[0];

                ClassList = dataSet.Tables[1];

                Modulepart1list = dataSet.Tables[2];

                if(!(Modulepart1list is null))
                {
                    Selectpart1 = Convert.ToInt32(Modulepart1list.Rows[0]["Idx"]);
                }

                if (partidx == 2)
                {
                    Gridselectpart1topart2 = "";
                    Modulepart2list = null;
                }
                else if (partidx == 3)
                {
                    Modulepart2list = F_07_Provider.GetComboboxPart2List(Gridselectpart1idx);
                }

                SetinitialzeModel();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F7_01_ViewModel (SetinitializeDB)");
            }
        }

        private async Task<DataSet> SetPartListSaveAsync()
        {
            DataSet dataset = null;

            await Task.Run(() => {
                dataset = F_07_Provider.GetComboboxList();
            });

            return dataset;
        }

        public ObservableCollection<F_07_01_CommonModel> Listitem
        {
            get { return Model._Listitem; }
            set
            {
                if (Model._Listitem != value)
                {
                    Model._Listitem = value;
                    RaisePropertyChanged(() => this.Listitem);
                }
            }
        }

        public DataTable Machinelist
        {
            get { return Model._Machinelist; }
            set
            {
                if (Model._Machinelist != value)
                {
                    Model._Machinelist = value;
                    RaisePropertyChanged(() => this.Machinelist);
                }
            }
        }

        public DataTable ClassList
        {
            get { return Model._ClassList; }
            set
            {
                if (Model._ClassList != value)
                {
                    Model._ClassList = value;
                    RaisePropertyChanged(() => this.ClassList);
                }
            }
        }

        public DataTable Modulepart1list
        {
            get { return Model._Modulepart1list; }
            set
            {
                if (Model._Modulepart1list != value)
                {
                    Model._Modulepart1list = value;
                    RaisePropertyChanged(() => this.Modulepart1list);
                }
            }
        }

        public DataTable Modulepart2list
        {
            get { return Model._Modulepart2list; }
            set
            {
                if (Model._Modulepart2list != value)
                {
                    Model._Modulepart2list = value;
                    RaisePropertyChanged(() => this.Modulepart2list);
                }
            }
        }

        public int Selectpart1
        {
            get { return Model._Selectpart1; }
            set
            {
                if (Model._Selectpart1 != value)
                {
                    Model._Selectpart1 = value;
                    RaisePropertyChanged(() => this.Selectpart1);
                }
            }
        }

        public int Gridselectpart1idx
        {
            get { return Model._Gridselectpart1idx; }
            set
            {
                if (Model._Gridselectpart1idx != value)
                {
                    Model._Gridselectpart1idx = value;
                    RaisePropertyChanged(() => this.Gridselectpart1idx);
                }
            }
        }

        public int Selectmodifypart
        {
            get { return Model._Selectmodifypart; }
            set
            {
                if (Model._Selectmodifypart != value)
                {
                    Model._Selectmodifypart = value;
                    RaisePropertyChanged(() => this.Selectmodifypart);

                    switch (value)
                    {
                        case 0:
                            Selectmodifypartname = "MACHINE";
                            break;
                        case 1:
                            Selectmodifypartname = "CLASS";
                            break;
                        case 2:
                            Selectmodifypartname = "PART1";
                            break;
                        case 3:
                            Selectmodifypartname = "PART2";
                            break;
                    }
                }
            }
        }

        public string Selectmodifypartname
        {
            get {
                if (Model._Selectmodifypartname is null)
                    return "MACHINE";

                return Model._Selectmodifypartname;
                }
            set
            {
                if (Model._Selectmodifypartname != value)
                {
                    Model._Selectmodifypartname = value;
                    RaisePropertyChanged(() => this.Selectmodifypartname);
                }
            }
        }

        public string Modifyidx
        {
            get { return Model._Modifyidx; }
            set
            {
                if (Model._Modifyidx != value)
                {
                    Model._Modifyidx = value;
                    RaisePropertyChanged(() => this.Modifyidx);
                }
            }
        }

        public string Modifyname
        {
            get { return Model._Modifyname; }
            set
            {
                if (Model._Modifyname != value)
                {
                    Model._Modifyname = value;
                    RaisePropertyChanged(() => this.Modifyname);
                }
            }
        }

        public string Gridselectpart1topart2
        {
            get { return Model._Gridselectpart1topart2; }
            set
            {
                if (Model._Gridselectpart1topart2 != value)
                {
                    Model._Gridselectpart1topart2 = value;
                    RaisePropertyChanged(() => this.Gridselectpart1topart2);
                }
            }
        }


        #region ICommand List

        #region SetPartListSave

        private RelayCommand<Window> _CmdSetPartListSave;

        public ICommand CmdSetPartListSave => _CmdSetPartListSave ?? (this._CmdSetPartListSave = new RelayCommand<Window>(SetPartListSave));

        private async void SetPartListSave(Window obj)
        {
            try
            {
                int ErrorOutput = 0;
                object[] Saveitemlist = SetPartListSaveGetItemName();


                if (MessageBox.Show("현재 정보를 저장 하시겠습니까?", "저장 확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                {
                    if (Saveitemlist[1].ToString().Replace(" ","") != "")
                    {
                        ErrorOutput = await SetPartListSaveAsync(Saveitemlist);
                    }
                    else
                    {
                        CommonUtil.MessageAlert("C0001", "선택된 아이템의 이름");
                        return;
                    }
                }
                else
                {
                    return;
                }

                switch (ErrorOutput)
                {
                    case 0:
                        CommonUtil.MessageAlert("I0040", "파트리스트에 대한 저장이");
                        await Task.Run(() => SetinitializeDB(Convert.ToInt32(Saveitemlist[0])));
                        WindwClose(obj);
                        break;
                    case 1:
                        CommonUtil.MessageAlert("I0070", "DB LIST TABLE");
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
                m_LOG.LOG(ex.ToString(), "F7_01_ViewModel Command(SetPartListSave)");
            }
        }

        private async Task<int> SetPartListSaveAsync(object[] obj)
        {
            try
            {
                int ErrorCheck = 0;

                await Task.Run(() =>
                {
                    ErrorCheck = F_07_Provider.SetPartListSave(Convert.ToInt32(obj[0]), Selectpart1, obj[1].ToString());
                });

                return ErrorCheck;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F7_01_ViewModel Command(SetPartListSaveAsync)");
                return 3;
            }
        }


        private object[] SetPartListSaveGetItemName()
        {
            object[] obj = new object[2];

            for (int i = 0; i < Listitem.Count; i++)
            {
                if(Listitem[i].Listitemcheck)
                {
                    obj[0] = i;
                    obj[1] = Listitem[i].Saveitemname;
                }
            }

            return obj;
        }

        #endregion

        #region SetPartListModifyPopupItem

        private RelayCommand<ModelToMultiObj> _CmdSetPartListModifyPopupItem;

        public ICommand CmdSetPartListModifyPopupItem => _CmdSetPartListModifyPopupItem ?? (this._CmdSetPartListModifyPopupItem = new RelayCommand<ModelToMultiObj>(SetPartListModifyPopupItem));

        private void SetPartListModifyPopupItem(ModelToMultiObj obj)
        {
            try
            {
                Selectmodifypart = Convert.ToInt32(obj.obj01);
                Modifyidx = obj.obj02.ToString();
                Modifyname = obj.obj03.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F7_01_ViewModel Command(SetPartListModifyPopupItem)");
            }
        }

        #endregion

        #region SetModifyUpdate

        private RelayCommand<Window> _CmdSetPartListModifyUpdate;

        public ICommand CmdSetPartListModifyUpdate => _CmdSetPartListModifyUpdate ?? (this._CmdSetPartListModifyUpdate = new RelayCommand<Window>(SetPartListModifyUpdate));

        private async void SetPartListModifyUpdate(Window obj)
        {
            try
            {
                int ErrorOutput = 0;

                if (MessageBox.Show("현재 정보를 변경 하시겠습니까?", "변경 확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                {
                    if (Modifyname.Replace(" ", "") != "")
                    {
                        ErrorOutput = await SetPartListModifyUpdateAsync();
                    }
                    else
                    {
                        CommonUtil.MessageAlert("C0001", "선택된 아이템의 이름");
                        return;
                    }
                }
                else
                {
                    return;
                }

                switch (ErrorOutput)
                {
                    case 0:
                        CommonUtil.MessageAlert("I0040", "파트리스트에 대한 업데이트가");
                        await Task.Run(() => SetinitializeDB(Selectmodifypart));
                        WindwClose(obj);
                        break;
                    case 1:
                        CommonUtil.MessageAlert("I0070", "DB LIST TABLE");
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
                m_LOG.LOG(ex.ToString(), "F7_01_ViewModel Command(SetPartListModifyUpdate)");
            }
        }

        private async Task<int> SetPartListModifyUpdateAsync()
        {
            try
            {
                int ErrorCheck = 0;

                await Task.Run(() =>
                {
                    ErrorCheck = F_07_Provider.SetPartListUpdate(Selectmodifypart, Modifyidx, Modifyname);
                });

                return ErrorCheck;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F7_01_ViewModel Command(SetPartListModifyUpdateAsync)");
                return 3;
            }
        }

        #endregion

        #region SetDeletePartList

        private RelayCommand<ModelToMultiObj> _CmdSetDeletePartList;

        public ICommand CmdSetDeletePartList => _CmdSetDeletePartList ?? (this._CmdSetDeletePartList = new RelayCommand<ModelToMultiObj>(SetDeletePartList));

        private async void SetDeletePartList(ModelToMultiObj obj)
        {
            try
            {
                int ErrorOutput = 0;

                if (MessageBox.Show(string.Format("현재 {0} 리스트를 삭제 하시겠습니까?",obj.obj03.ToString()), "삭제 확인", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                {

                   ErrorOutput = await SetDeletePartListAsync(Convert.ToInt32(obj.obj01), Convert.ToInt32(obj.obj02));

                }
                else
                {
                    return;
                }

                switch (ErrorOutput)
                {
                    case 0:
                        CommonUtil.MessageAlert("I0040", string.Format("{0} 리스트에 대한 삭제가", obj.obj03.ToString()));
                        await Task.Run(() => SetinitializeDB(Convert.ToInt32(obj.obj01)));
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
                m_LOG.LOG(ex.ToString(), "F7_01_ViewModel Command(SetDeletePartList)");
            }
        }

        private async Task<int> SetDeletePartListAsync(int deleteitempart, int deleteidx)
        {
            try
            {
                int ErrorCheck = 0;

                await Task.Run(() =>
                {
                    ErrorCheck = F_07_Provider.SetPartListDelete(deleteitempart, deleteidx);
                });


                return ErrorCheck;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F1_01_ViewModel Command(SetWorkListDeleteAsync)");
                return 2;
            }
        }

        #endregion

        #region SetDeletePartList

        private RelayCommand<ModelToMultiObj> _CmdGetPart2List;

        public ICommand CmdGetPart2List => _CmdGetPart2List ?? (this._CmdGetPart2List = new RelayCommand<ModelToMultiObj>(GetPart2List));

        private async void GetPart2List(ModelToMultiObj obj)
        {
            try
            {
                Gridselectpart1idx = Convert.ToInt32(obj.obj02);
                Gridselectpart1topart2 = obj.obj03.ToString();
                Modulepart2list =  await GetPart2ListAsync(Convert.ToInt32(obj.obj02));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F7_01_ViewModel Command(GetPart2List)");
            }
        }

        private async Task<DataTable> GetPart2ListAsync(int Part1Idx)
        {
            try
            {
                DataTable dt = null;

                await Task.Run(() =>
                {
                    dt = F_07_Provider.GetComboboxPart2List(Part1Idx);
                });

                return dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                m_LOG.LOG(ex.ToString(), "F1_01_ViewModel Command(GetPart2ListAsync)");
                return null;
            }
        }

        #endregion


        #region SetPopupWindowClose

        private RelayCommand<Window> _CmdWindowClose;

        public ICommand CmdWindowClose => _CmdWindowClose ?? (this._CmdWindowClose = new RelayCommand<Window>(WindwClose));

        private void WindwClose(Window obj)
        {
            obj.Close();
        }

        #endregion


        #region SetTest

        private RelayCommand _CmdListSearch;

        public ICommand CmdListSearch => _CmdListSearch ?? (this._CmdListSearch = new RelayCommand(ListSearch));

        private void ListSearch()
        {
            SetinitializeDB(100);
        }

        #endregion


        #endregion

    }
}
