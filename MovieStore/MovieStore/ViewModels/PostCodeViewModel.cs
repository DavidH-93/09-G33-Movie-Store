using MovieStore.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.ViewModels
{
    public class PostCodeViewModel
    {
        [Required(ErrorMessage = "Zip Code is Required")]
        [RegularExpression(@"^\d{4}?$", ErrorMessage = "Invalid Zip (Must be 4 Digits in length.")]
        [Display(Name = "PostCode")]
        public int Code { get; set; }
    }
}
