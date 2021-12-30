using System;
using System.Data;
using DBConnection.Base;
using DBConnection.DataAccess;

namespace DBConnection.F_07
{
    public class F_07_01_Provider : DataProviderBase
    {
        private DataBaseCommand command = null;

        public DataSet GetComboboxList()
        {
            command = base.GetCommand("dbo.F07_Select_ComboboxList");
            command.SetSqlParamInit();

            return command.ExecuteDataSet();
        }

        public DataTable GetComboboxPart2List(int Part1)
        {
            command = base.GetCommand("dbo.F07_Select_ComboboxPart2List");
            command.SetSqlParamInit();
            command.SetSqlParam("@Part1", SqlDbType.Int, Part1);

            return command.ExecuteDataSet().Tables[0];
        }

        public int SetPartListSave(int Selectitempart ,int Part1idx, string itemname)
        {
            command = base.GetCommand("dbo.F07_Insert_PartList");
            command.SetSqlParamInit();
            command.SetSqlParam("@SelectItemPart", SqlDbType.Int, Selectitempart);
            command.SetSqlParam("@Part1idx", SqlDbType.Int, Part1idx);
            command.SetSqlParam("@ItemName", SqlDbType.NVarChar, itemname);
            command.SetSqlParamOutPut("@ErrorOpt", SqlDbType.Int);

            command.ExecuteNonQuery();

            return (int)command.Command.Parameters["@ErrorOpt"].Value;
        }

        public int SetPartListUpdate(int Modifypart, string Modifyidx, string itemname)
        {
            command = base.GetCommand("dbo.F07_Update_PartList");
            command.SetSqlParamInit();
            command.SetSqlParam("@ModifyPart", SqlDbType.Int, Modifypart);
            command.SetSqlParam("@ModifyIdx", SqlDbType.VarChar, Modifyidx);
            command.SetSqlParam("@ItemName", SqlDbType.NVarChar, itemname);
            command.SetSqlParamOutPut("@ErrorOpt", SqlDbType.Int);

            command.ExecuteNonQuery();

            return (int)command.Command.Parameters["@ErrorOpt"].Value;
        }

        public int SetPartListDelete(int Deleteitempart, int Deleteidx)
        {
            command = base.GetCommand("dbo.F07_Delete_PartList");
            command.SetSqlParamInit();
            command.SetSqlParam("@DeleteItemPart", SqlDbType.Int, Deleteitempart);
            command.SetSqlParam("@DeleteIdx", SqlDbType.Int, Deleteidx);
            command.SetSqlParamOutPut("@ErrorOpt", SqlDbType.Int);

            command.ExecuteNonQuery();

            return (int)command.Command.Parameters["@ErrorOpt"].Value;
        }
    }
}
