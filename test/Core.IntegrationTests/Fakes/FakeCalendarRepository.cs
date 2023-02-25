using System.Text.Json;
using Ardalis.GuardClauses;
using Shedy.Core.Aggregates.Calendar;
using Shedy.Core.Interfaces;
using Xunit.Sdk;

namespace Shedy.Core.IntegrationTests.Fakes;

public class FakeCalendarRepository : ICalendarRepository
{
    private readonly List<CalendarAggregate> _calendars = new();

    public async Task<CalendarAggregate?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var calendar = _calendars.FirstOrDefault(x => x.Id == id);
        if (calendar is null)
        {
            throw new NullException(
                "Calendar removed so we don't update the Calendar Reference. UpdateAsync must be called.");
        }

        _calendars.Remove(calendar);

        return await ValueTask.FromResult(calendar);
    }

    public Task AddAsync(CalendarAggregate calendar, CancellationToken cancellationToken)
    {
        _calendars.Add(calendar);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(CalendarAggregate calendar, CancellationToken cancellationToken)
    {
        var old = _calendars.Find(x => x.Id == calendar.Id);
        _calendars.Remove(old!);
        _calendars.Add(calendar);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var calendar = _calendars.Find(x => x.Id == id);
        if (calendar is not null)
            _calendars.Remove(calendar);

        return Task.CompletedTask;
    }
}