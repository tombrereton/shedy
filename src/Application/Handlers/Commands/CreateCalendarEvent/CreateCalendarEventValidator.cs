using FluentValidation;

namespace Shedy.Application.Handlers.Commands.CreateCalendarEvent;

public class CreateCalendarEventValidator : AbstractValidator<CreateCalendarEvent>
{
    public CreateCalendarEventValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty().NotNull();
        RuleFor(x => x.Event).NotNull();
    }
}