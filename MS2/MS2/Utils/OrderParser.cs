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
        public bool IsDelivery { get; set; }
        public string CardInfo { get; set; }
        public string DriverId { get; set; }
        public string PaymentType { get; set; }


        public Order ParseOrder(ISiteRepository repository)
        {
            List<Product> products = repository.GetAllProducts().ToList();
            List<int> orderItemIds = this.OrderItems.Select(int.Parse).ToList();

            Order order = new Order();
            order.Items = new List<OrderEntry>();
            string[] orderTypes = { "Pick Up", "Delivery" };

            for(int i = 0; i < this.OrderItems.Count; i++)
            {
                Product newProduct = new Product();
                OrderEntry newEntry = new OrderEntry();

                newProduct = products.Where(p => p.Id == orderItemIds[i]).Select(p => p).FirstOrDefault() as Product;

                newEntry.Product = newProduct;
                newEntry.Quantity = this.ItemQuantity[i];
                newEntry.Size = this.ItemSize[i];

                order.Items.Add(newEntry);
            }

            if(this.CardInfo != string.Empty || this.CardInfo != null)
            {
                this.PaymentType = "Card";
            }

            order.DeliveryAddress = this.IsDelivery ? this.Address : string.Empty;
            order.OrderType = this.IsDelivery ? orderTypes[1] : orderTypes[0];
            order.OrderDate = DateTime.Now;
            order.Status = OrderStatus.Ordered.ToString();
            order.CardInfo = this.CardInfo;
            order.DeliveryDriverId = DriverId;
            order.PaymentType = this.PaymentType;

            return order;
        }
    }
}
