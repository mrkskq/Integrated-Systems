using EventsManagement.Domain.Common;

namespace EventsManagement.Domain.Models;

public class Seat : BaseEntity
{
    public string Row { get; set; }
    public int Number { get; set; }
    public string? Label { get; set; } 
    public bool IsAccessible { get; set; }
    
    public Guid SectionId { get; set; }
    public virtual Section Section { get; set; } = null!;
    
    public virtual ICollection<SeatReservation> SeatReservations { get; set; } = new List<SeatReservation>();
}