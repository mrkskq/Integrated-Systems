using Domain.Dto;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class AttendanceService : IAttendanceService
{
    // vo service -> dependency injection na INTERFEJSOT na repository, i konstruktor 
    // REGISTRIRAJ GO SERVISOT VO PROGRAM.CS

    private readonly IRepository<Attendance> _attendanceRepository;
    private readonly IConsultationService _consultationService;

    public AttendanceService(IRepository<Attendance> attendanceRepository, IConsultationService consultationService)
    {
        _attendanceRepository = attendanceRepository;
        _consultationService = consultationService;
    }

    public async Task<Attendance> GetByIdNotNullAsync(Guid id)
    {
        var result = await _attendanceRepository.GetAsync(
            selector: x => x, 
            predicate: x => x.Id == id,
            include: x => x.Include(a => a.User)); 

        if (result == null)
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public async Task<Attendance?> GetByIdAsync(Guid id)
    {
        return await _attendanceRepository.GetAsync(
            selector: x => x, 
            predicate: x => x.Id == id);
    }

    public async Task<List<Attendance>> GetAllAsync(string? dateAfter)
    {
        var result = await _attendanceRepository.GetAllAsync(
            selector: x => x);
        return result.ToList();
    }

    public async Task<Attendance> CreateAsync(AttendanceDto dto)
    {
        var attendanceToCreate = new Attendance()
        {
            Comment = dto.Comment,
            UserId = dto.UserId,
            RoomId = dto.RoomId,
            ConsultationId = dto.ConsultationId
        };

        var result = await _attendanceRepository.InsertAsync(attendanceToCreate);
        
        //Потребно е да се зголеми бројот на пријавени студенти во соодветниот термин за консултации
        
        await _consultationService.UpdateRegisteredStudentsById(dto.ConsultationId, 1);
        return await GetByIdNotNullAsync(result.Id);
    }

    public async Task<Attendance> UpdateAsync(Guid id, AttendanceDto dto)
    {
        var attendanceToUpdate = await GetByIdNotNullAsync(id);
        
        attendanceToUpdate.Comment = dto.Comment;
        attendanceToUpdate.UserId = dto.UserId;
        attendanceToUpdate.RoomId = dto.RoomId;
        attendanceToUpdate.ConsultationId = dto.ConsultationId;
        
        return await _attendanceRepository.UpdateAsync(attendanceToUpdate);
    }

    public async Task<Attendance> DeleteByIdAsync(Guid id)
    {
        var attendanceToDelete = await GetByIdNotNullAsync(id);

        var consultation = await _consultationService.GetByIdNotNullAsync(attendanceToDelete.ConsultationId);
        
        //Не е дозволено бришење на пријава за термин за консултации чиј StartTime е помалку од 1 час во иднина.
        //Потребно е да се намали бројот на пријавени студенти во терминто за консултации

        if (consultation.StartTime <= DateTime.Now.AddHours(1))
        {
            throw new InvalidOperationException();
        }
        
        await _consultationService.UpdateRegisteredStudentsById(attendanceToDelete.ConsultationId, -1);
        return await _attendanceRepository.DeleteAsync(attendanceToDelete);
    }

    public async Task<PaginatedResult<Attendance>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await _attendanceRepository.GetAllPagedAsync(
            selector: x => x,
            pageNumber: pageNumber,
            pageSize: pageSize);
    }

    public async Task<Attendance> UpdateReasonPathByIdAsync(Guid id, string path)
    {
        var attendance = await GetByIdNotNullAsync(id);
        attendance.CancellationReasonDocumentPath = path;
        return await _attendanceRepository.UpdateAsync(attendance);
    }

    public async Task<Attendance> MarkAsAbsentById(Guid id)
    {
        // Означува отсуство на студент во термин за консултации

        var attendance = await GetByIdNotNullAsync(id);
        attendance.Status = Status.Absent;
        return await _attendanceRepository.UpdateAsync(attendance);
    }

    public async Task<List<Attendance>> GetAllByConsultationIdAsync(Guid consultationId)
    {
        var attendances = await _attendanceRepository.GetAllAsync(
            selector: x => x,
            predicate: x => x.ConsultationId == consultationId,
            include: x => x.Include(a => a.User)
        );

        return attendances.ToList();
    }
}