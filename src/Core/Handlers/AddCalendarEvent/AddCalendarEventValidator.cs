using FluentValidation;

namespace Shedy.Core.Handlers.AddCalendarEvent;

public class AddCalendarEventValidator : AbstractValidator<AddCalendarEvent>
{
    public AddCalendarEventValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty().NotNull();
        RuleFor(x => x.Event).NotNull();
    }
}