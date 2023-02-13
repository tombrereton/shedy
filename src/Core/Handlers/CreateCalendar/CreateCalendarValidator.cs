using FluentValidation;

namespace Shedy.Core.Handlers.CreateCalendar;

public class CreateCalendarValidator : AbstractValidator<CreateCalendar>
{
    public CreateCalendarValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().NotNull();
    }
}