using EFCore.BulkExtensions;
using EventsManagement.Domain.Models;
using EventsManagement.Repository.Interface;

namespace EventsManagement.Repository.Implementation;

public class VenueRepository : Repository<Venue>, IVenueRepository
{
    public VenueRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task BulkInsertOrUpdateVenuesAsync(List<Venue> venues)
    {
        await _context.BulkInsertOrUpdateAsync(venues);
    }

    public async Task BulkInsertOrUpdateSectionsAsync(List<Section> sections)
    {
        await _context.BulkInsertOrUpdateAsync(sections);
    }

    public async Task BulkInsertOrUpdateSeatsAsync(List<Seat> seats)
    {
        await _context.BulkInsertOrUpdateAsync(seats);
    }
}

// od NuGet instaliraj EFCore.BulkExtensions vo Repository, aud 6, slajd 23