using System;
using System.Collections;
using System.Data;

namespace Common.Helper
{
    public class DataSetHelper
    {
        private ArrayList m_FieldInfo;
        private string m_FieldList;
        private ArrayList GroupByFieldInfo;
        private string GroupByFieldList;


        private DataSet _ds;

        //----------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="DataSet"></param>
        public DataSetHelper(ref DataSet DataSet)
        {
            this.InnerDataSet = DataSet;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DataSetHelper()
        {
            this.InnerDataSet = null;
        }

        #endregion
        //----------------------------------------------------------------------

        /// <summary>
        /// Property to get the Inner DataSet
        /// </summary>
        public DataSet InnerDataSet
        {
            get { return _ds; }
            set { _ds = value; }
        }

        //----------------------------------------------------------------------
        #region Private Methods

        /// <summary>
        /// This code parses FieldList into FieldInfo objects  and then 
        /// adds them to the m_FieldInfo private member
        /// 
        /// FieldList systax:  [relationname.]fieldname[ alias], ...
        /// </summary>
        /// <param name="FieldList"></param>
        /// <param name="AllowRelation"></param>
        /// <remarks>See MSDN Article:
        /// http://support.microsoft.com/default.aspx?scid=kb;en-us;326080
        /// </remarks>
        private void ParseFieldList(string FieldList, bool AllowRelation)
        {

            if (m_FieldList == FieldList)
                return;

            m_FieldInfo = new System.Collections.ArrayList();
            m_FieldList = FieldList;
            FieldInfo Field; string[] FieldParts; string[] Fields = FieldList.Split(',');

            int i;
            for (i = 0; i <= Fields.Length - 1; i++)
            {
                Field = new FieldInfo();
                //parse FieldAlias
                FieldParts = Fields[i].Trim().Split(' ');
                switch (FieldParts.Length)
                {
                    case 1:
                        //to be set at the end of the loop
                        break;
                    case 2:
                        Field.FieldAlias = FieldParts[1];
                        break;
                    default:
                        throw new Exception("Too many spaces in field definition: '" + Fields[i] + "'.");
                }
                //parse FieldName and RelationName
                FieldParts = FieldParts[0].Split('.');
                switch (FieldParts.Length)
                {
                    case 1:
                        Field.FieldName = FieldParts[0];
                        break;
                    case 2:
                        if (AllowRelation == false)
                            throw new Exception("Relation specifiers not permitted in field list: '" + Fields[i] + "'.");
                        Field.RelationName = FieldParts[0].Trim();
                        Field.FieldName = FieldParts[1].Trim();
                        break;
                    default:
                        throw new Exception("Invalid field definition: " + Fields[i] + "'.");
                }
                if (Field.FieldAlias == null)
                    Field.FieldAlias = Field.FieldName;
                m_FieldInfo.Add(Field);
            }
        }


        /// <summary>
        /// Parses FieldList into FieldInfo objects and adds them to the GroupByFieldInfo private member
        /// 
        /// FieldList syntax: fieldname[ alias]|operatorname(fieldname)[ alias],...
        ///
        /// Supported Operators: count,sum,max,min,first,last
        /// </summary>
        /// <param name="FieldList"></param>
        private void ParseGroupByFieldList(string FieldList)
        {

            if (GroupByFieldList == FieldList) return;
            GroupByFieldInfo = new System.Collections.ArrayList();
            FieldInfo Field; string[] FieldParts; string[] Fields = FieldList.Split(',');
            for (int i = 0; i <= Fields.Length - 1; i++)
            {
                Field = new FieldInfo();
                //Parse FieldAlias
                FieldParts = Fields[i].Trim().Split(' ');
                switch (FieldParts.Length)
                {
                    case 1:
                        //to be set at the end of the loop
                        break;
                    case 2:
                        Field.FieldAlias = FieldParts[1];
                        break;
                    default:
                        throw new ArgumentException("Too many spaces in field definition: '" + Fields[i] + "'.");
                }
                //Parse FieldName and Aggregate
                FieldParts = FieldParts[0].Split('(');
                switch (FieldParts.Length)
                {
                    case 1:
                        Field.FieldName = FieldParts[0];
                        break;
                    case 2:
                        Field.Aggregate = FieldParts[0].Trim().ToLower();    //we're doing a case-sensitive comparison later
                        Field.FieldName = FieldParts[1].Trim(' ', ')');
                        break;
                    default:
                        throw new ArgumentException("Invalid field definition: '" + Fields[i] + "'.");
                }
                if (Field.FieldAlias == null)
                {
                    if (Field.Aggregate == null)
                        Field.FieldAlias = Field.FieldName;
                    else
                        Field.FieldAlias = Field.Aggregate + "of" + Field.FieldName;
                }
                GroupByFieldInfo.Add(Field);
            }
            GroupByFieldList = FieldList;
        }


        /// <summary>
        /// Looks up a FieldInfo record based on FieldName
        /// </summary>
        /// <param name="FieldList"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        private FieldInfo LocateFieldInfoByName(System.Collections.ArrayList FieldList, string Name)
        {

            foreach (FieldInfo Field in FieldList)
            {
                if (Field.FieldName == Name)
                    return Field;
            }
            return null;
        }


        /// <summary>
        ///  Compares two values to see if they are equal. Also compares DBNULL.Value.
        ///  
        ///  Note: If your DataTable contains object fields, you must extend this
        ///  function to handle them in a meaningful way if you intend to group on them.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private bool ColumnEqual(object a, object b)
        {
            if ((a is DBNull) && (b is DBNull))
                return true;    //both are null
            if ((a is DBNull) || (b is DBNull))
                return false;    //only one is null
            return (a == b);    //value type standard comparison
        }

        /// <summary>
        /// Returns MIN of two values - DBNull is less than all others
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private object Min(object a, object b)
        {

            if ((a is DBNull) || (b is DBNull))
                return DBNull.Value;
            if (((IComparable)a).CompareTo(b) == -1)
                return a;
            else
                return b;
        }

        private object Max(object a, object b)
        {
            //Returns Max of two values - DBNull is less than all others
            if (a is DBNull)
                return b;
            if (b is DBNull)
                return a;
            if (((IComparable)a).CompareTo(b) == 1)
                return a;
            else
                return b;
        }

        private object Add(object a, object b)
        {
            //Adds two values - if one is DBNull, then returns the other
            if (a is DBNull)
                return b;
            if (b is DBNull)
                return a;
            return ((decimal)a + (decimal)b);
        }
        #endregion
        //----------------------------------------------------------------------
        #region Public Methods

        /// <summary>
        /// Creates a table based on aggregates of fields of another table
        ///  
        ///  RowFilter affects rows before GroupBy operation. No "Having" support
        ///  though this can be emulated by subsequent filtering of the table that results
        ///  
        ///   FieldList syntax: fieldname[ alias]|aggregatefunction(fieldname)[ alias], ...
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="SourceTable"></param>
        /// <param name="FieldList"></param>
        /// <returns></returns>
        public DataTable CreateGroupByTable(string TableName, DataTable SourceTable, string FieldList)
        {

            if (FieldList == null)
            {
                throw new ArgumentException("You must specify at least one field in the field list.");
                //return CreateTable(TableName, SourceTable);
            }
            else
            {
                DataTable dt = new DataTable(TableName);
                ParseGroupByFieldList(FieldList);
                foreach (FieldInfo Field in GroupByFieldInfo)
                {
                    DataColumn dc = SourceTable.Columns[Field.FieldName];
                    if (Field.Aggregate == null)
                        dt.Columns.Add(Field.FieldAlias, dc.DataType, dc.Expression);
                    else
                        dt.Columns.Add(Field.FieldAlias, dc.DataType);
                }
                if (this.InnerDataSet != null)
                    this.InnerDataSet.Tables.Add(dt);
                return dt;
            }
        }


        /// <summary>
        /// 
        ///  Copies the selected rows and columns from SourceTable and inserts them into DestTable
        ///  FieldList has same format as CreateGroupByTable
        ///  
        /// </summary>
        /// <param name="DestTable"></param>
        /// <param name="SourceTable"></param>
        /// <param name="FieldList"></param>
        /// <param name="RowFilter"></param>
        /// <param name="GroupBy"></param>
        public void InsertGroupByInto(DataTable DestTable,
                                        DataTable SourceTable,
                                        string FieldList,
                                        string RowFilter,
                                        string GroupBy)
        {

            if (FieldList == null)
                throw new ArgumentException("You must specify at least one field in the field list.");

            ParseGroupByFieldList(FieldList);	//parse field list
            ParseFieldList(GroupBy, false);			//parse field names to Group By into an arraylist
            DataRow[] Rows = SourceTable.Select(RowFilter, GroupBy);
            DataRow LastSourceRow = null, DestRow = null; bool SameRow; int RowCount = 0;

            foreach (DataRow SourceRow in Rows)
            {
                SameRow = false;
                if (LastSourceRow != null)
                {
                    SameRow = true;
                    foreach (FieldInfo Field in m_FieldInfo)
                    {
                        if (!ColumnEqual(LastSourceRow[Field.FieldName], SourceRow[Field.FieldName]))
                        {
                            SameRow = false;
                            break;
                        }
                    }
                    if (!SameRow)
                        DestTable.Rows.Add(DestRow);
                }
                if (!SameRow)
                {
                    DestRow = DestTable.NewRow();
                    RowCount = 0;
                }
                RowCount += 1;

                foreach (FieldInfo Field in GroupByFieldInfo)
                {
                    switch (Field.Aggregate)    //this test is case-sensitive
                    {
                        case null:        //implicit last
                        case "":        //implicit last
                        case "last":
                            DestRow[Field.FieldAlias] = SourceRow[Field.FieldName];
                            break;
                        case "first":
                            if (RowCount == 1)
                                DestRow[Field.FieldAlias] = SourceRow[Field.FieldName];
                            break;
                        case "count":
                            DestRow[Field.FieldAlias] = RowCount;
                            break;
                        case "sum":
                            DestRow[Field.FieldAlias] = Add(DestRow[Field.FieldAlias], SourceRow[Field.FieldName]);
                            break;
                        case "max":
                            DestRow[Field.FieldAlias] = Max(DestRow[Field.FieldAlias], SourceRow[Field.FieldName]);
                            break;
                        case "min":
                            if (RowCount == 1)
                                DestRow[Field.FieldAlias] = SourceRow[Field.FieldName];
                            else
                                DestRow[Field.FieldAlias] = Min(DestRow[Field.FieldAlias], SourceRow[Field.FieldName]);
                            break;
                    }
                }
                LastSourceRow = SourceRow;
            }
            if (DestRow != null)
                DestTable.Rows.Add(DestRow);
        }


        /// <summary>
        ///  Selects data from one DataTable to another and performs various 
        ///  aggregate functions along the way. See InsertGroupByInto and 
        ///  ParseGroupByFieldList for supported aggregate functions.
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="SourceTable"></param>
        /// <param name="FieldList"></param>
        /// <param name="RowFilter"></param>
        /// <param name="GroupBy"></param>
        /// <returns></returns>
        public DataTable SelectGroupByInto(string TableName,
                                          DataTable SourceTable,
                                          string FieldList,
                                          string RowFilter, string GroupBy)
        {

            DataTable dt = CreateGroupByTable(TableName, SourceTable, FieldList);
            InsertGroupByInto(dt, SourceTable, FieldList, RowFilter, GroupBy);
            return dt;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="SourceTable"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>

        public DataTable SelectDistinct(string TableName, DataTable SourceTable, string FieldName)
        {
            DataTable dt = new DataTable(TableName);
            dt.Columns.Add(FieldName, SourceTable.Columns[FieldName].DataType);

            object LastValue = null;
            foreach (DataRow dr in SourceTable.Select("", FieldName))
            {
                if (LastValue == null || !(ColumnEqual(LastValue, dr[FieldName])))
                {
                    LastValue = dr[FieldName];
                    dt.Rows.Add(new object[] { LastValue });
                }
            }
            if (this.InnerDataSet != null)
                this.InnerDataSet.Tables.Add(dt);
            return dt;
        }

        /// <summary>
        ///  This code creates a DataTable by using the SourceTable as a template and creates the fields in the
        ///  order that is specified in the FieldList. If the FieldList is blank, the code uses DataTable.Clone().
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="SourceTable"></param>
        /// <param name="FieldList"></param>
        /// <returns></returns>
        public DataTable CreateTable(string TableName, DataTable SourceTable, string FieldList)
        {

            DataTable dt;
            if (FieldList.Trim() == "")
            {
                dt = SourceTable.Clone();
                dt.TableName = TableName;
            }
            else
            {
                dt = new DataTable(TableName);
                ParseFieldList(FieldList, false);
                DataColumn dc;
                foreach (FieldInfo Field in m_FieldInfo)
                {
                    dc = SourceTable.Columns[Field.FieldName];
                    dt.Columns.Add(Field.FieldAlias, dc.DataType);
                }
            }
            if (this.InnerDataSet != null)
                this.InnerDataSet.Tables.Add(dt);
            return dt;
        }

        /// <summary>
        /// This code copies the selected rows and columns from SourceTable and inserts them into DestTable.
        /// </summary>
        /// <param name="DestTable"></param>
        /// <param name="SourceTable"></param>
        /// <param name="FieldList"></param>
        /// <param name="RowFilter"></param>
        /// <param name="Sort"></param>
        public void InsertInto(DataTable DestTable,
                                DataTable SourceTable,
                                string FieldList,
                                string RowFilter,
                                string Sort)
        {

            ParseFieldList(FieldList, false);
            DataRow[] Rows = SourceTable.Select(RowFilter, Sort);
            DataRow DestRow;
            foreach (DataRow SourceRow in Rows)
            {
                DestRow = DestTable.NewRow();
                if (FieldList == "")
                {
                    foreach (DataColumn dc in DestRow.Table.Columns)
                    {
                        if (dc.Expression == "")
                            DestRow[dc] = SourceRow[dc.ColumnName];
                    }
                }
                else
                {
                    foreach (FieldInfo Field in m_FieldInfo)
                    {
                        DestRow[Field.FieldAlias] = SourceRow[Field.FieldName];
                    }
                }
                DestTable.Rows.Add(DestRow);
            }
        }

        /// <summary>
        ///   This code selects values that are sorted and filtered from one DataTable into another.
        ///   The FieldList specifies which fields are to be copied.
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="SourceTable"></param>
        /// <param name="FieldList"></param>
        /// <param name="RowFilter"></param>
        /// <param name="Sort"></param>
        /// <returns></returns>
        public DataTable SelectInto(string TableName, DataTable SourceTable,
            string FieldList, string RowFilter, string Sort)
        {

            DataTable dt = CreateTable(TableName, SourceTable, FieldList);
            InsertInto(dt, SourceTable, FieldList, RowFilter, Sort);
            return dt;
        }


        /// <summary>
        ///  This code creates a DataTable by using the SourceTable as a template and creates the fields in the
        ///  order that is specified in the FieldList. If the FieldList is blank, the code uses DataTable.Clone().
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="FieldList"></param>
        /// <returns></returns>
        public DataTable CreateTable(string TableName, string FieldList)
        {
            DataTable dt = new DataTable(TableName);
            DataColumn dc;
            string[] Fields = FieldList.Split(',');
            string[] FieldsParts;
            string Expression;
            foreach (string Field in Fields)
            {
                FieldsParts = Field.Trim().Split(" ".ToCharArray(), 3); // allow for spaces in the expression
                // add fieldname and datatype			
                if (FieldsParts.Length == 2)
                {
                    dc = dt.Columns.Add(FieldsParts[0].Trim(), Type.GetType("System." + FieldsParts[1].Trim(), true, true));
                    dc.AllowDBNull = true;
                }
                else if (FieldsParts.Length == 3)  // add fieldname, datatype, and expression
                {
                    Expression = FieldsParts[2].Trim();
                    if (Expression.ToUpper() == "REQUIRED")
                    {
                        dc = dt.Columns.Add(FieldsParts[0].Trim(), Type.GetType("System." + FieldsParts[1].Trim(), true, true));
                        dc.AllowDBNull = false;
                    }
                    else
                    {
                        dc = dt.Columns.Add(FieldsParts[0].Trim(), Type.GetType("System." + FieldsParts[1].Trim(), true, true), Expression);
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid field definition: '" + Field + "'.");
                }

                if (FieldsParts[1].Trim() == "Int16" || FieldsParts[1].Trim() == "Int32" || FieldsParts[1].Trim() == "Int64" || FieldsParts[1].Trim() == "Decimal" || FieldsParts[1].Trim() == "Double")
                {
                    dc.DefaultValue = 0;
                }
            }
            if (this.InnerDataSet != null)
            {
                this.InnerDataSet.Tables.Add(dt);
            }
            return dt;
        }

        /// <summary>
        ///  This code creates a DataTable by using the SourceTable as a template and creates the fields in the
        ///  order that is specified in the FieldList. If the FieldList is blank, the code uses DataTable.Clone().
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="FieldList"></param>
        /// <param name="KeyFieldList"></param>
        /// <returns></returns>
        public DataTable CreateTable(string TableName, string FieldList, string KeyFieldList)
        {
            DataTable dt = CreateTable(TableName, FieldList);
            string[] KeyFields = KeyFieldList.Split(',');
            if (KeyFields.Length > 0)
            {
                DataColumn[] KeyFieldColumns = new DataColumn[KeyFields.Length];
                int i;
                for (i = 1; i == KeyFields.Length - 1; ++i)
                {
                    KeyFieldColumns[i] = dt.Columns[KeyFields[i].Trim()];
                }
                dt.PrimaryKey = KeyFieldColumns;
            }
            return dt;  // You do not have to add to DataSet - The CreateTable call does that
        }


        /// <summary>
        ///  Creates a table based on fields of another table and related parent tables
        ///  
        ///  FieldList syntax: [relationname.]fieldname[ alias][,[relationname.]fieldname[ alias]]...
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="SourceTable"></param>
        /// <param name="FieldList"></param>
        /// <returns></returns>
        public DataTable CreateJoinTable(string TableName, DataTable SourceTable, string FieldList)
        {
            if (FieldList == null)
            {
                throw new ArgumentException("You must specify at least one field in the field list.");
                //return CreateTable(TableName, SourceTable);
            }
            else
            {
                DataTable dt = new DataTable(TableName);
                ParseFieldList(FieldList, true);
                foreach (FieldInfo Field in m_FieldInfo)
                {
                    if (Field.RelationName == null)
                    {
                        DataColumn dc = SourceTable.Columns[Field.FieldName];
                        dt.Columns.Add(dc.ColumnName, dc.DataType, dc.Expression);
                    }
                    else
                    {
                        DataColumn dc = SourceTable.ParentRelations[Field.RelationName].ParentTable.Columns[Field.FieldName];
                        dt.Columns.Add(dc.ColumnName, dc.DataType, dc.Expression);
                    }
                }
                if (this.InnerDataSet != null)
                    this.InnerDataSet.Tables.Add(dt);
                return dt;
            }
        }


        /// <summary>
        ///  Copies the selected rows and columns from SourceTable and inserts them into DestTable
        ///  FieldList has same format as CreatejoinTable
        /// </summary>
        /// <param name="DestTable"></param>
        /// <param name="SourceTable"></param>
        /// <param name="FieldList"></param>
        /// <param name="RowFilter"></param>
        /// <param name="Sort"></param>
        public void InsertJoinInto(DataTable DestTable, DataTable SourceTable,
            string FieldList, string RowFilter, string Sort)
        {
            if (FieldList == null)
            {
                throw new ArgumentException("You must specify at least one field in the field list.");
                //InsertInto(DestTable, SourceTable, RowFilter, Sort);
            }
            else
            {
                ParseFieldList(FieldList, true);
                DataRow[] Rows = SourceTable.Select(RowFilter, Sort);
                foreach (DataRow SourceRow in Rows)
                {
                    DataRow DestRow = DestTable.NewRow();
                    foreach (FieldInfo Field in m_FieldInfo)
                    {
                        if (Field.RelationName == null)
                        {
                            DestRow[Field.FieldName] = SourceRow[Field.FieldName];
                        }
                        else
                        {
                            DataRow ParentRow = SourceRow.GetParentRow(Field.RelationName);
                            DestRow[Field.FieldName] = ParentRow[Field.FieldName];
                        }
                    }
                    DestTable.Rows.Add(DestRow);
                }
            }
        }

        /// <summary>
        ///  Selects sorted, filtered values from one DataTable to another.
        ///  Allows you to specify relationname.fieldname in the FieldList to include fields from
        ///   a parent table. The Sort and Filter only apply to the base table and not to related tables.
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="SourceTable"></param>
        /// <param name="FieldList"></param>
        /// <param name="RowFilter"></param>
        /// <param name="Sort"></param>
        /// <returns></returns>
        public DataTable SelectJoinInto(string TableName, DataTable SourceTable, string FieldList, string RowFilter, string Sort)
        {

            DataTable dt = CreateJoinTable(TableName, SourceTable, FieldList);
            InsertJoinInto(dt, SourceTable, FieldList, RowFilter, Sort);
            return dt;
        }

        #endregion
        //----------------------------------------------------------------------
    }
}
