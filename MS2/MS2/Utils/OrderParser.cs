using MS2.Data;
using MS2.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MS2.Utils
{
    public class OrderParser
    {
        public string Address { get; set; }
        public List<int> ItemQuantity { get; set; }
        public List<string> ItemSize { get; set; }
        public List<string> OrderItems { get; set; }


        public Order ParseOrder(ISiteRepository repository)
        {
            List<Product> products = repository.GetAllProducts().ToList();
            List<int> orderItemIds = this.OrderItems.Select(int.Parse).ToList();

            Order order = new Order();
            order.Items = new List<OrderEntry>();

            for(int i = 0; i< this.OrderItems.Count; i++)
            {
                Product newProduct = new Product();
                OrderEntry newEntry = new OrderEntry();

                newProduct = products.Where(p => p.Id == orderItemIds[i]).Select(p => p).FirstOrDefault() as Product;

                newEntry.Product = newProduct;
                newEntry.Quantity = this.ItemQuantity[i];
                newEntry.Size = this.ItemSize[i];

                order.Items.Add(newEntry);
            }
            order.DeliveryAddress = this.Address;
            order.OrderDate = DateTime.Now;
            order.Status = OrderStatus.Ordered.ToString();

            return order;
        }
    }
}
