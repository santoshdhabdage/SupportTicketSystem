using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Data;
using SupportTicketSystem.Models;
using SupportTicketSystem.Services;

namespace SupportTicketSystem.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;
        private readonly UserManager<IdentityUser> _userManager;

        public TicketController(
            ApplicationDbContext context,
            EmailService emailService,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _emailService = emailService;
            _userManager = userManager;
        }

        // GET: Index with search and role-based filtering
        public async Task<IActionResult> Index(string? searchString)
        {
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            var tickets = _context.Tickets
                .Where(t =>
                    string.IsNullOrEmpty(searchString) ||
                    t.Title.Contains(searchString) ||
                    t.Status.Contains(searchString) ||
                    t.Category.Contains(searchString));

            if (!isAdmin)
            {
                tickets = tickets.Where(t => t.CreatedBy == user.UserName);
            }

            var ticketList = await tickets.OrderByDescending(t => t.CreatedAt).ToListAsync();
            return View(ticketList);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.CreatedAt = DateTime.Now;
                ticket.CreatedBy = User.Identity?.Name;

                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync();

                // Email notification
                string subject = $"New Ticket Created: {ticket.Title}";
                string body = $@"
                    <p>A new ticket has been created:</p>
                    <ul>
                        <li><strong>Title:</strong> {ticket.Title}</li>
                        <li><strong>Status:</strong> {ticket.Status}</li>
                        <li><strong>Priority:</strong> {ticket.Priority}</li>
                        <li><strong>Created By:</strong> {ticket.CreatedBy}</li>
                        <li><strong>Created At:</strong> {ticket.CreatedAt}</li>
                    </ul>";

                await _emailService.SendEmailAsync("admin@example.com", subject, body);

                return RedirectToAction(nameof(Index));
            }

            return View(ticket);
        }

        // GET: Edit (Admin Only)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            return View(ticket);
        }

        // POST: Edit (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tickets.Any(e => e.Id == ticket.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(ticket);
        }

        // GET: Delete (Admin Only)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            if (ticket == null) return NotFound();

            return View(ticket);
        }

        // POST: DeleteConfirmed (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Details (All Users)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            if (ticket == null) return NotFound();

            return View(ticket);
        }
    }
}
