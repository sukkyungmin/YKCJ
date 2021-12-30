using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSGalaxy.Common.Utils
{
    public class GlobalProperty
    {
        // Nested Types
        public enum CUDFlag
        {
            NONE,
            INSERT,
            UPDATE,
            DELETE
        }

        /// <summary>
        /// 버튼 기능권한
        /// </summary>
        public enum uBttonMode
        {
            AUTH_NEW,
            AUTH_SAVE,
            AUTH_DELETE,
            AUTH_REPORT,
            AUTH_PRINT,
            AUTH_FILEATTACH,
            AUTH_EXPORT,
            AUTH_CONFIRM,
            AUTH_RETURN,
            AUTH_WITHDRAWAL,
            AUTH_NONE
        }

        /// <summary>
        /// 버튼 사용유형
        /// </summary>
        public enum uBttonType
        {
            /// <summary>
            /// NONE
            /// </summary>
            NONE,
            /// <summary>
            /// 메뉴선택
            /// </summary>
            BTN_MENU,
            /// <summary>
            /// 기능수행
            /// </summary>
            BTN_FUNC
        }
    }
}
