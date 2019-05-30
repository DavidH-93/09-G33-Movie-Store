using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class OrderItem
    {
        public Guid OrderItemID { get; set; }
        public string UserID { get; set; }
        public Guid OrderID { get; set; }
        public Guid MovieID { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }

        public OrderItem(string userID, Guid orderID, Guid movieID, string title, int quantity, double price, double total)
        {
            OrderItemID = Guid.NewGuid();
            UserID = userID;
            OrderID = orderID;
            MovieID = movieID;
            Title = title;
            Quantity = quantity;
            Price = price;
            Total = price;
        }

        public void setTotal()
        {
            Total = Quantity * Price;
        }

    }
}
