using HelpdeskCopilot.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskCopilot.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TicketSummaryEntity> TicketSummaries => Set<TicketSummaryEntity>();

        public DbSet<DocChunkEntity> DocChunks => Set<DocChunkEntity>();

    }
}
