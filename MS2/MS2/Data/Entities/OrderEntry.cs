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
        public string Size { get; set; }

        [NotMapped]
        public double EntryPrice
        {
            get
            {
                switch(Size)
                {
                    case "Small":
                        return Product.SmallPrice * Quantity;

                    case "Medium":
                        return Product.MediumPrice * Quantity;

                    case "Large":
                        return Product.LargePrice * Quantity;
                }
                return 0;
            }
        }

        public DateTime? PreparingTS { get; set; }
        public DateTime? CompletedTS { get; set; }
    }
}
