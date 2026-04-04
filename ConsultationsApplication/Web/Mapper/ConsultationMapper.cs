using Domain.Models;
using Service.Implementation;
using Service.Interface;
using Web.Extensions;
using Web.Request;
using Web.Response;

namespace Web.Mapper;

public class ConsultationMapper
{
    // vo mapper -> dependency injection na INTERFEJSOT na servisot, i konstruktor
    // REGISTRIRAJ GO MAPPEROT VO PROGRAM.CS
    
    private readonly IConsultationService _consultationService;

    public ConsultationMapper(IConsultationService consultationService)
    {
        _consultationService = consultationService;
    }

    public async Task<List<ConsultationDetailsResponse>> GetAllAsync(string? roomName, DateOnly? date)
    {
        var result = await _consultationService.GetAllAsync(roomName, date);
        return result.Select(x => x.ToResponse()).ToList();
    }

    public async Task<ConsultationResponse> InsertAsync(ConsultationRequest request)
    {
        var result = await _consultationService.CreateAsync(
            request.StartTime,
            request.EndTime,
            request.RoomId
            );
        
        return result.ToBasicResponse();
    }
    
    public async Task<ConsultationResponse> UpdateAsync(Guid id, ConsultationRequest request)
    {
        var result = await _consultationService.UpdateAsync(
            id: id,
            startTime: request.StartTime,
            endTime: request.EndTime,
            roomId: request.RoomId);
        
        return result.ToBasicResponse();
    }

    public async Task<ConsultationResponse> DeleteAsync(Guid id)
    {
        var result = await _consultationService.DeleteByIdAsync(id);
        return result.ToBasicResponse();
    }

    public async Task<PaginatedResponse<ConsultationDetailsResponse>> GetPagedAsync(PaginatedRequest request)
    {
        var result = await _consultationService.GetPagedAsync(request.PageNumber, request.PageSize);
        return result.ToPaginatedResponse(x => x.ToResponse());
    }
}