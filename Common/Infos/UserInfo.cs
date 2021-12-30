using System;
using System.Data;
using System.Xml;

namespace Common.Infos
{
    public class UserInfo
    {
        // Fields
        private static DateTime m_LoginDateTime = DateTime.Today;

        /// <summary>
        /// 사용자 고유 번호
        /// </summary>
        private static int m_US_Idx;

        /// <summary>
        /// 사용자 아이디
        /// </summary>
        private static string m_US_ID = string.Empty;

        /// <summary>
        /// 사용자 패스워드
        /// </summary>
        private static string m_US_PW = string.Empty;

        /// <summary>
        /// 사용자 이름
        /// </summary>
        private static string m_US_NM = string.Empty;

        /// <summary>
        /// 사용자 회사 포지션
        /// </summary>
        private static string m_US_JOB = string.Empty;

        /// <summary>
        /// 사용자 현재 Page 확인 
        /// </summary>
        private static string m_US_PAGE = string.Empty;

        /// <summary>
        /// 사용자 현재 Page 이름
        /// </summary>
        private static string m_US_PAGENAME = string.Empty;

        /// <summary>
        /// 사용자 이미지
        /// </summary>
        private static byte[] m_US_Images;

        /// <summary>
        /// 로그인 사용자 정보
        /// </summary>
        private static DataRow m_drUser = null;

        /// <summary>
        /// 로그인 사용자 아이템 정보
        /// </summary>
        private static DataRow m_drUserItem = null;

        /// <summary>
        /// 메뉴정보
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
        /// 로그인 사용자 아이템 정보
        /// </summary>
        public static DataRow USERITEM
        {
            get
            {
                return m_drUserItem;
            }
            set
            {
                m_drUserItem = value;
            }
        }

        /// <summary>
        /// 메뉴정보
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
        /// 사용자 고유 번호 (을)를 읽습니다.
        /// </summary>
        public static int US_IDX
        {
            get
            {
                return m_US_Idx;
            }
            set
            {
                m_US_Idx = value;
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
