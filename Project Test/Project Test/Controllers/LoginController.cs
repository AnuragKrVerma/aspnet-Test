using Microsoft.AspNetCore.Mvc;

namespace Project_Test.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
