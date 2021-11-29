using MS2.Data.Entities;
using System.Collections.Generic;

namespace MS2.Data
{
    public interface ISiteRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        IEnumerable<Order> GetAllOrders();
        bool SaveAll();
        public IEnumerable<OrderEntry> GetShoppingCartItems();
        public IEnumerable<JobPosting> GetAllJobPostings();
    }
}