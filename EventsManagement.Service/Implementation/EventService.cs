using EventsManagement.Domain.Dto;
using EventsManagement.Domain.Models;
using EventsManagement.Repository.Interface;
using EventsManagement.Service.Interface;

namespace EventsManagement.Service.Implementation;

public class EventService : IEventService
{

    private readonly IRepository<Event> _repository;

    public EventService(IRepository<Event> repository)
    {
        _repository = repository;
    }

    public async Task<List<Event>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync(x => x);
        return result.ToList();
    }

    public async Task<Event?> GetByIdAsync(Guid id)
    {
        return await _repository.Get(
            selector: x => x,
            predicate: x => x.Id == id);
    }
    
    public async Task<Event> GetByIdNotNullAsync(Guid id)
    {
        var result = await _repository.Get(
            selector: x => x,
            predicate: x => x.Id == id);

        if (result == null)
        {
            throw new InvalidOperationException($"Event with id {id} not found");
        }

        return result;
    }

    public async Task<Event> InsertAsync(EventDto dto)
    {
        var eventToAdd = new Event
        {
            Title = dto.Title,
            Description = dto.Description,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Status = dto.Status,
            BannerUrl = dto.BannerUrl,
            VenueId = dto.VenueId,
            UserId = dto.UserId,
        };

        return await _repository.InsertAsync(eventToAdd);
    }

    public async Task<Event> UpdateAsync(Guid id, EventDto dto)
    {
        var eventToUpdate = await GetByIdNotNullAsync(id);
        
        eventToUpdate.Title = dto.Title;
        eventToUpdate.Description = dto.Description;
        eventToUpdate.StartDate = dto.StartDate;
        eventToUpdate.EndDate = dto.EndDate;
        eventToUpdate.Status = dto.Status;
        eventToUpdate.BannerUrl = dto.BannerUrl;
        eventToUpdate.VenueId = dto.VenueId;
        eventToUpdate.UserId = dto.UserId;
        
        return await _repository.UpdateAsync(eventToUpdate);
    }

    public async Task<Event> DeleteByIdAsync(Guid id)
    {
        var eventToDelete = await GetByIdNotNullAsync(id);
        return await _repository.DeleteAsync(eventToDelete); 
    }
}