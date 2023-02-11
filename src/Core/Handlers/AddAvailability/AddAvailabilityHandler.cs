using MediatR;
using Shedy.Core.Calendar;
using Shedy.Core.Interfaces;

namespace Shedy.Core.Handlers.AddAvailability;

public class AddAvailabilityHandler : IRequestHandler<AddAvailability, AddAvailabilityResult>
{
    private readonly ICalendarRepository _repository;

    public AddAvailabilityHandler(ICalendarRepository repository)
    {
        _repository = repository;
    }

    public async Task<AddAvailabilityResult> Handle(AddAvailability request, CancellationToken cancellationToken)
    {
        var calendar = await _repository.GetAsync(request.CalendarId, cancellationToken);
        calendar.AddAvailability(request.Availability);
        await _repository.SaveAsync(calendar, cancellationToken);
        
        var result = new AddAvailabilityResult(calendar.GetOpeningHours());
        return await Task.FromResult(result);
    }
}
