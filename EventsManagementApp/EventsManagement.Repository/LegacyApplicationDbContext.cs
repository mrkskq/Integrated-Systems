using EventsManagement.Domain.ExternalModels;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.Repository;

public class LegacyApplicationDbContext(DbContextOptions<LegacyApplicationDbContext> options) : DbContext(options)
{
    public DbSet<LegacyVenue> Venues { get; set; }
    public DbSet<LegacySeat> Seats { get; set; }
    public DbSet<LegacySection> Sections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LegacyVenue>(e => e.HasKey(v => v.VenueId));
        modelBuilder.Entity<LegacySeat>(e => e.HasKey(v => v.SeatId));
        modelBuilder.Entity<LegacySection>(e => e.HasKey(v => v.SectionId));
    }
}

// aud 6, slajd 29