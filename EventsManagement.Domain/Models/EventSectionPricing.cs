using EventsManagement.Domain.Common;

namespace EventsManagement.Domain.Models;

public class EventSectionPricing : BaseAuditableEntity<EventsAppUser>
{
    public decimal Price { get; set; }
    public string Currency { get; set; } = "USD";
    
    public Guid EventId { get; set; }
    public Event Event { get; set; } = null!;
    
    public Guid SectionId { get; set; }
    public Section Section { get; set; } = null!;
}