using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MS2.Data.Entities
{
    public class Order
    {
        [Key]
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderEntry> Items { get; set; }
        public string Status { get; set; }
        public string DeliveryAddress { get; set; }
        public string UserId { get; set; } 
    }
}
