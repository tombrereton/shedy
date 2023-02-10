using MediatR;

namespace Shedy.Core.Handlers.AddAvailability;

public class AddAvailabilityHandler : IRequestHandler<AddAvailability, AddAvailabilityHandler>
{
    public Task<AddAvailabilityHandler> Handle(AddAvailability request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Availability Availability { get; set; }
}
