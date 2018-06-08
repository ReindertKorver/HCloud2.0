using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.DAL
{
    public class DBConnectionString
    {
        bool localDB = true;
            
        string server1 = "localhost";
        string user1 = "root";
        string password1 = "";
        string database1 = "db_hcloud";

      

        public string ConnectionString { get { return "server=" + server1 + ";user=" + user1 + ";password=" + password1 + ";database=" + database1 + ";convert zero datetime=True;"; } }
        
    }
}