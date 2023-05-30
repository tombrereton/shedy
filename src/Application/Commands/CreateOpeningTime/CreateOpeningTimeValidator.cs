using FluentValidation;

namespace Shedy.Application.Commands.CreateOpeningTime;

public class CreateOpeningTimeValidator : AbstractValidator<CreateOpeningTime>
{
    public CreateOpeningTimeValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty().NotNull();
    }
}