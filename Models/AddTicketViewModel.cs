using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SupportTicketSystem.Models.ViewModels
{
    public class AddTicketViewModel
    {
        [Required]
        [Display(Name = "Summary")]
        public string Summary { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }

        public List<SelectListItem> CategoryOptions { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "Equipment IT", Value = "Equipment IT" },
            new SelectListItem { Text = "Production IT", Value = "Production IT" },
            new SelectListItem { Text = "Operation IT", Value = "Operation IT" }
        };

        [Display(Name = "Assignment")]
        public string AssignedToUserId { get; set; }

        public List<SelectListItem> AssignmentOptions { get; set; } = new List<SelectListItem>();

        [Required]
        [Display(Name = "Impact")]
        public string Impact { get; set; }

        public List<string> ImpactOptions { get; set; } = new List<string> { "Low", "Medium", "High" };

        [Required]
        [EmailAddress]
        public string Email { get; set; } // Auto-filled from logged-in user

        public string Location { get; set; } // Auto-filled from logged-in user

        [Display(Name = "Asset Type")]
        public string AssetType { get; set; }

        public List<SelectListItem> AssetTypeOptions { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "Server", Value = "Server" },
            new SelectListItem { Text = "CPU", Value = "CPU" },
            new SelectListItem { Text = "Monitor", Value = "Monitor" },
            new SelectListItem { Text = "Keyboard", Value = "Keyboard" },
            new SelectListItem { Text = "Printer", Value = "Printer" }
        };
    }
}
