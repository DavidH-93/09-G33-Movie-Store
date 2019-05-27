using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class OrderItem
    {
        public Guid OrderItemID { get; set; }
        public Guid MovieID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total()
        {
            double total = Quantity * Price;
            return total;
        }

    }
}
