using Microsoft.AspNetCore.Identity;

namespace EventsManagement.Domain.Models;

// vo nuget instaliraj AspNetCore.Identity.EntityFrameworkCore
// i dodaj go vo Domain slojot

public class EventsAppUser : IdentityUser
{
    public required string FirstName { get; set; } 
    public required string LastName { get; set; }
    
    // ne morat da cuvame Email i PhoneNumber deka tie gi imat kaj IdentityUser
    
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}