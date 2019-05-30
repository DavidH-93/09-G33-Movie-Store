using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MovieStore.Models;

namespace MovieStore.Data {
    public class Seed {
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration) {
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //Create Custom Roles
            string[] roleNames = { "Admin", "Staff", "Customer" };

            IdentityResult roleResult;
            foreach (var roleName in roleNames) {
                //Seed Roles into Database
                var roleExist = await _roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        public static async Task SeedCustomers(IServiceProvider serviceProvider, IConfiguration Configuration, MovieStoreDbContext context)
        { 
            var _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            context.Database.EnsureCreated();

            ///////////////////////////////////////////////////////////////////////////

            //Ensure Database User Table Is Empty
            if (context.User.Any())
                return;

            //Create Administrator
            var poweruser = new User {
                UserName = "Admin",
                Email = Configuration.GetSection("AdminSettings")["UserEmail"]
            };
     
            var createPowerUser = await _userManager.CreateAsync(poweruser, Configuration.GetSection("AdminSettings")["UserPassword"]);

            if (createPowerUser.Succeeded)
                //Assign Admin Role
                await _userManager.AddToRoleAsync(poweruser, "Admin");

            ////////////////////////////////////////////////////////////////////////////

            //Create Customers
            var users = new User[] {
                new User() {
                    UserName = "Momyce1978",
                    FirstName = "Crystal",
                    LastName = "Cornish",
                    Email = "CrystalACornish@dayrep.com",
                    PhoneNumber = "0266096849",
                    Position = "Individual",
                    AddressID = GenerateAddress(
                        context,
                        "19 Link Road",
                        "",
                        "Value",
                        "BLUMONT",
                        7260,
                        "TAS",
                        "Australia"
                    )
                },

                new User() {
                    UserName = "Wasts1946",
                    FirstName = "Mary",
                    LastName = "Bergin",
                    Email = "MaryBergin@teleworm.us",
                    PhoneNumber = "0745601738",
                    Position = "Individual",
                    AddressID = GenerateAddress(
                        context,
                        "72 George Street",
                        "",
                        "Value",
                        "LUMEAH",
                        4478,
                        "QLD",
                        "Australia"
                    )
                },

                new User() {
                   UserName = "Rearight",
                   FirstName = "Justin",
                   LastName = "Carrington",
                   Email = "JustinCarrington@teleworm.us",
                   PhoneNumber = "0733401769",
                   Position = "Individual",
                   AddressID = GenerateAddress(
                       context,
                       "23 Savages Road",
                       "",
                       "Value",
                       "SUNNYBANK",
                       4109,
                       "QLD",
                       "Australia"
                   )
                },

                new User() {
                   UserName = "Frand1959",
                   FirstName = "Taj",
                   LastName = "Barkly",
                   Email = "TajBarkly@jourrapide.com",
                   PhoneNumber = "0390284685",
                   Position = "Individual",
                   AddressID = GenerateAddress(
                       context,
                       "8 Horsington Street",
                       "",
                       "Value",
                       "BLACKBURN",
                       3130,
                       "VIC",
                       "Australia"
                   )
                },

                new User() {
                   UserName = "Criall",
                   FirstName = "Ruby",
                   LastName = "McKenny",
                   Email = "RubyMcKenny@jourrapide.com",
                   PhoneNumber = "0353197021",
                   Position = "Individual",
                   AddressID = GenerateAddress(
                       context,
                       "67 Settlement Road",
                       "",
                       "Value",
                       "DAWSON",
                       3858,
                       "VIC",
                       "Australia"
                   )
                },

                new User() {
                   UserName = "Himsed",
                   FirstName = "Life's",
                   LastName = "Gold",
                   Email = "SkyePendred@armyspy.com",
                   PhoneNumber = "0362269381",
                   Position = "Company",
                   AddressID = GenerateAddress(
                       context,
                       "41 Jones Street",
                       "",
                       "Value",
                       "WEEGENA",
                       7304,
                       "TAS",
                       "Australia"
                   )
                },

                new User() {
                   UserName = "Peding",
                   FirstName = "Leaf",
                   LastName = "Management",
                   Email = "KaylaJose@rhyta.com",
                   PhoneNumber = "0249175677",
                   Position = "Company",
                   AddressID = GenerateAddress(
                       context,
                       "15 Woerdens Road",
                       "",
                       "Value",
                       "WYBONG",
                       2333,
                       "NSW",
                       "Australia"
                   )
                },

                new User() {
                    UserName = "Subjecould",
                    FirstName = "Lilian",
                    LastName = "Bendrodt",
                    Email = "LilianBendrodt@dayrep.com",
                    PhoneNumber = "0399293044",
                    Position = "Individual",
                    AddressID = GenerateAddress(
                        context,
                        "29 Lane Street",
                        "",
                        "Value",
                        "TUNSTALL SQUARE PO",
                        3109,
                        "VIC",
                        "Australia"
                    )
                },

                new User() {
                    UserName = "Wity1992",
                    FirstName = "Brayden",
                    LastName = "Giblin",
                    Email = "BraydenGiblin@rhyta.com",
                    PhoneNumber = "0390399530",
                    Position = "Individual",
                    AddressID = GenerateAddress(
                        context,
                        "99 Patton Street",
                        "",
                        "Value",
                        "DOMAIN ROAD PO",
                        3141,
                        "VIC",
                        "Australia"
                    )
                },

                new User() {
                   UserName = "Greder",
                   FirstName = "Ellie",
                   LastName = "Jones",
                   Email = "eJones@rhyta.com",
                   PhoneNumber = "0887423082",
                   Position = "Individual",
                   AddressID = GenerateAddress(
                       context,
                       "33 Corny Court",
                       "",
                       "Value",
                       "MUDAMUCKLA",
                       5680,
                       "SA",
                       "Australia"
                   )
                },

                new User() {
                   UserName = "Girut1952",
                   FirstName = "Isabelle",
                   LastName = "Anderson",
                   Email = "IsabelleAnderson@rhyta.com",
                   PhoneNumber = "0890738050",
                   Position = "Individual",
                   AddressID = GenerateAddress(
                       context,
                       "96 Murphy Street",
                       "",
                       "Value",
                       "MEENAAR",
                       6401,
                       "WA",
                       "Australia"
                   )
                },

                new User() {
                   UserName = "Wasken61",
                   FirstName = "Mary",
                   LastName = "Handcock",
                   Email = "MaryHandcock@armyspy.com",
                   PhoneNumber = "0883164745",
                   Position = "Individual",
                   AddressID = GenerateAddress(
                       context,
                       "18 Tapleys Hill Road",
                       "",
                       "Value",
                       "ELIZABETH DOWNS",
                       5113,
                       "SA",
                       "Australia"
                   )
                },

                new User() {
                   UserName = "Lesead1939",
                   FirstName = "McDade's",
                   LastName = "Company",
                   Email = "JacobBostock@dayrep.com",
                   PhoneNumber = "0740361959",
                   Position = "Company",
                   AddressID = GenerateAddress(
                       context,
                       "72 South Molle Boulevard",
                       "",
                       "Value",
                       "GIDYA",
                       4824,
                       "QLD",
                       "Australia"
                   )
                },

                new User() {
                   UserName = "Surce1992",
                   FirstName = "Holly",
                   LastName = "Inn",
                   Email = "MariamPraed@dayrep.com",
                   PhoneNumber = "0353387202",
                   Position = "Company",
                   AddressID = GenerateAddress(
                       context,
                       "93 Commercial Street",
                       "",
                       "Value",
                       "LAKE HINDMARSH",
                       3423,
                       "VIC",
                       "Australia"
                   )
                },

                new User() {
                    UserName = "Aforeg",
                    FirstName = "Tahlia",
                    LastName = "Micklem",
                    Email = "TahliaMicklem@rhyta.com",
                    PhoneNumber = "0267713036",
                    Position = "Individual",
                    AddressID = GenerateAddress(
                        context,
                        "89 Dora Creek",
                        "",
                        "Value",
                        "SOUTH GUNDURIMBA",
                        2480,
                        "NSW",
                        "Australia"
                    )
                },

                new User() {
                    UserName = "Museem",
                    FirstName = "Gemma",
                    LastName = "Edouard",
                    Email = "GemmaEdouard@jourrapide.com",
                    PhoneNumber = "0353490018",
                    Position = "Individual",
                    AddressID = GenerateAddress(
                        context,
                        "6 McKillop Street",
                        "",
                        "Value",
                        "BELLELLEN",
                        3380,
                        "VIC",
                        "Australia"
                    )
                },

                new User() {
                   UserName = "Marsureend",
                   FirstName = "Alice",
                   LastName = "O'May",
                   Email = "AliceOMay@teleworm.us",
                   PhoneNumber = "0246240499",
                   Position = "Individual",
                   AddressID = GenerateAddress(
                       context,
                       "86 Timms Drive",
                       "",
                       "Value",
                       "AIRDS",
                       2560,
                       "NSW",
                       "Australia"
                   )
                },

                new User() {
                   UserName = "Wiliptoo",
                   FirstName = "Evie",
                   LastName = "Nixon-Smith",
                   Email = "EvieNixon-Smith@teleworm.us",
                   PhoneNumber = "0889742078",
                   Position = "Individual",
                   AddressID = GenerateAddress(
                       context,
                       "31 Austin Road",
                       "",
                       "Value",
                       "BERRY SPRINGS",
                       0838,
                       "NT",
                       "Australia"
                   )
                },

                new User() {
                   UserName = "Poinsuldown",
                   FirstName = "Lilly",
                   LastName = "Moulden",
                   Email = "LillyMoulden@teleworm.us",
                   PhoneNumber = "0362644249",
                   Position = "Individual",
                   AddressID = GenerateAddress(
                       context,
                       "93 Link Road",
                       "",
                       "Value",
                       "BRIDPORT",
                       7252,
                       "TAS",
                       "Australia"
                   )
                },

                new User() {
                   UserName = "Whaderegret",
                   FirstName = "Phoebe",
                   LastName = "Mummery",
                   Email = "PhoebeMummery@teleworm.us",
                   PhoneNumber = "0382127615",
                   Position = "Individual",
                   AddressID = GenerateAddress(
                       context,
                       "38 Bayley Street",
                       "",
                       "Value",
                       "PHEASANT CREEK",
                       3757,
                       "VIC",
                       "Australia"
                   )
                }

            };

            //Seed Customers into Database
            foreach (User user in users) {
                var seedUser = await _userManager.CreateAsync(user, Configuration.GetSection("DefaultPassword")["Pass"]);

                user.LockoutEnabled = false;
                context.User.Update(user);

                if (seedUser.Succeeded)
                    //Add Customer Role
                    await _userManager.AddToRoleAsync(user, "Customer");
            }

            context.SaveChanges();
        }

        private static Guid GenerateAddress(MovieStoreDbContext context, string Line1, string Line2, string Locality, string City, int PostCode, string Region, string Country) {
            //Check for existing records
            Locality locality = context.Locality.FirstOrDefault(l => l.Name == Locality);
            City city = context.City.FirstOrDefault(c => c.Name == City);
            PostCode postcode = context.PostCode.FirstOrDefault(p => p.Code == PostCode);
            Region region = context.Region.FirstOrDefault(r => r.Name == Region);
            Country country = context.Country.FirstOrDefault(c => c.Name == Country);

            //Create new records if they don't exist
            if (locality == null)
            {
                locality = new Locality
                {
                    Name = Locality
                };

                context.Locality.Add(locality);
                context.SaveChanges();
            }

            if (city == null)
            {
                city = new City
                {
                    Name = City
                };

                context.City.Add(city);
                context.SaveChanges();
            }

            if (postcode == null)
            {
                postcode = new PostCode
                {
                    Code = PostCode
                };

                context.PostCode.Add(postcode);
                context.SaveChanges();
            }

            if (region == null)
            {
                region = new Region
                {
                    Name = Region
                };

                context.Region.Add(region);
                context.SaveChanges();
            }

            if (country == null)
            {
                country = new Country
                {
                    Name = Country
                };

                context.Country.Add(country);
                context.SaveChanges();
            }

            //Create Address
            Address address = new Address
            {
                Line1 = Line1,
                Line2 = Line2,
                LocalityID = locality.LocalityID,
                CityID = city.CityID,
                PostCodeID = postcode.PostCodeID,
                RegionID = region.RegionID,
                CountryID = country.CountryID
            };
            context.Address.Add(address);
            context.SaveChanges();

            return address.AddressID;
        }
    }
}
