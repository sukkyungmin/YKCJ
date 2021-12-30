using System;
using System.Data;
using DBConnection.Base;
using DBConnection.DataAccess;


namespace DBConnection.F_02
{
    public class F_02_01_Provider : DataProviderBase
    {
        private DataBaseCommand command = null;

        public DataSet GetComboboxList()
        {
            command = base.GetCommand("dbo.F02_Select_ComboboxList");
            command.SetSqlParamInit();

            return command.ExecuteDataSet();
        }

        public DataTable GetComboboxPart2List(string Part1)
        {
            command = base.GetCommand("dbo.F02_Select_ComboboxPart2List");
            command.SetSqlParamInit();
            command.SetSqlParam("@Part1", SqlDbType.NVarChar, Part1);

            return command.ExecuteDataSet().Tables[0];
        }

        public DataTable GetWorkList(string Sdate, string Edate, string Text, string Shift, string Machine, string Class, string Part1, string Part2, int Pagenumber)
        {
            command = base.GetCommand("dbo.F02_Select_WorkList");
            command.SetSqlParamInit();
            command.SetSqlParam("@Sdate", SqlDbType.NVarChar, Sdate);
            command.SetSqlParam("@Edate", SqlDbType.NVarChar, Edate);
            command.SetSqlParam("@Text", SqlDbType.NVarChar, Text);
            command.SetSqlParam("@Shift", SqlDbType.NVarChar, Shift);
            command.SetSqlParam("@Machine", SqlDbType.NVarChar, Machine);
            command.SetSqlParam("@Class", SqlDbType.NVarChar, Class);
            command.SetSqlParam("@Part1", SqlDbType.NVarChar, Part1);
            command.SetSqlParam("@Part2", SqlDbType.NVarChar, Part2);
            command.SetSqlParam("@PageNo", SqlDbType.Int, Pagenumber);

            return command.ExecuteDataSet().Tables[0];
        }

        public DataTable GetWorkIdxList(string Sdate, string Edate, string Text, string Shift, string Machine, string Class, string Part1, string Part2)
        {
            command = base.GetCommand("dbo.F02_Select_WorkIdxList");
            command.SetSqlParamInit();
            command.SetSqlParam("@Sdate", SqlDbType.NVarChar, Sdate);
            command.SetSqlParam("@Edate", SqlDbType.NVarChar, Edate);
            command.SetSqlParam("@Text", SqlDbType.NVarChar, Text);
            command.SetSqlParam("@Shift", SqlDbType.NVarChar, Shift);
            command.SetSqlParam("@Machine", SqlDbType.NVarChar, Machine);
            command.SetSqlParam("@Class", SqlDbType.NVarChar, Class);
            command.SetSqlParam("@Part1", SqlDbType.NVarChar, Part1);
            command.SetSqlParam("@Part2", SqlDbType.NVarChar, Part2);

            return command.ExecuteDataSet().Tables[0];
        }

        public DataTable GetAllWorkList(string Sdate, string Edate, string Text, string Shift, string Machine, string Class, string Part1, string Part2)
        {
            command = base.GetCommand("dbo.F02_Select_AllWorkList");
            command.SetSqlParamInit();
            command.SetSqlParam("@Sdate", SqlDbType.NVarChar, Sdate);
            command.SetSqlParam("@Edate", SqlDbType.NVarChar, Edate);
            command.SetSqlParam("@Text", SqlDbType.NVarChar, Text);
            command.SetSqlParam("@Shift", SqlDbType.NVarChar, Shift);
            command.SetSqlParam("@Machine", SqlDbType.NVarChar, Machine);
            command.SetSqlParam("@Class", SqlDbType.NVarChar, Class);
            command.SetSqlParam("@Part1", SqlDbType.NVarChar, Part1);
            command.SetSqlParam("@Part2", SqlDbType.NVarChar, Part2);

            return command.ExecuteDataSet().Tables[0];
        }

        public int GetWorkListRows(string Sdate, string Edate, string Text, string Shift, string Machine, string Class, string Part1, string Part2)
        {
            command = base.GetCommand("dbo.F02_Select_WorkListRows");
            command.SetSqlParamInit();
            command.SetSqlParam("@Sdate", SqlDbType.NVarChar, Sdate);
            command.SetSqlParam("@Edate", SqlDbType.NVarChar, Edate);
            command.SetSqlParam("@Text", SqlDbType.NVarChar, Text);
            command.SetSqlParam("@Shift", SqlDbType.NVarChar, Shift);
            command.SetSqlParam("@Machine", SqlDbType.NVarChar, Machine);
            command.SetSqlParam("@Class", SqlDbType.NVarChar, Class);
            command.SetSqlParam("@Part1", SqlDbType.NVarChar, Part1);
            command.SetSqlParam("@Part2", SqlDbType.NVarChar, Part2);

            return (command.ExecuteDataSet().Tables[0] is null) ? 0 : (int)command.ExecuteDataSet().Tables[0].Rows[0]["Rows"];
        }

        public DataTable GetWorkListModify(int workidx)
        {
            command = base.GetCommand("dbo.F02_Select_WorkListDetail");
            command.SetSqlParamInit();
            command.SetSqlParam("@WorkIdx", SqlDbType.BigInt, workidx);

            return command.ExecuteDataSet().Tables[0];
        }

        public int SetWorkList(bool Modifymode,int Workidx,string Name, string Title,string Shift ,string Machine, string Part1, string Part2, string Class
                               ,DateTime Eventdatetime, int Checktime, int Delaytime, string Detail, string DetailRtf, string DetailHtml, string SaveFileForm,
                                string SaveFileName, string SaveFileDirctory, string SaveFileSize)
        {
            command = base.GetCommand("dbo.F02_Insert_Or_Update_WorkList");

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
            command = base.GetCommand("dbo.F02_Delete_WorkList");
            command.SetSqlParamInit();
            command.SetSqlParam("@WorkIdx", SqlDbType.BigInt, WorkIdx);
            command.SetSqlParamOutPut("@ErrorOpt", SqlDbType.Int);

            command.ExecuteNonQuery();

            return (int)command.Command.Parameters["@ErrorOpt"].Value;
        }


        // 기존 데이터를 Html 에서 Rtf로 변경 후 업데이트 하기위한 로직 

        public DataTable GetWorkDetailid()
        {
            command = base.GetCommand("dbo.F02_Select_WorkDetailId");
            command.SetSqlParamInit();

            return command.ExecuteDataSet().Tables[0];
        }


        public int SetWorkDetailRtf(int WorkIdx, string WorkRtf)
        {
            command = base.GetCommand("dbo.F02_Update_WorkDetailRtf");
            command.SetSqlParamInit();
            command.SetSqlParam("@WorkIndex", SqlDbType.BigInt, WorkIdx);
            command.SetSqlParam("@DetailRtf", SqlDbType.NVarChar, WorkRtf);
            command.SetSqlParamOutPut("@ErrorOpt", SqlDbType.Int);

            command.ExecuteNonQuery();

            return (int)command.Command.Parameters["@ErrorOpt"].Value;
        }

    }
}
