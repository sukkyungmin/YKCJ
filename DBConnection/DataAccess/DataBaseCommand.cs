using System;
using System.Data;
using System.Data.SqlClient;

namespace DBConnection.DataAccess
{
    public class DataBaseCommand
    {
        // Fields
        private SqlCommand _sqlCommand = new SqlCommand();
        private SqlParameterCollection _sqlParamCollect = null;

        //Methods
        public DataSet ExecuteDataSet()
        {
            DataBaseSource source = null;
            try
            {
                source = new DataBaseSource(DataBaseFactory.ConnectString);
                source.Connection.Open();
                this._sqlCommand.Connection = source.Connection;
                using (SqlDataAdapter adapter = new SqlDataAdapter(this._sqlCommand))
                {
                    DataSet dataSet = new DataSet();
                    try
                    {
                        adapter.Fill(dataSet);
                        source.Connection.Close();
                    }
                    catch (Exception exception)
                    {
                        source.Connection.Close();
                        throw exception;
                    }
                    return dataSet;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != source)
                {
                    source.Connection.Close();
                }
            }
        }

        public int ExecuteNonQuery()
        {
            int num = 0;
            DataBaseSource source = new DataBaseSource(DataBaseFactory.ConnectString);
            SqlTransaction transaction = null;
            if (source.Connection.State != ConnectionState.Open)
            {
                source.Connection.Open();
            }
            if (this._sqlCommand.Transaction == null)
            {
                transaction = source.Connection.BeginTransaction();
            }
            this._sqlCommand.Connection = source.Connection;
            this._sqlCommand.Transaction = transaction;
            try
            {
                num = this._sqlCommand.ExecuteNonQuery();
                transaction.Commit();
                source.Connection.Close();
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw exception;
            }
            finally
            {
                //transaction = null;  // 문제 있을시 이걸로 교체
                source.Connection.Close();
                transaction.Dispose();
                source = null;
            }
            return num;
        }

        public int ExecuteNonQuery(SqlConnection dbcon, SqlTransaction trans)
        {
            this._sqlCommand.Connection = dbcon;
            this._sqlCommand.Transaction = trans;
            return this._sqlCommand.ExecuteNonQuery();
        }

        public DataBaseSource GetSqlConnection(string connectstring)
        {
            return new DataBaseSource(DataBaseFactory.ConnectString);
        }

        public object GetSqlParamOutPut(string ParameterName)
        {
            return this._sqlCommand.Parameters[ParameterName].Value;
        }

        public void SetSqlParam(string ParameterName, SqlDbType sqldbtype, object sqlvalue)
        {
            SqlParameter parameter = new SqlParameter(ParameterName, sqldbtype) { SqlValue = sqlvalue };
            this._sqlCommand.Parameters.Add(parameter);
        }

        public void SetSqlParamInit()
        {
            this._sqlCommand.Parameters.Clear();
        }

        public void SetSqlParamOutPut(string ParameterName, SqlDbType sqldbtype)
        {
            SqlParameter parameter = new SqlParameter(ParameterName, sqldbtype) { Size = 0xfa0, Direction = ParameterDirection.Output };
            this._sqlCommand.Parameters.Add(parameter);
        }

        // Properties
        public SqlCommand Command
        {
            get
            {
                return this._sqlCommand;
            }
        }

        public SqlParameterCollection Parameter
        {
            get
            {
                return this._sqlParamCollect;
            }
        }
    }
}
