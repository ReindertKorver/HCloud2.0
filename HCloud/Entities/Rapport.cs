using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.Entities
{
    public class Rapport
    {
        public int ID { get; set; }
        public List<Therapy> Therapies { get; set; }
        public decimal GetTotalCosts()
        {
            decimal TotalPrice = 0;
            foreach (var item in Therapies)
            {
                if (item == null || item.CostsInEuro == 0)
                {

                }
                else
                {
                    TotalPrice += item.CostsInEuro;
                }
            }
            return TotalPrice;
        }
    }
}