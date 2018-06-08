using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.Entities
{
    public class AgendaItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        private string description { get; set; }
        public string Description { get { return Name + "<br/>" + description; } set { description = value; } }
    }
}