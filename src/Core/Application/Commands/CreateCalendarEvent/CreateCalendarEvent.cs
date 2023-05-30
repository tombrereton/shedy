using MediatR;
using Shedy.Core.Aggregates.Calendar;

namespace Shedy.Core.Commands.CreateCalendarEvent;

public record CreateCalendarEvent(
    Guid CalendarId,
    CalendarEvent Event
    ) : IRequest<CreateCalendarEventResult>;