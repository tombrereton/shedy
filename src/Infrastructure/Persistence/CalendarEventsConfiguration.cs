// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Shedy.Core.Aggregates.Calendar;
//
// namespace Shedy.Infrastructure.Persistence;
//
// public class CalendarEventsConfiguration : IEntityTypeConfiguration<CalendarEvent>
// {
//     public void Configure(EntityTypeBuilder<CalendarEvent> builder)
//     {
//         builder
//             .HasKey(x => x.Id);
//
//         builder
//             .Property(x => x.Start)
//             .IsRequired();
//
//         builder
//             .Property(x => x.Finish)
//             .IsRequired();
//
//         builder
//             .Property(x => x.TimeZone)
//             .HasConversion(
//                 x => x.ToSerializedString(),
//                 x => TimeZoneInfo.FromSerializedString(x)
//             )
//             .IsRequired();
//
//         builder
//             .Property(x => x.Title)
//             .HasMaxLength(500);
//
//         builder
//             .Property(x => x.Notes)
//             .HasMaxLength(3000);
//
//         builder.OwnsMany(x => x.Attendees, ob2 =>
//         {
//             ob2
//                 .Property(x => x.Email)
//                 .IsRequired();
//
//             ob2
//                 .Property(x => x.Rsvp)
//                 .IsRequired();
//         });
//     }
// }