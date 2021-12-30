using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;

namespace DMSGalaxy.Common.Infos
{
    public class SystemInfo
    {

        private static DateTime m_ToDay = DateTime.Now.Date;

        /// <summary>
        /// 응용프로그램 위치 
        /// </summary>
        private static Point m_SPoint;

        /// <summary>
        /// 응용프로그램 사이즈
        /// </summary>
        private static Size m_SSize;

        /// <summary>
        /// 응용프로그램 실행모드
        /// </summary>
        private static Utils.GlobalVar.ExecuteMODE m_ExcMode = Utils.GlobalVar.ExecuteMODE.NONE;

        /// <summary>
        /// 응용프로그램 위치 
        /// </summary>
        public static Point SPoint
        {
            get
            {
                return m_SPoint;
            }
            set
            {
                m_SPoint = value;
            }
        }

        /// <summary>
        /// 응용프로그램 사이즈
        /// </summary>
        public static Size SSize
        {
            get
            {
                return m_SSize;
            }
            set
            {
                m_SSize = value;
            }
        }

        /// <summary>
        /// 응용프로그램 실행모드
        /// </summary>
        public static Utils.GlobalVar.ExecuteMODE ExcMode
        {
            get
            {
                return m_ExcMode;
            }
            set
            {
                m_ExcMode = value;
            }
        }

        public static DateTime ToDay
        {
            get
            {
                return m_ToDay;
            }
            set
            {
                m_ToDay = value;
            }
        }

    }
}
