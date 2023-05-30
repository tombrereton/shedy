using MediatR;

namespace Shedy.Core.Queries.GetCalendar;

public record GetCalendar(Guid CalendarId) : IRequest<GetCalendarResult>;