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
        public int NumItems { get; set; }

        public bool Closed { get; set; }
        public string Status { get; set; }
        public Order(string userID, double total)
        {
            OrderID = Guid.NewGuid();
            UserID = userID;
            Total = total;
            NumItems = 1;
            Creation = DateTime.Now;
            Closed = false;
            Status = "Open";
        }

        public string CreationToString()
        {
            return Creation.Day.ToString() + "/" + Creation.Month.ToString() + "/" + Creation.Year.ToString();
        }
        public string ClosedToString()
        {
            if (Closed == false)
            {
                return "Closed";
            }
            else return "Open";
        }
    }
}
