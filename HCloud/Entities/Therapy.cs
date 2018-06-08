using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.Entities
{
    public class Therapy
    {
        public int ID { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public TimeSpan Time { get; set; }
        public int therapistID { get; set; }
        public string therapistFirstName { get; set; }
        public string therapistLastName { get; set; }
        public List<Desease> Deseases { get; set; }
        public List<Medication> Medication { get; set; }
        public decimal CostsInEuro { get; set; }
        public string Location { get; set; }
        public bool Accepted { get; set; }
        public bool MakeTherapy()
        {
            return true;
        }
    }
}