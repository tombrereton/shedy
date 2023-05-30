using FluentValidation;

namespace Shedy.Application.Handlers.Queries.GetCalendar;

public class GetCalendarValidator : AbstractValidator<GetCalendar>
{
    public GetCalendarValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty().NotNull();
    }
}