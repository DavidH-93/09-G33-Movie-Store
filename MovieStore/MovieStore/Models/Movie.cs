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
        public List<MovieGenre> Genres { get; set; }
        public List<MovieActor> Actors { get; set; }
        public List<MovieDirector> Directors { get; set; }
        public List<MovieProducer> Producers { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool Published { get; set; }



    }
}
