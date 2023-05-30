using FluentValidation;

namespace Shedy.Application.Handlers.Commands.UpdateOpeningTimes;

public class UpdateOpeningTimesValidator : AbstractValidator<UpdateOpeningTimes>
{
    public UpdateOpeningTimesValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty().NotNull();
        RuleFor(x => x.OpeningTimes).NotNull();
    }
}