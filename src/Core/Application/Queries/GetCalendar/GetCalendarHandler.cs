using Ardalis.GuardClauses;
using MediatR;
using Shedy.Core.Interfaces;

namespace Shedy.Core.Queries.GetCalendar;

public class GetCalendarHandler : IRequestHandler<GetCalendar, GetCalendarResult>
{
    private readonly ICalendarRepository _repository;

    public GetCalendarHandler(ICalendarRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetCalendarResult> Handle(GetCalendar request, CancellationToken cancellationToken)
    {
        var calendar = await _repository.GetAsync(request.CalendarId, cancellationToken);
        Guard.Against.Null(calendar, nameof(calendar));
        return new GetCalendarResult(calendar);
    }
}