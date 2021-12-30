using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.Drawing;

namespace QTRS
{
    static class Utils
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
            string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);


        /**
        * Read ini
        */
        static public String GetIniValue(String Section, String Key, String iniPath)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, iniPath);
            return temp.ToString();
        }

        /**
         * 실수만 허용 
         */
        static public void AcceptOnlyRealNumber(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
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
      * 세자리마다 콤마 삽입
      * TextBox의 객체를 파리미터로 받아서 TextBox의 값 자체를 수정한다. 
      */
        static public void SetComma(object sender)
        {
            if ((sender as TextBox).Text.Trim() != "")
            {
                string digit = GetDigitString((sender as TextBox).Text);
                Int64 i = 0;
                if (Int64.TryParse(digit, out i) == false)
                    return;

                (sender as TextBox).Text = string.Format("{0:N0}", Convert.ToInt64(digit));
                (sender as TextBox).SelectionStart = (sender as TextBox).TextLength;
                (sender as TextBox).SelectionLength = 0;
            }
        }

        /**
         * 세자리마다 콤마 삽입
         */
        static public string SetComma(string value)
        {
            if (value.Trim() == "")
                return value;

            string digit = GetDigitString(value);
            Int64 i = 0;
            if (Int64.TryParse(digit, out i) == false)
                return value;

            return string.Format("{0:N0}", Convert.ToInt64(digit));
        }

        /**
        * 주어진 문자열에서 0-9 까지의 숫자와 소수점만 가져온다.
        * 음수에 대한 보완이 필요하다. 일단 무식한 방법으로다. 
        * 1개 이상의 소수에 대한 보완이 필요하다. 
        */
        static public string GetRealNumberString(string data)
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
         * 주어진 문자열에서 0-9 까지의 숫자만 가져온다. 
         * 음수에 대한 보완이 필요하다. 일단 무식한 방법으로다. 
         */
        static public string GetDigitString(string data)
        {
            if (data == null || data.Trim() == "")
                return "";

            string minus = "";
            if (data.Trim().Substring(0, 1) == "-")
                minus = "-";

            return (minus + Regex.Replace(data, "[^0-9]", "", RegexOptions.Singleline));
            //return Regex.Replace(data, "[^0-9]", "", RegexOptions.Singleline);
        }

        /**
         * DBMS에서 허용되지 않는 문자를 변경한다. 
         * MySQL, SQLite에서 테스트 됐다. 
         * 점진적으로 개선 필요
         */
        static public string ReplaceSpecialChar(string data)
        {
            return data.Replace("'", "''");
        }

        public static string EncryptString(string InputText, string Password)
        {

            // Rihndael class를 선언하고, 초기화
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            // 입력받은 문자열을 바이트 배열로 변환
            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);

            // 딕셔너리 공격을 대비해서 키를 더 풀기 어렵게 만들기 위해서 
            // Salt를 사용한다.
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

            // PasswordDeriveBytes 클래스를 사용해서 SecretKey를 얻는다.
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

            // Create a encryptor from the existing SecretKey bytes.
            // encryptor 객체를 SecretKey로부터 만든다.
            // Secret Key에는 32바이트
            // (Rijndael의 디폴트인 256bit가 바로 32바이트입니다)를 사용하고, 
            // Initialization Vector로 16바이트
            // (역시 디폴트인 128비트가 바로 16바이트입니다)를 사용한다.
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            // 메모리스트림 객체를 선언,초기화 
            MemoryStream memoryStream = new MemoryStream();

            // CryptoStream객체를 암호화된 데이터를 쓰기 위한 용도로 선언
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

            // 암호화 프로세스가 진행된다.
            cryptoStream.Write(PlainText, 0, PlainText.Length);

            // 암호화 종료
            cryptoStream.FlushFinalBlock();

            // 암호화된 데이터를 바이트 배열로 담는다.
            byte[] CipherBytes = memoryStream.ToArray();

            // 스트림 해제
            memoryStream.Close();
            cryptoStream.Close();

            // 암호화된 데이터를 Base64 인코딩된 문자열로 변환한다.
            string EncryptedData = Convert.ToBase64String(CipherBytes);

            // 최종 결과를 리턴
            return EncryptedData;
        }

        public static string DecryptString(string InputText, string Password)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            byte[] EncryptedData = Convert.FromBase64String(InputText);
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

            // Decryptor 객체를 만든다.
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream(EncryptedData);

            // 데이터 읽기(복호화이므로) 용도로 cryptoStream객체를 선언, 초기화
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

            // 복호화된 데이터를 담을 바이트 배열을 선언한다.
            // 길이는 알 수 없지만, 일단 복호화되기 전의 데이터의 길이보다는
            // 길지 않을 것이기 때문에 그 길이로 선언한다.
            byte[] PlainText = new byte[EncryptedData.Length];

            // 복호화 시작
            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);

            memoryStream.Close();
            cryptoStream.Close();

            // 복호화된 데이터를 문자열로 바꾼다.
            string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);

            // 최종 결과 리턴
            return DecryptedData;
        }

        /**
         * 엔터키 체크
         */
        static public bool IsReturnKey(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                return true;
            return false;
        }

        static public double GetDoubleValue(string data)
        {
            string valueStr = GetRealNumberString(data);
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
         * 평균 구하기
         */
        static public double GetAvgFromList(List<double> valueList)
        {
            if (valueList == null || valueList.Count == 0)
                return 0.0d;


            double total = 0.0d;
            for (int i = 0; i < valueList.Count; i++)
                total += valueList[i];

            return total / valueList.Count;
        }

        /**
         * 표준편차 구하기
         * 참조 URL : http://cafe.daum.net/_c21_/bbs_search_read?grpid=1IvRA&fldid=Jij3&datanum=20
         */
        static public double GetStdDeviation(List<double> valueList, double avg)
        {
            if (valueList.Count < 2)
                return 0.0d;

            double[] valueArray = valueList.ToArray();

            double sdSum = valueArray.Select(val => (val - avg) * (val - avg)).Sum();
            return Math.Sqrt(sdSum / (valueArray.Length - 1));
        }

        /**
         * Max 값 구하기 
         */
        static public double GetMaxFromList(List<double> valueList)
        {
            if (valueList == null || valueList.Count == 0)
                return 0.0d;

            valueList.Sort();

            return valueList[valueList.Count - 1];
        }

        /**
         * Min 값 구하기 
         */
        static public double GetMinFromList(List<double> valueList)
        {
            if (valueList == null || valueList.Count == 0)
                return 0.0d;

            valueList.Sort();

            return valueList[0];
        }

        static public string GetUniquFileNameByIndex(string filePath, string fileName)
        {
            string fileNameWithoutExt = fileName;
            string fileExt = "";

            int commmaIndex = fileName.LastIndexOf('.');
            if (commmaIndex != -1)
            {
                fileNameWithoutExt = fileName.Substring(0, commmaIndex);
                fileExt = fileName.Substring(commmaIndex);
            }

            int i = 0;
            string newFileName = string.Format("{0}\\{1}{2}", filePath, fileNameWithoutExt, fileExt);
            while (File.Exists(newFileName) == true)
            {
                newFileName = string.Format("{0}\\{1}({2}){3}", filePath, fileNameWithoutExt, (i++).ToString(), fileExt);
            }

            return newFileName;
        }

        static public string GetDoubleString(object data)
        {
            if (data == null)
                return "0";

            string valueStr = GetRealNumberString(data.ToString());
            double value = 0.0d;
            if (valueStr != "")
                value = Double.Parse(valueStr);

            return value.ToString();
        }

        static public string GetStringArrayToString(string[] valueArray, string delimiter)
        {
            string retVal = "";

            if (valueArray == null)
                return retVal;

            for (int i = 0; i < valueArray.Length; i++)
                retVal += valueArray[i] + delimiter;

            if (retVal.Length > 1)
                retVal = retVal.Substring(0, retVal.Length - 1);

            return retVal;
        }

        static public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        static public System.Drawing.Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }


        static public void OddDataGridViewRow(DataGridView dataGridView)
        {
            if (dataGridView.Rows.Count == 0)
                return;

            int currentRow = (dataGridView.Rows.Count - 1);
            if (currentRow % 2 == 0)
                dataGridView.Rows[currentRow].DefaultCellStyle.BackColor = Color.FromArgb(204, 236, 254);
            else
                dataGridView.Rows[currentRow].DefaultCellStyle.BackColor = Color.White;
        }

        static public void ResetOddDataGridViewRow(DataGridView dataGridView)
        {
            for (int currentRow = 0; currentRow < dataGridView.Rows.Count; currentRow++)
            {
                if (currentRow % 2 == 0)
                    dataGridView.Rows[currentRow].DefaultCellStyle.BackColor = Color.FromArgb(204, 236, 254);
                else
                    dataGridView.Rows[currentRow].DefaultCellStyle.BackColor = Color.White;
            }
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
         * 단순 스트링 반환, null 값에 대한 예외처리
         */
        static public string GetString(object value)
        {
            if (value == null)
                return "";
            else
                return value.ToString();
        }

        static public string GetStringArray(string value1, string value2, string value3, string value4)
        {
            string[] stringarray = new string[4] {value1, value2, value3, value4};

            string retnstring = "";

            foreach (string index in stringarray)
            {
                if (index != "0")
                    retnstring += index + " ";
            }

            return retnstring;

        }

        /**
      * 입력한 알파벳 문자를 하나 증가 시킨다. 
      */
        public static string IncreaseAlphabet(char alph, int count = 1)
        {
            int value = (int)alph;
            int zValue = (int)'Z';
            if (value == zValue || (value + count) > zValue)
                return alph.ToString();
            else
            {
                value += count;
                return ((char)value).ToString();
            }
        }

        /**
        * 입력한 알파벳 문자를 하나 감소 시킨다. 
        */
        public static string DecreaseAlphabet(char alph)
        {
            int value = (int)alph;
            int aValue = (int)'A';
            if (value == aValue)
                return alph.ToString();
            else
            {
                value -= 1;
                return ((char)value).ToString();
            }
        }

        /**
       * Write log
       */
        static public void WriteLog(string logFilePath, string contents)
        {
            string dirPath = logFilePath.Substring(0, logFilePath.LastIndexOf(Path.DirectorySeparatorChar));
            if (Directory.Exists(dirPath) == false)
                Directory.CreateDirectory(dirPath);

            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.Write(contents);
                writer.Write("\r\n");
            }
        }

        static public void SelectComboBoxItem(ComboBox comboBox, string value)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if ((comboBox.Items[i] as ComboBoxItem).Value.ToString() == value)
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
            }
        }
        static public void SelectComboBoxItemByText(ComboBox comboBox, string value)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if ((comboBox.Items[i] as ComboBoxItem).Text == value)
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        static public string GetSelectedComboBoxItemValue(ComboBox comboBox, string type = "s")
        {
            string value = "";
            if (comboBox.SelectedItem == null)
            {
                value = type == "s" ? "" : "0";
            }
            else
            {
                value = (comboBox.SelectedItem as ComboBoxItem).Value.ToString();
            }

            return value;
        }

        static public string GetSelectedComboBoxItemText(ComboBox comboBox, string type = "s")
        {
            string text = "";
            if (comboBox.SelectedItem == null)
            {
                text = type == "s" ? "" : "0";
            }
            else
            {
                text = (comboBox.SelectedItem as ComboBoxItem).Text;
            }

            return text;
        }

        static public DateTime GetDateTimeFormatFromString(string date, string format = "yyyy-MM-dd")
        {
            if (format == "yyyy-MM-dd")
            {
                string[] dateArray = date.Split('-');
                if (dateArray.Length == 3)
                {
                    return new DateTime(
                        Int32.Parse(dateArray[0]),
                        Int32.Parse(dateArray[1]),
                        Int32.Parse(dateArray[2]));
                }
                else
                {
                    return DateTime.Now;
                }
            }
            else
            {
                return DateTime.Now;
            }
        }

        static public DateTime GetDateTimeFormatFromObject(object date, string format = "yyyy-MM-dd")
        {
            if (format == "yyyy-MM-dd")
            {
                if (date == null)
                    return DateTime.Now;

                if (date.ToString().Length < 10)
                    return DateTime.Now;

                string dateString = date.ToString().Substring(0, 10);
                string[] dateArray = dateString.Split('-');
                if (dateArray.Length == 3)
                {
                    return new DateTime(
                        Int32.Parse(dateArray[0]),
                        Int32.Parse(dateArray[1]),
                        Int32.Parse(dateArray[2]));
                }
                else
                {
                    return DateTime.Now;
                }
            }
            else
            {
                return DateTime.Now;
            }
        }

        static public T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        static public double GetAverage<T>(this T[] data)
        {
            double retVal = 0.0;
            double sumValue = 0.0; 
            for(int i=0; i< data.Length; i++)
            {
                sumValue += double.Parse(data[i].ToString().Replace("..","."));
            }

            retVal = sumValue / data.Length;
            return retVal; 
        }
    }
}
