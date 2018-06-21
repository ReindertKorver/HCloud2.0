using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.Entities
{
    public class File
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTime Date { get; set; }
        public enum Sortby
        {
            dateDESC,
            dateASC,
            descDESC,
            descASC
        }
    }

}