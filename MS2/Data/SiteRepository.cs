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
        private readonly SiteContext _context;
        private readonly ILogger<SiteRepository> _logger;

        public SiteRepository(SiteContext ctx, ILogger<SiteRepository> logger)
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
    }
}
