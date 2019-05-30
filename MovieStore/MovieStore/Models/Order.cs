using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class Order
    {
        public Guid OrderID { get; set; }
        public string UserID { get; set; }
        public double Total { get; set; }
        public DateTime Creation { get; set; }
        public bool Closed { get; set; }
        public string Status { get; set; }

        public double CalculateTotal(IEnumerable<OrderItem> l)
        {
            double t = 0;
            foreach (var i in l)
            {
                t += i.Amount;
            }
            return t;
        }
    }
}
