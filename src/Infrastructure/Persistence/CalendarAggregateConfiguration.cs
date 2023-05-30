using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Infrastructure.Persistence;

public class CalendarAggregateConfiguration : IEntityTypeConfiguration<CalendarAggregate>
{
    public void Configure(EntityTypeBuilder<CalendarAggregate> builder)
    {
        ConfigureCalendarAggregate(builder);
        ConfigureOpeningTimes(builder);
        ConfigureCalendarEvents(builder);
    }

    private static void ConfigureCalendarAggregate(EntityTypeBuilder<CalendarAggregate> builder)
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
            .Property(x => x.Version)
            .IsRowVersion();
    }

    private static void ConfigureCalendarEvents(EntityTypeBuilder<CalendarAggregate> builder)
    {
        builder
            .OwnsMany(x => x.Events, ownedBuilder =>
            {
                ConfigureAttendees(ownedBuilder);
                
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
                    .Property(x => x.Notes)
                    .HasMaxLength(3000);
            });
    }

    private static void ConfigureAttendees(OwnedNavigationBuilder<CalendarAggregate, CalendarEvent> ownedBuilder)
    {
        // ownedBuilder.OwnsMany(x => x.Attendees, ob2 =>
        // {
        //     ob2
        //         .Property(x => x.Email)
        //         .IsRequired();
        //
        //     ob2
        //         .Property(x => x.Rsvp)
        //         .IsRequired();
        // });
    }

    private static void ConfigureOpeningTimes(EntityTypeBuilder<CalendarAggregate> builder)
    {
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
    }
}