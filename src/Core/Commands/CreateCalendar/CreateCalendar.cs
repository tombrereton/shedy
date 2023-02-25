using MediatR;

namespace Shedy.Core.Commands.CreateCalendar;

public record CreateCalendar(Guid UserId) : IRequest<CreateCalendarResult>;
