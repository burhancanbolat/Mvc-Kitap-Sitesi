using Microsoft.AspNetCore.Mvc;

namespace Kitaplar.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
