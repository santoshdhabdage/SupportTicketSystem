using System;
using System.ComponentModel.DataAnnotations;

namespace SupportTicketSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; } = "Open"; // Open, In Progress, Closed

        [Required]
        public string Priority { get; set; } = "Medium"; // Low, Medium, High

        public string Category { get; set; } // IT, HR, Admin, etc.

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public string? CreatedBy { get; set; } // Username or userId
    }
}
