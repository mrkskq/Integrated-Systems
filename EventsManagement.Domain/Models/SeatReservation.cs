using EventsManagement.Domain.Common;

namespace EventsManagement.Domain.Models;

public class SeatReservation : BaseAuditableEntity<EventsAppUser>
{
    public Guid SeatId { get; set; }
    public Seat Seat { get; set; } = null!;
    
    public Guid ReservationId { get; set; }
    public Reservation Reservation { get; set; } = null!;
    
    public Guid EventId { get; set; }
    public Event Event { get; set; } = null!;
    
    public Ticket? Ticket { get; set; }
}