using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Windows.Forms;

using DBConnection.DataAccess;
using Common.Infos;

namespace DBConnection.Base
{
    public class DataProviderBase
    {
        // Fields
        private DataBaseCommand _dbComand = null;

        // Methods
        public DataProviderBase()
        {
            try
            {
                SetDbCommand();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

        public DataBaseCommand GetCommand(string StoredProcedure)
        {

            if (null == this.DbCommand)
            {
                SetDbCommand();
            }

            this.DbCommand.Command.CommandText = StoredProcedure;

            return this.DbCommand;
        }

        // Properties
        public DataBaseCommand DbCommand
        {
            get
            {
                return this._dbComand;
            }
            set
            {
                this._dbComand = value;
            }
        }


        private void SetDbCommand()
        {
            if (true == string.IsNullOrEmpty(DataBaseFactory.ConnectString))
            {
                NameValueCollection appSettings = ConfigurationManager.AppSettings;

                //DataBaseFactory.ConnectString = appSettings[string.Format("{0}_SYSTEMConnectString", SystemInfo.ExcMode.ToString())].ToString();

                //TEST PC
                //DataBaseFactory.ConnectString = appSettings[string.Format("{0}_SYSTEMConnectString", SystemInfo.ExcMode.ToString())].ToString() + " DATABASE=ElectricityDiary; USER ID = sa; PASSWORD = hgfa7180";

                //Local
                DataBaseFactory.ConnectString = appSettings[string.Format("{0}_SYSTEMConnectString", SystemInfo.ExcMode.ToString())].ToString() + " DATABASE=ElectricityDiary; USER ID = sa; PASSWORD = hgfa7180";

                //YKCJ Server PC
                //DataBaseFactory.ConnectString = appSettings[string.Format("{0}_SYSTEMConnectString", SystemInfo.ExcMode.ToString())].ToString() + " DATABASE=ElectricityDiary; USER Id = sa; PASSWORD = dms001!";
            }

            this._dbComand = DataBaseFactory.GetDataCommand();
        }
    }
}
