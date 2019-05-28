using System;
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

        public async Task<IActionResult> Index(string EmailSearchString, string PhoneSearchString) {
            var users = from m in _context.User
                         select m;

            if(!String.IsNullOrEmpty(EmailSearchString)) {
                users = users.Where(s => s.Email.Contains(EmailSearchString));
            }

            if (!String.IsNullOrEmpty(PhoneSearchString))
            {
                users = users.Where(s => s.PhoneNumber.Contains(PhoneSearchString));
            }

            return View(await users.Select(user => new UserViewModel {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Address = new AddressViewModel {
                    City = new CityViewModel {
                        Name = _context.City.FirstOrDefault(c => c.CityID == (_context.Address.FirstOrDefault(a => a.AddressID == user.AddressID).CityID)).Name
                    },
                    Region = new RegionViewModel {
                        Name = _context.Region.FirstOrDefault(r => r.RegionID == (_context.Address.FirstOrDefault(a => a.AddressID == user.AddressID).RegionID)).Name
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

            Address address = _context.Address.FirstOrDefault(l => l.AddressID == user.AddressID);
            Locality locality = _context.Locality.FirstOrDefault(l => l.LocalityID == address.LocalityID);
            City city = _context.City.FirstOrDefault(l => l.CityID == address.CityID);
            PostCode postcode = _context.PostCode.FirstOrDefault(l => l.PostCodeID == address.PostCodeID);
            Region region = _context.Region.FirstOrDefault(l => l.RegionID == address.RegionID);
            Country country = _context.Country.FirstOrDefault(l => l.CountryID == address.CountryID);

            return View(new UserViewModel {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = new AddressViewModel {
                    Line1 = address.Line1,
                    Line2 = address.Line2,
                    Locality = new LocalityViewModel {
                        Name = locality.Name
                    },
                    City = new CityViewModel {
                        Name = city.Name
                    },
                    PostCode = new PostCodeViewModel {
                        Code = postcode.Code
                    },
                    Region = new RegionViewModel {
                        Name = region.Name
                    },
                    Country = new CountryViewModel {
                        Name = country.Name
                    }
                }
            });
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("UserName, FirstName, LastName, Email, Password, PhoneNumber, Address")] UserViewModel userViewModel) {
            if (ModelState.IsValid) {
                Locality locality = _context.Locality.FirstOrDefault(l => l.Name == userViewModel.Address.Locality.Name);
                City city = _context.City.FirstOrDefault(c => c.Name == userViewModel.Address.City.Name);
                PostCode postcode = _context.PostCode.FirstOrDefault(p => p.Code == userViewModel.Address.PostCode.Code);
                Region region = _context.Region.FirstOrDefault(r => r.Name == userViewModel.Address.Region.Name);
                Country country = _context.Country.FirstOrDefault(c => c.Name == userViewModel.Address.Country.Name);

                if (locality == null) {
                    locality = new Locality {
                        Name = userViewModel.Address.Locality.Name
                    };

                    _context.Locality.Add(locality);
                    _context.SaveChanges();
                }

                if (city == null) {
                    city = new City {
                        Name = userViewModel.Address.City.Name
                    };

                    _context.City.Add(city);
                    _context.SaveChanges();
                }

                if (postcode == null) {
                    postcode = new PostCode {
                        Code = userViewModel.Address.PostCode.Code
                    };

                    _context.PostCode.Add(postcode);
                    _context.SaveChanges();
                }

                if (region == null) {
                    region = new Region {
                        Name = userViewModel.Address.Region.Name
                    };

                    _context.Region.Add(region);
                    _context.SaveChanges();
                }

                if (country == null) {
                    country = new Country {
                        Name = userViewModel.Address.Country.Name
                    };

                    _context.Country.Add(country);
                    _context.SaveChanges();
                }

                Address address = new Address {
                    Line1 = userViewModel.Address.Line1,
                    Line2 = userViewModel.Address.Line2,
                    LocalityID = locality.LocalityID,
                    CityID = city.CityID,
                    PostCodeID = postcode.PostCodeID,
                    RegionID = region.RegionID,
                    CountryID = country.CountryID
                };
                _context.Address.Add(address);
                _context.SaveChanges();

                await _userManager.CreateAsync(new User {
                    UserName = userViewModel.UserName,
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName,
                    Email = userViewModel.Email,
                    PhoneNumber = userViewModel.PhoneNumber,
                    AddressID = address.AddressID
                }, userViewModel.Password);

                return RedirectToAction(nameof(Index));
            }

            return View(userViewModel);
        }

        public async Task<IActionResult> Edit(string id) {
            if (id == null) {
                return NotFound();
            }

            var user = await _userManager.FindByEmailAsync(id);
            if (user == null) {
                return NotFound();
            }

            Address address = _context.Address.FirstOrDefault(l => l.AddressID == user.AddressID);
            Locality locality = _context.Locality.FirstOrDefault(l => l.LocalityID == address.LocalityID);
            City city = _context.City.FirstOrDefault(l => l.CityID == address.CityID);
            PostCode postcode = _context.PostCode.FirstOrDefault(l => l.PostCodeID == address.PostCodeID);
            Region region = _context.Region.FirstOrDefault(l => l.RegionID == address.RegionID);
            Country country = _context.Country.FirstOrDefault(l => l.CountryID == address.CountryID);

            return View(new UserEditViewModel {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = new AddressViewModel {
                    Line1 = address.Line1,
                    Line2 = address.Line2,
                    Locality = new LocalityViewModel {
                        Name = locality.Name
                    },
                    City = new CityViewModel {
                        Name = city.Name
                    },
                    PostCode = new PostCodeViewModel {
                        Code = postcode.Code
                    },
                    Region = new RegionViewModel {
                        Name = region.Name
                    },
                    Country = new CountryViewModel {
                        Name = country.Name
                    }
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("UserName, FirstName, LastName, Email, PhoneNumber, Address")] UserEditViewModel userViewModel) {
            if (id != userViewModel.Email) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                var user = await _userManager.FindByEmailAsync(id);

                user.UserName = userViewModel.UserName;
                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;

                Address address = _context.Address.FirstOrDefault(l => l.AddressID == user.AddressID);
                Locality locality = _context.Locality.FirstOrDefault(l => l.Name == userViewModel.Address.Locality.Name);
                City city = _context.City.FirstOrDefault(c => c.Name == userViewModel.Address.City.Name);
                PostCode postcode = _context.PostCode.FirstOrDefault(c => c.Code == userViewModel.Address.PostCode.Code);
                Region region = _context.Region.FirstOrDefault(c => c.Name == userViewModel.Address.Region.Name);
                Country country = _context.Country.FirstOrDefault(c => c.Name == userViewModel.Address.Country.Name);

                if (locality == null) {
                    locality = new Locality {
                        Name = userViewModel.Address.Locality.Name
                    };

                    _context.Locality.Add(locality);
                    _context.SaveChanges();
                }

                if (city == null) {
                    city = new City {
                        Name = userViewModel.Address.City.Name
                    };

                    _context.City.Add(city);
                    _context.SaveChanges();
                }

                if (postcode == null) {
                    postcode = new PostCode {
                        Code = userViewModel.Address.PostCode.Code
                    };

                    _context.PostCode.Add(postcode);
                    _context.SaveChanges();
                }

                if (region == null) {
                    region = new Region {
                        Name = userViewModel.Address.Region.Name
                    };

                    _context.Region.Add(region);
                    _context.SaveChanges();
                }

                if (country == null) {
                    country = new Country {
                        Name = userViewModel.Address.Country.Name
                    };

                    _context.Country.Add(country);
                    _context.SaveChanges();
                }

                address.Line1 = userViewModel.Address.Line1;
                address.Line2 = userViewModel.Address.Line2;
                address.LocalityID = locality.LocalityID;
                address.CityID = city.CityID;
                address.PostCodeID = postcode.PostCodeID;
                address.RegionID = region.RegionID;
                address.CountryID = country.CountryID;

                await _userManager.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }

            return View(userViewModel);
        }

        public async Task<IActionResult> Delete(string id) {
            if (id == null) {
                return NotFound();
            }

            var user = await _userManager.FindByEmailAsync(id);
            if (user == null) {
                return NotFound();
            }

            return View(new UserViewModel {
                Email = user.Email
            });
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id) {
            var user = await _userManager.FindByEmailAsync(id);
            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }

        private bool UserViewModelExists(string id) {
            return _context.Users.Any(e => e.Email == id);
        }
    }
}
