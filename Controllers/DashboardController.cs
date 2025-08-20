using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Data;

namespace SupportTicketSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var totalTickets = await _context.Tickets.CountAsync();
            var openTickets = await _context.Tickets.CountAsync(t => t.Status == "Open");
            var closedTickets = await _context.Tickets.CountAsync(t => t.Status == "Closed");

            ViewBag.TotalTickets = totalTickets;
            ViewBag.OpenTickets = openTickets;
            ViewBag.ClosedTickets = closedTickets;

            return View();
        }
    }
}
