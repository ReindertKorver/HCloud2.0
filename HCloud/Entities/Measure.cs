using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.Entities
{
    public class Measure
    {
        public int ID { get; set; }
        public int TherapistID { get; set; }
        public double Temperature { get; set; }
        public string BsnNumber { get; set; }
        public string BloodPressure { get; set; }
        public DateTime Date { get; set; }
    }
}