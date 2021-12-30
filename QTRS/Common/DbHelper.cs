using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QTRS
{
    /**
    * MSSQL DB Helper by Jungil Lim
    * Version : 1.1.0
    */
    class DbHelper
    {
        // 시연 테스트
        static public string _ip = "192.168.10.58";
        static public string _id = "sa";
        static public string _pw = "7180";

        //static public string _connectionString = "Server=127.0.0.1;Database=QTRS;Uid=sa;Pwd=skekdns7;";

        // 충주 서버
        //static public string _connectionString = "Server=192.168.10.120;Database=QTRS;Uid=sa;Pwd=ykcjqtrs;";
        static public string _connectionString = "Server=(local);Database=QTRS;Uid=sa;Pwd=hgfa7180;";
        //static public string _connectionString = "Server=121.137.95.29;Database=QTRS;Uid=sa;Pwd=hgfa7180;";


        static public void SetConnectionString(string dbPassword)
        {
            //_connectionString = string.Format("Server={0};Database=RealTimeRMT;Uid={1};Pwd={2};", _ip, _id, _pw);
            //_connectionString = string.Format("Server=127.0.0.1;Database=QTRS;Uid=sa;Pwd={0}", dbPassword);  // 테스트 서버 
            //_connectionString = string.Format("Server=Kotjmfgapp3.mosaic.sys;Database=RealTimeRMT;Uid=KOTJRMTADM;Pwd={0}", dbPassword);  // 유한킴벌리 실서버
        }



        static public long ExecuteNonQuery(string query)
        {
            long retVal = -1;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    retVal = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.Write(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.Write(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
            return retVal;
        }

        static public long ExecuteNonQueryWithTransaction(string[] queries)
        {
            long retVal = -1;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command  = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction
                transaction = connection.BeginTransaction();

                // Must assign both transaction object and connection to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    for (int i = 0; i < queries.Length; i++)
                    {
                        command.CommandText = queries[i];
                        retVal = command.ExecuteNonQuery();
                        if (retVal == -1) // 어떤 에러든 발생되면 이 절까지 오지 않고 catch 로 빠질 것 같다. 
                            throw new Exception("Occurs an error");
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        retVal = -1;
                        transaction.Rollback();
                    }
                    catch (SqlException sqlEx)
                    {
                        if (transaction.Connection != null)
                        {
                            Console.WriteLine("An exception of type " + sqlEx.GetType() +
                            " was encountered while attempting to roll back the transaction.");
                        }
                    }

                    Console.WriteLine("An exception of type " + ex.GetType() +
                    " was encountered while inserting the data.");
                    Console.WriteLine("Neither record was written to database.");
                }
                finally
                {
                    connection.Close();
                }
            }
            return retVal;
        }

        static public long ExecuteScalar(string query)
        {
            long retVal = -1;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    retVal = (long)command.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    Console.Write(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.Write(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
            return retVal;
        }

        static public int ExecuteNonQueryWithFileData(string query, SqlParameter fileParam)
        {
            int retVal = -1;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add(fileParam);
                    retVal = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.Write(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.Write(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
            return retVal;
        }

        static public DataSet SelectQuery(string query)
        {

            DataSet dataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    SqlDataAdapter adpt = new SqlDataAdapter(query, connection);
                    //adpt.SelectCommand.CommandTimeout = 120;
                    adpt.Fill(dataSet, "List");
                }
                catch (SqlException ex)
                {
                    Console.Write(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.Write(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return dataSet;
        }

     

        static public string GetValue(string query, string field, string defaultValue)
        {
            string retVal = "";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet != null)
            {
                DataTableCollection collection = dataSet.Tables;
                if (collection.Count > 0)
                {
                    DataTable table = collection[0];
                    if (table.Rows.Count > 0)
                    {
                        DataRow dataRow = table.Rows[0];
                        retVal = (System.DBNull.Value == dataRow[field]) ? defaultValue : dataRow[field].ToString();
                    }
                    else
                    {
                        retVal = defaultValue;
                    }

                }
            }

            return retVal;
        }

        static public long GetLongValue(string query, string field, long defaultValue)
        {
            long retVal = -1;
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet != null)
            {
                DataTableCollection collection = dataSet.Tables;
                if (collection.Count > 0)
                {
                    DataTable table = collection[0];
                    if (table.Rows.Count > 0)
                    {
                        DataRow dataRow = table.Rows[0];
                        retVal = (System.DBNull.Value == dataRow[field]) ? defaultValue : long.Parse(dataRow[field].ToString());
                    }
                    else
                    {
                        retVal = defaultValue;
                    }

                }
            }

            return retVal;
        }
    }
}
