using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Application.Commands.CreateCalendarEvent;

public record CreateCalendarEventResult(Guid CalendarId, CalendarEvent Event);