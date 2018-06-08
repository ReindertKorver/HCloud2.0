using HCloud.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.DAL
{
    public class DBAgendaConnection
    {
        private static DAL.DBConnectionString BCredentials = new DAL.DBConnectionString();
        MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(BCredentials.ConnectionString);

        public AgendaClass GetAll(User user)
        {

            AgendaClass agendaClass = new AgendaClass();
            agendaClass.Owner = user;
            using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select agendaitems.ID ,agendaitems.Name,agendaitems.Description,agendaitems.StartTime, agendaitems.EndTime from agendaitems ", con))
            {


                try
                {
                    List<AgendaItem> agendaItems = new List<AgendaItem>();
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {


                        AgendaItem agendaItem = new AgendaItem();
                        agendaItem.ID = (int)reader["ID"];
                        agendaItem.Description = (string)reader["Description"];
                        agendaItem.Name = (string)reader["Name"];
                        agendaItem.StartTime = (DateTime)reader["StartTime"];
                        agendaItem.EndTime = (DateTime)reader["EndTime"];
                        agendaItems.Add(agendaItem);
                        

                    }
                    agendaClass.AgendaItems = agendaItems;
                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                    throw new Exception(ex.Message);
                }
                if (con.State != System.Data.ConnectionState.Closed) { con.Close(); }
                return agendaClass;
            }
        }
        public int AcceptItem(User user, int AgendItemID)
        {
            MySql.Data.MySqlClient.MySqlCommand Using;
            Using = new MySql.Data.MySqlClient.MySqlCommand("Update therapies set therapies.Accepted=1 where therapies.Accepted=0 and therapies.BsnNumber=@bsn and therapies.ID=@itemID", con);
            int result = 0;
            using (MySql.Data.MySqlClient.MySqlCommand cmd = Using)
            {

                try
                {
                    cmd.Parameters.AddWithValue("@itemID", AgendItemID);
                    cmd.Parameters.AddWithValue("@bsn", user.BsnNumber ?? throw new Exception("BSN nummer is leeg"));
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

    }
}