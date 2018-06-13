using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.Entities
{
    public class Role
    {
        public string Description { get; set; }
        public int RoleID { get; set; }
        public bool ShowOwnDeseases { get; set; }
        public bool ShowOwnTherapies { get; set; }
        public bool ShowAllDeseases { get; set; }
        public bool ShowAllTherapies { get; set; }
        public bool ShowNewTherapy { get; set; }
        public bool ShowNewDesease { get; set; }
        public bool ShowNewMedication { get; set; }
        public bool ShowOwnMedication { get; set; }
        public bool ShowNewRapport { get; set; }
        public bool ShowOwnRapports { get; set; }
        public bool ShowAllRapports { get; set; }
        public bool Management { get; set; }
        public bool ShowAllMedications { get; set; }
        public bool ShowNewFile { get; set; }
        public bool ShowAllFiles { get; set; }
        public bool ShowOwnFiles { get; set; }
        public bool ShowClientData { get; set; }
    }
}