using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using MovieStore.Data;
using MovieStore.Models;
using MovieStore.ViewModels;

namespace MovieStore.Controllers {
    public class AdminController : Controller {
        private readonly MovieStoreDbContext _context;
        private readonly UserManager<User> _userManager;

        public AdminController(MovieStoreDbContext context, UserManager<User> userManager) {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index() {
            return View(await _context.User.Select(user => new UserViewModel {
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = new AddressViewModel() {
                    City = new CityViewModel() {
                        Name = user.Address.City.Name
                    },
                    Country = new CountryViewModel() {
                        Name = user.Address.Country.Name
                    }
                }
            }).ToListAsync());
        }

        public async Task<IActionResult> Details(string id) {
            if (id == null) {
                return NotFound();
            }

            var user = await _context.User.FirstOrDefaultAsync(m => m.Email == id);
            if (user == null) {
                return NotFound();
            }

            return View(new UserViewModel {FirstName = user.FirstName, LastName = user.LastName, Email = user.Email});
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("UserName, FirstName, LastName, Email, PhoneNumber, Address")] UserViewModel userViewModel) {
            if (ModelState.IsValid) {
                await _userManager.CreateAsync(new User {
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName,
                    Email = userViewModel.Email,
                    UserName = userViewModel.UserName,
                    Address = new Address() {
                        City = new City() {
                            Name = userViewModel.Address.City.Name
                        },
                        Country = new Country() {
                            Name = userViewModel.Address.Country.Name
                        }
                    }
                }, userViewModel.Password);

                return RedirectToAction(nameof(Index));
            }

            return View(userViewModel);
        }

        private bool UserViewModelExists(string id) {
            return _context.Users.Any(e => e.Email == id);
        }
    }
}
