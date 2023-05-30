using MediatR;

namespace Shedy.Application.Commands.CreateCalendar;

public record CreateCalendar(Guid UserId) : IRequest<CreateCalendarResult>;
