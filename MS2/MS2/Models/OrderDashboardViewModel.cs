using MS2.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS2.Models
{
    public class OrderDashboardViewModel
    {
        public string Period { get; set; }
        public double TotalAmount { get; set; }
        public List<Order> Orders { get; set; }
    }
}
