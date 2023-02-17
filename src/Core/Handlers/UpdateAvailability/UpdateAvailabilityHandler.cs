using Ardalis.GuardClauses;
using MediatR;
using Shedy.Core.Interfaces;

namespace Shedy.Core.Handlers.UpdateAvailability;

public class AddAvailabilityHandler : IRequestHandler<UpdateAvailability, UpdateAvailabilityResult>
{
    private readonly ICalendarRepository _repository;

    public AddAvailabilityHandler(ICalendarRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateAvailabilityResult> Handle(UpdateAvailability request, CancellationToken cancellationToken)
    {
        var calendar = await _repository.GetAsync(request.CalendarId, cancellationToken);
        Guard.Against.Null(calendar, nameof(calendar));
        
        calendar.UpdateAvailability(request.Availability);
        await _repository.SaveAsync(calendar, cancellationToken);
        
        return new UpdateAvailabilityResult(calendar.OpeningHours);
    }
}
