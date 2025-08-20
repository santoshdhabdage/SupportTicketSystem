using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SupportTicketSystem.Controllers
{
    [Authorize]
    public class AddTicketController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Auto-fill email and location from logged-in user
            var model = new AddTicket
            {
                Email = User.FindFirstValue(ClaimTypes.Email),
                Location = "01, Nilkanthpark Society, Near Maheshwarinagar, Odhav, Ahmedabad" // or fetch from user profile
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(AddTicket model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Save to DB (we’ll do next step)

                // Redirect or show success
                TempData["SuccessMessage"] = "Ticket submitted successfully!";
                return RedirectToAction("Index");
            }

            // If validation fails, show same view with errors
            return View(model);
        }
    }
}
