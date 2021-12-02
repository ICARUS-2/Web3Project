using MS2.Data.Entities;
using System.Collections.Generic;

namespace MS2.Data
{
    public interface ISiteRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetOrdersByUserId(string uId);
        bool SaveAll();
        public IEnumerable<JobPosting> GetAllJobPostings();
        public void InsertOrder(Order order);
        public void InsertOrderEntry(OrderEntry orderEntry);

        public IEnumerable<Favourite> GetAllFavourites();

        public IEnumerable<Favourite> GetFavsByUserId(string userId);

        public void AddFavorite(string userID, string productID);
        public IEnumerable<Favourite> DidUserFavorite(string id, string productID);
        void RemoveFav(Favourite fav);
    }
}