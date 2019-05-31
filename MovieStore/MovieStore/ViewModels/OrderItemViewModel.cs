using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieStore.Models;
namespace MovieStore.ViewModels
{
    public class OrderItemViewModel
    {
        public Guid OrderItemID { get; set; }
        public string UserID { get; set; }
        public Guid OrderID { get; set; }
        public Guid MovieID { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }

        public OrderItemViewModel(OrderItem i)
        {
            OrderItemID = i.OrderItemID;
            UserID = i.UserID;
            OrderID = i.OrderID;
            MovieID = i.MovieID;
            Title = i.Title;
            Quantity = i.Quantity;
            Price = i.Price;
            Total = i.Total;
        }
    }
}
