using MediatR;
using Shedy.Core.Builders;
using Shedy.Core.Interfaces;

namespace Shedy.Core.Handlers.CreateCalendar;

public class CreateCalendarHandler : IRequestHandler<CreateCalendar, CreateCalendarResult>
{
    private readonly ICalendarRepository _repository;

    public CreateCalendarHandler(ICalendarRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<CreateCalendarResult> Handle(CreateCalendar request, CancellationToken cancellationToken)
    {
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithUserId(request.UserId)
            .WithEmptyOpeningHours()
            .Build();

        await _repository.SaveAsync(calendar, cancellationToken);

        return new CreateCalendarResult(calendar.UserId, calendar.Id);
    }
}