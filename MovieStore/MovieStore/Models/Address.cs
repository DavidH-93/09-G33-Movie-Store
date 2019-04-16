using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class Address
    {
        public Guid AddressID { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public City City { get; set; }
        public Locality Locality { get; set; }
        public PostCode PostCode { get; set; }
        public Region Region { get; set; }
        public Country Country { get; set; }

    }
}
