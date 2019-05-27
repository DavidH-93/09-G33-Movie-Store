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
        public int Amount { get; set; }
        public double Price { get; set; }
        public double Total()
        {
            double total = Amount * Price;
            return total;
        }


    }
}
