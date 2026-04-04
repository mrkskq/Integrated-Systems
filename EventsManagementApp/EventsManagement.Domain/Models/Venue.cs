using EventsManagement.Domain.Common;

namespace EventsManagement.Domain.Models;

public class Venue : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string? ZipCode { get; set; }
    public int TotalCapacity { get; set; }
    
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
}