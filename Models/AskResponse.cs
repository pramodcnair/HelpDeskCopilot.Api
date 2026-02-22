namespace HelpDeskCopilot.Api.Models;

public record AskSource(string sourceFile, int chunkIndex, double score);

public record AskResponse(string answer, IReadOnlyList<AskSource> sources);
