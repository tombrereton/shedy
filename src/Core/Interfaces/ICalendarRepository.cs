using Shedy.Core.Calendar;

namespace Shedy.Core.Interfaces;

public interface ICalendarRepository
{
    Task<CalendarAggregate?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task SaveAsync(CalendarAggregate calendar, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}