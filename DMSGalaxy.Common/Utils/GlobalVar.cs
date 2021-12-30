using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSGalaxy.Common.Utils
{
    public class GlobalVar
    {

        /// <summary>
        /// 응용프로그램 실행모드
        /// </summary>
        public enum ExecuteMODE
        {
            /// <summary>
            /// NONE
            /// </summary>
            NONE = 0,
            /// <summary>
            /// 개발모드
            /// </summary>
            DEV = 1,
            /// <summary>
            /// 운영모드
            /// </summary>
            REAL = 2
        }

    }
}
