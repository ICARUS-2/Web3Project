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

        public IEnumerable<JobPosting> GetAllJobPostings()
        {
            return _context.JobPostings;
        }

        public void InsertOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public void InsertOrderEntry(OrderEntry orderEntry)
        {
            _context.OrderEntries.Add(orderEntry);
        }
        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.Include(o => o.Items)
                .ThenInclude(oi => oi.Product);
        }

        public IEnumerable<Order> GetOrdersByUserId(string uId)
        {
            return _context.Orders.Where(o => o.UserId == uId)
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product);
        }

        public IEnumerable<Favourite> GetAllFavourites()
        {
            return _context.Favourites;
        }

        public IEnumerable<Favourite> GetFavsByUserId(string userId)
        {
            return _context.Favourites.Where(f => f.UserId == userId);
        }

        public void AddFavorite(string userID, string productID)
        {
            Favourite fav = new Favourite(userID, productID);
            _context.Favourites.Add(fav);
            _context.SaveChanges();
        }

        public IEnumerable<Favourite> DidUserFavorite(string id, string productID)
        {
            return GetAllFavourites().Where(f => f.UserId.ToString() == id && f.ProductId == productID).ToList();
        }

        public void RemoveFav(Favourite fav)
        {
            _context.Favourites.Remove(fav);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetOrdersByStatus(string status)
        {
            return _context.Orders.Where(o => o.Status == status)
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product);
        }

        public IEnumerable<Order> GetOrdersByDriverId(string driverId)
        {
            return _context.Orders.Where(o => o.DeliveryDriverId == driverId)
                 .Include(o => o.Items)
                .ThenInclude(oi => oi.Product); 
        }

        public Order GetOrderByOrderNumber(int orderNum)
        {
            return _context.Orders.Where(o => o.OrderNumber == orderNum)
                .Include(o => o.Items)
               .ThenInclude(oi => oi.Product).FirstOrDefault();
        }

    }
}
