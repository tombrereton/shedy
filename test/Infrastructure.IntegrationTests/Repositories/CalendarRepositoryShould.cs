using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Builders;
using Shedy.Core.Domain.Builders;
using Shedy.Core.Interfaces;
using Shedy.Infrastructure.IntegrationTests.Helpers;
using Shedy.Infrastructure.Persistence;

#pragma warning disable CS0618

namespace Shedy.Infrastructure.IntegrationTests.Repositories;

public sealed class CalendarRepositoryShould : ServiceCollectionSetup
{
    [Fact]
    public async Task GetCalendar()
    {
        // arrange
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithUserId(Guid.NewGuid())
            .WithDefaultOpeningHours(TimeZoneInfo.Local)
            .Build();
        
        var db = Services.GetRequiredService<ShedyDbContext>();
        await db.Calendars.AddAsync(calendar);
        await db.SaveChangesAsync();

        var repo = Services.GetRequiredService<ICalendarRepository>();

        // act
        var result = await repo.GetAsync(calendar.Id, default);

        // assert
        result!.OpeningTimes.Should().Equal(calendar.OpeningTimes);
        result.UserId.Should().Be(calendar.UserId);
    }
    
    [Fact]
    public async Task UpdateCalendar()
    {
        // arrange
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithUserId(Guid.NewGuid())
            .WithDefaultOpeningHours(TimeZoneInfo.Local)
            .Build();
        var openingTimes = new OpeningTimesBuilder()
            .WithDay(DayOfWeek.Monday)
            .WithTimeZone(TimeZoneInfo.Local)
            .Build();
        
        var calendarEvent = new CalendarEventBuilder()
            .CreateCalendarEvent()
            .WithStart(DateTimeOffset.Parse("2023-01-02T09:00:00Z"))
            .WithDurationInMinutes(30)
            .WithTimeZone(TimeZoneInfo.Local)
            .Build();
        var db = Services.GetRequiredService<ShedyDbContext>();
        await db.Calendars.AddAsync(calendar);
        await db.SaveChangesAsync();

        var repo = Services.GetRequiredService<ICalendarRepository>();

        // act
        var result = await repo.GetAsync(calendar.Id, default);
        result!.AddEvent(calendarEvent);
        await repo.SaveChangesAsync(default);

        // assert
        var updatedCalendar = db.Calendars.FirstOrDefault(x => x.Id == calendar.Id);
        updatedCalendar.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async Task SaveCalendar()
    {
        // arrange
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithUserId(Guid.NewGuid())
            .WithDefaultOpeningHours(TimeZoneInfo.Local)
            .Build();
        
        var db = Services.GetRequiredService<ShedyDbContext>();
        var repo = Services.GetRequiredService<ICalendarRepository>();

        // act 
        await repo.AddAsync(calendar, default);
        await repo.SaveChangesAsync(default);

        // assert
        var result = await db.Calendars.FirstOrDefaultAsync(x => x.Id == calendar.Id);
        result.Should().NotBeNull();
        result!.OpeningTimes.Should().Equal(calendar.OpeningTimes);
        result.UserId.Should().Be(calendar.UserId);
    }
    
    [Fact]
    public async Task DeleteCalendar()
    {
        // arrange
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithUserId(Guid.NewGuid())
            .WithDefaultOpeningHours(TimeZoneInfo.Local)
            .Build();
        
        var db = Services.GetRequiredService<ShedyDbContext>();
        await db.Calendars.AddAsync(calendar);
        await db.SaveChangesAsync();

        var repo = Services.GetRequiredService<ICalendarRepository>();

        // act
        await repo.DeleteAsync(calendar.Id, default);

        // assert
        var result = await db.Calendars.FirstOrDefaultAsync(x => x.Id == calendar.Id);
        result.Should().BeNull();
    }

}