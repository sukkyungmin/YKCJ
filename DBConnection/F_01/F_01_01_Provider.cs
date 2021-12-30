using System;
using System.Data;
using DBConnection.Base;
using DBConnection.DataAccess;


namespace DBConnection.F_01
{
    public class F_01_01_Provider : DataProviderBase
    {
        private DataBaseCommand command = null;

        public DataSet GetComboboxList()
        {
            command = base.GetCommand("dbo.F01_Select_ComboboxList");
            command.SetSqlParamInit();

            return command.ExecuteDataSet();
        }

        public DataTable GetComboboxPart2List(string Part1)
        {
            command = base.GetCommand("dbo.F01_Select_ComboboxPart2List");
            command.SetSqlParamInit();
            command.SetSqlParam("@Part1", SqlDbType.NVarChar, Part1);

            return command.ExecuteDataSet().Tables[0];
        }

        public DataSet GetWorkListIdx(string Searchdate, bool Search)
        {
            command = base.GetCommand("dbo.F01_Select_WorkIdxList");
            command.SetSqlParamInit();
            command.SetSqlParam("@Searchdate", SqlDbType.NVarChar, Searchdate);
            command.SetSqlParam("@SearchType", SqlDbType.Bit, Search);

            return command.ExecuteDataSet();
        }

        public DataTable GetWorkListReport(string Searchdate, bool Search)
        {
            command = base.GetCommand("dbo.F01_Select_WorkListReportExport");
            command.SetSqlParamInit();
            command.SetSqlParam("@Searchdate", SqlDbType.NVarChar, Searchdate);
            command.SetSqlParam("@SearchType", SqlDbType.Bit, Search);

            return command.ExecuteDataSet().Tables[0];
        }

        public DataTable GetWorkList(int Idx)
        {
            command = base.GetCommand("dbo.F01_Select_WorkListDetail");
            command.SetSqlParamInit();
            command.SetSqlParam("@WorkIdx", SqlDbType.Int, Idx);

            return command.ExecuteDataSet().Tables[0];
        }

        public int SetWorkList(bool Modifymode, int Workidx, string Name, string Title, string Shift, string Machine, string Part1, string Part2, string Class
                       , DateTime Eventdatetime, int Checktime, int Delaytime, string Detail, string DetailRtf, string DetailHtml, string SaveFileForm,
                        string SaveFileName, string SaveFileDirctory, string SaveFileSize)
        {
            command = base.GetCommand("dbo.F01_Insert_Or_Update_WorkList");

            command.SetSqlParamInit();

            command.SetSqlParam("@ModifyMode", SqlDbType.Bit, Modifymode);
            command.SetSqlParam("@WorkIndex", SqlDbType.BigInt, Workidx);
            command.SetSqlParam("@Name", SqlDbType.NVarChar, Name);
            command.SetSqlParam("@Title", SqlDbType.NVarChar, Title);
            command.SetSqlParam("@Shift", SqlDbType.NVarChar, Shift);
            command.SetSqlParam("@Machine", SqlDbType.NVarChar, Machine);
            command.SetSqlParam("@Part1", SqlDbType.NVarChar, Part1);
            command.SetSqlParam("@Part2", SqlDbType.NVarChar, Part2);
            command.SetSqlParam("@Class", SqlDbType.NVarChar, Class);
            command.SetSqlParam("@Eventdatetime", SqlDbType.SmallDateTime, Eventdatetime);
            command.SetSqlParam("@Checktime", SqlDbType.Int, Checktime);
            command.SetSqlParam("@Delaytime", SqlDbType.Int, Delaytime);
            command.SetSqlParam("@Detail", SqlDbType.NVarChar, Detail);
            command.SetSqlParam("@DetailRtf", SqlDbType.NVarChar, DetailRtf);
            command.SetSqlParam("@DetailHtml", SqlDbType.NVarChar, DetailHtml);
            command.SetSqlParam("@SaveFileForm", SqlDbType.NChar, SaveFileForm);
            command.SetSqlParam("@SaveFileName", SqlDbType.NVarChar, SaveFileName);
            command.SetSqlParam("@SaveFileDirctory", SqlDbType.NVarChar, SaveFileDirctory);
            command.SetSqlParam("@SaveFileSize", SqlDbType.NChar, SaveFileSize);
            command.SetSqlParamOutPut("@ErrorOpt", SqlDbType.Int);

            command.ExecuteNonQuery();

            return (int)command.Command.Parameters["@ErrorOpt"].Value;
        }

        public int SetWorkListDelete(int WorkIdx)
        {
            command = base.GetCommand("dbo.F01_Delete_WorkList");
            command.SetSqlParamInit();
            command.SetSqlParam("@WorkIdx", SqlDbType.BigInt, WorkIdx);
            command.SetSqlParamOutPut("@ErrorOpt", SqlDbType.Int);

            command.ExecuteNonQuery();

            return (int)command.Command.Parameters["@ErrorOpt"].Value;
        }

    }
}
