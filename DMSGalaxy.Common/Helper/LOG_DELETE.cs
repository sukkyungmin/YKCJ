using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DMSGalaxy.Common.Helper
{
    //로그가 1년 전 것이면 지워버리는 클래스
    public class LOG_DELETE : IDisposable
    {
        #region 메모리 해제 함수
        private bool Disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool bManage)
        {
            if (Disposed == true) return;
            Disposed = true;

            if (bManage == true)
            {
                if (m_DeleteThread != null)
                {
                    try
                    {
                        m_DeleteThread.Abort();
                        m_DeleteThread = null;
                    }
                    catch { }
                }
            }
        }
        ~LOG_DELETE()
        {
            Dispose(false);
        }
        #endregion 메모리 해제 함수

        private Thread m_DeleteThread = null;

        public void RunLogDelete()
        {
            if (m_DeleteThread != null) m_DeleteThread.Abort();

            m_DeleteThread = new Thread(new ThreadStart(Log_Delete));
            m_DeleteThread.Start();
        }

        private void Log_Delete()
        {
            try
            {
                while (true)
                {
                    if ((m_DeleteThread == null) || (m_DeleteThread.IsAlive == false)) break;

                    int now_year = DateTime.Now.Year;
                    string[] log_folder = System.IO.Directory.GetDirectories("./LOG/");

                    foreach (string year in log_folder)
                    {
                        if (year.Length == 10)
                        {
                            try
                            {
                                int old = int.Parse(year.Substring(6, 4));

                                if (old < now_year)
                                {
                                    System.IO.Directory.Delete(string.Format("./LOG/{0}/", old), true);

                                    new LOG_Write().LOG(old.ToString() + " LOG DELETED", "DELETE_LOG");

                                }
                            }
                            catch { }
                        }
                    }

                    System.Threading.Thread.Sleep(TimeSpan.FromDays(1));    //하루 한번 스레드 돌며 이전 로그 폴더삭제
                }
            }
            catch (Exception ex)
            {
                new LOG_Write().LOG(ex.ToString(), "DELETE_LOG");
            }
        }
    }
}
