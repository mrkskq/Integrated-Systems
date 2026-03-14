using EventsManagement.Domain.Common;
using EventsManagement.Domain.Enums;

namespace EventsManagement.Domain.Models;

public class Ticket : BaseAuditableEntity<EventsAppUser>
{
    public string Barcode { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; }
    public TicketStatus Status { get; set; }
    
    public string UserId { get; set; } = null!;
    public EventsAppUser User { get; set; } = null!;
    
    public Guid EventId { get; set; }
    public Event Event { get; set; } = null!;
    
    public Guid SeatReservationId { get; set; }
    public SeatReservation SeatReservation { get; set; } = null!;
}