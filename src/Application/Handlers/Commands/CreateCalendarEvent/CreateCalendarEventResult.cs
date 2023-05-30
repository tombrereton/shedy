using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Application.Handlers.Commands.CreateCalendarEvent;

public record CreateCalendarEventResult(Guid CalendarId, CalendarEvent Event);