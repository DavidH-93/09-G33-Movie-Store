using System.ComponentModel.DataAnnotations;

namespace MovieStore.ViewModels
{
    public class UserEditViewModel
    { 
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(30)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(30)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public AddressViewModel Address { get; set; }

        [Required]
        [Display(Name = "Current Status (Lockout)")]
        public bool LockoutEnabled { get; set; }

        [Required]
        [Display(Name = "User Type")]
        public string Type { get; set; }
    }
}
