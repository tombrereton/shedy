using Ardalis.GuardClauses;
using MediatR;
using Shedy.Core.Interfaces;

namespace Shedy.Core.Handlers.CreateOpeningTime;

public class CreateOpeningTimeHandler : IRequestHandler<CreateOpeningTime, CreateOpeningTimeResult>
{
    private readonly ICalendarRepository _repository;

    public CreateOpeningTimeHandler(ICalendarRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateOpeningTimeResult> Handle(CreateOpeningTime request, CancellationToken cancellationToken)
    {
        var calendar = await _repository.GetAsync(request.CalendarId, cancellationToken);
        Guard.Against.Null(calendar, nameof(calendar));

        calendar.AddOpeningTime(request.OpeningTime);
        await _repository.AddAsync(calendar, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new CreateOpeningTimeResult(calendar.OpeningTimes);
    }
}