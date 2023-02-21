using FluentValidation;

namespace Shedy.Core.Handlers.CreateOpeningTime;

public class CreateOpeningTimeValidator : AbstractValidator<CreateOpeningTime>
{
    public CreateOpeningTimeValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty().NotNull();
    }
}