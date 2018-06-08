using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAdress { get; set; }
        public string BsnNumber { get; set; }
        public string PassWordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string UniqueUserID { get; set; }
        public bool Confirmed { get; set; }
        public int RoleID { get; set; }
        public List<Therapy> Therapies { get; set; }
    }
}