using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xaml;
using System.Xml;

using DMSGalaxy.DBConnection.Base;
using DMSGalaxy.DBConnection.DataAccess;

namespace DMSGalaxy.DBConnection.F1
{
    public class F1_01Provider : DataProviderBase
    {
        DataBaseCommand command = null;

        public DataTable GetMCStatus(string MCType)
        {
            command = base.GetCommand("dbo.Select_MCStatus");
            command.SetSqlParamInit();
            command.SetSqlParam("@Type", SqlDbType.NVarChar, MCType);

            return command.ExecuteDataSet().Tables[0];
        }

    }
}
