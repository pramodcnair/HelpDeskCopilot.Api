using System.ComponentModel.DataAnnotations;

namespace HelpdeskCopilot.Api.Data;

public class DocChunkEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string SourceFile { get; set; } = string.Empty;

    public int ChunkIndex { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;

    public string? EmbeddingJson { get; set; }

    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}
