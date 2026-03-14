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
}