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

            ClearDatabase();

            if (!_context.Products.Any())
            {
                List<Product> sampleData = new List<Product>();

                //Pizza
                sampleData.Add(new Product() { ItemName = "Pepperoni Pizza", Category = "Pizzas", SmallPrice = 7.99, MediumPrice = 8.49, LargePrice = 8.99 });
                sampleData.Add(new Product() { ItemName = "Cheese Pizza", Category = "Pizzas", SmallPrice = 7.49, MediumPrice = 7.99, LargePrice = 8.49 });
                sampleData.Add(new Product() { ItemName = "All Dressed Pizza", Category = "Pizzas", SmallPrice = 8.49, MediumPrice = 8.99, LargePrice = 9.49 });
                sampleData.Add(new Product() { ItemName = "Meat Lovers' Pizza", Category = "Pizzas", SmallPrice = 8.99, MediumPrice = 9.49, LargePrice = 9.99 });
                sampleData.Add(new Product() { ItemName = "Vegetarian Pizza", Category = "Pizzas", SmallPrice = 7.49, MediumPrice = 7.99, LargePrice = 8.49 });

                sampleData.Add(new Product() { ItemName = "Fries", Category = "Fries", SmallPrice = 1.99, MediumPrice = 2.49, LargePrice = 2.99 });
                sampleData.Add(new Product() { ItemName = "Spicy Fries", Category = "Fries", SmallPrice = 1.99, MediumPrice = 2.49, LargePrice = 2.99 });

                //Drinks
                sampleData.Add(new Product() { ItemName = "Water", Category = "Drinks", SmallPrice = 0.49, MediumPrice = 0.79, LargePrice = 0.99 });
                sampleData.Add(new Product() { ItemName = "Pepsi", Category = "Drinks", SmallPrice = 0.99, MediumPrice = 1.29, LargePrice = 1.79 });
                sampleData.Add(new Product() { ItemName = "Diet Pepsi", Category = "Drinks", SmallPrice = 0.99, MediumPrice = 1.29, LargePrice = 1.79 });
                sampleData.Add(new Product() { ItemName = "Coke", Category = "Drinks", SmallPrice = 0.99, MediumPrice = 1.29, LargePrice = 1.79 });
                sampleData.Add(new Product() { ItemName = "Diet Coke", Category = "Drinks", SmallPrice = 0.99, MediumPrice = 1.29, LargePrice = 1.79 });
                sampleData.Add(new Product() { ItemName = "7-Up", Category = "Drinks", SmallPrice = 0.99, MediumPrice = 1.29, LargePrice = 1.79 });
                sampleData.Add(new Product() { ItemName = "Ginger Ale", Category = "Drinks", SmallPrice = 0.99, MediumPrice = 1.29, LargePrice = 1.79 });

                //Burgers
                sampleData.Add(new Product() { ItemName = "Burger", Category = "Burgers", SmallPrice = 3.99, MediumPrice = 4.49, LargePrice = 4.79 });
                sampleData.Add(new Product() { ItemName = "Chicken Burger", Category = "Burgers", SmallPrice = 3.99, MediumPrice = 4.49, LargePrice = 4.79 });
                sampleData.Add(new Product() { ItemName = "Veggie Burger", Category = "Burgers", SmallPrice = 4.99, MediumPrice = 5.49, LargePrice = 5.79 });
                _context.Products.AddRange(sampleData);

                Order order = new Order()
                {
                    OrderDate = DateTime.Now,
                    Status = "Pending",
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

                _context.JobPostings.Add(new JobPosting() { Title = "Cook", Wage = 17 });
                _context.JobPostings.Add(new JobPosting() { Title = "Cashier", Wage = 16.50 });
                _context.JobPostings.Add(new JobPosting() { Title = "Delivery Driver", Wage = 17 });
                _context.JobPostings.Add(new JobPosting() { Title = "Supervisor", Wage = 22.50 });

                _context.SaveChanges();
            }
        }

        public void ClearDatabase()
        {
            foreach (JobPosting jp in _context.JobPostings)
            {
                _context.JobPostings.Remove(jp);
            }

            foreach (OrderEntry oe in _context.OrderEntries)
            {
                _context.OrderEntries.Remove(oe);
            }

            foreach(Order o in _context.Orders)
            {
                _context.Orders.Remove(o);
            }

            foreach (Product p in _context.Products)
            {
                _context.Products.Remove(p);
            }

            _context.SaveChanges();
        }
    }
}
