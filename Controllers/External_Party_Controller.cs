using Microsoft.AspNetCore.Mvc;

namespace External_Party.Controllers
{
    public class External_Party_Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
