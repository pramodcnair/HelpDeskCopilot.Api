namespace HelpDeskCopilot.Api.Services
{
    using HelpDeskCopilot.Api.Models;
    using Microsoft.Extensions.Options;
    using System.Collections.Immutable;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;

    public class OpenAiChatClient : IAiClient
    {
        private readonly HttpClient _http;
        private readonly AiOptions _opt;
        private static readonly JsonSerializerOptions JsonOpts = new(JsonSerializerDefaults.Web);

        public OpenAiChatClient(HttpClient http, IOptions<AiOptions> opt)
        {
            _http = http;
            _opt = opt.Value;
        }
        public async Task<string> ChatAsync(string userMessage, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(_opt.ApiKey))
                throw new InvalidOperationException("AI:ApiKey is missing. Set it via user-secrets.");

            var req = new ChatRequest(
                    model: _opt.Model,
                    messages: ImmutableArray.Create
                    (
                        new ChatMessage("system","You are a helpful assistant. Keep answers short."),
                        new ChatMessage("user", userMessage)
                    ),
                    temperature: 0.2
                );

            var json = JsonSerializer.Serialize(req, JsonOpts);

            using var httpReq = new HttpRequestMessage(HttpMethod.Post, $"{_opt.BaseUrl.TrimEnd('/')}/chat/completions");
            httpReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _opt.ApiKey);
            httpReq.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using var resp = await _http.SendAsync(httpReq, ct);
            var body = await resp.Content.ReadAsStringAsync(ct);
            if (!resp.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"AI call failed: {(int)resp.StatusCode} {resp.ReasonPhrase}. Body: {body}");
            }

            var parsed = JsonSerializer.Deserialize<ChatResponse>(body, JsonOpts);
            var answer = parsed is { choices.Length: > 0 } ? parsed.choices[0].message.content : null;

            return string.IsNullOrWhiteSpace(answer) ? "(empty response)" : answer;
        }

        public async Task<string> ChatRawAsync(string systemPrompt, string userPrompt, CancellationToken ct = default)
        {
            var req = new ChatRequest(
               model: _opt.Model,
               messages: ImmutableArray.Create(
                   new ChatMessage("system", systemPrompt),
                   new ChatMessage("user", userPrompt)
               ),
               temperature: 1.2);
                
            var json = JsonSerializer.Serialize(req, JsonOpts);
            using var httpReq = new HttpRequestMessage(HttpMethod.Post, $"{_opt.BaseUrl.TrimEnd('/')}/chat/completions");
            httpReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _opt.ApiKey);
            httpReq.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using var resp = await _http.SendAsync(httpReq, ct);
            var body = await resp.Content.ReadAsStringAsync(ct);

            if (!resp.IsSuccessStatusCode)
                throw new InvalidOperationException(body);

            var parsed = JsonSerializer.Deserialize<ChatResponse>(body, JsonOpts);

            return parsed is { choices.Length: > 0 }
                ? parsed.choices[0].message.content
                : "(empty)";
        }

        public async Task<ImmutableArray<double>> EmbedAsync(string text, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(_opt.ApiKey))
                throw new InvalidOperationException("AI:ApiKey is missing.");

            if (string.IsNullOrWhiteSpace(_opt.EmbeddingModel))
                throw new InvalidOperationException("AI:EmbeddingModel is missing.");

            var req = new EmbeddingRequest(model: _opt.EmbeddingModel, input: text);

            var json = JsonSerializer.Serialize(req, JsonOpts);

            using var httpReq = new HttpRequestMessage(HttpMethod.Post, $"{_opt.BaseUrl.TrimEnd('/')}/embeddings");
            httpReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _opt.ApiKey);
            httpReq.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using var resp = await _http.SendAsync(httpReq, ct);
            var body = await resp.Content.ReadAsStringAsync(ct);

            if (!resp.IsSuccessStatusCode)
                throw new InvalidOperationException($"Embeddings failed: {(int)resp.StatusCode} {resp.ReasonPhrase}. Body: {body}");

            var parsed = JsonSerializer.Deserialize<EmbeddingResponse>(body, JsonOpts);

            if (parsed is null || parsed.data.Length == 0)
                throw new InvalidOperationException("Embeddings returned empty data.");

            return parsed.data[0].embedding;
        }

    }
}
