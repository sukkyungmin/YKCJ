using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;
using System.Collections.ObjectModel;

namespace DMSGalaxy.Common.Utils
{
    public class XmlUtils
    {
        // Fields
        public const string CudAttributeName = "CUDflag";
        public const string RecordNodeName = "XmlRowData";
        public const string RootNodeName = "root";

        //// Methods
        //private XmlUtils()
        //{
        //}

        private static void GetTableRowsToXmlString(DataTable dt, XmlWriter writer, string cudType)
        {
            foreach (DataRow row in dt.Rows)
            {
                writer.WriteStartElement("XmlRowData");
                writer.WriteAttributeString("CUDflag", cudType);
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.DataType.ToString() == "System.DateTime" || column.DataType.ToString() == "System.DateTime")
                    {
                        writer.WriteAttributeString(column.ColumnName, Convert.ToDateTime(row[column]).ToString("yyyy.MM.dd HH:mm:ss") + "");
                    }
                    else
                    {
                        writer.WriteAttributeString(column.ColumnName, row[column] + "");
                    }
                }
                writer.WriteEndElement();
            }
        }

        private static void GetTableRowsToXmlString(DataRow[] drows, XmlWriter writer, string cudType)
        {
            foreach (DataRow row in drows)
            {
                writer.WriteStartElement("XmlRowData");
                writer.WriteAttributeString("CUDflag", cudType);
                foreach (DataColumn column in row.Table.Columns)
                {
                    if (column.DataType.ToString() == "DateTime" || column.DataType.ToString() == "System.DateTime")
                    {
                        writer.WriteAttributeString(column.ColumnName, Convert.ToDateTime(row[column]).ToString("yyyy.MM.dd HH:mm:ss") + "");
                    }
                    else
                    {
                        writer.WriteAttributeString(column.ColumnName, row[column] + "");
                    }
                }
                writer.WriteEndElement();
            }
        }

        public static XmlNodeList GetXmlNodeList(string xml)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            return document.GetElementsByTagName("XmlRowData");
        }
        
        public static string MakeXml3(DataRow[] iarr, DataRow[] uarr, DataRow[] darr)
        {
            StringBuilder output = null;
            XmlWriter writer = null;
            string str = null;
            if ((((iarr == null) || (iarr.Length <= 0)) && ((uarr == null) || (uarr.Length <= 0))) && ((darr == null) || (darr.Length <= 0)))
            {
                return str;
            }
            output = new StringBuilder();
            writer = XmlWriter.Create(output);
            writer.WriteStartElement("root");
            if ((iarr != null) && (iarr.Length > 0))
            {
                GetTableRowsToXmlString(iarr, writer, "C");
            }
            if ((uarr != null) && (uarr.Length > 0))
            {
                GetTableRowsToXmlString(uarr, writer, "U");
            }
            if ((darr != null) && (darr.Length > 0))
            {
                GetTableRowsToXmlString(darr, writer, "D");
            }
            writer.WriteEndElement();
            writer.Flush();

            return output.ToString().Replace("utf-16", "euc-kr");
        }

        public static string MakeXml3(DataTable idt, DataTable udt, DataTable ddt)
        {
            StringBuilder output = null;
            XmlWriter writer = null;
            string str = null;
            if ((((idt == null) || (idt.Rows.Count <= 0)) && ((udt == null) || (udt.Rows.Count <= 0))) && ((ddt == null) || (ddt.Rows.Count <= 0)))
            {
                return str;
            }
            output = new StringBuilder();
            writer = XmlWriter.Create(output);
            writer.WriteStartElement("root");
            if ((idt != null) && (idt.Rows.Count > 0))
            {
                GetTableRowsToXmlString(idt, writer, "C");
            }
            if ((udt != null) && (udt.Rows.Count > 0))
            {
                GetTableRowsToXmlString(udt, writer, "U");
            }
            if ((ddt != null) && (ddt.Rows.Count > 0))
            {
                GetTableRowsToXmlString(ddt, writer, "D");
            }
            writer.WriteEndElement();
            writer.Flush();
            return output.ToString().Replace("utf-16", "euc-kr");
        }

        public static string MakeXmlUserSet(DataTable dt, string UserSetState)
        {
            string str = string.Empty;
            foreach (DataRow row in dt.Rows)
            {
                if (UserSetState == "C")
                {
                    row.SetAdded();
                }
                else if (UserSetState == "U")
                {
                    row.SetModified();
                }
            }
            if (UserSetState == "C")
            {
                return MakeXml3(dt, null, null);
            }
            if (UserSetState == "U")
            {
                str = MakeXml3(null, dt, null);
            }
            return str;
        }

        public  string XmlUserSelectToString(XmlDocument doc, string Node, string Comma, string StartText, string EndText)
        {

            string _Line = "";

            _Line = StartText;

            for (int i = 0; i < doc.SelectNodes(Node).Count; i++)
            {
                if (i == (doc.SelectNodes(Node).Count - 1))
                {
                    _Line += doc.SelectNodes(Node)[i].InnerText;
                }
                else
                {
                    _Line += doc.SelectNodes(Node)[i].InnerText + Comma;
                }
            }

            _Line += EndText;

            return _Line;
        }

        public ObservableCollection<string> XmlUserSelectToCombox(XmlDocument doc, string Node ,string StartText)
        {

            ObservableCollection<string> list = new ObservableCollection<string>();

            for (int i = 0; i < doc.SelectNodes(Node).Count; i++)
            {
                list.Add(StartText + doc.SelectNodes(Node)[i].InnerText);
            }

            return list;
        }

    }
}
