using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Application.Interfaces;

public interface ICalendarRepository
{
    Task<CalendarAggregate?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(CalendarAggregate calendar, CancellationToken cancellationToken);
    void Update(CalendarAggregate calendar);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}