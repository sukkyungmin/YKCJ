using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;

namespace DMSGalaxy.Common.Infos
{
    public class UserInfo
    {
        // Fields
        private static DateTime m_LoginDateTime = DateTime.Today;

        /// <summary>
        /// 사용자 아이디
        /// </summary>
        private static string m_US_ID = string.Empty;

        /// <summary>
        /// 사용자 패스워드
        /// </summary>
        private static string m_US_PW = string.Empty;

        /// <summary>
        /// 사용자 권한
        /// </summary>
        private static int m_US_LEV;

        /// <summary>
        /// 사용자 이름
        /// </summary>
        private static string m_US_NM = string.Empty;

        /// <summary>
        /// 사용자 사원번호
        /// </summary>
        private static string m_US_EPBER = string.Empty;

        /// <summary>
        /// 사용자 성별
        /// </summary>
        private static string m_US_SEX = string.Empty;

        /// <summary>
        /// 사용자 전화
        /// </summary>
        private static string m_US_PON = string.Empty;

        /// <summary>
        /// 사용자 생일
        /// </summary>
        private static string m_US_DAY = string.Empty;

        /// <summary>
        /// 사용자 회사 포지션
        /// </summary>
        private static string m_US_JOB = string.Empty;

        /// <summary>
        /// 사용자 E-Mail
        /// </summary>
        private static string m_US_MAIL = string.Empty;

        /// <summary>
        /// 사용자 소속 WG
        /// </summary>
        private static string m_US_IP = string.Empty;

        /// <summary>
        /// 사용자 IP
        /// </summary>
        private static string m_US_WG = string.Empty;

        /// <summary>
        /// 사용자 현재 Page 확인 
        /// </summary>
        private static string m_US_PAGE = string.Empty;

        /// <summary>
        /// 사용자 현재 Page 이름
        /// </summary>
        private static string m_US_PAGENAME = string.Empty;

        /// <summary>
        /// 사용자 담당 Line
        /// </summary>
        private static XmlDocument m_US_LINE;

        /// <summary>
        /// 사용자 이미지
        /// </summary>
        private static byte[] m_US_Images;

        /// <summary>
        /// 사용자 메세지 카운트
        /// </summary>
        private static int m_US_COUNT;

        /// <summary>
        /// 로그인 사용자 정보
        /// </summary>
        private static DataRow m_drUser = null;

        /// <summary>
        /// 로그인 사용자 레벨 메뉴정보
        /// </summary>
        private static DataSet m_dsUserSet = null;

        /// <summary>
        /// 각 메뉴별 정보
        /// </summary>
        private static DataSet m_dsMenuSet = null;

        /// <summary>
        /// 로그인 사용자 정보
        /// </summary>
        public static DataRow USERINFO
        {
            get
            {
                return m_drUser;
            }
            set
            {
                m_drUser = value;
            }
        }

        /// <summary>
        /// 로그인 사용자 레벨 메뉴정보
        /// </summary>
        public static DataSet USERSET
        {
            get
            {
                return m_dsUserSet;
            }
            set
            {
                m_dsUserSet = value;
            }
        }

        /// <summary>
        /// 각 메뉴별 정보
        /// </summary>
        public static DataSet MENUSET
        {
            get
            {
                return m_dsMenuSet;
            }
            set
            {
                m_dsMenuSet = value;
            }
        }

        /// <summary>
        /// 로그인 사용자 레벨 메뉴정보
        /// </summary>
        public static XmlDocument US_LINE
        {
            get
            {
                return m_US_LINE;
            }
            set
            {
                m_US_LINE = value;
            }
        }

        /// <summary>
        /// 사용자 아이디 (을)를 읽거나 설정합니다
        /// </summary>
        public static string US_ID
        {
            get
            {
                return m_US_ID;
            }
            set
            {
                m_US_ID = value;
            }
        }

        /// <summary>
        /// 사용자 패스워드 (을)를 읽거나 설정합니다
        /// </summary>
        public static string US_PW
        {
            get
            {
                return m_US_PW;
            }
            set
            {
                m_US_PW = value;
            }
        }

        /// <summary>
        /// 사용자 권한 (을)를 읽거나 설정합니다
        /// </summary>
        public static int US_LEV
        {
            get
            {
                return m_US_LEV;
            }
            set
            {
                m_US_LEV = value;
            }
        }

        /// <summary>
        /// 사용자 이름 (을)를 읽거나 설정합니다
        /// </summary>
        public static string US_NM
        {
            get
            {
                return m_US_NM;
            }
            set
            {
                m_US_NM = value;
            }
        }

        /// <summary>
        /// 사용자 사원번호 (을)를 읽거나 설정합니다
        /// </summary>
        public static string US_EPBER
        {
            get
            {
                return m_US_EPBER;
            }
            set
            {
                m_US_EPBER = value;
            }
        }

        /// <summary>
        /// 사용자 성별 (을)를 읽거나 설정합니다
        /// </summary>
        public static string US_SEX
        {
            get
            {
                return m_US_SEX;
            }
            set
            {
                m_US_SEX = value;
            }
        }

        /// <summary>
        /// 사용자 핸드폰번호 (을)를 읽거나 설정합니다
        /// </summary>
        public static string US_PON
        {
            get
            {
                return m_US_PON;
            }
            set
            {
                m_US_PON = value;
            }
        }

        /// <summary>
        /// 사용자 생일 (을)를 읽거나 설정합니다
        /// </summary>
        public static string US_DAY
        {
            get
            {
                return m_US_DAY;
            }
            set
            {
                m_US_DAY = value;
            }
        }

        /// <summary>
        /// 사용자 회사 포지션 (을)를 읽거나 설정합니다
        /// </summary>
        public static string US_JOB
        {
            get
            {
                return m_US_JOB;
            }
            set
            {
                m_US_JOB = value;
            }
        }

        /// <summary>
        /// 사용자 E-Mail (을)를 읽거나 설정합니다
        /// </summary>
        public static string US_MAIL
        {
            get
            {
                return m_US_MAIL;
            }
            set
            {
                m_US_MAIL = value;
            }
        }

        /// <summary>
        /// 사용자 소속WG (을)를 읽거나 설정합니다
        /// </summary>
        public static string US_WG
        {
            get
            {
                return m_US_WG;
            }
            set
            {
                m_US_WG = value;
            }
        }

        /// <summary>
        /// 사용자 IP (을)를 읽거나 설정합니다
        /// </summary>
        public static string US_IP
        {
            get
            {
                return m_US_IP;
            }
            set
            {
                m_US_IP = value;
            }
        }

        /// <summary>
        /// 사용자 현재 Page 확인 (을)를 읽거나 설정합니다
        /// </summary>
        public static string US_PAGE
        {
            get
            {
                return m_US_PAGE;
            }
            set
            {
                m_US_PAGE = value;
            }
        }

        /// <summary>
        /// 사용자 현재 Page 확인 (을)를 읽거나 설정합니다
        /// </summary>
        public static string US_PAGENAME
        {
            get
            {
                return m_US_PAGENAME;
            }
            set
            {
                m_US_PAGENAME = value;
            }
        }

        /// <summary>
        /// 사용자 이미지 (을)를 읽거나 설정합니다
        /// </summary>
        public static byte[] US_IMAGES
        {
            get
            {
                return m_US_Images;
            }
            set
            {
                m_US_Images = value;
            }
        }

        /// <summary>
        /// 사용자 메세지 카운트 (을)를 읽거나 설정합니다
        /// </summary>
        public static int US_COUNT
        {
            get
            {
                return m_US_COUNT;
            }
            set
            {
                m_US_COUNT = value;
            }
        }


        public static DateTime LoginDateTime
        {
            get
            {
                return m_LoginDateTime;
            }
            set
            {
                m_LoginDateTime = value;
            }
        }

    }
}
