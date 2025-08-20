using System.ComponentModel.DataAnnotations;

namespace SupportTicketSystem.Models
{
    public class AddTicket
    {
        [Required]
        public string Summary { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Assignment { get; set; }

        [Required]
        public string Impact { get; set; }

        public string Email { get; set; }

        public string Location { get; set; }

        [Required]
        public string AssetType { get; set; }
    }
}
