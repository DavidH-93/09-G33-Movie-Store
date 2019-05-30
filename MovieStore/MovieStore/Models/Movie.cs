using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class Movie
    {
        public Guid MovieID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Release { get; set; }
        public int Duration { get; set; }
        public int Rating { get; set; }
        public string Genre { get; set; }
    }
}
