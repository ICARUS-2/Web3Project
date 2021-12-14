using MS2.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MS2.Data
{
    public interface ISiteRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetOrdersByUserId(string uId);
        IEnumerable<Order> GetOrdersByStatus(string status);
        IEnumerable<Order> GetOrdersByDriverId(string driverId);
        Order GetOrderByOrderNumber(int orderNum);
        bool SaveAll();
        public IEnumerable<JobPosting> GetAllJobPostings();
        public void InsertOrder(Order order);
        public void InsertOrderEntry(OrderEntry orderEntry);

        public IEnumerable<Favourite> GetAllFavourites();

        public IEnumerable<Favourite> GetFavsByUserId(string userId);

        public void AddFavorite(string userID, string productID);
        public IEnumerable<Favourite> DidUserFavorite(string id, string productID);
        void RemoveFav(Favourite fav);
        public IEnumerable<Order> GetOrdersByDate(DateTime day);
        public IEnumerable<Order> GetOrdersByDateRange(DateTime start, DateTime end);
        public Dictionary<string, List<Order>> GetOrdersGroupedByYear();
        public Dictionary<string, List<Order>> GetOrdersGroupedByMonth();
        public Dictionary<string, List<Order>> GetOrdersGroupedByWeek();
        public Dictionary<string, List<Order>> GetOrdersGroupedByDay();
    }
}