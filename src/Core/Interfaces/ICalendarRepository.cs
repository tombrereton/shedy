using Shedy.Core.Aggregates.Calendar;

namespace Shedy.Core.Interfaces;

public interface ICalendarRepository
{
    Task<CalendarAggregate?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(CalendarAggregate calendar, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}