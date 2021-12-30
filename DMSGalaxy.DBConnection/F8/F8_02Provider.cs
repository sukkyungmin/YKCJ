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

namespace DMSGalaxy.DBConnection.F8
{
    public class F8_02Provider : DataProviderBase
    {

        DataBaseCommand command = null;

        public DataTable GetUserList()
        {
            command = base.GetCommand("dbo.Select_AllUser");

            return command.ExecuteDataSet().Tables[0];
        }

        public int SetUserUpdateDelete(bool DBUpdateSelect,string _ID, string _PW, int _Level, string _Name, string _Gender, string _EpNumber, string _JobPosition, string _Phone, string _Email
                                                    , string _WG, string _IP, string _Birthday, XmlDocument _MCLine, byte[] _Images)
        {
            command = base.GetCommand("dbo.UpdateORDelete_User");

            command.SetSqlParamInit();

            command.SetSqlParam("@Select", SqlDbType.Bit, DBUpdateSelect);
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

    }
}
