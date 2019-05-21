using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class MovieDirector
    {
        public Guid MovieDirectorID { get; set; }
        public Guid MovieID { get; set; }
        public Guid DirectorID { get; set; }

    }
}
