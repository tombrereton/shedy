using FluentValidation;

namespace Shedy.Core.Queries.GetCalendar;

public class GetCalendarValidator : AbstractValidator<GetCalendar>
{
    public GetCalendarValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty().NotNull();
    }
}