using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS2.Data.Entities
{
    public class Order
    {
        [Key]
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderEntry> Items { get; set; }
        public string Status { get; set; }
        public string DeliveryAddress { get; set; }
        public string UserId { get; set; } 
        public string DeliveryDriverId { get; set; }
        public string OrderType{ get; set; }
        public string CardInfo { get; set; }
        public string PaymentType { get; set; }

        [NotMapped]
        public double Price
        {
            get
            {
                double acc = 0;

                Items.ForEach(i =>
                {
                    acc += i.EntryPrice;
                });

                return acc;
            }
        }

        public Order()
        {
            Items = new List<OrderEntry>();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder($"Order #: {OrderNumber} \nDate: {OrderDate} \nUserId: {UserId} \nOrder Items:\n");
            foreach(OrderEntry item in Items)
            {
                builder.Append($"\t{item.Product.ItemName} ${item.EntryPrice} x{item.Quantity}\n");
            }
            builder.Append($"Total: ${Price}");
            builder.Append("\nThank you for ordering from us!");
            return builder.ToString();
        }

        public string ToHtmlText()
        {
            StringBuilder builder = new StringBuilder($"<h1>Order #: {OrderNumber}</h1> \n<h3>Date: {OrderDate}</h3> <h3>UserId: \n{UserId}</h3> \n<h3>Order Items:</h3>\n<ul>\n");
            foreach (OrderEntry item in Items)
            {
                builder.Append($"<li>{item.Product.ItemName} ${item.EntryPrice} x{item.Quantity}</li>\n");
            }
            builder.Append($"</ul><h3>Total: ${Price}</h3>");
            builder.Append("\n<p>Thank you for ordering from us!</p>");
            return builder.ToString();
        }
    }

}
