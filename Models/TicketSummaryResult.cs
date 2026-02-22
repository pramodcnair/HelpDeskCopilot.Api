namespace HelpDeskCopilot.Api.Models
{
    public record TicketSummaryResult(string summary, IReadOnlyList<string> key_points, IReadOnlyList<string> action_items, string draft_reply);
}
