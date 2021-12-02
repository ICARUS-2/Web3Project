using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS2.Data.Entities
{
    public class Favourite
    {
        public Favourite() { }
        public Favourite(string userID, string productID)
        {
            UserId = userID;
            ProductId = productID;
        }

        public int Id { get; set; }
        public string ProductId { get; set; }
        public string UserId { get; set; }
    }
}
