using EventsManagement.Domain.Models;

namespace EventsManagement.Repository.Interface;

public interface ILegacyVenueRepository
{
    Task<List<Venue>> GetVenuesModifiedSinceAsync(DateTime since);
    Task<List<Section>> GetSectionsModifiedSinceAsync(DateTime since);
    Task<List<Seat>> GetSeatsModifiedSinceAsync(DateTime since);
}