using FluentValidation;

namespace Shedy.Application.Commands.CreateCalendar;

public class CreateCalendarValidator : AbstractValidator<CreateCalendar>
{
    public CreateCalendarValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().NotNull();
    }
}