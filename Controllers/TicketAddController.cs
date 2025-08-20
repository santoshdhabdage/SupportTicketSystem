using Microsoft.AspNetCore.Mvc;

namespace SupportTicketSystem.Controllers
{
    public class TicketAddController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
