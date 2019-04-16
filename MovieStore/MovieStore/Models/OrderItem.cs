using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class OrderItem
    {
        public Guid OrderItemID { get; set; }
        public Movie Movie { get; set; }
        public int Quantity { get; set; }

    }
}
