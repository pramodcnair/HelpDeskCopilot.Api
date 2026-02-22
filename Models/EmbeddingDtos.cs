using System.Collections.Immutable;

namespace HelpDeskCopilot.Api.Models;

public record EmbeddingRequest(string model, string input);

public record EmbeddingData(ImmutableArray<double> embedding);

public record EmbeddingResponse(ImmutableArray<EmbeddingData> data);
