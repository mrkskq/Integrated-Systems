using EventsManagement.Domain.Models;

namespace EventsManagement.Repository.Interface;

public interface IVenueRepository
{
    Task BulkInsertOrUpdateVenuesAsync(List<Venue> venues);
    Task BulkInsertOrUpdateSectionsAsync(List<Section> sections);
    Task BulkInsertOrUpdateSeatsAsync(List<Seat> seats);
}