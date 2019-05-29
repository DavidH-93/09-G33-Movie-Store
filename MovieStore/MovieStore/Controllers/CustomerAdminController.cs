using System;
using System.Collections.Generic;
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
    public class CustomerAdminController : Controller
    {
        private readonly MovieStoreDbContext _context;
        private readonly UserManager<User> _userManager;

        public CustomerAdminController(MovieStoreDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(string nameSearchString, string phoneSearchString, string positionSearchString)
        {
            IQueryable<User> users = from a in _context.Roles
                                     join h in _context.UserRoles on a.Id equals h.RoleId
                                     join c in _context.User on h.UserId equals c.Id
                                     into temp
                                     from m in temp
                                     where a.Name == "Customer"
                                     select m;

            if (!String.IsNullOrEmpty(nameSearchString))
            {
                if (nameSearchString.Trim().Contains(' '))
                {
                    String[] names = nameSearchString.Split(' ');
                    users = users.Where(u => u.FirstName.Contains(names[0]) && u.LastName.Contains(names[1]));
                }
                else
                {
                    users = users.Where(u => u.FirstName.Contains(nameSearchString) || u.LastName.Contains(nameSearchString));
                }
            }

            if (!String.IsNullOrEmpty(phoneSearchString))
            {
                users = users.Where(u => u.PhoneNumber.Contains(phoneSearchString.Trim()));
            }

            if (!String.IsNullOrEmpty(positionSearchString))
            {
                users = users.Where(u => u.Position.Contains(positionSearchString.Trim()));
            }

            return View(users.Select(user => new UserViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Position = user.Position,
                Address = new AddressViewModel
                {
                    City = new CityViewModel
                    {
                        Name = _context.City.FirstOrDefault(c => c.CityID == (
                            _context.Address.FirstOrDefault(a => a.AddressID == user.AddressID).CityID)).Name
                    },
                    Region = new RegionViewModel
                    {
                        Name = _context.Region.FirstOrDefault(r => r.RegionID == (
                            _context.Address.FirstOrDefault(a => a.AddressID == user.AddressID).RegionID)).Name
                    }
                },
                LockoutEnabled = user.LockoutEnabled
            }).ToList());
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            User user = _context.User.FirstOrDefault(u => u.Email == id);

            Address address = _context.Address.FirstOrDefault(a => a.AddressID == user.AddressID);

            Locality locality = _context.Locality.FirstOrDefault(l => l.LocalityID == address.LocalityID);
            City city = _context.City.FirstOrDefault(c => c.CityID == address.CityID);
            PostCode postcode = _context.PostCode.FirstOrDefault(p => p.PostCodeID == address.PostCodeID);
            Region region = _context.Region.FirstOrDefault(r => r.RegionID == address.RegionID);
            Country country = _context.Country.FirstOrDefault(c => c.CountryID == address.CountryID);

            return View(new UserViewModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Position = user.Position,

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

                LockoutEnabled = user.LockoutEnabled
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("UserName, FirstName, LastName, Email, Password, PhoneNumber, Position, Address")] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                Locality locality = _context.Locality.FirstOrDefault(l => l.Name == userViewModel.Address.Locality.Name);
                City city = _context.City.FirstOrDefault(c => c.Name == userViewModel.Address.City.Name);
                PostCode postcode = _context.PostCode.FirstOrDefault(p => p.Code == userViewModel.Address.PostCode.Code);
                Region region = _context.Region.FirstOrDefault(r => r.Name == userViewModel.Address.Region.Name);
                Country country = _context.Country.FirstOrDefault(c => c.Name == userViewModel.Address.Country.Name);

                if (locality == null)
                {
                    locality = new Locality
                    {
                        Name = userViewModel.Address.Locality.Name
                    };

                    _context.Locality.Add(locality);
                    _context.SaveChanges();
                }

                if (city == null)
                {
                    city = new City
                    {
                        Name = userViewModel.Address.City.Name
                    };

                    _context.City.Add(city);
                    _context.SaveChanges();
                }

                if (postcode == null)
                {
                    postcode = new PostCode
                    {
                        Code = userViewModel.Address.PostCode.Code
                    };

                    _context.PostCode.Add(postcode);
                    _context.SaveChanges();
                }

                if (region == null)
                {
                    region = new Region
                    {
                        Name = userViewModel.Address.Region.Name
                    };

                    _context.Region.Add(region);
                    _context.SaveChanges();
                }

                if (country == null)
                {
                    country = new Country
                    {
                        Name = userViewModel.Address.Country.Name
                    };

                    _context.Country.Add(country);
                    _context.SaveChanges();
                }

                Address address = new Address
                {
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

                await _userManager.CreateAsync(new User
                {
                    UserName = userViewModel.UserName,
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName,
                    Email = userViewModel.Email,
                    PhoneNumber = userViewModel.PhoneNumber,
                    Position = userViewModel.Position,
                    AddressID = address.AddressID,
                }, userViewModel.Password);

                User user = await _userManager.FindByEmailAsync(userViewModel.Email);

                await _userManager.AddToRoleAsync(user, "Customer");
                user.LockoutEnabled = false;

                await _userManager.UpdateAsync(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(userViewModel);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            User user = _context.User.FirstOrDefault(u => u.Email == id);

            Address address = _context.Address.FirstOrDefault(a => a.AddressID == user.AddressID);

            Locality locality = _context.Locality.FirstOrDefault(l => l.LocalityID == address.LocalityID);
            City city = _context.City.FirstOrDefault(c => c.CityID == address.CityID);
            PostCode postcode = _context.PostCode.FirstOrDefault(p => p.PostCodeID == address.PostCodeID);
            Region region = _context.Region.FirstOrDefault(r => r.RegionID == address.RegionID);
            Country country = _context.Country.FirstOrDefault(c => c.CountryID == address.CountryID);

            return View(new UserViewModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Position = user.Position,
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
                LockoutEnabled = user.LockoutEnabled
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("UserName, FirstName, LastName, Email, Password, PhoneNumber, Position, Address, LockoutEnabled")] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.User.FirstOrDefaultAsync(u => u.Email == userViewModel.Email);

                user.UserName = userViewModel.UserName;
                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;
                user.Position = userViewModel.Position;

                Address address = _context.Address.FirstOrDefault(a => a.AddressID == user.AddressID);

                Locality locality = _context.Locality.FirstOrDefault(l => l.Name == userViewModel.Address.Locality.Name);
                City city = _context.City.FirstOrDefault(c => c.Name == userViewModel.Address.City.Name);
                PostCode postcode = _context.PostCode.FirstOrDefault(p => p.Code == userViewModel.Address.PostCode.Code);
                Region region = _context.Region.FirstOrDefault(r => r.Name == userViewModel.Address.Region.Name);
                Country country = _context.Country.FirstOrDefault(c => c.Name == userViewModel.Address.Country.Name);

                if (locality == null)
                {
                    locality = new Locality
                    {
                        Name = userViewModel.Address.Locality.Name
                    };

                    _context.Locality.Add(locality);
                    _context.SaveChanges();
                }

                if (city == null)
                {
                    city = new City
                    {
                        Name = userViewModel.Address.City.Name
                    };

                    _context.City.Add(city);
                    _context.SaveChanges();
                }

                if (postcode == null)
                {
                    postcode = new PostCode
                    {
                        Code = userViewModel.Address.PostCode.Code
                    };

                    _context.PostCode.Add(postcode);
                    _context.SaveChanges();
                }

                if (region == null)
                {
                    region = new Region
                    {
                        Name = userViewModel.Address.Region.Name
                    };

                    _context.Region.Add(region);
                    _context.SaveChanges();
                }

                if (country == null)
                {
                    country = new Country
                    {
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

                if (userViewModel.LockoutEnabled)
                {
                    user.LockoutEnabled = true;
                    user.LockoutEnd = DateTime.Today.AddYears(100);
                }
                else
                {
                    user.LockoutEnabled = false;
                    user.LockoutEnd = DateTime.Now;
                }

                await _userManager.UpdateAsync(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(userViewModel);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            User user = _context.User.FirstOrDefault(u => u.Email == id);

            return View(new UserViewModel {
                Email = user.Email
            });
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id) {
            User user = _context.User.FirstOrDefault(u => u.Email == id);

            var ordersQuery = from o in _context.Order
                         where o.UserID == user.Id
                         select o;
            var orders = ordersQuery.ToList();

            foreach (Order order in orders)
            {
                order.Status = "Cancelled";

                _context.Order.Update(order);
                await _context.SaveChangesAsync();
            }

            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}
