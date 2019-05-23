using MovieStore.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.ViewModels
{
    public class AddressViewModel
    {
        [Required]
        [Display(Name = "Address Line 1")]
        public string Line1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string Line2 { get; set; }

        [Required]
        public CityViewModel City { get; set; }

        [Required]
        public LocalityViewModel Locality { get; set; }

        [Required]
        public PostCodeViewModel PostCode { get; set; }

        [Required]
        public RegionViewModel Region { get; set; }

        [Required]
        public CountryViewModel Country { get; set; }
    }
}
