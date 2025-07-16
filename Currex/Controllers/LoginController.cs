using Microsoft.AspNetCore.Mvc;

namespace Currex.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
