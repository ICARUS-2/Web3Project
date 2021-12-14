using MS2.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MS2.Data
{
    public class DatabaseSeeder
    {
        private readonly ApplicationDbContext _context;

        public DatabaseSeeder(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            //ClearDatabase();

            if (!_context.Products.Any())
            {
                List<Product> products = GetMenuItems();
                _context.Products.AddRange(products);

                List<Order> orders = GetOrders(products);
                _context.Orders.AddRange(orders);

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

            foreach (Order o in _context.Orders)
            {
                _context.Orders.Remove(o);
            }

            foreach (Product p in _context.Products)
            {
                _context.Products.Remove(p);
            }

            _context.SaveChanges();
        }

        public List<Product> GetMenuItems()
        {
            List<Product> data = new List<Product>();

            //Pizza
            data.Add(new Product() { ItemName = "Pepperoni Pizza", Category = "Pizzas", SmallPrice = 7.99, MediumPrice = 8.49, LargePrice = 8.99 });
            data.Add(new Product() { ItemName = "Cheese Pizza", Category = "Pizzas", SmallPrice = 7.49, MediumPrice = 7.99, LargePrice = 8.49 });
            data.Add(new Product() { ItemName = "All Dressed Pizza", Category = "Pizzas", SmallPrice = 8.49, MediumPrice = 8.99, LargePrice = 9.49 });
            data.Add(new Product() { ItemName = "Meat Lovers' Pizza", Category = "Pizzas", SmallPrice = 8.99, MediumPrice = 9.49, LargePrice = 9.99 });
            data.Add(new Product() { ItemName = "Vegetarian Pizza", Category = "Pizzas", SmallPrice = 7.49, MediumPrice = 7.99, LargePrice = 8.49 });

            data.Add(new Product() { ItemName = "Fries", Category = "Fries", SmallPrice = 1.99, MediumPrice = 2.49, LargePrice = 2.99 });
            data.Add(new Product() { ItemName = "Spicy Fries", Category = "Fries", SmallPrice = 1.99, MediumPrice = 2.49, LargePrice = 2.99 });

            //Drinks
            data.Add(new Product() { ItemName = "Water", Category = "Drinks", SmallPrice = 0.49, MediumPrice = 0.79, LargePrice = 0.99 });
            data.Add(new Product() { ItemName = "Pepsi", Category = "Drinks", SmallPrice = 0.99, MediumPrice = 1.29, LargePrice = 1.79 });
            data.Add(new Product() { ItemName = "Diet Pepsi", Category = "Drinks", SmallPrice = 0.99, MediumPrice = 1.29, LargePrice = 1.79 });
            data.Add(new Product() { ItemName = "Coke", Category = "Drinks", SmallPrice = 0.99, MediumPrice = 1.29, LargePrice = 1.79 });
            data.Add(new Product() { ItemName = "Diet Coke", Category = "Drinks", SmallPrice = 0.99, MediumPrice = 1.29, LargePrice = 1.79 });
            data.Add(new Product() { ItemName = "7-Up", Category = "Drinks", SmallPrice = 0.99, MediumPrice = 1.29, LargePrice = 1.79 });
            data.Add(new Product() { ItemName = "Ginger Ale", Category = "Drinks", SmallPrice = 0.99, MediumPrice = 1.29, LargePrice = 1.79 });

            //Burgers
            data.Add(new Product() { ItemName = "Burger", Category = "Burgers", SmallPrice = 3.99, MediumPrice = 4.49, LargePrice = 4.79 });
            data.Add(new Product() { ItemName = "Chicken Burger", Category = "Burgers", SmallPrice = 3.99, MediumPrice = 4.49, LargePrice = 4.79 });
            data.Add(new Product() { ItemName = "Veggie Burger", Category = "Burgers", SmallPrice = 4.99, MediumPrice = 5.49, LargePrice = 5.79 });

            return data;
        }

        public List<Order> GetOrders(List<Product> products)
        {
            List<Order> data = new List<Order>();

            Order o1 = new Order()
            {
                OrderDate = DateTime.Now.AddDays(-7),
                Status = "Pending",
                Items = new List<OrderEntry>()
                    {
                        new OrderEntry()
                        {
                            Product = products.Find((p) => p.ItemName == "Diet Coke"),
                            Quantity = 4,
                            Size = "Small"
                        },
                        new OrderEntry()
                        {
                            Product = products.Find((p) => p.ItemName == "Fries"),
                            Quantity = 2,
                            Size = "Large"
                        },
                        new OrderEntry()
                        {
                            Product = products.Find((p) => p.ItemName == "Meat Lovers' Pizza"),
                            Quantity = 1,
                            Size = "Large"
                        }
                    },
                DeliveryAddress = "Order sample address"
            };

            Order o2 = new Order()
            {
                OrderDate = DateTime.Now,
                Status = "Pending",
                Items = new List<OrderEntry>()
                    {
                        new OrderEntry()
                        {
                            Product = products.First(),
                            Quantity = 4,
                            Size = "Small"
                        },
                        new OrderEntry()
                        {
                            Product = products.Last(),
                            Quantity = 5,
                            Size = "Large"
                        }
                    },
                DeliveryAddress = "Another sample address"
            };

            Order o3 = new Order()
            {
                OrderDate = DateTime.Now.AddDays(-1),
                Status = "Pending",
                Items = new List<OrderEntry>()
                    {
                        new OrderEntry()
                        {
                            Product = products.Find((p) => p.ItemName == "Spicy Fries"),
                            Quantity = 1,
                            Size = "Large"
                        },
                        new OrderEntry()
                        {
                            Product = products.Find((p) => p.ItemName == "Burger"),
                            Quantity = 1,
                            Size = "Large"
                        },
                        new OrderEntry()
                        {
                            Product = products.Find((p) => p.ItemName == "Pepsi"),
                            Quantity = 1,
                            Size = "Medium"
                        }
                    },
                DeliveryAddress = "Yet another sample address"
            };

            Order o4 = new Order()
            {
                OrderDate = DateTime.Now.AddMonths(-1).AddDays(-7),
                Status = "Pending",
                Items = new List<OrderEntry>()
                    {
                        new OrderEntry()
                        {
                            Product = products.Find((p) => p.ItemName == "All Dressed Pizza"),
                            Quantity = 1,
                            Size = "Large"
                        },
                        new OrderEntry()
                        {
                            Product = products.Find((p) => p.ItemName == "Coke"),
                            Quantity = 1,
                            Size = "Large"
                        }
                    },
                DeliveryAddress = "I can't believe it's another sample address"
            };

            Order o5 = new Order()
            {
                OrderDate = DateTime.Now.AddYears(-1).AddMonths(-1).AddDays(-7),
                Status = "Pending",
                Items = new List<OrderEntry>()
                    {
                        new OrderEntry()
                        {
                            Product = products.Find((p) => p.ItemName == "Vegetarian Pizza"),
                            Quantity = 1,
                            Size = "Large"
                        },
                        new OrderEntry()
                        {
                            Product = products.Find((p) => p.ItemName == "Fries"),
                            Quantity = 1,
                            Size = "Medium"
                        }
                    },
                DeliveryAddress = "Sample address (again)"
            };

            data.Add(o1);
            data.Add(o2);
            data.Add(o3);
            data.Add(o4);
            data.Add(o5);

            return data;
        }
    }
}
