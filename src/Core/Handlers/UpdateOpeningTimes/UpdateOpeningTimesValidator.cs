using FluentValidation;

namespace Shedy.Core.Handlers.UpdateOpeningTimes;

public class UpdateOpeningTimesValidator : AbstractValidator<UpdateOpeningTimes>
{
    public UpdateOpeningTimesValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty().NotNull();
        RuleFor(x => x.OpeningTimes).NotNull();
    }
}