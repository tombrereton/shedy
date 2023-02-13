using MediatR;
using Shedy.Core.Behaviours;

namespace Shedy.Core.Handlers.CreateCalendar;

public record CreateCalendar(Guid UserId) : IRequest<CreateCalendarResult>;
