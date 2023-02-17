using FluentValidation;

namespace Shedy.Core.Handlers.UpdateAvailability;

public class UpdateAvailabilityValidator : AbstractValidator<UpdateAvailability>
{
    public UpdateAvailabilityValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty().NotNull();
        RuleFor(x => x.Availability).NotNull();
    }
}