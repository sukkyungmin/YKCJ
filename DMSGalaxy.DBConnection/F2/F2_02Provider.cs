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

namespace DMSGalaxy.DBConnection.F2
{
    public class F2_02Provider : DataProviderBase
    {
        DataBaseCommand command = null;

        public DataTable GetF2_02Grid(string Type , string sDate, string eDate, string WhereMode ,string WhereCode,string MCNum ,string TopCount)
        {
            switch(Type)
            {
                case "ProductionDate":
                    command = base.GetCommand("dbo.Select_F2_02_PProduction");
                    break;
                case "ProductionShift":
                    command = base.GetCommand("dbo.Select_F2_02_PProductionShift");
                    break;
                case "ChangeOver":
                    command = base.GetCommand("dbo.Select_F2_02_PChangeover");
                    break;
                case "WasteDate":
                    command = base.GetCommand("dbo.Select_F2_02_WWaste");
                    break;
                case "WasteGroup":
                    command = base.GetCommand("dbo.Select_F2_02_WGroup");
                    break;
                case "WasteCode":
                    command = base.GetCommand("dbo.Select_F2_02_WCode");
                    break;
                case "DelayDate":
                    command = base.GetCommand("dbo.Select_F2_02_DDelay");
                    break;
                case "DelayGroup":
                    command = base.GetCommand("dbo.Select_F2_02_DGroup");
                    break;
                case "DelayCode":
                    command = base.GetCommand("dbo.Select_F2_02_DCode");
                    break;
            }

            command.SetSqlParamInit();
            command.SetSqlParam("@sDate", SqlDbType.NVarChar, sDate);
            command.SetSqlParam("@eDate", SqlDbType.NVarChar, eDate);
            command.SetSqlParam("@WhereMode", SqlDbType.NVarChar, WhereMode);
            command.SetSqlParam("@whereCode", SqlDbType.Char, WhereCode);
            command.SetSqlParam("@MCNum", SqlDbType.VarChar, MCNum);
            command.SetSqlParam("@TOPCount", SqlDbType.Char, TopCount);

            return command.ExecuteDataSet().Tables[0];
        }

        public DataTable GetF2_02ProductList(string MCNum)
        {
            command = base.GetCommand("dbo.Select_F2_02_ProductList");

            command.SetSqlParamInit();
            command.SetSqlParam("@MCNum", SqlDbType.Char, MCNum);

            return command.ExecuteDataSet().Tables[0];
        }
    }
}
