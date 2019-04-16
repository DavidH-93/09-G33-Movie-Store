using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class MovieProducer
    {
        public Guid MovieProducerID { get; set; }
        public Movie Movie { get; set; }
        public Producer Producer { get; set; }

    }
}
