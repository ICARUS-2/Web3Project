using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MS2.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS2.Data
{
    public class SiteRepository : ISiteRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SiteRepository> _logger;

        public SiteRepository(ApplicationDbContext ctx, ILogger<SiteRepository> logger)
        {
            _context = ctx;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Get all products was called");

                return _context.Products.ToList();
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to get product list");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products.Where(p => p.Category == category).ToList();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
        // this is a temporary implementation for testing
        public IEnumerable<OrderEntry> GetShoppingCartItems()
        {
            List<OrderEntry> orderEntries = new List<OrderEntry>()
            {
                new OrderEntry()
                {
                    Product = GetAllProducts().ToList()[1],
                    Quantity = 1,
                    Size = "Large"
                },
                new OrderEntry()
                {
                    Product = GetAllProducts().ToList()[5],
                    Quantity = 2,
                    Size = "Small"
                },
                new OrderEntry()
                {
                    Product = GetAllProducts().ToList()[14],
                    Quantity = 1,
                    Size = "Large"
                },
            };

            return orderEntries;
        }

        public IEnumerable<JobPosting> GetAllJobPostings()
        {
            return _context.JobPostings;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.Include(o => o.Items)
                .ThenInclude(oi => oi.Product);
        }

        public IEnumerable<Favourite> GetAllFavourites()
        {
            return _context.Favourites;
        }

        public IEnumerable<Favourite> GetFavsById(string userId)
        {
            return _context.Favourites.Where(f => f.UserId == userId);
        }

        public void AddFavorite(string userID, string productID)
        {
            Favourite fav = new Favourite(userID, productID);
            _context.Favourites.Add(fav);
            _context.SaveChanges();
        }
    }
}
