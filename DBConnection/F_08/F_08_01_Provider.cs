using System.Data;

using DBConnection.Base;
using DBConnection.DataAccess;

namespace DBConnection.F_08
{
    public class F_08_01_Provider : DataProviderBase
    {
        private DataBaseCommand command = null;

        public int SetUserSave(string ID, string PW, string Name, string JobPosition, byte[] Images, string Menuitem)
        {
            command = base.GetCommand("dbo.F08_Insert_UserSave");

            command.SetSqlParamInit();

            command.SetSqlParam("@ID", SqlDbType.NVarChar, ID);
            command.SetSqlParam("@PW", SqlDbType.NVarChar, PW);
            command.SetSqlParam("@Name", SqlDbType.NVarChar, Name);
            command.SetSqlParam("@JobPosition", SqlDbType.NVarChar, JobPosition);
            command.SetSqlParam("@Image", SqlDbType.Image, Images);
            command.SetSqlParam("@MenuItem", SqlDbType.Char, Menuitem);
            command.SetSqlParamOutPut("@ErrorOpt", SqlDbType.Int);

            command.ExecuteNonQuery();

            return (int)command.Command.Parameters["@ErrorOpt"].Value;
        }

        public int SetUserUpdate(int Idx, string PW, string Name, string JobPosition, byte[] Images, string Menuitem)
        {
            command = base.GetCommand("dbo.F08_Update_User");

            command.SetSqlParamInit();

            command.SetSqlParam("@Idx", SqlDbType.Int, Idx);
            command.SetSqlParam("@PW", SqlDbType.NVarChar, PW);
            command.SetSqlParam("@Name", SqlDbType.NVarChar, Name);
            command.SetSqlParam("@JobPosition", SqlDbType.NVarChar, JobPosition);
            command.SetSqlParam("@Image", SqlDbType.Image, Images);
            command.SetSqlParam("@MenuItem", SqlDbType.Char, Menuitem);
            command.SetSqlParamOutPut("@ErrorOpt", SqlDbType.Int);

            command.ExecuteNonQuery();

            return (int)command.Command.Parameters["@ErrorOpt"].Value;
        }

        public int SetUserDelete (int Idx)
        {
            command = base.GetCommand("dbo.F08_Delete_User");

            command.SetSqlParamInit();

            command.SetSqlParam("@Idx", SqlDbType.Int, Idx);
            command.SetSqlParamOutPut("@ErrorOpt", SqlDbType.Int);

            command.ExecuteNonQuery();

            return (int)command.Command.Parameters["@ErrorOpt"].Value;
        }

        public DataTable GetUserList()
        {
            command = base.GetCommand("dbo.F08_Select_UserList");
            command.SetSqlParamInit();

            return command.ExecuteDataSet().Tables[0];
        }

        public DataSet GetUser(int Idx)
        {
            command = base.GetCommand("dbo.F08_Select_User");
            command.SetSqlParamInit();
            command.SetSqlParam("@Idx", SqlDbType.Int, Idx);

            return command.ExecuteDataSet();
        }
    }
}
