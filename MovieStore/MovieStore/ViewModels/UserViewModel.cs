using System.ComponentModel.DataAnnotations;

namespace MovieStore.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "The {0} can only be {1} characters long at max")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "{0} must contain a capital letter")]
        [StringLength(30, ErrorMessage = "{0}'s can only be {1} characters long at max")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "{0} must contain a capital letter")]
        [StringLength(30, ErrorMessage = "{0}'s can only be {1} characters long at max")]
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
