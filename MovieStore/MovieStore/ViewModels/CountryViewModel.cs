using MovieStore.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.ViewModels
{
    public class CountryViewModel
    {
        [Required]
        [Display(Name = "Country")]
        public string Name { get; set; }
    }
}
