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
        // var calendar = new CalendarAggregate();
        // calendar.AddAvailability(request.Availability);

        var avails = new List<Availability>();
        avails.Add(request.Availability);
        var result = new AddAvailabilityResult(avails);
        return await Task.FromResult(result);
    }
}
