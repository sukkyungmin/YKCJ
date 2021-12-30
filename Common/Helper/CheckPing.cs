using System.Diagnostics;
using System.Net.NetworkInformation;
using Common.Infos;

namespace Common.Helper
{
    public class CheckPing
    {
        public bool Check_PGM_Overlap()
        {
            Process my_prc = Process.GetCurrentProcess();
            Process[] all_procs = Process.GetProcesses();

            int cnt = 0;
            foreach (Process prc in all_procs)
            {
                if (prc.ProcessName == my_prc.ProcessName)
                    cnt++;
            }

            //return (cnt >= 2) ? true : false;

            if (cnt >= 2) return true;
            else return false;
        }

        public bool Check_IP()
        {
            if (SystemInfo.ExcMode == Utils.GlobalVar.ExecuteMODE.REAL)
            {
                using (Ping pingSender = new Ping())
                {
                    PingReply reply = pingSender.Send("121.137.95.29");
                    if (reply.Status != IPStatus.Success)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
