using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.Entities
{
    public class AgendaClass
    {
        public User Owner { get; set; }
        public List<AgendaItem> AgendaItems { get; set; }
    }
}