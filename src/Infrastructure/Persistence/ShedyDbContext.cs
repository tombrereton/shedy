using Microsoft.EntityFrameworkCore;
using Shedy.Core.Aggregates.Calendar;
using Shedy.Core.Domain.Aggregates.Calendar;

namespace Shedy.Infrastructure.Persistence;

public class ShedyDbContext : DbContext
{
    public ShedyDbContext(DbContextOptions<ShedyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(ShedyDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<CalendarAggregate> Calendars { get; set; }
}