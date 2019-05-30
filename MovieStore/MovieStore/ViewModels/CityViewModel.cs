using MovieStore.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.ViewModels
{
    public class CityViewModel
    {
        [Required]
        [Display(Name = "City")]
        public string Name { get; set; }
    }
}
