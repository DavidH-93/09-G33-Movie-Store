using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieStore.Data;
using MovieStore.Models;
using MovieStore.Services;
namespace MovieStore.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly MovieStoreDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IUserRepository _userRepo;
        private readonly IAccessLogRepository _accessRepo;

        public IndexModel(
            MovieStoreDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            IUserRepository userRepo,
            IAccessLogRepository accessRepo)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _userRepo = userRepo;
            _accessRepo = accessRepo;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [StringLength(30)]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone")]
            public string PhoneNumber { get; set; }
            //user details

            [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
            [StringLength(30)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
            [StringLength(30)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Address Line 1")]
            public string Line1 { get; set; }

            [Display(Name = "Address Line 2")]
            public string Line2 { get; set; }

            [Required]
            [Display(Name = "Locality")]
            public string Locality { get; set; }

            [Required]
            [Display(Name = "City")]
            public string City { get; set; }

            [Required(ErrorMessage = "Zip Code is Required")]
            [RegularExpression(@"^\d{4}?$", ErrorMessage = "Invalid Zip")]
            [Display(Name = "PostCode")]
            public int PostCode { get; set; }

            [Required]
            [Display(Name = "Region")]
            public string Region { get; set; }

            [Required]
            [Display(Name = "Country")]
            public string Country { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            Address address = _context.Address.FirstOrDefault(l => l.AddressID == user.AddressID);
            Locality locality = _context.Locality.FirstOrDefault(l => l.LocalityID == address.LocalityID);
            City city = _context.City.FirstOrDefault(l => l.CityID == address.CityID);
            PostCode postcode = _context.PostCode.FirstOrDefault(l => l.PostCodeID == address.PostCodeID);
            Region region = _context.Region.FirstOrDefault(l => l.RegionID == address.RegionID);
            Country country = _context.Country.FirstOrDefault(l => l.CountryID == address.CountryID);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{user.Id}'.");
            }
            //user viewModel data binding
            Input = new InputModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Line1 = address.Line1,
                Line2 = address.Line2,
                Locality = locality.Name,
                City = city.Name,
                PostCode = postcode.Code,
                Region = region.Name,
                Country = country.Name
            };

            IsEmailConfirmed = user.EmailConfirmed;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{user.Id}'.");
                }

                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.Email = Input.Email;
                user.PhoneNumber = Input.PhoneNumber;

                Address address = _context.Address.FirstOrDefault(l => l.AddressID == user.AddressID);
                Locality locality = _context.Locality.FirstOrDefault(l => l.Name == Input.Locality);
                City city = _context.City.FirstOrDefault(c => c.Name == Input.City);
                PostCode postcode = _context.PostCode.FirstOrDefault(p => p.Code == Input.PostCode);
                Region region = _context.Region.FirstOrDefault(r => r.Name == Input.Region);
                Country country = _context.Country.FirstOrDefault(c => c.Name == Input.Country);

                if (locality == null)
                {
                    locality = new Locality
                    {
                        Name = Input.Locality
                    };

                    _context.Locality.Add(locality);
                    _context.SaveChanges();
                }

                if (city == null)
                {
                    city = new City
                    {
                        Name = Input.City
                    };

                    _context.City.Add(city);
                    _context.SaveChanges();
                }

                if (postcode == null)
                {
                    postcode = new PostCode
                    {
                        Code = Input.PostCode
                    };

                    _context.PostCode.Add(postcode);
                    _context.SaveChanges();
                }

                if (region == null)
                {
                    region = new Region
                    {
                        Name = Input.Region
                    };

                    _context.Region.Add(region);
                    _context.SaveChanges();
                }

                if (country == null)
                {
                    country = new Country
                    {
                        Name = Input.Country
                    };

                    _context.Country.Add(country);
                    _context.SaveChanges();
                }

                address.Line1 = Input.Line1;
                address.Line2 = Input.Line2;
                address.LocalityID = locality.LocalityID;
                address.CityID = city.CityID;
                address.PostCodeID = postcode.PostCodeID;
                address.RegionID = region.RegionID;
                address.CountryID = country.CountryID;

                await _userManager.UpdateAsync(user);
                await _signInManager.RefreshSignInAsync(user);

                StatusMessage = "Your profile has been updated";

                return RedirectToPage();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
