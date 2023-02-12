using MediatR;

namespace Shedy.Core.Handlers.CreateCalendar;

public record CreateCalendar(Guid UserId) : IRequest<CreateCalendarResult>;
