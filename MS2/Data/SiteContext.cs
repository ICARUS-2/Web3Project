using Microsoft.EntityFrameworkCore;
using MS2.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MS2.Models;

namespace MS2.Data
{
    public class SiteContext : DbContext
    {
        public SiteContext(DbContextOptions opt) : base(opt)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderEntry> OrderEntries { get; set; }
        public DbSet<MS2.Models.ContactModel> ContactModel { get; set; }
    }
}
