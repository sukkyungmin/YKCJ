using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DMSGalaxy.DBConnection.DataAccess
{
     public class DataBaseSource
    {
        // Fields
        private SqlConnection _connection = null;

        // Methods
        public DataBaseSource(string ConnectString)
        {
            this._connection = new SqlConnection(ConnectString);
        }

        // Properties
        public SqlConnection Connection
        {
            get
            {
                return this._connection;
            }
        }
    }
}
