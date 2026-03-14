using EventsManagement.Domain.Common;
using EventsManagement.Domain.Enums;

namespace EventsManagement.Domain.Models;

public class Event : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } 
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public EventStatus Status { get; set; }
    public string? BannerUrl { get; set; }
    
    public Guid VenueId { get; set; }
    public Venue Venue { get; set; } = null!;
    
    // key kaj User po default e string ne e Guid, vo IdentityUser
    public string UserId { get; set; }
    public EventsAppUser User { get; set; } = null!;
    
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public virtual ICollection<SeatReservation> SeatReservations { get; set; } = new List<SeatReservation>();
    public virtual ICollection<EventSectionPricing> EventSectionPricings { get; set; } = new List<EventSectionPricing>();
    
}