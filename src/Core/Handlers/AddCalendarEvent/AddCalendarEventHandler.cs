using MediatR;
using Shedy.Core.Builders;
using Shedy.Core.Interfaces;

namespace Shedy.Core.Handlers.AddCalendarEvent;

public class AddCalendarEventHandler : IRequestHandler<AddCalendarEvent, AddCalendarEventResult>
{
    private readonly ICalendarRepository _repository;

    public AddCalendarEventHandler(ICalendarRepository repository)
    {
        _repository = repository;
    }

    public async Task<AddCalendarEventResult> Handle(AddCalendarEvent request, CancellationToken cancellationToken)
    {
        // var calendarEvent = new CalendarEventBuilder()
        //     .CreateCalendarEvent()
        //     .Build();
        return new AddCalendarEventResult(request.CalendarId, request.Event);
    }
}