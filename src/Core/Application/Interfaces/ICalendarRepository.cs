using Shedy.Core.Aggregates.Calendar;
using Shedy.Core.Domain.Aggregates.Calendar;

namespace Shedy.Core.Interfaces;

public interface ICalendarRepository
{
    Task<CalendarAggregate?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(CalendarAggregate calendar, CancellationToken cancellationToken);
    void Update(CalendarAggregate calendar);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}