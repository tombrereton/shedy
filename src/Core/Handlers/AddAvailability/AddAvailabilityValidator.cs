using FluentValidation;
using Shedy.Core.Calendar;

namespace Shedy.Core.Handlers.AddAvailability;

public class AddAvailabilityValidator : AbstractValidator<AddAvailability>
{
    public AddAvailabilityValidator()
    {
        RuleFor(x => x.CalendarId).NotEmpty().NotNull();
    }
}