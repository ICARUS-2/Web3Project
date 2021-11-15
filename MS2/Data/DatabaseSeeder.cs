using MS2.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS2.Data
{
    public class DatabaseSeeder
    {
        private readonly SiteContext _context;

        public DatabaseSeeder(SiteContext ctx)
        {
            _context = ctx;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            if (!_context.Products.Any())
            {
                List<Product> sampleData = new List<Product>();
                sampleData.Add(new Product() { ItemName = "Pepperoni Pizza", Category = "Pizzas", Price = 7.99});
                sampleData.Add(new Product() { ItemName = "Cheese Pizza", Category = "Pizzas", Price = 7.49 });
                sampleData.Add(new Product() { ItemName = "All Dressed Pizza", Category = "Pizzas", Price = 8.49 });
                sampleData.Add(new Product() { ItemName = "Meat Lovers' Pizza", Category = "Pizzas", Price = 8.99 });
                sampleData.Add(new Product() { ItemName = "Vegetarian Pizza", Category = "Pizzas", Price = 7.49 });

                _context.Products.AddRange(sampleData);

                Order order = new Order()
                {
                    OrderDate = DateTime.Now,
                    Status = Order.OrderStatus.Pending,
                    Items = new List<OrderEntry>()
                    {
                        new OrderEntry()
                        {
                            Product = sampleData.First(),
                            Quantity = 4,
                        }
                    }
                };
                _context.Orders.Add(order);

                _context.SaveChanges();
            }
        }
    }
}
