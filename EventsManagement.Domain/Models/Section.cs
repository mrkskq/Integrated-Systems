using EventsManagement.Domain.Common;

namespace EventsManagement.Domain.Models;

public class Section : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    
    public Guid VenueId { get; set; }
    public Venue Venue { get; set; } = null!;
    
    public virtual ICollection<EventSectionPricing> EventSectionPricings { get; set; } = new List<EventSectionPricing>();
    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
}