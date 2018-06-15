using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HCloud.Entities;
using MySql.Data.MySqlClient;

namespace HCloud.DAL
{
    public class DBMedicationConnection
    {

        private static DAL.DBConnectionString BCredentials = new DAL.DBConnectionString();
        MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(BCredentials.ConnectionString);
        public void Delete(User user)
        {
        }
        /// <summary>
        /// Saves a medication
        /// </summary>
        /// <param name="user"></param>
        public bool Save(User user, Medication medication, int BSNNumber)
        {
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("insert into medications (medications.ID,medications.Description,medications.HandedOut,medications.HandedOutDate,medications.ExpirationDate,medications.BSNNumber,medications.HandedOutByID,medications.Determiner) values(null,@description,0,@dateextra,@dateextra1,@bsnnumber,0,@determiner)", con))
            {
                try
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@description", medication.Description);
                    cmd.Parameters.AddWithValue("@bsnnumber", BSNNumber);
                    cmd.Parameters.AddWithValue("@determiner", medication.Determiner);
                    cmd.Parameters.AddWithValue("@dateextra", DateTime.Now);
                    cmd.Parameters.AddWithValue("@dateextra1", DateTime.Now);
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

        public List<Medication> GetAll(User user)
        {
            //select desease.Description from user_deseases join desease on user_deseases.deseaseID = desease.ID where user_deseases.BsnNumber =''
            List<Medication> ListMedications = new List<Medication>();
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select medications.ID, medications.BSNNumber,medications.Description as 'MedicationDescription', medications.HandedOut as 'MedicationHandedOut',medications.HandedOutDate as 'MedicationHandedOutDate', medications.ExpirationDate as 'MedicationExpirationDate', medications.HandedOutByID as 'MedicationHandedOutByID' from medications", con))
            {


                try
                {

                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        if (!((string)reader["MedicationDescription"] == "Geen"))
                        {
                            Medication medication = new Medication();
                            medication.ID = (int)reader["ID"];
                            medication.Description = (string)reader["MedicationDescription"] + " " + (int)reader["BSNNumber"];
                            medication.HandedOut = (bool)reader["MedicationHandedOut"];
                            medication.HandedOutDate = (DateTime)reader["MedicationHandedOutDate"];
                            medication.ExpirationDate = (DateTime)reader["MedicationExpirationDate"];
                            medication.HandedOutByID = (int)reader["MedicationHandedOutByID"];


                            ListMedications.Add(medication);
                        }
                    }

                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                return ListMedications;
            }
        }

        public List<Medication> GetOwn(User user)
        {
            //select desease.Description from user_deseases join desease on user_deseases.deseaseID = desease.ID where user_deseases.BsnNumber =''
            List<Medication> ListMedications = new List<Medication>();
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select medications.Description as 'MedicationDescription', medications.HandedOut as 'MedicationHandedOut',medications.HandedOutDate as 'MedicationHandedOutDate', medications.ExpirationDate as 'MedicationExpirationDate', medications.HandedOutByID as 'MedicationHandedOutByID' from medications  where medications.BsnNumber = @bsnnumber", con))
            {
                cmd.Parameters.AddWithValue("@bsnnumber", user.BsnNumber ?? throw new Exception("BSN nummer is leeg"));

                try
                {

                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {


                        Medication medication = new Medication();
                        medication.Description = (string)reader["MedicationDescription"];
                        medication.HandedOut = (bool)reader["MedicationHandedOut"];
                        medication.HandedOutDate = (DateTime)reader["MedicationHandedOutDate"];
                        medication.ExpirationDate = (DateTime)reader["MedicationExpirationDate"];
                        medication.HandedOutByID = (int)reader["MedicationHandedOutByID"];


                        ListMedications.Add(medication);

                    }

                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                return ListMedications;
            }
        }

        public void New(User user)
        {
        }

        /// <summary>
        /// Gets the Medications by logged in User role
        /// </summary>
        /// <param name="LoggedInUser"></param>
        /// <returns></returns>
        public List<Medication> getMedications(User LoggedInUser)
        {
            MySql.Data.MySqlClient.MySqlCommand Using;
            List<Medication> Medications = new List<Medication>();
            DBRoleConnection dBRoleConnection = new DBRoleConnection();
            var result = dBRoleConnection.GetUserRights(LoggedInUser);
            if (result.ShowAllMedications)
            {

                Using = new MySql.Data.MySqlClient.MySqlCommand("select medications.ID, medications.Description, medications.BSNNumber, medications.Determiner from medications", con);

            }
            else if (!result.ShowAllMedications)
            {
                Using = new MySql.Data.MySqlClient.MySqlCommand("select medications.ID, medications.Description,medications.BSNNumber,medications.Determiner from medications where medications.Determiner=@therapistid", con);
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
                        Medication medication = new Medication();
                        medication.Description = (string)reader["Description"] + " - BSN: " + (int)reader["BSNNumber"];
                        medication.ID = (int)reader["ID"];
                        medication.Determiner = (int)reader["Determiner"];
                        Medications.Add(medication);
                    }


                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }

            }


            return Medications;
        }
        public string HandOutMedication(Entities.User LoggedInuser, Entities.Medication medication)
        {
            MySql.Data.MySqlClient.MySqlCommand Using;
            Using = new MySql.Data.MySqlClient.MySqlCommand("UPDATE medications set medications.HandedOut=1,medications.HandedOutDate=@handoutdate,medications.ExpirationDate=@expdate,medications.HandedOutByID=@donebyid where medications.ID=@id", con);

            using (MySql.Data.MySqlClient.MySqlCommand cmd = Using)
            {

                try
                {
                    cmd.Parameters.AddWithValue("@handoutdate", medication.HandedOutDate);
                    cmd.Parameters.AddWithValue("@expdate", medication.ExpirationDate);
                    cmd.Parameters.AddWithValue("@donebyid", medication.HandedOutByID);
                    cmd.Parameters.AddWithValue("@id", medication.ID);
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