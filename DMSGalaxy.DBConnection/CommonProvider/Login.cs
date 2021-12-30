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


namespace DMSGalaxy.DBConnection.CommonProvider
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

        public DataTable GetUserInfoById(string strID)
        {
            command = base.GetCommand("dbo.Select_ID");
            command.SetSqlParamInit();
            command.SetSqlParam("@strID", SqlDbType.NVarChar, strID);

            return command.ExecuteDataSet().Tables[0];
        }

        public int SetUserSave(string _ID, string _PW, int _Level, string _Name, string _Gender, string _EpNumber, string _JobPosition, string _Phone, string _Email
                                                            , string _WG, string _IP, string _Birthday, XmlDocument _MCLine , byte[] _Images)
        {
            command = base.GetCommand("dbo.Common_UserSave");

            command.SetSqlParamInit();

            command.SetSqlParam("@ID", SqlDbType.NVarChar, _ID);
            command.SetSqlParam("@PW", SqlDbType.NVarChar, _PW);
            command.SetSqlParam("@LEVEL", SqlDbType.Int, _Level);
            command.SetSqlParam("@Name", SqlDbType.NVarChar, _Name);
            command.SetSqlParam("@Gender", SqlDbType.NChar, _Gender);
            command.SetSqlParam("@EpNumber", SqlDbType.NChar, _EpNumber);
            command.SetSqlParam("@JobPosition", SqlDbType.NChar, _JobPosition);
            command.SetSqlParam("@Phone", SqlDbType.NChar, _Phone);
            command.SetSqlParam("@Email", SqlDbType.NChar, _Email);
            command.SetSqlParam("@WG", SqlDbType.NVarChar, _WG);
            command.SetSqlParam("@IP", SqlDbType.NChar, _IP);
            command.SetSqlParam("@Birthday", SqlDbType.NChar, _Birthday);
            command.SetSqlParam("@MCLine", SqlDbType.Xml, _MCLine.InnerXml);
            command.SetSqlParam("@Image", SqlDbType.Image, _Images);
            command.SetSqlParamOutPut("@ErrorOpt", SqlDbType.Int);

            command.ExecuteNonQuery();

            return (int)command.Command.Parameters["@ErrorOpt"].Value;
        }

        public void UserStatusReset(string _ID, bool _Reset)
        {
            command = base.GetCommand("dbo.Update_UserContactStatus");

            command.SetSqlParamInit();

            command.SetSqlParam("@ID", SqlDbType.NVarChar, _ID);
            command.SetSqlParam("@Status", SqlDbType.Bit, _Reset);

            command.ExecuteNonQuery();
        }

    }
}
