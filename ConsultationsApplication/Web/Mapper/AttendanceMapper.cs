using Domain.Dto;
using Service.Implementation;
using Service.Interface;
using Web.Extensions;
using Web.Request;
using Web.Response;

namespace Web.Mapper;

public class AttendanceMapper
{
    // vo mapper -> dependency injection na INTERFEJSOT na servisot, i konstruktor
    // REGISTRIRAJ GO MAPPEROT VO PROGRAM.CS
    
    private readonly IAttendanceService _attendanceService;
    private readonly IFileUploadService _fileUploadService;

    public AttendanceMapper(IAttendanceService attendanceService, IFileUploadService fileUploadService)
    {
        _attendanceService = attendanceService;
        _fileUploadService = fileUploadService;
    }

    public async Task<AttendanceResponse?> RegisterAsync(AttendanceRequest request)
    {
        var result = await _attendanceService.CreateAsync(new AttendanceDto()
        {
            Comment = request.Comment,
            UserId =  request.UserId,
            RoomId =  request.RoomId,
            ConsultationId = request.ConsultationId
        });
        
        return result.ToResponse();
    }

    public async Task<AttendanceResponse> DeleteByIdAsync(Guid id)
    {
        var result = await _attendanceService.DeleteByIdAsync(id);
        return result.ToResponse();
    }

    public async Task<List<AttendanceResponse>> GetAllByConsultationIdAsync(Guid consultationId)
    {
        var result = await _attendanceService.GetAllByConsultationIdAsync(consultationId);
        return result.Select(x => x.ToResponse()).ToList();
    }

    public async Task<AttendanceResponse> MarkAsAbsentAsync(Guid id)
    {
        var result = await _attendanceService.MarkAsAbsentById(id);
        return result.ToResponse();
    }

    public async Task<AttendanceResponse?> UploadReasonByIdInFileSystemAsync(Guid id, IFormFile file)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);

        var path = await _fileUploadService.UploadFileAsync(
            ms.ToArray(),
            file.FileName
        );

        var result = await _attendanceService.UpdateReasonPathByIdAsync(id, path);
        
        return result.ToResponse();
    }
}