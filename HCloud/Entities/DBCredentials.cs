using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.Entities
{
    public class DBCredentials
    {
        public string Server { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Database { get; set; }
    }
}