using MediatR;

namespace Shedy.Application.Queries.GetCalendar;

public record GetCalendar(Guid CalendarId) : IRequest<GetCalendarResult>;