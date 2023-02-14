using Microsoft.EntityFrameworkCore;
using Shedy.Core.Builders;
using Shedy.Core.Calendar;
using Shedy.Core.Interfaces;
using Shedy.Infrastructure.Database;

namespace Shedy.Infrastructure.Repositories;

public class CalendarRepository : ICalendarRepository
{
    private readonly ShedyDbContext _dbContext;

    public CalendarRepository(ShedyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CalendarAggregate> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var calendar =  await _dbContext.Calendars.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (calendar is not null)
        {
            return calendar;
        }

        return new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithEmptyOpeningHours()
            .Build();
    }

    public async Task SaveAsync(CalendarAggregate calendar, CancellationToken cancellationToken)
    {
        _dbContext.Calendars.Add(calendar);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var calendar = await _dbContext.Calendars.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (calendar is not null)
        {
            _dbContext.Calendars.Remove(calendar);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}