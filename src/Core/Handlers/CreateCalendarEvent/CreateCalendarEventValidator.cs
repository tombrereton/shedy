using FluentValidation;

namespace Shedy.Core.Handlers.CreateCalendarEvent;

public class CreateCalendarEventValidator : AbstractValidator<CreateCalendarEvent>
{
    public CreateCalendarEventValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty().NotNull();
        RuleFor(x => x.Event).NotNull();
    }
}