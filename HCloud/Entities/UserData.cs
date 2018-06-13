using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.Entities
{
    public class UserData
    {
        public  enum Types
        {
            PostCode,
            Woonplaats,
            Straat,
            Huisnummer,
            Provincie,
            Nationaliteit,
            Geboorteplaats,
            GeboorteDatum,
            Bloedgroep,
            Bankrekeningnummer
        }
        public int UserID { get; set; }
        public string PostCode { get; set; }
        public string Woonplaats { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Provincie { get; set; }
        public string Nationaliteit { get; set; }
        public string Geboorteplaats { get; set; }
        public DateTime GeboorteDatum { get; set; }

        public string Bloedgroep { get; set; }
        public string Bankrekeningnummer { get; set; }
        public static UserData GetUserDataFromDB(User LoggedInUser)
        {

            DAL.DBUserConnection dBUserConnection = new DAL.DBUserConnection();
            
            return dBUserConnection.GetUserData(LoggedInUser);
        }
    }
}