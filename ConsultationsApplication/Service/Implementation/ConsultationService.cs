using Domain.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class ConsultationService : IConsultationService
{
    // vo service -> dependency injection na INTERFEJSOT na repository, i konstruktor
    // REGISTRIRAJ GO SERVISOT VO PROGRAM.CS

    private readonly IRepository<Consultation> _consultationRepository;

    public ConsultationService(IRepository<Consultation> consultationRepository)
    {
        _consultationRepository = consultationRepository;
    }

    public async Task<Consultation> GetByIdNotNullAsync(Guid id)
    {
        var result = await _consultationRepository.GetAsync(
            selector: x => x,
            predicate: x => x.Id == id);

        if (result == null)
        {
            throw new InvalidOperationException();
        }
        
        return result;
    }

    public async Task<Consultation?> GetByIdAsync(Guid id)
    {
        return await _consultationRepository.GetAsync(
            selector: x => x,
            predicate: x => x.Id == id);
    }

    public async Task<List<Consultation>> GetAllAsync(string? roomName, DateOnly? date)
    {
        // Доколку не се прати вредност за параматерите истиот треба да се игнорира
        // zato e proverkata so null
        // consultation -> room -> attendances -> user

        var result = await _consultationRepository.GetAllAsync(
            selector: x => x,
            predicate: x =>
                (roomName == null || x.Room.Name.Contains(roomName)) &&
                (date == null || DateOnly.FromDateTime(x.StartTime) == date),
            include: x => x.Include(c => c.Room)
                .Include(c => c.Attendances).ThenInclude(a => a.User));

        return result.ToList();
    }

    public async Task<Consultation> CreateAsync(DateTime startTime, DateTime endTime, Guid roomId)
    {
        //При иницијално креирање на запис за консултации:
        //Потребно е RegistredStudents да се постави на 0.
        
        var consultationToCreate = new Consultation()
        {
            StartTime = startTime,
            EndTime = endTime,
            RoomId = roomId,
            RegisteredStudents = 0 
        };

        return await _consultationRepository.InsertAsync(consultationToCreate);
    }

    public async Task<Consultation> UpdateAsync(Guid id, DateTime startTime, DateTime endTime, Guid roomId)
    {
        var consultationToUpdate = await GetByIdNotNullAsync(id);
        
        //Не е дозволено менување на терминот за консултации ако веќе има пријавени студенти
        
        if (consultationToUpdate.RegisteredStudents > 0)
        {
            throw new InvalidOperationException();
        }
        
        consultationToUpdate.StartTime = startTime;
        consultationToUpdate.EndTime = endTime;
        consultationToUpdate.RoomId = roomId;
        return await _consultationRepository.UpdateAsync(consultationToUpdate);
    }

    public async Task<Consultation> DeleteByIdAsync(Guid id)
    {
        var consultationToDelete = await GetByIdNotNullAsync(id);

        //Не е дозволено бришење на терминот за консултации ако има пријавени студенти.
        
        if (consultationToDelete.RegisteredStudents > 0)
        {
            throw new InvalidOperationException();
        }
        
        return await _consultationRepository.DeleteAsync(consultationToDelete);
    }

    
    public async Task<PaginatedResult<Consultation>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await _consultationRepository.GetAllPagedAsync(
            selector: x => x,
            pageNumber: pageNumber,
            pageSize: pageSize,
            include: x=> x.Include(c => c.Attendances).ThenInclude(a => a.User),
            asNoTracking: true);
    }

    public async Task UpdateRegisteredStudentsById(Guid consultationId, int count)
    {
        var consultation = await GetByIdNotNullAsync(consultationId);
        consultation.RegisteredStudents += count; // primer prakjame +1 ili -1 ili koj bilo broj
        await _consultationRepository.UpdateAsync(consultation);
    }
}