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

namespace MovieStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StaffAdminController : Controller
    {

        private readonly MovieStoreDbContext _context;

        private readonly UserManager<Staff> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StaffAdminController(MovieStoreDbContext context, UserManager<Staff> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;

            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string nameSearchString, string phoneSearchString, string typeSearchString)
        {
            var staffs = from a in _context.Roles
                        join h in _context.UserRoles on a.Id equals h.RoleId
                        join c in _context.User on h.UserId equals c.Id
                        into temp
                        from m in temp
                        where a.Name == "Staff"
                        select m;

            if (!String.IsNullOrEmpty(nameSearchString))
            {
                if (nameSearchString.Trim().Contains(' '))
                {
                    String[] names = nameSearchString.Split(' ');
                    staffs = staffs.Where(s => s.FirstName.Contains(names[0]) && s.LastName.Contains(names[1]));
                }
                else
                {
                    staffs = staffs.Where(s => s.FirstName.Contains(nameSearchString) || s.LastName.Contains(nameSearchString));
                }
            }

            if (!String.IsNullOrEmpty(phoneSearchString))
            {
                staffs = staffs.Where(s => s.PhoneNumber.Contains(phoneSearchString.Trim()));
            }

            if (!String.IsNullOrEmpty(typeSearchString))
            {
                staffs = staffs.Where(s => s.Position.Contains(typeSearchString.Trim()));
            }

            return View(await staffs.Select(staff => new StaffEditViewModel
            {
                Email = staff.Email,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                PhoneNumber = staff.PhoneNumber,
                Position = staff.Position,
                Address = new AddressViewModel
                {
                    City = new CityViewModel
                    {
                        Name = _context.City.FirstOrDefault(c => c.CityID == (_context.Address.FirstOrDefault(a => a.AddressID == staff.AddressID).CityID)).Name
                    },
                    Region = new RegionViewModel
                    {
                        Name = _context.Region.FirstOrDefault(r => r.RegionID == (_context.Address.FirstOrDefault(a => a.AddressID == staff.AddressID).RegionID)).Name
                    }
                },
                LockoutEnabled = staff.LockoutEnabled
            }).ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.User.FirstOrDefaultAsync(m => m.Email == id);
            if (staff == null)
            {
                return NotFound();
            }

            Address address = _context.Address.FirstOrDefault(l => l.AddressID == staff.AddressID);

            Locality locality = _context.Locality.FirstOrDefault(l => l.LocalityID == address.LocalityID);
            City city = _context.City.FirstOrDefault(l => l.CityID == address.CityID);
            PostCode postcode = _context.PostCode.FirstOrDefault(l => l.PostCodeID == address.PostCodeID);
            Region region = _context.Region.FirstOrDefault(l => l.RegionID == address.RegionID);
            Country country = _context.Country.FirstOrDefault(l => l.CountryID == address.CountryID);

            return View(new StaffViewModel
            {
                UserName = staff.UserName,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Email = staff.Email,
                PhoneNumber = staff.PhoneNumber,
                Position = staff.Position,
                Address = new AddressViewModel
                {
                    Line1 = address.Line1,
                    Line2 = address.Line2,
                    Locality = new LocalityViewModel
                    {
                        Name = locality.Name
                    },
                    City = new CityViewModel
                    {
                        Name = city.Name
                    },
                    PostCode = new PostCodeViewModel
                    {
                        Code = postcode.Code
                    },
                    Region = new RegionViewModel
                    {
                        Name = region.Name
                    },
                    Country = new CountryViewModel
                    {
                        Name = country.Name
                    }
                }
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("UserName, FirstName, LastName, Email, Password, PhoneNumber, Position, Address")] StaffViewModel StaffViewModel)
        {
            if (ModelState.IsValid)
            {
                Locality locality = _context.Locality.FirstOrDefault(l => l.Name == StaffViewModel.Address.Locality.Name);
                City city = _context.City.FirstOrDefault(c => c.Name == StaffViewModel.Address.City.Name);
                PostCode postcode = _context.PostCode.FirstOrDefault(p => p.Code == StaffViewModel.Address.PostCode.Code);
                Region region = _context.Region.FirstOrDefault(r => r.Name == StaffViewModel.Address.Region.Name);
                Country country = _context.Country.FirstOrDefault(c => c.Name == StaffViewModel.Address.Country.Name);

                if (locality == null)
                {
                    locality = new Locality
                    {
                        Name = StaffViewModel.Address.Locality.Name
                    };

                    _context.Locality.Add(locality);
                    _context.SaveChanges();
                }

                if (city == null)
                {
                    city = new City
                    {
                        Name = StaffViewModel.Address.City.Name
                    };

                    _context.City.Add(city);
                    _context.SaveChanges();
                }

                if (postcode == null)
                {
                    postcode = new PostCode
                    {
                        Code = StaffViewModel.Address.PostCode.Code
                    };

                    _context.PostCode.Add(postcode);
                    _context.SaveChanges();
                }

                if (region == null)
                {
                    region = new Region
                    {
                        Name = StaffViewModel.Address.Region.Name
                    };

                    _context.Region.Add(region);
                    _context.SaveChanges();
                }

                if (country == null)
                {
                    country = new Country
                    {
                        Name = StaffViewModel.Address.Country.Name
                    };

                    _context.Country.Add(country);
                    _context.SaveChanges();
                }

                Address address = new Address
                {
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

                await _userManager.CreateAsync(new User
                {
                    UserName = StaffViewModel.UserName,
                    FirstName = StaffViewModel.FirstName,
                    LastName = StaffViewModel.LastName,
                    Email = StaffViewModel.Email,
                    PhoneNumber = StaffViewModel.PhoneNumber,
                    Position = StaffViewModel.Position,
                    AddressID = address.AddressID,
                }, StaffViewModel.Password);

                User staff = await _userManager.FindByEmailAsync(StaffViewModel.Email);
                staff.LockoutEnabled = false;
                await _userManager.UpdateAsync(staff);

                return RedirectToAction(nameof(Index));
            }

            return View(StaffViewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _userManager.FindByEmailAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            Address address = _context.Address.FirstOrDefault(l => l.AddressID == staff.AddressID);

            Locality locality = _context.Locality.FirstOrDefault(l => l.LocalityID == address.LocalityID);
            City city = _context.City.FirstOrDefault(l => l.CityID == address.CityID);
            PostCode postcode = _context.PostCode.FirstOrDefault(l => l.PostCodeID == address.PostCodeID);
            Region region = _context.Region.FirstOrDefault(l => l.RegionID == address.RegionID);
            Country country = _context.Country.FirstOrDefault(l => l.CountryID == address.CountryID);

            return View(new StaffEditViewModel
            {
                UserName = staff.UserName,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Email = staff.Email,
                PhoneNumber = staff.PhoneNumber,
                Position = staff.Position,
                Address = new AddressViewModel
                {
                    Line1 = address.Line1,
                    Line2 = address.Line2,
                    Locality = new LocalityViewModel
                    {
                        Name = locality.Name
                    },
                    City = new CityViewModel
                    {
                        Name = city.Name
                    },
                    PostCode = new PostCodeViewModel
                    {
                        Code = postcode.Code
                    },
                    Region = new RegionViewModel
                    {
                        Name = region.Name
                    },
                    Country = new CountryViewModel
                    {
                        Name = country.Name
                    }
                },
                LockoutEnabled = staff.LockoutEnabled
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("UserName, FirstName, LastName, Email, PhoneNumber, Position, Address, LockoutEnabled")] StaffEditViewModel StaffViewModel)
        {
            if (id != StaffViewModel.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var staff = await _userManager.FindByEmailAsync(id);

                staff.UserName = StaffViewModel.UserName;
                staff.FirstName = StaffViewModel.FirstName;
                staff.LastName = StaffViewModel.LastName;
                staff.Email = StaffViewModel.Email;
                staff.PhoneNumber = StaffViewModel.PhoneNumber;
                staff.Position = StaffViewModel.Position;

                Address address = _context.Address.FirstOrDefault(l => l.AddressID == staff.AddressID);

                Locality locality = _context.Locality.FirstOrDefault(l => l.Name == StaffViewModel.Address.Locality.Name);
                City city = _context.City.FirstOrDefault(c => c.Name == StaffViewModel.Address.City.Name);
                PostCode postcode = _context.PostCode.FirstOrDefault(c => c.Code == StaffViewModel.Address.PostCode.Code);
                Region region = _context.Region.FirstOrDefault(c => c.Name == StaffViewModel.Address.Region.Name);
                Country country = _context.Country.FirstOrDefault(c => c.Name == StaffViewModel.Address.Country.Name);

                if (locality == null)
                {
                    locality = new Locality
                    {
                        Name = StaffViewModel.Address.Locality.Name
                    };

                    _context.Locality.Add(locality);
                    _context.SaveChanges();
                }

                if (city == null)
                {
                    city = new City
                    {
                        Name = StaffViewModel.Address.City.Name
                    };

                    _context.City.Add(city);
                    _context.SaveChanges();
                }

                if (postcode == null)
                {
                    postcode = new PostCode
                    {
                        Code = StaffViewModel.Address.PostCode.Code
                    };

                    _context.PostCode.Add(postcode);
                    _context.SaveChanges();
                }

                if (region == null)
                {
                    region = new Region
                    {
                        Name = StaffViewModel.Address.Region.Name
                    };

                    _context.Region.Add(region);
                    _context.SaveChanges();
                }

                if (country == null)
                {
                    country = new Country
                    {
                        Name = StaffViewModel.Address.Country.Name
                    };

                    _context.Country.Add(country);
                    _context.SaveChanges();
                }

                address.Line1 = StaffViewModel.Address.Line1;
                address.Line2 = StaffViewModel.Address.Line2;
                address.LocalityID = locality.LocalityID;
                address.CityID = city.CityID;
                address.PostCodeID = postcode.PostCodeID;
                address.RegionID = region.RegionID;
                address.CountryID = country.CountryID;

                if (StaffViewModel.LockoutEnabled)
                {
                    staff.LockoutEnabled = true;
                    staff.LockoutEnd = DateTime.Today.AddYears(100);
                }
                else
                {
                    staff.LockoutEnabled = false;
                    staff.LockoutEnd = DateTime.Now;
                }

                await _userManager.UpdateAsync(staff);
                return RedirectToAction(nameof(Index));
            }

            return View(StaffViewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _userManager.FindByEmailAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(new StaffViewModel
            {
                Email = staff.Email
            });
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var staff = await _userManager.FindByEmailAsync(id);
            await _userManager.DeleteAsync(staff);

            return RedirectToAction(nameof(Index));
        }

        private bool StaffViewModelExists(string id)
        {
            return _context.User.Any(e => e.Email == id);
        }
    }
}
