using Shedy.Core.Builders;
using Shedy.Core.Calendar;
using Shedy.Core.Interfaces;

namespace Shedy.Core.IntegrationTests.Fakes;

public class FakeCalendarRepository : ICalendarRepository
{
    private readonly List<CalendarAggregate> _calendarAggregates = new();

    public async Task<CalendarAggregate> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var calendar = _calendarAggregates.FirstOrDefault(x => x.Id == id);
        return await Task.FromResult(calendar) ?? throw new InvalidOperationException();
    }

    public Task SaveAsync(CalendarAggregate calendar, CancellationToken cancellationToken)
    {
        _calendarAggregates.Add(calendar);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var calendar = _calendarAggregates.Find(x => x.Id == id);
        if (calendar is not null)
            _calendarAggregates.Remove(calendar);

        return Task.CompletedTask;
    }
}