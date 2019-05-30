using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class MovieProducer
    {
        public Guid MovieProducerID { get; set; }
        public Guid MovieID { get; set; }
        public Guid ProducerID { get; set; }

    }
}
