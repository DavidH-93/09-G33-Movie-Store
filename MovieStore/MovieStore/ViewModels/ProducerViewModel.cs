using System;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.ViewModels
{
    public class ProducerViewModel
    {

        public Guid ProducerID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Born")]
        public DateTime DOB { get; set; }
    }
}
