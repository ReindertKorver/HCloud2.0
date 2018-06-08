using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HCloud.Entities;
using MySql.Data.MySqlClient;

namespace HCloud.DAL
{
    public class DBDeseaseConnection
    {
        private static DAL.DBConnectionString BCredentials = new DAL.DBConnectionString();
        MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(BCredentials.ConnectionString);
        public List<Desease> GetOwn(User user)
        {
            //select desease.Description from user_deseases join desease on user_deseases.deseaseID = desease.ID where user_deseases.BsnNumber =''
            List<Desease> ListDeseases = new List<Desease>();
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("SELECT deseases.ID,deseases.BSNNumber, deseases.DeterminerID, determiner.FirstName, determiner.LastName, deseases.Description ,deseases.Date, deseases.DeclaredHealthy, deseases.DeclaredHealthyDate from deseases join user as determiner on determiner.ID=deseases.DeterminerID   where deseases.BSNNumber =@bsnnumber", con))
            {
                cmd.Parameters.AddWithValue("@bsnnumber", user.BsnNumber ?? throw new Exception("BSN nummer is leeg"));

                try
                {

                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {


                        Desease desease = new Desease();
                        desease.ID= (int)reader["ID"];
                        desease.Description = (string)reader["Description"];
                        desease.determiner = (int)reader["DeterminerID"];
                        desease.determinerFirstName = (string)reader["FirstName"];
                        desease.determinerLastName = (string)reader["LastName"];
                        desease.date = (DateTime)reader["Date"];
                        desease.DeclaredHealthy = (bool)reader["DeclaredHealthy"];
                        desease.DeclaredHealthyDate = (DateTime)reader["DeclaredHealthyDate"];


                        ListDeseases.Add(desease);

                    }

                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                return ListDeseases;
            }
        }


        public List<Desease> GetAll(User user)
        {
            List<Desease> ListDeseases = new List<Desease>();
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("SELECT deseases.ID,deseases.BSNNumber, deseases.DeterminerID, determiner.FirstName, determiner.LastName, deseases.Description ,deseases.Date, deseases.DeclaredHealthy, deseases.DeclaredHealthyDate from deseases join user as determiner on determiner.ID=deseases.DeterminerID  ", con))
            {
               

                try
                {

                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {


                        Desease desease = new Desease();
                        desease.ID = (int)reader["ID"];
                        desease.Description = (string)reader["Description"] + "<br/> Gevonden bij: " + (int)reader["BSNNumber"];
                        desease.determiner = (int)reader["DeterminerID"];
                        desease.determinerFirstName = (string)reader["FirstName"];
                        desease.determinerLastName = (string)reader["LastName"];
                        desease.date = (DateTime)reader["Date"];
                        desease.DeclaredHealthy = (bool)reader["DeclaredHealthy"];
                        desease.DeclaredHealthyDate = (DateTime)reader["DeclaredHealthyDate"];


                        ListDeseases.Add(desease);

                    }

                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                return ListDeseases;
            }
        }

        public void New(User user)
        {
        }

        public void Delete(User user)
        {
        }
        //Saves a Desease
        public bool Save(User user, Desease desease, int bsnNumber)
        {
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("insert into deseases (ID,BSNNumber,Description,DeterminerID,Date,DeclaredHealthy,DeclaredHealthyDate) values('',@bsnnumber,@description,@determiner,@date,0,0)", con))
            {
                try
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@description", desease.Description);
                    cmd.Parameters.AddWithValue("@bsnnumber", bsnNumber);
                    cmd.Parameters.AddWithValue("@determiner", desease.determiner);
                    cmd.Parameters.AddWithValue("@date", desease.date);
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


        /// <summary>
        /// Gets the Deseases by logged in User role
        /// </summary>
        /// <param name="LoggedInUser"></param>
        /// <returns></returns>
        public List<Desease> getDeseases(User LoggedInUser)
        {
            MySql.Data.MySqlClient.MySqlCommand Using;
            List<Desease> Deseases = new List<Desease>();
            DBRoleConnection dBRoleConnection = new DBRoleConnection();
            var result = dBRoleConnection.GetUserRights(LoggedInUser);
            if (result.ShowAllDeseases)
            {

                Using = new MySql.Data.MySqlClient.MySqlCommand("select deseases.ID,deseases.BSNNumber,deseases.Description,deseases.DeterminerID from deseases", con);

            }
            else if (!result.ShowAllDeseases)
            {
                Using = new MySql.Data.MySqlClient.MySqlCommand("select deseases.ID,deseases.BSNNumber,deseases.Description,deseases.DeterminerID from deseases where deseases.DeterminerID=@therapistid", con);
            }
            else
            {
                throw new Exception("No rights to perform this action");
            }
            using (MySql.Data.MySqlClient.MySqlCommand cmd = Using)
            {
                cmd.Parameters.AddWithValue("@therapistid", (LoggedInUser.ID != 0) ? LoggedInUser.ID : throw new Exception("No rights to perform this action (User.ID = null)"));

                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Desease desease = new Desease();
                        desease.ID = (int)reader["ID"];
                        desease.Description = (string)reader["Description"] + " - BSN: " + (int)reader["BSNNumber"];
                        desease.determiner = (int)reader["DeterminerID"];
                        Deseases.Add(desease);
                    }


                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }

            }


            return Deseases;
        }
        public MySqlDataReader GetAllDeseasesInRegion(User user,UserData userData)
        {
            MySqlDataReader reader;
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select deseases.Description from deseases join user on user.BsnNumber=deseases.BSNNumber left join userdata on userdata.UserID=user.ID where userdata.Woonplaats=@place ", con))
            {
                cmd.Parameters.AddWithValue("@place",userData.Woonplaats);

                try
                {

                    con.Open();
                    reader = cmd.ExecuteReader();
                    

                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                return reader;
            }
        }
    }
    

}