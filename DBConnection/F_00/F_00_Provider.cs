using System;
using System.Data;
using DBConnection.Base;
using DBConnection.DataAccess;

namespace DBConnection.F_00
{
    public class F_00_Provider : DataProviderBase
    {

        private DataBaseCommand command = null;
        public DataSet GetMainHomeData(string Sdate, string Edate)
        {
            command = base.GetCommand("dbo.F00_Select_MainHomeMonthlyData");

            command.SetSqlParamInit();

            command.SetSqlParam("@Sdate", SqlDbType.NVarChar, Sdate);
            command.SetSqlParam("@Edate", SqlDbType.NVarChar, Edate);

            return command.ExecuteDataSet();
        }
    }
}
