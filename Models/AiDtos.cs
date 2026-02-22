using System.Collections.Immutable;

namespace HelpDeskCopilot.Api.Models
{
    public record ChatMessage(string role, string content);

    public record ChatRequest(string model, ImmutableArray<ChatMessage> messages, double temperature =0.2);

    public record ChatChoice(ChatMessage message);

    public record ChatResponse(ImmutableArray<ChatChoice> choices);
    
}
