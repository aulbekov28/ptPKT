using Microsoft.AspNetCore.Mvc;

namespace ptPKT.WebUI.Controllers
{
    public class ToDoItemsController : BaseApiController
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}