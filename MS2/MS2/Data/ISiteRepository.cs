﻿using MS2.Data.Entities;
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

        public IEnumerable<Favourite> GetAllFavourites();

        public IEnumerable<Favourite> GetFavsByUserId(string userId);

        public void AddFavorite(string userID, string productID);
        public IEnumerable<Favourite> DidUserFavorite(string id, string productID);
        void RemoveFav(Favourite fav);
    }
}