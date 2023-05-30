using MediatR;
using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Application.Commands.CreateCalendarEvent;

public record CreateCalendarEvent(
    Guid CalendarId,
    CalendarEvent Event
    ) : IRequest<CreateCalendarEventResult>;