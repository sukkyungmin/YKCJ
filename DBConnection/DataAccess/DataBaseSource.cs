using System.Data.SqlClient;

namespace DBConnection.DataAccess
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
