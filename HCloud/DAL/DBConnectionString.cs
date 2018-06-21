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


        string server = "db4free.net";
        string user = "foxyerror";
        string password = "FFgqFUPn54No";
        string database = "hcloud";
        string port = "3306";
        public string ConnectionString { get { return "server=" + server+ ";port="+port+";user=" + user + ";password=" + password + ";database=" + database + ";convert zero datetime=True;"; } }
        
    }
}