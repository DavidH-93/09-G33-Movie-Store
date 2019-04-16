using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class Order
    {
        public Guid OrderID { get; set; }
        public User Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public double Total { get; set; }
        public DateTime Creation { get; set; }
        public DateTime Closed { get; set; }
        public int Status { get; set; }


    }
}
