using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using MovieStore.Data;
using MovieStore.Models;
using MovieStore.ViewModels;

namespace MovieStore.Controllers {
    [Authorize(Roles="Staff")]
    public class StaffController : Controller {

        private readonly MovieStoreDbContext _context;

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StaffController(MovieStoreDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager) {
            _context = context;

            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string nameSearchString, string phoneSearchString, string positionSearchString) {
            var users = from a in _context.Roles
                        join h in _context.UserRoles on a.Id equals h.RoleId
                        join c in _context.User on h.UserId equals c.Id
                        into temp
                        from m in temp
                        where a.Name == "Staff"
                        select m;

            if(!String.IsNullOrEmpty(nameSearchString)) {
                if (nameSearchString.Trim().Contains(' ')) { 
                    String[] names = nameSearchString.Split(' ');
                    users = users.Where(s => s.FirstName.Contains(names[0]) && s.LastName.Contains(names[1]));
                }
                else {
                    users = users.Where(s => s.FirstName.Contains(nameSearchString) || s.LastName.Contains(nameSearchString));
                }
            }

            if (!String.IsNullOrEmpty(phoneSearchString)) {
                users = users.Where(s => s.PhoneNumber.Contains(phoneSearchString.Trim()));
            }

            //if (!String.IsNullOrEmpty(PositionSearchString)) {
            //    users = users.Where(s => s.position.Contains(positionSearchString.Trim()));
            //}

            //return View(await users.Select(user => new StaffEditViewModel {
            //    Email = user.Email,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    StaffID = user.StaffID,
            //    PhoneNumber = user.PhoneNumber,
            //    Position = user.Position,
            //    Address = new AddressViewModel {
            //        City = new CityViewModel {
            //            Name = _context.City.FirstOrDefault(c => c.CityID == (_context.Address.FirstOrDefault(a => a.AddressID == user.AddressID).CityID)).Name
            //        },
            //        Region = new RegionViewModel {
            //            Name = _context.Region.FirstOrDefault(r => r.RegionID == (_context.Address.FirstOrDefault(a => a.AddressID == user.AddressID).RegionID)).Name
            //        }
            //    },
            //    LockoutEnabled = user.LockoutEnabled
            //}).ToListAsync());

            // Remove this once the above actually works
            return View();
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

            //return View(new StaffViewModel {
            //    UserName = user.UserName,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    StaffID = user.StaffID,
            //    Email = user.Email,
            //    PhoneNumber = user.PhoneNumber,
            //    Position = user.Position,
            //    Address = new AddressViewModel {
            //        Line1 = address.Line1,
            //        Line2 = address.Line2,
            //        Locality = new LocalityViewModel {
            //            Name = locality.Name
            //        },
            //        City = new CityViewModel {
            //            Name = city.Name
            //        },
            //        PostCode = new PostCodeViewModel {
            //            Code = postcode.Code
            //        },
            //        Region = new RegionViewModel {
            //            Name = region.Name
            //        },
            //        Country = new CountryViewModel {
            //            Name = country.Name
            //        }
            //    }
            //});

            // Remove this once the above actually works
            return View();
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("UserName, FirstName, LastName, Email, Password, PhoneNumber, Position, Address")] StaffViewModel StaffViewModel) {
            if (ModelState.IsValid) {
                Locality locality = _context.Locality.FirstOrDefault(l => l.Name == StaffViewModel.Address.Locality.Name);
                City city = _context.City.FirstOrDefault(c => c.Name == StaffViewModel.Address.City.Name);
                PostCode postcode = _context.PostCode.FirstOrDefault(p => p.Code == StaffViewModel.Address.PostCode.Code);
                Region region = _context.Region.FirstOrDefault(r => r.Name == StaffViewModel.Address.Region.Name);
                Country country = _context.Country.FirstOrDefault(c => c.Name == StaffViewModel.Address.Country.Name);

                if (locality == null) {
                    locality = new Locality {
                        Name = StaffViewModel.Address.Locality.Name
                    };

                    _context.Locality.Add(locality);
                    _context.SaveChanges();
                }

                if (city == null) {
                    city = new City {
                        Name = StaffViewModel.Address.City.Name
                    };

                    _context.City.Add(city);
                    _context.SaveChanges();
                }

                if (postcode == null) {
                    postcode = new PostCode {
                        Code = StaffViewModel.Address.PostCode.Code
                    };

                    _context.PostCode.Add(postcode);
                    _context.SaveChanges();
                }

                if (region == null) {
                    region = new Region {
                        Name = StaffViewModel.Address.Region.Name
                    };

                    _context.Region.Add(region);
                    _context.SaveChanges();
                }

                if (country == null) {
                    country = new Country {
                        Name = StaffViewModel.Address.Country.Name
                    };

                    _context.Country.Add(country);
                    _context.SaveChanges();
                }

                Address address = new Address {
                    Line1 = StaffViewModel.Address.Line1,
                    Line2 = StaffViewModel.Address.Line2,
                    LocalityID = locality.LocalityID,
                    CityID = city.CityID,
                    PostCodeID = postcode.PostCodeID,
                    RegionID = region.RegionID,
                    CountryID = country.CountryID
                };
                _context.Address.Add(address);
                _context.SaveChanges();

                //await _userManager.CreateAsync(new User {
                //    UserName = StaffViewModel.UserName,
                //    FirstName = StaffViewModel.FirstName,
                //    LastName = StaffViewModel.LastName,
                //    StaffID = StaffViewModel.StaffID,
                //    Email = StaffViewModel.Email,
                //    PhoneNumber = StaffViewModel.PhoneNumber,
                //    Position = StaffViewModel.Position,
                //    AddressID = address.AddressID,
                //}, StaffViewModel.Password);

                //User user = await _userManager.FindByEmailAsync(StaffViewModel.Email);
                //user.LockoutEnabled = false;
                //await _userManager.UpdateAsync(user);

                return RedirectToAction(nameof(Index));
            }

            return View(StaffViewModel);
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

            //return View(new StaffEditViewModel {
            //    UserName = user.UserName,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    Email = user.Email,
            //    PhoneNumber = user.PhoneNumber,
            //    Position = user.Position,
            //    Address = new AddressViewModel {
            //        Line1 = address.Line1,
            //        Line2 = address.Line2,
            //        Locality = new LocalityViewModel {
            //            Name = locality.Name
            //        },
            //        City = new CityViewModel {
            //            Name = city.Name
            //        },
            //        PostCode = new PostCodeViewModel {
            //            Code = postcode.Code
            //        },
            //        Region = new RegionViewModel {
            //            Name = region.Name
            //        },
            //        Country = new CountryViewModel {
            //            Name = country.Name
            //        }
            //    },
            //    LockoutEnabled = user.LockoutEnabled
            //});

            // Remove this once the above actually works
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(string id, [Bind("UserName, FirstName, LastName, StaffID, Email, PhoneNumber, Position, Address, LockoutEnabled")] StaffEditViewModel StaffViewModel) {
        //    if (id != StaffViewModel.Email) {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid) {
        //        var user = await _userManager.FindByEmailAsync(id);

        //        user.UserName = StaffViewModel.UserName;
        //        user.FirstName = StaffViewModel.FirstName;
        //        user.LastName = StaffViewModel.LastName;
        //        //user.StaffID = StaffViewModel.StaffID;
        //        user.Email = StaffViewModel.Email;
        //        user.PhoneNumber = StaffViewModel.PhoneNumber;
        //        user.Position = StaffViewModel.Position;

        //        Address address = _context.Address.FirstOrDefault(l => l.AddressID == user.AddressID);

        //        Locality locality = _context.Locality.FirstOrDefault(l => l.Name == StaffViewModel.Address.Locality.Name);
        //        City city = _context.City.FirstOrDefault(c => c.Name == StaffViewModel.Address.City.Name);
        //        PostCode postcode = _context.PostCode.FirstOrDefault(c => c.Code == StaffViewModel.Address.PostCode.Code);
        //        Region region = _context.Region.FirstOrDefault(c => c.Name == StaffViewModel.Address.Region.Name);
        //        Country country = _context.Country.FirstOrDefault(c => c.Name == StaffViewModel.Address.Country.Name);

        //        if (locality == null) {
        //            locality = new Locality {
        //                Name = StaffViewModel.Address.Locality.Name
        //            };

        //            _context.Locality.Add(locality);
        //            _context.SaveChanges();
        //        }

        //        if (city == null) {
        //            city = new City {
        //                Name = StaffViewModel.Address.City.Name
        //            };

        //            _context.City.Add(city);
        //            _context.SaveChanges();
        //        }

        //        if (postcode == null) {
        //            postcode = new PostCode {
        //                Code = StaffViewModel.Address.PostCode.Code
        //            };

        //            _context.PostCode.Add(postcode);
        //            _context.SaveChanges();
        //        }

        //        if (region == null) {
        //            region = new Region {
        //                Name = StaffViewModel.Address.Region.Name
        //            };

        //            _context.Region.Add(region);
        //            _context.SaveChanges();
        //        }

        //        if (country == null) {
        //            country = new Country {
        //                Name = StaffViewModel.Address.Country.Name
        //            };

        //            _context.Country.Add(country);
        //            _context.SaveChanges();
        //        }

        //        address.Line1 = StaffViewModel.Address.Line1;
        //        address.Line2 = StaffViewModel.Address.Line2;
        //        address.LocalityID = locality.LocalityID;
        //        address.CityID = city.CityID;
        //        address.PostCodeID = postcode.PostCodeID;
        //        address.RegionID = region.RegionID;
        //        address.CountryID = country.CountryID;

        //        if (StaffViewModel.LockoutEnabled) {
        //            user.LockoutEnabled = true;
        //            user.LockoutEnd = DateTime.Today.AddYears(100);
        //        }
        //        else {
        //            user.LockoutEnabled = false;
        //            user.LockoutEnd = DateTime.Now;
        //        }

        //        await _userManager.UpdateAsync(user);
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(StaffViewModel);
        //}

        // Remove this once the above actually works
        [HttpPost]
        public async Task<IActionResult> Edit() {
            return View();
        }

        public async Task<IActionResult> Delete(string id) {
            if (id == null) {
                return NotFound();
            }

            var user = await _userManager.FindByEmailAsync(id);
            if (user == null) {
                return NotFound();
            }

            return View(new StaffViewModel {
                Email = user.Email
            });
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id) {
            var user = await _userManager.FindByEmailAsync(id);
            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }

        private bool StaffViewModelExists(string id) {
            return _context.Users.Any(e => e.Email == id);
        }
    }
}
