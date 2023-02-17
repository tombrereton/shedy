using FluentValidation;

namespace Shedy.Core.Handlers.AddAvailability;

public class AddAvailabilityValidator : AbstractValidator<AddAvailability>
{
    public AddAvailabilityValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty().NotNull();
    }
}