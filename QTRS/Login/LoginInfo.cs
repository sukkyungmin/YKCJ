using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTRS
{
    class LoginInfo
    {
        public string id { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public int authorityId { get; set; }
        public string authorityName { get; set; }
        public int departmentId { get; set; }
        public string departmentName { get; set; }
        public int jobId { get; set; }
        public string jobName { get; set; }
        public string phoneNumber { get; set; }
        public string cellphoneNumber { get; set; }
        public string emailAddress { get; set; }
        public DateTime loginTime { get; set; }
    }
}
