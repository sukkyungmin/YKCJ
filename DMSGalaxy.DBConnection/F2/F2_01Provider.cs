using System.Data;

using DMSGalaxy.DBConnection.Base;
using DMSGalaxy.DBConnection.DataAccess;

namespace DMSGalaxy.DBConnection.F2
{
    public class F2_01Provider : DataProviderBase
    {
        DataBaseCommand command = null;

        public DataTable GetF1_01Product(string sDate, string eDate, string WhereCode, string MCNum, int Rowspage, int Pagenumber)
        {

            command = base.GetCommand("dbo.Select_F2_01_Production");

            command.SetSqlParamInit();
            command.SetSqlParam("@sDate", SqlDbType.NVarChar, sDate);
            command.SetSqlParam("@eDate", SqlDbType.NVarChar, eDate);
            command.SetSqlParam("@whereCode", SqlDbType.Char, WhereCode);
            command.SetSqlParam("@MCNum", SqlDbType.VarChar, MCNum);
            command.SetSqlParam("@RowsPerPage", SqlDbType.Int, Rowspage);
            command.SetSqlParam("@PageNumber", SqlDbType.Int, Pagenumber);

            return command.ExecuteDataSet().Tables[0];
        }

    }
}
