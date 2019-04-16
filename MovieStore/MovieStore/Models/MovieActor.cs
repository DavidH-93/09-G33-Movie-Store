using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class MovieActor
    {
        public Guid MovieActorID { get; set; }
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }

    }
}
