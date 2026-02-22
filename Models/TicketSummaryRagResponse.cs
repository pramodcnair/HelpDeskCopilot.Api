namespace HelpDeskCopilot.Api.Models
{
    public record TicketSummaryRagResponse(
    TicketSummaryResult result,
    IReadOnlyList<AskSource> sources);
}
