using MediatR;

namespace Shedy.Application.Handlers.Commands.CreateCalendar;

public record CreateCalendar(Guid UserId) : IRequest<CreateCalendarResult>;
