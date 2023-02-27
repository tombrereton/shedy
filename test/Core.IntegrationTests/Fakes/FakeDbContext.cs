using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Shedy.Core.Aggregates.Calendar;

namespace Shedy.Core.IntegrationTests.Fakes;

public class FakeDbContext : DbContext
{
    public FakeDbContext(DbContextOptions<FakeDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new TimeZoneConverter() }
        };
        modelBuilder.Entity<CalendarAggregate>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<CalendarAggregate>()
            .Property(x => x.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<CalendarAggregate>()
            .Property(x => x.UserId)
            .IsRequired();

        modelBuilder.Entity<CalendarAggregate>()
            .Property(x => x.OpeningTimes)
            .HasConversion(
                x => JsonSerializer.Serialize(x, jsonOptions),
                y => JsonSerializer.Deserialize<List<OpeningTime>>(y, jsonOptions) ?? new List<OpeningTime>()
            );

        modelBuilder.Entity<CalendarAggregate>()
            .Property(x => x.Events)
            .HasConversion(
                x => JsonSerializer.Serialize(x, jsonOptions),
                y => JsonSerializer.Deserialize<List<CalendarEvent>>(y, jsonOptions) ?? new List<CalendarEvent>()
            );


        base.OnModelCreating(modelBuilder);
    }

    public DbSet<CalendarAggregate> Calendars { get; set; }
}