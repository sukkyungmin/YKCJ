using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Net.NetworkInformation;
using System.Net;
using System.Text.RegularExpressions;


namespace DMSGalaxy.Common.Utils
{
    public class CommonUtil
    {
        /// <summary>
        /// 메시지 박스 생성
        /// </summary>
         //<param name="msgcode"></param>
        /// <param name="strmsg"></param>
        /// <returns></returns>
        public static DialogResult MessageAlert(string msgcode, string strmsg)
        {
            //Assembly assembly = new Form().GetType().Assembly;

            string text = new ResourceManager("DMSGalaxy.Common.NewResMessage", System.Reflection.Assembly.GetExecutingAssembly()).GetString(msgcode);

            text = string.Format("[{0}]\r\n{1}", msgcode, text);

            string caption = string.Empty;
            MessageBoxIcon _MsgBoxIcon = new MessageBoxIcon();
            MessageBoxButtons _MsgBoxBtn = new MessageBoxButtons();

            string strMsgGroup = msgcode.Substring(0, 1);
            if (false == string.IsNullOrWhiteSpace(strMsgGroup))
            {
                if (strMsgGroup == "I")
                {
                    _MsgBoxIcon = MessageBoxIcon.Asterisk;
                    _MsgBoxBtn = MessageBoxButtons.OK;
                    caption = "알림";
                    text = string.Format(text, strmsg);
                }
                else if (strMsgGroup == "Q")
                {
                    _MsgBoxIcon = MessageBoxIcon.Asterisk;
                    _MsgBoxBtn = MessageBoxButtons.OK;
                    caption = "확인";
                    text = string.Format(text, strmsg);
                }
                else if (strMsgGroup == "E")
                {
                    _MsgBoxIcon = MessageBoxIcon.Asterisk;
                    _MsgBoxBtn = MessageBoxButtons.OK;
                    caption = "오류";
                    text = string.Format("{0}\r\n{1}", text, strmsg);
                }
                else if (strMsgGroup == "X")
                {
                    _MsgBoxIcon = MessageBoxIcon.Asterisk;
                    _MsgBoxBtn = MessageBoxButtons.OK;
                    caption = "오류(Exception)";
                    text = string.Format("{0}\r\n{1}", text, strmsg);
                }
                else if (strMsgGroup == "C")
                {
                    _MsgBoxIcon = MessageBoxIcon.Asterisk;
                    _MsgBoxBtn = MessageBoxButtons.OK;
                    caption = "입력확인";
                    text = string.Format(text, strmsg);
                }
            }

            return MessageBox.Show(text, caption, _MsgBoxBtn, _MsgBoxIcon);
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            Image img = (Image)converter.ConvertFrom(byteArrayIn);

            return img;
        }

        public BitmapImage byteArrayToBitmaplmage(byte[] byteArrayln)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(byteArrayln);
            bi.EndInit();

            return bi;
        }

        public string TrimStrDate(string strDate)
        {
            if (strDate.Length >= 10)
            {
                strDate = strDate.Substring(0, 10);
            }
            else
            {
                strDate = string.Empty;
            }
            return strDate;
        }

        public DataTable CopyTable(DataTable originalTable)
        {

            DataTable newTable;
            newTable = originalTable.Copy();

            return newTable;

        }


        #region VersionInfos Table

        public DataTable DMSGVersionInfos(int versionCount)
        {
            DataTable newTable = new DataTable();
            DataRow row = null;

            newTable.Columns.Add(new DataColumn("Number", typeof(string)));
            newTable.Columns.Add(new DataColumn("Date", typeof(string)));
            newTable.Columns.Add(new DataColumn("Description", typeof(string)));

            for (int i = versionCount; i >= 0; i--)
            {
                row = newTable.NewRow();
                row.ItemArray = new object[]{new ResourceManager("DMSGalaxy.Common.VersionInforMessage", System.Reflection.Assembly.GetExecutingAssembly()).GetString("VNum" + i.ToString()),
                                                                                     new ResourceManager("DMSGalaxy.Common.VersionInforMessage", System.Reflection.Assembly.GetExecutingAssembly()).GetString("VDate" + i.ToString()),
                                                                                     new ResourceManager("DMSGalaxy.Common.VersionInforMessage", System.Reflection.Assembly.GetExecutingAssembly()).GetString("VDcs" + i.ToString())};
                newTable.Rows.Add(row);
            }

            return newTable;
        }

        #endregion


        #region Internet Connected Status Check

        public bool IsInternetConnected()
        {
            const string NCSI_TEST_URL = "http://www.msftncsi.com/ncsi.txt";
            const string NCSI_TEST_RESULT = "Microsoft NCSI";
            const string NCSI_DNS = "dns.msftncsi.com";
            const string NCSI_DNS_IP_ADDRESS = "131.107.255.255";

            try
            {
                // Check NCSI test link
                var webClient = new WebClient();
                string result = webClient.DownloadString(NCSI_TEST_URL);
                if (result != NCSI_TEST_RESULT)
                {
                    return false;
                }

                // Check NCSI DNS IP
                var dnsHost = Dns.GetHostEntry(NCSI_DNS);
                if (dnsHost.AddressList.Count() < 0 || dnsHost.AddressList[0].ToString() != NCSI_DNS_IP_ADDRESS)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Current Memory Status

        enum MemorySizeType
        {
            Byte, KByte, MByte, GByte, TByte
        }

        public string Memoryusage ()
        {
            long size = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64;
            string sizeStr = GetMemorySize(size);
            return sizeStr;         
        }

        private string GetMemorySize(Int64 usageMemory)
        {
            String retStr = String.Empty;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            int i = 0;

            while(usageMemory > 1024L)
            {
                usageMemory = (Int64)(usageMemory / 1024L);
                i++;
            }

            MemorySizeType sizeType = (MemorySizeType)i;

            sb.AppendFormat("{0}{1}", usageMemory, sizeType.ToString());
            retStr = sb.ToString();

            return retStr;
        }

#endregion  

        #region RMT Program Utils 

        /**
         * 엔터키 체크
         */
        static public bool IsReturnKey(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                return true;
            return false;
        }

        /**
         * 지정한 파일이 사용중인지 확인한다. 
         */
        static public bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;
            try
            {
                if (file.Exists == true)
                {
                    stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is: 
                //still being written to 
                //or being processed by another thread 
                //or does not exist (has already been processed) 
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            //file is not locked 
            return false;
        }

        /**
         * 주어진 문자열이 숫자인지 확인
         */
        static public bool IsDigit(string data)
        {
            long number = 0;
            return long.TryParse(data, out number);
        }

        /**
         * 주어진 문자열에서 0-9 까지의 숫자만 가져온다. 
         * 이름 변경 필요
         */
        static public string GetDigitFromString(string data)
        {
            return Regex.Replace(data, "[^0-9]", "", RegexOptions.Singleline);
        }

        /**
         * 주어진 문자열에서 0-9 까지의 숫자와 소수점만 가져온다.
         * 이름 변경 필요
         */
        static public string GetDecimalFromString(string data)
        {
            return Regex.Replace(data, "[^0-9.]", "", RegexOptions.Singleline);
        }

        /**
         * DBMS에서 허용되지 않는 문자를 변경한다. 
         * MySQL, SQLite, MSSQL에서 테스트 됐다. 
         * 점진적으로 개선 필요
         */
        static public string ReplaceSpecialChar(string data)
        {
            return data.Replace("'", "''");
        }

        /**
         * 세자리마다 콤마 삽입
         */
        static public string SetComma(string value)
        {
            if (value.Trim() == "")
                return value;

            string digit = value.Replace(",", "");
            Int64 i = 0;
            if (Int64.TryParse(digit, out i) == false)
                return value;

            return string.Format("{0:N0}", Convert.ToInt64(digit));
        }

        /**
         * 0~9까지의 숫자만 허용
         */
        static public void AcceptOnlyDigit(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /**
         * 소수만 허용
         */
        static public void AcceptOnlyDecimal(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !(char.IsDigit(e.KeyChar) || '.' == e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /**
         * 세자리마다 콤마 삽입
         * TextBox의 객체를 파리미터로 받아서 TextBox의 값 자체를 수정한다. 
         */
        static public void SetComma(object sender)
        {
            if ((sender as TextBox).Text.Trim() != "")
            {
                string digit = (sender as TextBox).Text.Replace(",", "");
                Int64 i = 0;
                if (Int64.TryParse(digit, out i) == false)
                    return;

                (sender as TextBox).Text = string.Format("{0:N0}", Convert.ToInt64(digit));
                (sender as TextBox).SelectionStart = (sender as TextBox).TextLength;
                (sender as TextBox).SelectionLength = 0;
            }
        }

        /** 
         * 지정한 소수 자리에서 반올림 한 값을 리턴한다. 
         */
        static public string GetValueRoundOff(string value, int decimalPlaces)
        {
            return string.Format("{0:F" + decimalPlaces.ToString() + "}", Convert.ToDouble(value));
        }

        /**
         * 입력된 문자열의 자리수만큼 * 문자로 변경한다.  
         */
        static public string ReplacePasswordToAsterisk(string data)
        {
            string asterisk = "";
            int len = data.Length;
            //return Regex.Replace(data, "", "*", RegexOptions.Singleline);
            for (int i = 0; i < len; i++)
                asterisk += "*";

            return asterisk;
        }


        /**
         * Image to Byte Array
         * http://www.codeproject.com/Articles/15460/C-Image-to-Byte-Array-and-Byte-Array-to-Image-Conv
         */
        static public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        /**
         * Byte Array to Image
         * http://www.codeproject.com/Articles/15460/C-Image-to-Byte-Array-and-Byte-Array-to-Image-Conv
         */
        static public System.Drawing.Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        /**
         * 영문자만 허용
         */
        static public void AcceptOnlyAlphaChar(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122)) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        /**
         * 단순 스트링 반환, null 값에 대한 예외처리
         */
        static public string GetString(object value)
        {
            if (value == null)
                return "";
            else
                return value.ToString();
        }
        /**
                 * 입력된 값으로부터 double 값을 가져온다.
                 */
        static public double GetDoubleValue(string data)
        {
            string valueStr = GetDecimalString(data);
            double value = 0.0d;
            if (valueStr != "")
                value = Double.Parse(valueStr);

            return value;
        }
        static public double GetDoubleValue(object data)
        {
            if (data == null)
                return 0.0d;

            return GetDoubleValue(data.ToString());
        }

        /**
          * 주어진 문자열에서 0-9 까지의 숫자와 소수점만 가져온다.
          * 음수에 대한 보완이 필요하다. 일단 무식한 방법으로다. 
          * 1개 이상의 소수에 대한 보완이 필요하다. 
          */
        static public string GetDecimalString(string data)
        {
            if (data == null || data.Trim() == "")
                return "";

            string minus = "";
            if (data.Trim().Substring(0, 1) == "-")
                minus = "-";

            return (minus + Regex.Replace(data, "[^0-9.]", "", RegexOptions.Singleline));
            //return Regex.Replace(data, "[^0-9.]", "", RegexOptions.Singleline);
        }

        /**
 * 주어진 문자열에서 int형을 리턴한다. 공백일 경우, 0을 리턴한다. 
 */
        static public int GetIntValue(string data)
        {
            string valueStr = Regex.Replace(data, "[^0-9]", "", RegexOptions.Singleline);
            int value = 0;
            if (valueStr != "")
                value = Int32.Parse(valueStr);

            return value;
        }

        /**
         * parent 영역 안에 target 영역에 커서가 있는지 검사한다.
         */
        static public bool InArea(Control target, Control parent)
        {
            Point cr = parent.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y));

            //Console.WriteLine("parent : " + cr.X.ToString() + " " + cr.Y.ToString());
            //Console.WriteLine("target : " + target.Left.ToString() + " " + target.Top.ToString());
            if (target.Bounds.Contains(cr))
                return true;
            else
                return false;

        }

        #endregion

        public bool   IPCheck()
        {
            bool pingable = false;
            using (Ping pinger = new Ping())
                try
                {
                    //PingReply reply = pinger.Send("192.168.10.28");
                    //pingable = reply.Status == IPStatus.Success;
                    pingable = true;
                }
                catch (PingException)
                {
                    // Discard PingExceptions and return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            return pingable;
            //return true;
        }

    }
}
