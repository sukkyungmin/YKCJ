using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace DMSGalaxy.DBConnection.DataAccess
{
    public class DataBaseFactory
    {
        // Fields
        private static string _connectstring = string.Empty;
        private static DataBaseCommand _databasecommand = null;

        // Methods
        static DataBaseFactory()
        {
            LoadCache();
        }

        public static DataBaseCommand GetDataCommand()
        {
            _databasecommand = new DataBaseCommand();
            _databasecommand.Command.CommandTimeout = 600;
            _databasecommand.Command.CommandType = CommandType.StoredProcedure;
            return _databasecommand;
        }

        private static void LoadCache()
        {
            try
            {

                GetDataCommand();
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Error loading cache from config.", exception);
            }
        }

        // Properties
        public static string ConnectString
        {
            get
            {
                return _connectstring;
            }
            set
            {
                _connectstring = value;
            }
        }
    }
}
