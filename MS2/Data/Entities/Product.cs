using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS2.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }
        public double SmallPrice { get; set; }
        public double MediumPrice { get; set; }
        public double LargePrice { get; set; }
    }
}
