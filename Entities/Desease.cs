using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.Entities
{
    public class Desease
    {
        public int ID { get; set; }
        public int determiner { get; set; }
        public string determinerFirstName { get; set; }
        public string determinerLastName { get; set; }
        public string Description { get; set; }
        public DateTime date { get; set; }
        public bool DeclaredHealthy { get; set; }
        public DateTime DeclaredHealthyDate { get; set; }
       
    }
}