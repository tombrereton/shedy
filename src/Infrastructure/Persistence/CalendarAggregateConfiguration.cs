using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shedy.Core.Calendar;

namespace Shedy.Infrastructure.Persistence;

public class CalendarAggregateConfiguration : IEntityTypeConfiguration<CalendarAggregate>
{
    public void Configure(EntityTypeBuilder<CalendarAggregate> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedNever();

        builder
            .Property(x => x.UserId)
            .IsRequired();

        builder
            .OwnsMany(x => x.OpeningTimes, ownedBuilder =>
            {
                ownedBuilder
                    .Property(x => x.Day)
                    .IsRequired();

                ownedBuilder
                    .Property(x => x.Start)
                    .IsRequired();

                ownedBuilder
                    .Property(x => x.Finish)
                    .IsRequired();

                ownedBuilder
                    .Property(x => x.TimeZone)
                    .HasConversion(
                        x => x.ToSerializedString(),
                        x => TimeZoneInfo.FromSerializedString(x)
                    )
                    .IsRequired();
            });
        
        builder
            .OwnsMany(x => x.Events, ownedBuilder =>
            {
                ownedBuilder
                    .HasKey(x => x.Id);

                ownedBuilder
                    .Property(x => x.Start)
                    .IsRequired();

                ownedBuilder
                    .Property(x => x.Finish)
                    .IsRequired();

                ownedBuilder
                    .Property(x => x.TimeZone)
                    .HasConversion(
                        x => x.ToSerializedString(),
                        x => TimeZoneInfo.FromSerializedString(x)
                    )
                    .IsRequired();

                ownedBuilder
                    .Property(x => x.Title)
                    .HasMaxLength(500);
                
                ownedBuilder
                    .Property(x => x.Description)
                    .HasMaxLength(3000);
            });
    }
}