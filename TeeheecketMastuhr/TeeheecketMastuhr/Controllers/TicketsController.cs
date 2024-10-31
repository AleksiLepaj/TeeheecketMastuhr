using Microsoft.AspNetCore.Mvc;

namespace TeeheecketMastuhr.Controllers
{
    public class TicketsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
