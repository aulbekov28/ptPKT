using Microsoft.AspNetCore.Mvc;

namespace ptPKT.WebUI.Controllers
{
    public abstract class AccountController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}