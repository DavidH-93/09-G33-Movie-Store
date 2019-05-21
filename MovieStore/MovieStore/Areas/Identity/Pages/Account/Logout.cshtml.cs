using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MovieStore.Models;
using MovieStore.Services;

namespace MovieStore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly IAccessLogRepository _accessRepo;

        public LogoutModel(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<LogoutModel> logger, IAccessLogRepository accessRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _accessRepo = accessRepo;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                AccessLog log = new AccessLog()
                {
                    UserID = _userManager.GetUserId(User),
                    AccessLogID = new Guid(),
                    AccessType = "Logged Out",
                    LogTime = DateTime.Now
                };

                _accessRepo.Create(log);

                return LocalRedirect(returnUrl);
            }
            else
            {
                return Page();
            }
        }
    }
}