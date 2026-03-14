using EventsManagement.Domain.Common;
using EventsManagement.Domain.Enums;

namespace EventsManagement.Domain.Models;

public class Reservation : BaseAuditableEntity<EventsAppUser>
{
    public DateTime ReservedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public ReservationStatus Status { get; set; }
    
    public Guid EventId { get; set; }
    public Event Event { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public EventsAppUser User { get; set; } = null!;
    
    public virtual ICollection<SeatReservation> SeatReservations { get; set; } = new List<SeatReservation>();
}