using FluentValidation;

namespace Shedy.Core.Commands.CreateCalendarEvent;

public class CreateCalendarEventValidator : AbstractValidator<CreateCalendarEvent>
{
    public CreateCalendarEventValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty().NotNull();
        RuleFor(x => x.Event).NotNull();
    }
}