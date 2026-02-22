using System.Collections.Immutable;

namespace HelpDeskCopilot.Api.Services
{
    public interface IAiClient
    {
        Task<string> ChatAsync(string userMessage, CancellationToken ct = default);
        Task<string> ChatRawAsync(string systemPrompt, string userPrompt, CancellationToken ct = default);
        Task<ImmutableArray<double>> EmbedAsync(string text, CancellationToken ct = default);

    }
}
