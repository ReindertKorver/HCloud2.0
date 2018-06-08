using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.Entities
{
    public class Medication
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public bool HandedOut { get; set; }
        public DateTime HandedOutDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int HandedOutByID { get; set; }
        public int Determiner { get; set; }
        public string getHandedOutByName()
        {
            try
            {
                DAL.DBUserConnection dBUserConnection = new DAL.DBUserConnection();
                if (HandedOutByID != 0)
                {
                    User user = dBUserConnection.GetUser(HandedOutByID);
                    return user.FirstName+" "+user.LastName;
                }
                else { return ""; }
            }
            catch (Exception ex)
            {
                return "";

            }

        }
    }
}