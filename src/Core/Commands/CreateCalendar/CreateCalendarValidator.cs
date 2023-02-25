using FluentValidation;

namespace Shedy.Core.Commands.CreateCalendar;

public class CreateCalendarValidator : AbstractValidator<CreateCalendar>
{
    public CreateCalendarValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().NotNull();
    }
}