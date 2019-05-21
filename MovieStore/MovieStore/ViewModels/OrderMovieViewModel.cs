using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.ViewModels
{
    public class OrderMovieViewModel
    {
        public Guid MovieID { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
