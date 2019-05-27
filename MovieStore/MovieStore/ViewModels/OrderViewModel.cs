using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MovieStore.ViewModels
{
    public class OrderViewModel
    {
        public Guid OrderID { get; set; }
        public string UserID { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        public DateTime Creation { get; set; }
        public DateTime Closed { get; set; }
        public string Status { get; set; }
        public double Total()
        {
            double t = 0;
            foreach (OrderItemViewModel order in OrderItems)
            {
                t += order.Total;
            }
            return t;
        }

    }
}
