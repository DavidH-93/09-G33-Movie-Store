using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieStore.Models;
namespace MovieStore.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel(Order o, IEnumerable<OrderItem> iList)
        {
            OrderID = o.OrderID;
            UserID = o.UserID;
            Total = o.Total;
            Creation = o.CreationToString();
            Status = o.Status;
            Closed = Closed;
            ClosedString = o.ClosedToString();
            NumItems = o.NumItems;
            OrderItems = new List<OrderItemViewModel>();
            foreach (OrderItem i in iList)
            {
                OrderItemViewModel ivm = new OrderItemViewModel(i);
                OrderItems.Add(ivm);
            }
        }

        public Guid OrderID { get; set; }
        public string UserID { get; set; }
        public double Total { get; set; }
        public string Creation { get; set; }
        public string Status { get; set; }
        public bool Closed { get; set; }
        public string ClosedString { get; set; }
        public int NumItems { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }




    }
}
