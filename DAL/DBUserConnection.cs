using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using HCloud.BLL;
using HCloud.Entities;
using MySql.Data.MySqlClient;

namespace HCloud.DAL
{
    public class DBUserConnection
    {
        private static DAL.DBConnectionString BCredentials = new DAL.DBConnectionString();
        MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(BCredentials.ConnectionString);

        /// <summary>
        /// Adds a user
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(Entities.User user)
        {
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("INSERT INTO user VALUES ('',@BsnNumber, @PhoneNumber,@FirstName,@LastName,@EmailAdress,@PasswordHash,@Confirmed,@UniqueID,0,0)", con))
            {
                cmd.Parameters.AddWithValue("@BsnNumber", user.BsnNumber ?? throw new Exception("BSN nummer is leeg"));
                cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber ?? "");
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName ?? throw new Exception("Voornaam is leeg"));
                cmd.Parameters.AddWithValue("@LastName", user.LastName ?? throw new Exception("Achternaam is leeg"));
                cmd.Parameters.AddWithValue("@EmailAdress", user.EmailAdress ?? throw new Exception("EmailAdress is leeg"));
                if (string.IsNullOrEmpty(user.PassWordHash))
                {
                    throw new Exception("Wachtwoord is leeg");
                }
                else
                {
                    if (user.UniqueUserID == null)
                    {
                        throw new Exception("uniek id is leeg");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@UniqueID", user.UniqueUserID);
                    }
                    cmd.Parameters.AddWithValue("@PasswordHash", user.UniqueUserID.ToString() + PassWordSecurity.Hash(user.PassWordHash));
                }
                cmd.Parameters.AddWithValue("@Confirmed", user.Confirmed);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
            }
        }
        public void DeleteUser()
        {

        }
        public Entities.User GetUserSignInCredentials(string username, string passwordHash)
        {
            string uniqueId;
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(username.ToLower()));
                uniqueId = PassWordSecurity.NameUUIDFromBytes(hash);
            }

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("Select * from  user where Lower(user.EmailAdress) =Lower(@Username) and user.PasswordHash =@password", con))
            {
                cmd.Parameters.AddWithValue("@Username", username ?? throw new Exception("Username is leeg"));
                cmd.Parameters.AddWithValue("@password", passwordHash ?? throw new Exception("Password is leeg"));
                Entities.User usercredentials = new Entities.User();
                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        usercredentials.EmailAdress = (string)reader["EmailAdress"];
                        usercredentials.PassWordHash = (string)reader["PasswordHash"];
                        usercredentials.Confirmed = (bool)reader["Confirmed"];
                        usercredentials.UniqueUserID = (string)reader["UniqueID"];
                        usercredentials.FirstName = (string)reader["FirstName"];
                        usercredentials.LastName = (string)reader["LastName"];
                        usercredentials.BsnNumber = (string)reader["BsnNumber"];
                        usercredentials.RoleID = (int)reader["RoleID"];
                        usercredentials.ID = (int)reader["ID"];
                    }


                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                if (!string.IsNullOrEmpty(usercredentials.EmailAdress) && !string.IsNullOrEmpty(usercredentials.PassWordHash))
                    return usercredentials;
                else
                    return null;

            }
        }
        /// <summary>
        /// Gets the Clients by logged in User role
        /// </summary>
        /// <param name="LoggedInUser"></param>
        /// <returns></returns>
        public List<User> getClients(User LoggedInUser)
        {
            MySql.Data.MySqlClient.MySqlCommand Using;
            List<User> Clients = new List<User>();
            DAL.DBRoleConnection dBRoleConnection = new DBRoleConnection();
            switch (LoggedInUser.RoleID)
            {

                case 2:
                    Using = new MySql.Data.MySqlClient.MySqlCommand("select user.ID, user.BsnNumber,user.FirstName,user.LastName from user where user.Confirmed!=0", con);
                    break;
                case 6:
                    Using = new MySql.Data.MySqlClient.MySqlCommand("select user.ID, user.BsnNumber,user.FirstName,user.LastName from user where user.MainTherapistID=@therapistid and user.Confirmed!=0", con);
                    break;
                case 7:
                    Using = new MySql.Data.MySqlClient.MySqlCommand("select user.ID, user.BsnNumber,user.FirstName,user.LastName from user where user.Confirmed!=0", con);
                    break;
                default:
                    Using = new MySql.Data.MySqlClient.MySqlCommand("select user.ID, user.BsnNumber,user.FirstName,user.LastName from user where user.MainTherapistID=@therapistid and user.Confirmed!=0", con);
                    break;


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
                        User Client = new User();
                        Client.ID = (int)reader["ID"];
                        Client.BsnNumber = (string)reader["BsnNumber"];
                        Client.FirstName = (string)reader["BsnNumber"] + " " + (string)reader["FirstName"] + " " + (string)reader["LastName"];
                        Clients.Add(Client);
                    }


                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }

            }


            return Clients;
        }
        /// <summary>
        /// Gets the pending clients that registered
        /// </summary>
        /// <param name="LoggedInUser"></param>
        /// <returns></returns>
        public List<User> getPendingUsers(User LoggedInUser)
        {
            MySql.Data.MySqlClient.MySqlCommand Using;
            List<User> Users = new List<User>();
            Using = new MySql.Data.MySqlClient.MySqlCommand("SELECT user.BsnNumber,user.EmailAdress, user.FirstName,user.LastName from user  where user.Confirmed=0", con);

            using (MySql.Data.MySqlClient.MySqlCommand cmd = Using)
            {
                cmd.Parameters.AddWithValue("@therapistid", (LoggedInUser.ID != 0) ? LoggedInUser.ID : throw new Exception("No rights to perform this action (User.ID = null)"));

                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User();
                        user.BsnNumber = (string)reader["BsnNumber"];
                        user.FirstName = (string)reader["FirstName"] + " " + (string)reader["LastName"];
                        user.EmailAdress = (string)reader["EmailAdress"];

                        Users.Add(user);
                    }


                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }

            }


            return Users;
        }
        public int ConfirmUser(Entities.User LoggedInuser, string bsnNumber, string emailadress)
        {
            MySql.Data.MySqlClient.MySqlCommand Using;
            Using = new MySql.Data.MySqlClient.MySqlCommand("Update user set user.Confirmed=1 where user.Confirmed=0 and user.BsnNumber=@bsn and user.EmailAdress=@email", con);
            int result = 0;
            using (MySql.Data.MySqlClient.MySqlCommand cmd = Using)
            {

                try
                {
                    cmd.Parameters.AddWithValue("@email", emailadress ?? throw new Exception("EmailAdress is leeg"));
                    cmd.Parameters.AddWithValue("@bsn", bsnNumber ?? throw new Exception("BSN nummer is leeg"));
                    con.Open();
                    result = cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    return 0;
                }
            }
            if (result != 1)
                return +result;
            else
            {
                return result;
            }

        }
        public List<User> GetAllUsers(Entities.User LoggedInuser)
        {
            MySql.Data.MySqlClient.MySqlCommand Using;
            List<User> Users = new List<User>();
            Using = new MySql.Data.MySqlClient.MySqlCommand("SELECT user.BsnNumber,user.EmailAdress, user.FirstName,user.LastName from user where user.RoleID!=7 and user.RoleID!=8", con);

            using (MySql.Data.MySqlClient.MySqlCommand cmd = Using)
            {
                var a = (LoggedInuser.ID != 0) ? LoggedInuser.ID : throw new Exception("No rights to perform this action (User.ID = null)");

                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User();
                        user.BsnNumber = (string)reader["BsnNumber"];
                        user.FirstName = (string)reader["FirstName"] + " " + (string)reader["LastName"] + " " + (string)reader["BsnNumber"];


                        Users.Add(user);
                    }


                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }

            }


            return Users;
        }
        public string ChangeRole(Entities.User LoggedInuser, string bsnnumber, int roleID)
        {
            MySql.Data.MySqlClient.MySqlCommand Using;
            Using = new MySql.Data.MySqlClient.MySqlCommand("Update user set user.RoleID=@role where user.BsnNumber=@bsn", con);
            
            using (MySql.Data.MySqlClient.MySqlCommand cmd = Using)
            {

                try
                {
                    cmd.Parameters.AddWithValue("@bsn", bsnnumber ?? throw new Exception("BSN nummer is leeg"));
                    cmd.Parameters.AddWithValue("@role", roleID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    return "Succesvol opgeslagen";
                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    return "FOUT: "+ex.Message;
                }
            }
        }

        public List<User> GetAllUsersWithRole(Entities.User LoggedInuser)
        {
            MySql.Data.MySqlClient.MySqlCommand Using;
            List<User> Users = new List<User>();
            Using = new MySql.Data.MySqlClient.MySqlCommand("SELECT user.ID, user.BsnNumber,user.EmailAdress, user.FirstName,user.LastName, roles.Description from user join roles on roles.ID=user.RoleID where user.RoleID!=7 and user.RoleID!=8", con);

            using (MySql.Data.MySqlClient.MySqlCommand cmd = Using)
            {
                var a = (LoggedInuser.ID != 0) ? LoggedInuser.ID : throw new Exception("No rights to perform this action (User.ID = null)");

                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User();
                        user.ID = (int)reader["ID"];
                        user.FirstName = (string)reader["Description"] + " " + (string)reader["FirstName"] + " " + (string)reader["LastName"] + " " + (string)reader["BsnNumber"];


                        Users.Add(user);
                    }


                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }

            }


            return Users;
        }
        public User GetUser(int id)
        {
            MySql.Data.MySqlClient.MySqlCommand Using;
            List<User> Users = new List<User>();
            Using = new MySql.Data.MySqlClient.MySqlCommand("SELECT * from user where user.ID=@id", con);
            
            User user = new User();
            using (MySql.Data.MySqlClient.MySqlCommand cmd = Using)
            {
                cmd.Parameters.AddWithValue("@id", id);
                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user.BsnNumber = (string)reader["BSNNumber"];
                        user.Confirmed = (bool)reader["Confirmed"];
                        user.EmailAdress = (string)reader["EmailAdress"];
                        user.FirstName = (string)reader["FirstName"];
                        user.LastName = (string)reader["LastName"];
                        user.UniqueUserID = (string)reader["UniqueID"];
                        user.ID = (int)reader["ID"];
                    }

                    
                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                
            }

            return user;
        }
        public string ChangeTherapist(Entities.User LoggedInuser, string bsnnumber, int UserID)
        {
            MySql.Data.MySqlClient.MySqlCommand Using;
            Using = new MySql.Data.MySqlClient.MySqlCommand("Update user set user.MainTherapistID=@UserID where user.BsnNumber=@bsn", con);

            using (MySql.Data.MySqlClient.MySqlCommand cmd = Using)
            {

                try
                {
                    cmd.Parameters.AddWithValue("@bsn", bsnnumber ?? throw new Exception("BSN nummer is leeg"));
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    return "Succesvol opgeslagen";
                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    return "FOUT: " + ex.Message;
                }
            }
        }
        public UserData GetUserData(User user)
        {
            MySql.Data.MySqlClient.MySqlCommand Using;
            Using = new MySql.Data.MySqlClient.MySqlCommand("SELECT * from userdata where userdata.userID=@id", con);

            UserData userdata = new UserData();
            using (MySql.Data.MySqlClient.MySqlCommand cmd = Using)
            {
                cmd.Parameters.AddWithValue("@id", user.ID);
                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        userdata.UserID = (int)reader["UserID"];
                        userdata.Bankrekeningnummer = (string)reader["Bankrekeningnummer"];
                        userdata.Bloedgroep = (string)reader["Bloedgroep"];
                        userdata.GeboorteDatum = (DateTime)reader["GeboorteDatum"];
                        userdata.Geboorteplaats = (string)reader["Geboorteplaats"];
                        userdata.Huisnummer = (string)reader["Huisnummer"];
                        userdata.Nationaliteit = (string)reader["Nationaliteit"];
                        userdata.PostCode = (string)reader["PostCode"];
                        userdata.Provincie = (string)reader["Provincie"];
                        userdata.Straat = (string)reader["Straat"];
                        userdata.Woonplaats = (string)reader["Woonplaats"];
                    }


                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                return userdata;
            }
        }
        public string SetUserData(User user,string setter, string Value)
        {

            MySql.Data.MySqlClient.MySqlCommand Using;
            Using = new MySql.Data.MySqlClient.MySqlCommand("Update UserData set @setter=@value where userData.UserID=@userid", con);

            using (MySql.Data.MySqlClient.MySqlCommand cmd = Using)
            {

                try
                {
                    cmd.Parameters.AddWithValue("@setter", setter ?? throw new Exception("SetUserData setter is leeg"));
                    cmd.Parameters.AddWithValue("@value", Value ?? throw new Exception("SetUserData value is leeg"));
                    cmd.Parameters.AddWithValue("@userid", user.ID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    return "Succesvol opgeslagen";
                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    return "FOUT: " + ex.Message;
                }
            }
        }
    }
}