using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class MovieGenre
    {
        public Guid MovieGenreID { get; set; }
        public Guid MovieID { get; set; }
        public Guid GenreID { get; set; }

    }
}
