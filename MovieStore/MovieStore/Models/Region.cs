using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class Region
    {
        public Guid RegionID { get; set; }
        public string Name { get; set; }
        public City Capital { get; set; }
        public List<Locality> Localities { get; set; }


    }
}
