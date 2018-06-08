using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.DAL
{
    public class DBRoleConnection
    {
        //Select user.RoleID, rights.ShowOwnDeseases, rights.ShowOwnTherapies, rights.ShowAllDeseases, rights.ShowAllTherapies, rights.ShowNewTherapy, rights.ShowNewDesease, rights.ShowNewMedication, rights.ShowOwnMedication, rights.ShowNewRapport, rights.ShowOwnRapports, rights.ShowAllRapports, rights.ChangeClientNAW, rights.ShowAllMedications from user inner join roles on roles.ID = user.RoleID inner join rights on rights.ID= roles.RightsID  where user.BsnNumber='0123456790'and user.UniqueID='82a7969a-3570-f75b-a2cd-76c7ff0a396d'
        private static DAL.DBConnectionString BCredentials = new DAL.DBConnectionString();
        MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(BCredentials.ConnectionString);

        public Entities.Role GetUserRights(Entities.User user)
        {
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("Select user.RoleID, rights.ShowOwnDeseases, rights.ShowOwnTherapies, rights.ShowAllDeseases, rights.ShowAllTherapies, rights.ShowNewTherapy, rights.ShowNewDesease, rights.ShowNewMedication, rights.ShowOwnMedication, rights.ShowNewRapport, rights.ShowOwnRapports, rights.ShowAllRapports, rights.ChangeClientNAW, rights.ShowAllMedications, rights.ShowOwnFiles,rights.ShowAllFiles,rights.ShowNewFile, roles.Description from user inner join roles on roles.ID = user.RoleID inner join rights on rights.ID= roles.RightsID  where user.BsnNumber=@bsnnummer and user.UniqueID=@uniqueid", con))
            {
                Entities.Role role = new Entities.Role();
                cmd.Parameters.AddWithValue("@bsnnummer", user.BsnNumber ?? throw new Exception("BSN nummer is leeg"));
                if (user.UniqueUserID == null)
                {
                    throw new Exception("uniek id is leeg");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@uniqueid", user.UniqueUserID);
                }
                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        role.Description = (string)reader["Description"];
                        role.ShowOwnDeseases = (bool)reader["ShowOwnDeseases"];
                        role.ShowOwnTherapies = (bool)reader["ShowOwnTherapies"];
                        role.ShowAllDeseases = (bool)reader["ShowAllDeseases"];
                        role.ShowAllTherapies = (bool)reader["ShowAllTherapies"];
                        role.ShowNewTherapy = (bool)reader["ShowNewTherapy"];
                        role.ShowNewDesease = (bool)reader["ShowNewDesease"];
                        role.ShowNewMedication = (bool)reader["ShowNewMedication"];
                        role.ShowOwnMedication = (bool)reader["ShowOwnMedication"];
                        role.ShowNewRapport = (bool)reader["ShowNewRapport"];
                        role.ShowOwnRapports = (bool)reader["ShowOwnRapports"];
                        role.ShowAllRapports = (bool)reader["ShowAllRapports"];
                        role.Management = (bool)reader["ChangeClientNAW"];
                        role.ShowAllMedications = (bool)reader["ShowAllMedications"];
                        role.ShowNewFile = (bool)reader["ShowNewFile"];
                        role.ShowAllFiles = (bool)reader["ShowAllFiles"];
                        role.ShowOwnFiles = (bool)reader["ShowOwnFiles"];

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return role;
            }
        }
        public List<Entities.Role> GetRoles()
        {
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("SELECT roles.ID, roles.Description FROM roles", con))
            {
                List<Entities.Role> roles = new List<Entities.Role>();

                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Entities.Role role = new Entities.Role();
                        role.RoleID = (int)reader["ID"];
                        role.Description = (string)reader["Description"];
                        if (role.Description != "Administrator")
                            roles.Add(role);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return roles;
            }
        }
        public List<Entities.Role> GetRights()
        {
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("SHOW FIELDS FROM rights;", con))
            {
                List<Entities.Role> roles = new List<Entities.Role>();

                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Entities.Role role = new Entities.Role();
                        role.Description = reader.GetString(0);
                        if (role.Description != "ID")
                            roles.Add(role);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return roles;
            }
        }
        public string ChangeRole(Entities.User LoggedInuser, Entities.Role role)
        {
            if (LoggedInuser.RoleID!=0)
            {

                MySql.Data.MySqlClient.MySqlCommand Using;
                Using = new MySql.Data.MySqlClient.MySqlCommand("UPDATE rights set rights.ShowAllDeseases=@ShowAllDeseases,rights.ShowAllMedications=@ShowAllMedications,rights.ShowAllRapports=@ShowAllRapports,rights.ShowAllTherapies=@ShowAllTherapies,rights.ShowNewDesease=@ShowNewDesease,rights.ShowNewMedication=@ShowNewMedication,rights.ShowNewRapport=@ShowNewRapport,rights.ShowNewTherapy=@ShowNewTherapy,rights.ShowOwnDeseases=@ShowOwnDeseases,rights.ShowOwnMedication=@ShowOwnMedication,rights.ShowOwnRapports=@ShowOwnRapports,rights.ShowOwnTherapies=@ShowOwnTherapies,rights.ChangeClientNAW=@Management,rights.ShowNewFile=@shownewfile, rights.ShowAllFiles=@showallfiles,rights.ShowOwnFiles=@showownfiles where rights.ID=@roleID", con);

                using (MySql.Data.MySqlClient.MySqlCommand cmd = Using)
                {

                    try
                    {
                        cmd.Parameters.AddWithValue("@desc", role.Description ?? throw new Exception("Rol beschrijving is leeg in ChangeRole()"));
                        if (role.RoleID != 0)
                            cmd.Parameters.AddWithValue("@roleID", role.RoleID);
                        else
                            throw new Exception("Rol id is 0 in ChangeRole()");

                        cmd.Parameters.AddWithValue("@ShowAllDeseases", role.ShowAllDeseases);
                        cmd.Parameters.AddWithValue("@ShowAllMedications", role.ShowAllMedications);
                        cmd.Parameters.AddWithValue("@ShowAllRapports", role.ShowAllRapports);
                        cmd.Parameters.AddWithValue("@ShowAllTherapies", role.ShowAllTherapies);
                        cmd.Parameters.AddWithValue("@ShowNewDesease", role.ShowNewDesease);
                        cmd.Parameters.AddWithValue("@ShowNewMedication", role.ShowNewMedication);
                        cmd.Parameters.AddWithValue("@ShowNewRapport", role.ShowNewRapport);
                        cmd.Parameters.AddWithValue("@ShowNewTherapy", role.ShowNewTherapy);
                        cmd.Parameters.AddWithValue("@ShowOwnDeseases", role.ShowOwnDeseases);
                        cmd.Parameters.AddWithValue("@ShowOwnMedication", role.ShowOwnMedication);
                        cmd.Parameters.AddWithValue("@ShowOwnRapports", role.ShowOwnRapports);
                        cmd.Parameters.AddWithValue("@ShowOwnTherapies", role.ShowOwnTherapies);
                        cmd.Parameters.AddWithValue("@Management", role.Management);
                        cmd.Parameters.AddWithValue("@shownewfile", role.ShowNewFile);
                        cmd.Parameters.AddWithValue("@showownfiles", role.ShowOwnFiles);
                        cmd.Parameters.AddWithValue("@showallfiles", role.ShowAllFiles);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        return "Succesvol opgeslagen";
                    }
                    catch (Exception ex)
                    {
                        if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                        return "FOUT: " + ex.Message;
                    }
                }
            }
            else
            {
                return "Not allowed to perform this action";
            }
        }
    }
}