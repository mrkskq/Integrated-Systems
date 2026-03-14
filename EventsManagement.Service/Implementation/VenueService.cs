using EventsManagement.Domain.Models;
using EventsManagement.Repository.Interface;
using EventsManagement.Service.Interface;

namespace EventsManagement.Service.Implementation;

public class VenueService : IVenueService
{
    
    private IRepository<Venue> _repository;

    public VenueService(IRepository<Venue> repository)
    {
        _repository = repository;
    }

    public async Task<List<Venue>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync(x => x);
        return result.ToList();
    }

    public async Task<Venue?> GetByIdAsync(Guid id)
    {
        return await _repository.Get(
            selector: x => x,
            predicate: x => x.Id == id); 
    }
    
    public async Task<Venue?> GetByIdNotNullAsync(Guid id)
    {
        var result = await _repository.Get(
            selector: x => x,
            predicate: x => x.Id == id);

        if (result == null)
        {
            throw new InvalidOperationException($"Venue with id {id} not found");
        }

        return result;
    }

    public async Task<Venue> InsertAsync(string name, string address, string city, string country, string? zipCode, int totalCapacity)
    {
        var venueToInsert = new Venue()
        {
            Name = name,
            Address = address,
            City = city,
            Country = country,
            ZipCode = zipCode,
            TotalCapacity = totalCapacity
        };

        return await _repository.InsertAsync(venueToInsert);
    }

    public async Task<Venue> UpdateAsync(Guid id, string name, string address, string city, string country, string? zipCode,
        int totalCapacity)
    {
        var venueToUpdate = await GetByIdNotNullAsync(id);
        
        venueToUpdate.Name = name;
        venueToUpdate.Address = address;
        venueToUpdate.City = city;
        venueToUpdate.Country = country;
        venueToUpdate.ZipCode = zipCode;
        venueToUpdate.TotalCapacity = totalCapacity;

        return await _repository.UpdateAsync(venueToUpdate);
    }

    public async Task<Venue> DeleteByIdAsync(Guid id)
    {
        var venueToDelete = await GetByIdNotNullAsync(id);
        return await _repository.DeleteAsync(venueToDelete);
    }
}