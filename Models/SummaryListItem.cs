namespace HelpDeskCopilot.Api.Models
{
    public record SummaryListItem(int id,
        string inputHash,
        DateTime createdUtc,
        string summary,
        int keyPointsCount,
        int actionItemsCount);
}
