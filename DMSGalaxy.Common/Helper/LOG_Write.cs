using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSGalaxy.Common.Helper
{
    public class LOG_Write
    {
        public void LOG(string txt, string filename)
        {
            for (int cnt = 0; cnt < 2; cnt++)
            {
                try
                {
                    lock (this)
                    {
                        TextRW LOG = new TextRW();

                        if (LOG.WriteTxt(string.Format("./LOG/{0}/{1}/", DateTime.Now.Year, DateTime.Now.Month),
                            string.Format("{0}_{1}.TXT", DateTime.Now.Day.ToString(), filename),
                            string.Format("{0}\t\t//{1}", DateTime.Now.ToString(), txt), false) == true) return;
                    }
                }
                catch { }
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }

            try
            {
                TextRW LOG_ERR = new TextRW();

                LOG_ERR.WriteTxt(string.Format("./LOG/{0}/{1}/", DateTime.Now.Year, DateTime.Now.Month),
                    string.Format("{0}_{1}.TXT", DateTime.Now.Day.ToString(), "LOG_ERR"),
                    string.Format("{0}\t\t//{1}", DateTime.Now.ToString(), txt), false);
            }
            catch { }
        }
    }
}
