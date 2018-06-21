using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HCloud.Entities;
using MySql.Data.MySqlClient;

namespace HCloud.DAL
{
    public class DBTherapyConnection
    {
        private static DAL.DBConnectionString BCredentials = new DAL.DBConnectionString();
        MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(BCredentials.ConnectionString);

        /// <summary>
        /// Gets the deseases of a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<Therapy> GetOwn(User user)
        {
            List<Therapy> therapies = new List<Therapy>();
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select therapies.ID, therapies.Description, therapies.Location,therapies.MedicationID,therapies.DeseaseID, therapies.TherapistID,therapies.Date,therapies.BSNNumber,therapies.Time," +
                "therapies.Accepted,user.FirstName, user.LastName, medications.Description as 'MedicationDescription', deseases.Description as 'DeseaseDescription' from therapies join user on user.ID = therapies.TherapistID left join medications on medications.ID=therapies.MedicationID left join deseases on deseases.ID=therapies.DeseaseID where therapies.BSNNumber=@bsnnumber;", con))
            {

                cmd.Parameters.AddWithValue("@bsnnumber", user.BsnNumber ?? throw new Exception("BSN nummer is leeg"));
                Entities.User result = user;
                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Therapy therapy = new Therapy();
                        therapy.ID = (int)reader["ID"];
                        therapy.description = (string)reader["Description"];
                        therapy.date = (DateTime)reader["Date"];
                        therapy.Time = (TimeSpan)reader["Time"];
                        therapy.therapistID = (int)reader["TherapistID"];
                        therapy.Accepted = (bool)reader["Accepted"];
                        therapy.Location = (string)reader["Location"];
                        therapy.therapistFirstName = (string)reader["FirstName"];
                        therapy.therapistLastName = (string)reader["LastName"];

                        Desease desease = new Desease();

                        if (!reader.IsDBNull(reader.GetOrdinal("DeseaseDescription")))
                        {
                            desease.Description = (string)reader["DeseaseDescription"] ?? "";
                        }
                        else
                        {
                            desease.Description = "Geen";
                        }
                        Medication medication = new Medication();
                        if (!reader.IsDBNull(reader.GetOrdinal("MedicationDescription")))
                        {
                            medication.Description = (string)reader["MedicationDescription"] ?? "";
                        }
                        else
                        {
                            medication.Description = "Geen";
                        }

                        List<Medication> ListMedication = new List<Medication>();
                        ListMedication.Add(medication);


                        List<Desease> ListDeseases = new List<Desease>();
                        ListDeseases.Add(desease);

                        therapy.Deseases = ListDeseases;
                        therapy.Medication = ListMedication;
                        therapies.Add(therapy);
                    }
                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                return therapies;
            }
        }
        /// <summary>
        /// GetFilteredTherapies by Int BsnNumber and DateTime  
        /// </summary>
        /// <param name="bsn"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Therapy> GetFilteredTherapies(int bsn, DateTime date)
        {
            return getFilteredTherapies(bsn, date);
        }
        /// <summary>
        /// GetFilteredTherapies by only the BsnNumber
        /// </summary>
        /// <param name="bsn"></param>
        /// <returns></returns>
        public List<Therapy> GetFilteredTherapies(int bsn)
        {
            return getFilteredTherapies(bsn, DateTime.MinValue);
        }
        /// <summary>
        /// GetFilteredTherapies by only a DateTime  
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Therapy> GetFilteredTherapies(DateTime date)
        {
            return getFilteredTherapies(0, date);
        }
        /// <summary>
        /// getFilteredTherapies gets the data(therapies) for the public methods GetFilteredTherapies
        /// </summary>
        /// <param name="bsn"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private List<Therapy> getFilteredTherapies(int bsn, DateTime date)
        {
            MySql.Data.MySqlClient.MySqlCommand Using;
            if (bsn != 0 && date != DateTime.MinValue)//filter on both user and date
            {
                Using = new MySql.Data.MySqlClient.MySqlCommand("select therapies.ID, therapies.Description, therapies.MedicationID,therapies.DeseaseID, therapies.TherapistID,therapies.Date,therapies.BSNNumber,user.LastName, medications.Description as 'MedicationDescription', deseases.Description as 'DeseaseDescription' from therapies join user on user.ID = therapies.TherapistID left join medications on medications.ID=therapies.MedicationID left join deseases on deseases.ID=therapies.DeseaseID where therapies.Date=@date AND therapies.BSNNumber=@bsn", con);
            }
            else if (bsn == 0 && date != DateTime.MinValue)//only filter on date
            {
                Using = new MySql.Data.MySqlClient.MySqlCommand("select therapies.ID, therapies.Description, therapies.MedicationID,therapies.DeseaseID, therapies.TherapistID,therapies.Date,therapies.BSNNumber,user.LastName, medications.Description as 'MedicationDescription', deseases.Description as 'DeseaseDescription' from therapies join user on user.ID = therapies.TherapistID left join medications on medications.ID=therapies.MedicationID left join deseases on deseases.ID=therapies.DeseaseID where therapies.Date=@date", con);
            }
            else if (date == DateTime.MinValue && bsn != 0)//only filter on user
            {
                Using = new MySql.Data.MySqlClient.MySqlCommand("select therapies.ID, therapies.Description, therapies.MedicationID,therapies.DeseaseID, therapies.TherapistID,therapies.Date,therapies.BSNNumber,user.LastName, medications.Description as 'MedicationDescription', deseases.Description as 'DeseaseDescription' from therapies join user on user.ID = therapies.TherapistID left join medications on medications.ID=therapies.MedicationID left join deseases on deseases.ID=therapies.DeseaseID where therapies.BSNNumber=@bsn", con);
            }
            else
            {
                return null;
            }
            //------------start data request
            if (Using != null)
            {
                List<Therapy> therapies = new List<Therapy>();
                using (MySql.Data.MySqlClient.MySqlCommand cmd = Using)
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@bsn", bsn);

                        cmd.Parameters.AddWithValue("@date", date);

                        try
                        {
                            con.Open();
                            MySqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                Therapy therapy = new Therapy();
                                therapy.ID = (int)reader["ID"];
                                therapy.description = (string)reader["Description"];
                                therapy.date = (DateTime)reader["Date"];
                                therapy.therapistID = (int)reader["TherapistID"];
                                therapy.therapistLastName = (string)reader["LastName"];

                                Desease desease = new Desease();

                                if (!reader.IsDBNull(reader.GetOrdinal("DeseaseDescription")))
                                {
                                    desease.Description = (string)reader["DeseaseDescription"] ?? "";
                                }
                                else
                                {
                                    desease.Description = "Geen";
                                }
                                Medication medication = new Medication();
                                if (!reader.IsDBNull(reader.GetOrdinal("MedicationDescription")))
                                {
                                    medication.Description = (string)reader["MedicationDescription"] ?? "";
                                }
                                else
                                {
                                    medication.Description = "Geen";
                                }

                                List<Medication> ListMedication = new List<Medication>();
                                ListMedication.Add(medication);


                                List<Desease> ListDeseases = new List<Desease>();
                                ListDeseases.Add(desease);

                                therapy.Deseases = ListDeseases;
                                therapy.Medication = ListMedication;
                                therapies.Add(therapy);
                            }
                        }
                        catch (Exception ex)
                        {
                            if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                            throw new Exception(ex.Message);
                        }
                        if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                        return therapies;

                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
            }
            else
            {
                return null;
            }
        }
        public List<Therapy> GetAll(User user)
        {
            List<Therapy> therapies = new List<Therapy>();
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select therapies.ID, therapies.Description, therapies.Location, therapies.Costs, therapies.MedicationID,therapies.DeseaseID, therapies.TherapistID,therapies.Date,therapies.BSNNumber,user.LastName, medications.Description as 'MedicationDescription', deseases.Description as 'DeseaseDescription' from therapies join user on user.ID = therapies.TherapistID left join medications on medications.ID=therapies.MedicationID left join deseases on deseases.ID=therapies.DeseaseID", con))
            {


                Entities.User result = user;
                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Therapy therapy = new Therapy();
                        therapy.ID = (int)reader["ID"];
                        therapy.description = (string)reader["Description"];
                        therapy.date = (DateTime)reader["Date"];
                        therapy.therapistID = (int)reader["TherapistID"];
                        therapy.therapistLastName = (string)reader["LastName"];
                        therapy.Location = (string)reader["Location"];
                        therapy.CostsInEuro = Convert.ToDecimal((double)reader["Costs"]);
                        Desease desease = new Desease();

                        if (!reader.IsDBNull(reader.GetOrdinal("DeseaseDescription")))
                        {
                            desease.Description = (string)reader["DeseaseDescription"] ?? "";
                        }
                        else
                        {
                            desease.Description = "Geen";
                        }
                        Medication medication = new Medication();
                        if (!reader.IsDBNull(reader.GetOrdinal("MedicationDescription")))
                        {
                            medication.Description = (string)reader["MedicationDescription"] ?? "";
                        }
                        else
                        {
                            medication.Description = "Geen";
                        }

                        List<Medication> ListMedication = new List<Medication>();
                        ListMedication.Add(medication);


                        List<Desease> ListDeseases = new List<Desease>();
                        ListDeseases.Add(desease);

                        therapy.Deseases = ListDeseases;
                        therapy.Medication = ListMedication;
                        therapies.Add(therapy);
                    }
                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                return therapies;
            }
        }

        public void New(User user)
        {
        }

        public void Delete(User user)
        {
        }
        public bool Save(User user, Therapy therapy, Medication medication, Desease desease, string BSNNumber)
        {

            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("insert into therapies (therapies.ID,therapies.Description,therapies.MedicationID,therapies.DeseaseID,therapies.TherapistID,therapies.Date,therapies.Time,therapies.DateEnd,therapies.BSNNumber,therapies.Costs,therapies.Location,therapies.Accepted) values(0,@description,@medicationID,@deseaseID,@therapistID,@date,@endtime,@dateend,@bsnnumber,@costs,@location,0)", con))
            {
                try
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@bsnnumber", BSNNumber);
                    cmd.Parameters.AddWithValue("@description", therapy.description);
                    cmd.Parameters.AddWithValue("@medicationID", medication.ID);
                    cmd.Parameters.AddWithValue("@deseaseID", desease.ID);
                    cmd.Parameters.AddWithValue("@therapistID", user.ID);
                    cmd.Parameters.AddWithValue("@date", therapy.date);
                    cmd.Parameters.AddWithValue("@endtime", therapy.Time);
                    cmd.Parameters.AddWithValue("@dateend", therapy.date + therapy.Time);
                    cmd.Parameters.AddWithValue("@costs", MySqlDbType.Decimal).Value = therapy.CostsInEuro;
                    cmd.Parameters.AddWithValue("@location", therapy.Location);
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
        /// Checks if the therapy can fit in the other therapies that the client and the therapist have
        /// </summary>
        /// <param name="user"></param>
        /// <param name="therapy"></param>
        /// <returns></returns>
        public bool IsTherapyAllowed(User user, Therapy therapy, string BSN)
        {
            try
            {

                List<Therapy> therapies = new List<Therapy>();
                using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select therapies.ID,therapies.BSNNumber,therapies.TherapistID  from therapies  where" +
                    "(@startDate between Date and DateEnd) " +
                    "OR (@endDate between Date and DateEnd) " +
                    "OR (Date between @startDate and @endDate) " +
                    "OR (DateEnd between @startDate and @endDate )", con))
                {
                    cmd.Parameters.AddWithValue("@startDate", therapy.date);
                    cmd.Parameters.AddWithValue("@endDate", therapy.date + therapy.Time);
                    cmd.Parameters.AddWithValue("@therapist", user.ID);
                    cmd.Parameters.AddWithValue("@bsn", BSN);
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    List<Therapy> result = new List<Therapy>();
                    while (reader.Read())
                    {
                        Therapy nwtherapy = new Therapy();
                        nwtherapy.ID = (int)reader["ID"];
                        nwtherapy.therapistID = (int)reader["TherapistID"];
                        //fill description with bsn
                        nwtherapy.description = ((int)reader["BSNNumber"]).ToString();

                        result.Add(nwtherapy);
                    }
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    if (result != null)
                    {
                        List<bool> bl = new List<bool>();
                        foreach (var item in result)
                        {

                            if ((item.therapistID != user.ID)==true && (item.description != BSN)==true)
                            {
                                bl.Add(true);   
                            }
                            else
                            {
                                bl.Add(false);
                            }
                        }
                        if(bl.Contains(false))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}