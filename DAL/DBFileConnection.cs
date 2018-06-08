using HCloud.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.DAL
{
    public class DBFileConnection
    {

        private static DAL.DBConnectionString BCredentials = new DAL.DBConnectionString();
        MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(BCredentials.ConnectionString);

        public List<File> GetFiles(User user)
        {
            List<File> files = new List<File>();
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select * from files where files.userID=@userid", con))
            {

                cmd.Parameters.AddWithValue("@userid", user.ID);
                Entities.User result = user;
                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        File file = new File();
                        file.ID = (int)reader["ID"];
                        file.Description = (string)reader["fileDescription"];
                        file.FileName = (string)reader["fileName"];
                        file.FilePath = (string)reader["filePath"];
                        file.Date = (DateTime)reader["Date"];
                        files.Add(file);
                    }
                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                return files;
            }
        }

        public bool Save(User user,File file, int userid)
        {
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("insert into files (files.ID,files.fileName,files.filePath,files.userID,files.fileDescription,files.Date ) values('',@filename,@filepath,@userid,@filedesc,@date)", con))
            {
                try
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@filename", file.FileName);
                    cmd.Parameters.AddWithValue("@filepath", file.FilePath);
                    cmd.Parameters.AddWithValue("@userid",userid);
                    cmd.Parameters.AddWithValue("@filedesc", file.Description);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {

                    }
                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
            }
            return true;

        }

    }
}