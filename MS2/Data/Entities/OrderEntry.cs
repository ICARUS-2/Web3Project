using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MS2.Data.Entities
{
    public class OrderEntry
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        [NotMapped]
        public double EntryPrice
        {
            get
            {
                return Product.Price * Quantity;
            }
        }
    }
}
