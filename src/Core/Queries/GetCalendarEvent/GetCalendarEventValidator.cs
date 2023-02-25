using FluentValidation;

namespace Shedy.Core.Queries.GetCalendarEvent;

public class GetCalendarEventValidator : AbstractValidator<GetCalendarEvent>
{
    public GetCalendarEventValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty().NotNull();
        RuleFor(x => x.EventId).NotEmpty().NotNull();
    }
}