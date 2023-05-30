using MediatR;

namespace Shedy.Application.Handlers.Queries.GetCalendar;

public record GetCalendar(Guid CalendarId) : IRequest<GetCalendarResult>;