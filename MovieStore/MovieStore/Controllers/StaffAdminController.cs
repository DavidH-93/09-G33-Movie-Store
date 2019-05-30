using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieStore.Controllers {
    [Authorize(Roles = "Admin")]
    public class StaffAdminController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}