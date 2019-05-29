using System.ComponentModel.DataAnnotations;

namespace MovieStore.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [StringLength(30)]
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

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "User Type")]
        public string Position { get; set; }

        [Required]
        public AddressViewModel Address { get; set; }

        [Required]
        [Display(Name = "Lockout Status")]
        public bool LockoutEnabled { get; set; }
    }
}
