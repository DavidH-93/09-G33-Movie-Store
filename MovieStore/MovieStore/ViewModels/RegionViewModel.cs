using MovieStore.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.ViewModels
{
    public class RegionViewModel
    {
        [Required]
        [Display(Name = "Region")]
        public string Name { get; set; }
    }
}
