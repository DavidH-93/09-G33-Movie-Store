using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class Country
    {
        public int CountryID { get; set; }
        public string Name { get; set; }
        public City Capital { get; set; }
        public List<Region> Regions { get; set; }

    }
}
