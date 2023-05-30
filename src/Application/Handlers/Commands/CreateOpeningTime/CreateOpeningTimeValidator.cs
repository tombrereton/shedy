using FluentValidation;

namespace Shedy.Application.Handlers.Commands.CreateOpeningTime;

public class CreateOpeningTimeValidator : AbstractValidator<CreateOpeningTime>
{
    public CreateOpeningTimeValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty().NotNull();
    }
}