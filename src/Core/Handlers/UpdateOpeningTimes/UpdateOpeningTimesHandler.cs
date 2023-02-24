using Ardalis.GuardClauses;
using MediatR;
using Shedy.Core.Interfaces;

namespace Shedy.Core.Handlers.UpdateOpeningTimes;

public class UpdateOpeningTimesHandler : IRequestHandler<UpdateOpeningTimes, UpdateOpeningTimesResult>
{
    private readonly ICalendarRepository _repository;

    public UpdateOpeningTimesHandler(ICalendarRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateOpeningTimesResult> Handle(UpdateOpeningTimes request, CancellationToken cancellationToken)
    {
        var calendar = await _repository.GetAsync(request.CalendarId, cancellationToken);
        Guard.Against.Null(calendar, nameof(calendar));
        
        calendar.UpdateOpeningTimes(request.OpeningTimes);
        await _repository.SaveChangesAsync(cancellationToken);
        
        return new UpdateOpeningTimesResult(calendar.OpeningTimes);
    }
}
