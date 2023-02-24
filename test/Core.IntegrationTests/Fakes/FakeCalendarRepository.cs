using Shedy.Core.Aggregates.Calendar;
using Shedy.Core.Interfaces;

namespace Shedy.Core.IntegrationTests.Fakes;

public class FakeCalendarRepository : ICalendarRepository
{
    private readonly List<CalendarAggregate> _calendars = new();

    public async Task<CalendarAggregate?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var calendar = _calendars.FirstOrDefault(x => x.Id == id);
        return await Task.FromResult(calendar) ?? throw new InvalidOperationException();
    }

    public Task AddAsync(CalendarAggregate calendar, CancellationToken cancellationToken)
    {
        _calendars.Add(calendar);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CalendarAggregate calendar, CancellationToken cancellationToken)
    {
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