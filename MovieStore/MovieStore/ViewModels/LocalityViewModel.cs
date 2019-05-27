using MovieStore.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.ViewModels
{
    public class LocalityViewModel
    {
        [Required]
        [Display(Name = "Locality")]
        public string Name { get; set; }
    }
}
