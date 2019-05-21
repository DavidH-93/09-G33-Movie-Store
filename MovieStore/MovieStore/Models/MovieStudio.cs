using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class MovieStudio
    {
        public Guid MovieStudioID { get; set; }
        public Guid MovieID { get; set; }
        public Guid StudioID { get; set; }

    }
}
