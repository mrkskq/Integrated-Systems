using EventsManagement.Domain.Dto;
using EventsManagement.Domain.Models;

namespace EventsManagement.Service.Interface;

public interface IEventService
{
    Task<List<Event>> GetAllAsync();
    Task<Event> GetByIdAsync(Guid id);
    
    // so dto
    Task<Event> InsertAsync(EventDto dto);
    Task<Event> UpdateAsync(Guid id, EventDto dto);
    Task<Event> DeleteByIdAsync(Guid id);
    
    // pagination
    public Task<PaginatedResult<Event>> GetAllPagedAsync(int pageNumber, int pageSize);
    
    // sakame da gi zejme site Events, pa od niv EventSectionPricings, pa od niv Sections, pa od niv Venues
    // Get all events with included eventPricing, section and venue;
    Task<List<Event>> GetAllEventsAsyncWithEventPricingWithoutInclude();
    Task<List<Event>> GetAllEventsAsyncWithEventPricingUsingInclude();

    
    public Task<Event> UploadImageById(Guid eventId, string fileName, string contentType, int size, byte[] data);
    public Task<Event> UpdateImagePathByIdAsync(Guid eventId, string path);
}