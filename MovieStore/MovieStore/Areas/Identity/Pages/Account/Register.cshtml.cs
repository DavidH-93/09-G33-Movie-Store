using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MovieStore.Data;
using MovieStore.Models;
using MovieStore.Services;
namespace MovieStore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly MovieStoreDbContext _context;

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IAccessLogRepository _accessRepo;
        private readonly IUserRepository _userRepository;

        public RegisterModel(
            MovieStoreDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IAccessLogRepository accessRepo,
            IUserRepository userRepository)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _accessRepo = accessRepo;
            _userRepository = userRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
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

            [Required]
            [Phone]
            [Display(Name = "Phone")]
            public string PhoneNumber { get; set; }

            [BindProperty, Required]
            public string Position { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Confirm password")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

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

            [Display(Name = "Staff Key")]
            public string key { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
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

                Address address = new Address
                {
                    Line1 = Input.Line1,
                    Line2 = Input.Line2,
                    LocalityID = locality.LocalityID,
                    CityID = city.CityID,
                    PostCodeID = postcode.PostCodeID,
                    RegionID = region.RegionID,
                    CountryID = country.CountryID
                };
                _context.Address.Add(address);
                _context.SaveChanges();

                var user = new User
                {
                    UserName = Input.UserName,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Email = Input.Email,
                    PhoneNumber = Input.PhoneNumber,
                    AddressID = address.AddressID
                };

                //var user = new User { UserName = Input.UserName, Email = Input.Email, FirstName = Input.FirstName, LastName = Input.LastName, PhoneNumber = Input.PhoneNumber, LockoutEnabled = false};
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    user = await _userManager.FindByEmailAsync(Input.Email);

                    Dictionary<string, string> keys = new Dictionary<string, string>();

                    keys.Add("xzo67", "Sales Clerk");
                    keys.Add("pdtv&", "Warehouse");
                    keys.Add("s3m6b", "Manager");
                    keys.Add("a!6tk", "Accountant");


                    foreach (KeyValuePair<string, string> key in keys)
                    {
                        if (Input.key == key.Key)
                        {
                            user.Position = key.Value;
                            await _userManager.AddToRoleAsync(user, "Staff");
                        }
                    }

                    if (user.Position == null)
                        user.Position = Input.Position;
                        await _userManager.AddToRoleAsync(user, "Customer");

                    user.LockoutEnabled = false;
                    await _userManager.UpdateAsync(user);

                    AccessLog log = new AccessLog()
                    {
                        UserID = user.Id,
                        AccessLogID = new Guid(),
                        AccessType = "Registered",
                        LogTime = DateTime.Now
                    };

                    _accessRepo.Create(log);
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
