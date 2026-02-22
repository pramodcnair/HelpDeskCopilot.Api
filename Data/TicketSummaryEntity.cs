using System.ComponentModel.DataAnnotations;

namespace HelpDeskCopilot.Api.Data
{
    public class TicketSummaryEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string InputHash { get; set; } = string.Empty;

        [Required]
        public string TicketText { get; set; } = string.Empty;

        [Required]
        public string SummaryJson { get; set; } = string.Empty;

        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    }
}
