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
        public double Total { get; set; }
        public DateTime Creation { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        public string Status { get; set; }

    }
}
