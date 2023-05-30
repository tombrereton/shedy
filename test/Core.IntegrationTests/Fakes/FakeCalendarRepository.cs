using Microsoft.EntityFrameworkCore;
using Shedy.Core.Aggregates.Calendar;
using Shedy.Core.Interfaces;

namespace Shedy.Core.IntegrationTests.Fakes;

public class FakeCalendarRepository : ICalendarRepository
{
    private readonly FakeDbContext _dbContext;

    public FakeCalendarRepository(FakeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CalendarAggregate?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Calendars.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public async Task AddAsync(CalendarAggregate calendar, CancellationToken cancellationToken)
    {
        await _dbContext.Calendars.AddAsync(calendar, cancellationToken);
    }

    public void Update(CalendarAggregate calendar)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
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