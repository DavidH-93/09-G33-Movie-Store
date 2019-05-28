using System;

namespace MovieStore.Models
{
    public class Address
    {
        public Guid AddressID { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public Guid CityID { get; set; }
        public Guid LocalityID { get; set; }
        public Guid PostCodeID { get; set; }
        public Guid RegionID { get; set; }
        public Guid CountryID { get; set; }

    }
}
