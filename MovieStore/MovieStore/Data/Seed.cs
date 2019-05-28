using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MovieStore.Models;

namespace MovieStore.Data {
    public class Seed {
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration) {
            //adding customs roles
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            string[] roleNames = { "Admin", "Staff", "Customer" };

            IdentityResult roleResult;

            foreach (var roleName in roleNames) {
                // creating the roles and seeding them to the database
                var roleExist = await _roleManager.RoleExistsAsync(roleName);

                if (!roleExist) {
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // creating a super user who could maintain the web api
            var poweruser = new User {
                UserName = "Admin",
                Email = Configuration.GetSection("UserSettings")["UserEmail"]
            };

            string userPassword = Configuration.GetSection("UserSettings")["UserPassword"];
            var user = await _userManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["UserEmail"]);

            if (user == null) {
                var createPowerUser = await _userManager.CreateAsync(poweruser, userPassword);
            
                if (createPowerUser.Succeeded) {
                    // here we assign the new user the "Admin" role
                    await _userManager.AddToRoleAsync(poweruser, "Admin");
                }
            }
        }
    }
}
