using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.DAL
{
    public class DBTestConnection
    {
        private static DAL.DBConnectionString BCredentials = new DAL.DBConnectionString();
        MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(BCredentials.ConnectionString);

        public bool TestDBConnection()
        {
            try
            {
                using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("", con))
                {
                    con.Open();
                }
            }
            catch (Exception ex)
            {
                con.Close();
                throw new Exception("Fout bij de verbinding met de database!");
            }
            con.Close();
                return true;
        }
    }
}