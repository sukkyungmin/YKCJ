using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;

using DBConnection.Base;
using DBConnection.DataAccess;


namespace DBConnection.CommonProvider
{
    public class Login : DataProviderBase
    {
        DataBaseCommand command = null;

        //public DataSet CM_USER_SELECTS(string strID, string strPW)
        //{
        //    command = base.GetCommand("dbo.CM_USER_SELECTS");
        //    command.SetSqlParamInit();
        //    command.SetSqlParam("@strID", SqlDbType.VarChar, strID);
        //    command.SetSqlParam("@strPW", SqlDbType.VarChar, strPW);

        //    return command.ExecuteDataSet().Tables[0];
        //}

        public DataSet GetUserInfoById(string strID)
        {
            command = base.GetCommand("dbo.Select_ID");
            command.SetSqlParamInit();
            command.SetSqlParam("@strID", SqlDbType.NVarChar, strID);

            return command.ExecuteDataSet();
        }

        //public int SetUserSave(string _ID, string _PW, string _Name, string _JobPosition, byte[] _Images, string Menuitem)
        //{
        //    command = base.GetCommand("dbo.F08_Insert_UserSave");

        //    command.SetSqlParamInit();

        //    command.SetSqlParam("@ID", SqlDbType.NVarChar, _ID);
        //    command.SetSqlParam("@PW", SqlDbType.NVarChar, _PW);
        //    command.SetSqlParam("@Name", SqlDbType.NVarChar, _Name);
        //    command.SetSqlParam("@JobPosition", SqlDbType.NVarChar, _JobPosition);
        //    command.SetSqlParam("@Image", SqlDbType.Image, _Images);
        //    command.SetSqlParam("@MenuItem", SqlDbType.Char, Menuitem);
        //    command.SetSqlParamOutPut("@ErrorOpt", SqlDbType.Int);

        //    command.ExecuteNonQuery();

        //    return (int)command.Command.Parameters["@ErrorOpt"].Value;
        //}

    }
}
